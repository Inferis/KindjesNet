using System;
using Migrator.Framework;

namespace Inferis.Core.Migrations
{
    public class IndexKeyColumn
    {
        public Column Column { get; private set; }
        public SortDirection SortDirection { get; private set; }

        public IndexKeyColumn(Column column, SortDirection sortDirection)
        {
            if (column == null) throw new ArgumentNullException("column");

            Column = column;
            SortDirection = sortDirection;
        }
    }
}