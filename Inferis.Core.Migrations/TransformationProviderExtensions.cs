using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Migrator.Framework;
using Inferis.Core.Extensions;

namespace Inferis.Core.Migrations
{
    public static class TransformationProviderExtensions
    {
        /// <summary>
        /// Constant that defines the splitters in an SQL Server batch script
        /// </summary>
        public static readonly string[] SQLServerBatchScriptSplitters = new string[] { "GO\r\n", "GO ", "GO\t" };

        #region Add / Remove Index

        /// <summary>
        /// Adds an index to the database using one column
        /// </summary>
        /// <param name="transformationProvider"></param>
        /// <param name="indexName">name of the index</param>
        /// <param name="tableName">name of the table</param>
        /// <param name="column">name of the column</param>
        /// <param name="sortDirection">ascending or descending</param>
        /// <param name="unique">unique or nonunique index</param>
        /// <param name="nonClustered">clustered or nonclustered index</param>
        public static void AddIndex(this ITransformationProvider transformationProvider, string indexName, string tableName, Column column,
                                    SortDirection sortDirection, bool unique, bool nonClustered)
        {
            transformationProvider.AddIndex(indexName, tableName, new[] { new IndexKeyColumn(column, sortDirection) }, unique, nonClustered);

        }

        /// <summary>
        /// Adds an index to the database using a number of columns
        /// </summary>
        /// <param name="transformationProvider"></param>
        /// <param name="indexName">name of the index</param>
        /// <param name="tableName">name of the table</param>
        /// <param name="indexKeyColumns">collection of columns and the sortdirection of the column</param>
        /// <param name="unique">unique or nonunique index</param>
        /// <param name="nonClustered">clustered or nonclustered index</param>
        public static void AddIndex(this ITransformationProvider transformationProvider, string indexName, string tableName,
                                    IEnumerable<IndexKeyColumn> indexKeyColumns, bool unique, bool nonClustered)
        {
            if (transformationProvider == null) throw new ArgumentNullException("transformationProvider");
            if (indexName == null) throw new ArgumentNullException("indexName");
            if (tableName == null) throw new ArgumentNullException("tableName");
            if (indexKeyColumns == null) throw new ArgumentNullException("indexKeyColumns");

            var str = new StringBuilder();
            str.Append(string.Format("CREATE {0} {1} INDEX {2} ON {3}",
                                     unique ? "UNIQUE" : string.Empty,
                                     nonClustered ? "NONCLUSTERED" : "CLUSTERED",
                                     FormatSQLParameter(indexName),
                                     FormatSQLParameter(tableName)));
            str.Append("(");
            var listColumns = indexKeyColumns.ToList();
            for (var i = 0; i < listColumns.Count; i++) {
                var indexKeyColumn = listColumns[i];
                if (i > 0)
                    str.Append(", ");

                str.Append(FormatSQLParameter(indexKeyColumn.Column.Name));
                str.Append(" ");
                str.Append(indexKeyColumn.SortDirection.ToString());
            }
            str.Append(")");

            transformationProvider.ExecuteNonQuery(str.ToString());
        }

        /// <summary>
        /// Remove the index from the database
        /// </summary>
        /// <param name="transformationProvider"></param>
        /// <param name="indexName">name of the index to remove</param>
        /// <param name="tableName">name of the table</param>
        public static void RemoveIndex(this ITransformationProvider transformationProvider, string indexName, string tableName)
        {
            if (transformationProvider == null) throw new ArgumentNullException("transformationProvider");
            if (indexName == null) throw new ArgumentNullException("indexName");
            if (tableName == null) throw new ArgumentNullException("tableName");

            var str = string.Format("DROP INDEX {0} ON {1}", indexName, tableName);

            transformationProvider.ExecuteNonQuery(str);
        }

        #endregion

        #region Add Version Trigger

