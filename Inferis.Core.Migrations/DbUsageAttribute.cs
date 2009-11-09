using System;

namespace Inferis.Core.Migrations
{
    [AttributeUsage(AttributeTargets.Enum, AllowMultiple = true)]
    public class DbUsageAttribute : Attribute
    {
        private readonly string tableName;
        private readonly string columnName;

        public DbUsageAttribute(string tableName, string columnName)
        {
            this.tableName = tableName;
            this.columnName = columnName;
        }

        public string TableName
        {
            get { return tableName; }
        }

        public string ColumnName
        {
            get { return columnName; }
        }
    }
}