        /// <summary>
        /// Adds an update trigger to increase the version column on update
        /// </summary>
        /// <param name="transformationProvider"></param>
        /// <param name="triggerName">name of the trigger</param>
        /// <param name="tableName">name of the table</param>
        public static void AddVersionTrigger(this ITransformationProvider transformationProvider, string triggerName, string tableName)
        {
            if (transformationProvider == null) throw new ArgumentNullException("transformationProvider");
            if (tableName == null) throw new ArgumentNullException("tableName");

            var str =
                string.Format(@"CREATE TRIGGER {1} ON  [dbo].[{0}] AFTER UPDATE
                        AS 
                        BEGIN
	                        -- SET NOCOUNT ON added to prevent extra result sets from
	                        -- interfering with SELECT statements.
	                        SET NOCOUNT ON;

                            -- Update statements for trigger here
	                        UPDATE dbo.[{0}]
	                        SET dbo.[{0}].Version = Inserted.Version + 1
	                        FROM dbo.[{0}], Inserted
	                        WHERE dbo.[{0}].{0}Id = Inserted.{0}Id
                        END", tableName, FormatSQLParameter(triggerName));

            transformationProvider.ExecuteNonQuery(str);
        }

        #endregion

        #region Remove Trigger

        /// <summary>
        /// Removes a trigger from the database
        /// </summary>
        /// <param name="transformationProvider"></param>
        /// <param name="triggerName"></param>
        public static void RemoveTrigger(this ITransformationProvider transformationProvider, string triggerName)
        {
            var str = string.Format("DROP TRIGGER {0}", FormatSQLParameter(triggerName));

            transformationProvider.ExecuteNonQuery(str);
        }

        #endregion

        #region Execute script

        /// <summary>
        /// Executes a script that is an embedded resource in the calling assembly
        /// </summary>
        /// <param name="transformationProvider"></param>
        /// <param name="scriptFileName">file name of the script</param>
        public static void ExecuteScript(this ITransformationProvider transformationProvider, string scriptFileName)
        {
            ExecuteScript(transformationProvider, string.Empty, scriptFileName);
        }

        /// <summary>
        /// Executes a script that is an embedded resource in the calling assembly
        /// </summary>
        /// <param name="transformationProvider"></param>
        /// <param name="scriptsFolder">folder containing the scripts</param>
        /// <param name="scriptFileName">file name of the script</param>
        public static void ExecuteScript(this ITransformationProvider transformationProvider, string scriptsFolder, string scriptFileName)
        {
            if (transformationProvider == null) throw new ArgumentNullException("transformationProvider");
            if (scriptFileName == null) throw new ArgumentNullException("scriptFileName");
            if (scriptsFolder == null) scriptsFolder = string.Empty;

            var assembly = Assembly.GetCallingAssembly();
            var sqlScript = GetEmbeddedScript(assembly, scriptsFolder, scriptFileName);

            transformationProvider.ExecuteNonQuery(sqlScript);
        }



        /// <summary>
        /// Executes a batch script that is an embedded resource in the calling assembly
        /// </summary>
        /// <param name="transformationProvider"></param>
        /// <param name="batchScriptFileName">file name of the batch script</param>
        /// <param name="batchScriptSplitters">strings which split commands in the batchScript</param>
        public static void ExecuteBatchScripts(this ITransformationProvider transformationProvider, string batchScriptFileName, string[] batchScriptSplitters)
        {
            ExecuteBatchScripts(transformationProvider, string.Empty, batchScriptFileName, batchScriptSplitters);
        }


        /// <summary>
        /// Executes a batch script that is an embedded resource in the calling assembly
        /// </summary>
        /// <param name="transformationProvider"></param>
        /// <param name="scriptsFolder">folder containing the scripts</param>
        /// <param name="batchScriptFileName">file name of batch the script</param>
        /// <param name="batchScriptSplitters">strings which split commands in the batchScript</param>
        public static void ExecuteBatchScripts(this ITransformationProvider transformationProvider, string scriptsFolder, string batchScriptFileName, string[] batchScriptSplitters)
        {
            if (transformationProvider == null) throw new ArgumentNullException("transformationProvider");
            if (batchScriptFileName == null) throw new ArgumentNullException("batchScriptFileName");
            if (scriptsFolder == null) scriptsFolder = string.Empty;

            var assembly = Assembly.GetCallingAssembly();
            var sqlScript = GetEmbeddedScript(assembly, scriptsFolder, batchScriptFileName);
            string[] commands = sqlScript.Split(batchScriptSplitters, StringSplitOptions.RemoveEmptyEntries);
            foreach (var command in commands) {
                transformationProvider.ExecuteNonQuery(command);
            }
        }
        #endregion

        #region Add / Remove Entity Table

        /// <summary>
        /// Adds an entity table.
        /// The entity table contains following columns:
        /// <list type="bullet">
        /// <item>ID column with name 'tableName'ID</item>
        /// <item>Version column</item>
        /// <item>CreationDate column</item>
        /// <item>ModificationDate column</item>
        /// <item>CreationUser column</item>
        /// <item>ModificaitonUser column</item>
        /// </list>
        /// A trigger to the version-column is also added.
        /// </summary>
        /// <param name="transformationProvider"></param>
        /// <param name="tableName">name of the table</param>
        public static void AddEntityTable(this ITransformationProvider transformationProvider, string tableName)
        {
            transformationProvider.AddEntityTable(tableName, new Column[] { });
        }

        /// <summary>
        /// Adds an entity table.
        /// The entity table contains following columns:
        /// <list type="bullet">
        /// <item>ID column with name 'tableName'ID</item>
        /// <item>Version column</item>
        /// <item>CreationDate column</item>
        /// <item>ModificationDate column</item>
        /// <item>CreationUser column</item>
        /// <item>ModificaitonUser column</item>
        /// </list>
        /// A trigger to the version-column is also added.
        /// </summary>
        /// <param name="transformationProvider"></param>
        /// <param name="tableName">name of the table</param>
        /// <param name="columns">columns to add to the table</param>
        public static void AddEntityTable(this ITransformationProvider transformationProvider, string tableName, params Column[] columns)
        {
            if (transformationProvider == null) throw new ArgumentNullException("transformationProvider");
            if (tableName == null) throw new ArgumentNullException("tableName");
            if (columns == null) throw new ArgumentNullException("columns");

            var columnList = new List<Column>
                                 {
                                     new Column(tableName + "ID", DbType.Int32, ColumnProperty.PrimaryKeyWithIdentity),
                                     new Column("Version", DbType.Int32, ColumnProperty.NotNull),
                                     new Column("CreationDate", DbType.DateTime, ColumnProperty.Null),
                                     new Column("ModificationDate", DbType.DateTime, ColumnProperty.Null),
                                     new Column("CreationUser", DbType.String, 50, ColumnProperty.Null),
                                     new Column("ModificationUser", DbType.String, 50, ColumnProperty.Null)
                                 };

            foreach (var column in columns) {
                columnList.Add(column);
            }

            transformationProvider.AddTable(tableName, columnList.ToArray());

            transformationProvider.AddVersionTrigger("UDP_" + tableName, tableName);
        }

        /// <summary>
        /// Removes the version trigger and the entity table from the database
        /// </summary>
        /// <param name="transformationProvider"></param>
        /// <param name="tableName"></param>
        public static void RemoveEntityTable(this ITransformationProvider transformationProvider, string tableName)
        {
            if (transformationProvider == null) throw new ArgumentNullException("transformationProvider");
            if (tableName == null) throw new ArgumentNullException("tableName");

            transformationProvider.RemoveTrigger("UDP_" + tableName);
            transformationProvider.RemoveTable(tableName);
        }

        #endregion

        #region Add / Remove Foreignkey with index

        /// <summary>
        /// Adds a foreign key and creates an index (non unique, non clustered) on the foreignkey columns in the foreign table
        /// </summary>
        /// <param name="transformationProvider"></param>
        /// <param name="name"></param>
        /// <param name="foreignTable"></param>
        /// <param name="foreignColumn"></param>
        /// <param name="primaryTable"></param>
        /// <param name="primaryColumn"></param>
        public static void AddForeignKeyWithIndex(this ITransformationProvider transformationProvider, string name, string foreignTable, string foreignColumn, string primaryTable, string primaryColumn)
        {
            transformationProvider.AddForeignKeyWithIndex(name, foreignTable, new[] { foreignColumn }, primaryTable, new[] { primaryColumn });
        }

        /// <summary>
        /// Adds a foreign key and creates an index (non unique, non clustered) on the foreignkey columns in the foreign table
        /// </summary>
        /// <param name="transformationProvider"></param>
        /// <param name="name"></param>
        /// <param name="foreignTable"></param>
        /// <param name="foreignColumns"></param>
        /// <param name="primaryTable"></param>
        /// <param name="primaryColumns"></param>
        public static void AddForeignKeyWithIndex(this ITransformationProvider transformationProvider, string name, string foreignTable, string[] foreignColumns, string primaryTable, string[] primaryColumns)
        {
            transformationProvider.AddForeignKey(name, foreignTable, foreignColumns, primaryTable, primaryColumns);

            var columns = foreignColumns.Select(x => transformationProvider.GetColumnByName(foreignTable, x));
            var indexColumns = columns.Select(x => new IndexKeyColumn(x, SortDirection.Asc));

            var indexName = FormatIndexName(foreignTable, foreignColumns);

            transformationProvider.AddIndex(indexName, foreignTable, indexColumns, false, true);
        }

        /// <summary>
        /// Removes a foreign key and the created index
        /// </summary>
        /// <param name="transformationProvider"></param>
        /// <param name="name"></param>
        /// <param name="foreignTable"></param>
        /// <param name="foreignColumn"></param>
        public static void RemoveForeignKeyWithIndex(this ITransformationProvider transformationProvider, string name, string foreignTable, string foreignColumn)
        {
            transformationProvider.RemoveForeignKeyWithIndex(name, foreignTable, new[] { foreignColumn });
        }

        public static void RemoveForeignKeyWithIndex(this ITransformationProvider transformationProvider, string name, string foreignTable, string[] foreignColumns)
        {
            var indexName = FormatIndexName(foreignTable, foreignColumns);

            transformationProvider.RemoveIndex(indexName, foreignTable);

            transformationProvider.RemoveForeignKey(foreignTable, name);
        }

        #endregion

        #region RefreshEnumConstraints

        /// <summary>
        /// Creates a constraint on every column specified using the <see cref="DbUsageAttribute"/>
        /// Only values in the enum type will be allowed
        /// </summary>
        /// <param name="transformationProvider"></param>
        /// <param name="defaultValue">Default value: all unknown values in the column will be set to this value</param>
        public static void RefreshEnumConstraints<T>(this ITransformationProvider transformationProvider, T defaultValue)
        {
            transformationProvider.RefreshEnumConstraints<T>(defaultValue, false);
        }

        public static void RefreshEnumConstraints<T>(this ITransformationProvider transformationProvider, T defaultValue, bool throwOnError)
        {
            var enumType = typeof(T);
            var dbUsages = enumType.GetCustomAttributes<DbUsageAttribute>();

            foreach (var usage in dbUsages) {
                transformationProvider.RefreshEnumConstraint<T>(usage.TableName, usage.ColumnName, defaultValue, throwOnError);
            }
        }

        /// <summary>
        /// Creates a constraint on every column specified using the <see cref="DbUsageAttribute"/>
        /// Only values in the enum type will be allowed
        /// </summary>
        /// <param name="transformationProvider"></param>
        public static void RefreshEnumConstraints<T>(this ITransformationProvider transformationProvider)
        {
            transformationProvider.RefreshEnumConstraints<T>(false);
        }

        /// <summary>
        /// Creates a constraint on every column specified using the <see cref="DbUsageAttribute"/>
        /// Only values in the enum type will be allowed
        /// </summary>
        /// <param name="transformationProvider"></param>
        /// <param name="throwOnError"></param>
        public static void RefreshEnumConstraints<T>(this ITransformationProvider transformationProvider, bool throwOnError)
        {
            var enumType = typeof(T);
            var dbUsages = enumType.GetCustomAttributes<DbUsageAttribute>();

            foreach (var usage in dbUsages) {
                transformationProvider.RefreshEnumConstraint<T>(usage.TableName, usage.ColumnName, throwOnError);
            }
        }

        /// <summary>
        /// Creates a constraint on the columns of the table
        /// Only values in the enum type will be allowed
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="transformationProvider"></param>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="defaultValue">Default value: all unknown values in the column will be set to this value</param>
        public static void RefreshEnumConstraint<T>(this ITransformationProvider transformationProvider, string tableName, string columnName, T defaultValue)
        {
            transformationProvider.RefreshEnumConstraint<T>(tableName, columnName, defaultValue, false);
        }

        /// <summary>
        /// Creates a constraint on the columns of the table
        /// Only values in the enum type will be allowed
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="transformationProvider"></param>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="defaultValue">Default value: all unknown values in the column will be set to this value</param>
        public static void RefreshEnumConstraint<T>(this ITransformationProvider transformationProvider, string tableName, string columnName, T defaultValue, bool throwOnError)
        {
            try {
                var enumType = typeof(T);

                var enumConstraint = string.Format("NOT({0})", BuildConstraintExpression(enumType, columnName));
                var updateStatement = string.Format("UPDATE {0} SET {1} = {2} WHERE {3} AND ({1} IS NOT NULL)", FormatSQLParameter(tableName), FormatSQLParameter(columnName),
                                                    Convert.ToInt32(defaultValue), enumConstraint);

                transformationProvider.ExecuteNonQuery(updateStatement);

                transformationProvider.RefreshEnumConstraint<T>(tableName, columnName, throwOnError);
            }
            catch (SqlException) {
                if (throwOnError) throw;
            }
        }

        /// <summary>
        /// Creates a constraint on the columns of the table
        /// Only values in the enum type will be allowed
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="transformationProvider"></param>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        public static void RefreshEnumConstraint<T>(this ITransformationProvider transformationProvider, string tableName, string columnName)
        {
            transformationProvider.RefreshEnumConstraint<T>(tableName, columnName, false);
        }

        /// <summary>
        /// Creates a constraint on the columns of the table
        /// Only values in the enum type will be allowed
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="transformationProvider"></param>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="throwOnError"></param>
        public static void RefreshEnumConstraint<T>(this ITransformationProvider transformationProvider, string tableName, string columnName, bool throwOnError)
        {
            try {
                var enumType = typeof(T);

                //first drop constraint
                transformationProvider.RemoveEnumConstraint(tableName, columnName, true, throwOnError);

                var enumConstraint = BuildConstraintExpression(enumType, columnName);
                var constraintName = FormatEnumConstraintName(tableName, columnName);

                var constraintStatement = string.Format("ALTER TABLE {0} WITH CHECK ADD CONSTRAINT {1} CHECK {2}", FormatSQLParameter(tableName), constraintName, enumConstraint);
                var checkStatement = string.Format("ALTER TABLE {0} CHECK CONSTRAINT {1}", FormatSQLParameter(tableName), constraintName);

                transformationProvider.ExecuteNonQuery(constraintStatement);
                transformationProvider.ExecuteNonQuery(checkStatement);
            }
            catch (SqlException) {
                if (throwOnError) throw;
            }
        }

        /// <summary>
        /// Drops all constraints created using RefreshEnumConstraints<T>.
        /// Will silently fail when an error occurs.
        /// </summary>
        /// <param name="transformationProvider"></param>
        public static void RemoveEnumConstraints<T>(this ITransformationProvider transformationProvider)
        {
            transformationProvider.RemoveEnumConstraints<T>(false);
        }

        /// <summary>
        /// Drops all constraints created using RefreshEnumConstraints<T>
        /// </summary>
        /// <param name="transformationProvider"></param>
        /// <param name="throwOnError"></param>
        public static void RemoveEnumConstraints<T>(this ITransformationProvider transformationProvider, bool throwOnError)
        {
            try {
                var enumType = typeof(T);
                var dbUsages = enumType.GetCustomAttributes<DbUsageAttribute>();

                foreach (var usage in dbUsages) {
                    //first drop constraint
                    transformationProvider.RemoveEnumConstraint(usage.TableName, usage.ColumnName, true, throwOnError);
                }
            }
            catch (SqlException) {
                if (throwOnError) throw;
            }
        }

        /// <summary>
        /// Drops a constraint created using RefreshEnumConstraints<T>
        /// </summary>
        /// <param name="transformationProvider"></param>
        /// <param name="enumType"></param>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="addIfExists"></param>
        public static void RemoveEnumConstraint(this ITransformationProvider transformationProvider, string tableName, string columnName, bool addIfExists)
        {
            if (tableName == null) throw new ArgumentNullException("tableName");
            if (columnName == null) throw new ArgumentNullException("columnName");

            var constraintName = FormatEnumConstraintName(tableName, columnName);
            var ifExistsStatement = string.Format("if exists(select * from dbo.sysobjects where id = object_id(N'[dbo].{0}')) ", constraintName);
            var statement = addIfExists ? ifExistsStatement : string.Empty;

            statement = statement + string.Format("ALTER TABLE {0} DROP CONSTRAINT {1}", FormatSQLParameter(tableName), constraintName);

            transformationProvider.ExecuteNonQuery(statement);
        }

        /// <summary>
        /// Drops a constraint created using RefreshEnumConstraints<T>
        /// </summary>
        /// <param name="transformationProvider"></param>
        /// <param name="enumType"></param>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="addIfExists"></param>
        /// <param name="throwOnError"></param>
        public static void RemoveEnumConstraint(this ITransformationProvider transformationProvider, string tableName, string columnName, bool addIfExists, bool throwOnError)
        {
            try {
                if (tableName == null) throw new ArgumentNullException("tableName");
                if (columnName == null) throw new ArgumentNullException("columnName");

                var constraintName = FormatEnumConstraintName(tableName, columnName);
                var ifExistsStatement = string.Format("if exists(select * from dbo.sysobjects where id = object_id(N'[dbo].{0}')) ", constraintName);
                var statement = addIfExists ? ifExistsStatement : string.Empty;

                statement = statement + string.Format("ALTER TABLE {0} DROP CONSTRAINT {1}", FormatSQLParameter(tableName), constraintName);

                transformationProvider.ExecuteNonQuery(statement);
            }
            catch (SqlException) {
                if (throwOnError) throw;
            }
        }

        #endregion

        #region Private Helper Classes

        private static string FormatSQLParameter(string parameter)
        {
            return string.Format("[{0}]", parameter);
        }

        private static string GetEmbeddedScript(Assembly assembly, string scriptsFolder, string fileName)
        {
            var fullFileName = scriptsFolder.Replace('\\', '.') + "." + fileName;

            //We get the first resource with an name ending to the fullFilename. This is done to ignore the namespace of the assembly
            var resourceName = assembly.GetManifestResourceNames().Where(x => x.EndsWith(fullFileName)).FirstOrDefault();
            var str = assembly.GetManifestResourceStream(resourceName);

            if (str == null)
                throw new IOException(string.Format("Could not locate embedded resource '{0}' in assembly '{1}'", fullFileName, assembly.GetName()));

            var reader = new StreamReader(str);

            return reader.ReadToEnd();
        }

        private static string FormatIndexName(string tableName, string[] columnName)
        {
            return string.Format("IX_{0}_{1}", tableName, string.Concat(columnName));
        }

        private static string FormatEnumConstraintName(string tableName, string columnName)
        {
            return FormatSQLParameter(string.Format("CK_{0}_{1}", tableName, columnName));
        }

        private static string BuildConstraintExpression(Type enumType, string columnName)
        {
            var enumValues = Enum.GetValues(enumType);
            if (enumValues.Length == 0) throw new MigrationException("Unable to build enum constraint. Enum has no values.");

            var str = new StringBuilder();
            str.Append("(");

            for (var i = 0; i < enumValues.Length; i++) {
                if (i > 0) str.Append(" OR ");

                str.Append(string.Format("({0} = {1})", FormatSQLParameter(columnName), (int)enumValues.GetValue(i)));
            }

            str.Append(")");
            return str.ToString();
        }

        #endregion

    }
}