using System;

namespace Inferis.KindjesNet.Core.Managers
{
    public class DateFixer : IDateFixer
    {
        public DateTime Fix(ref int year, ref int month, ref int day)
        {
            return new DateTime(year, month, day);
        }

        public DateTime Fix(int year, int month, int day)
        {
            return new DateTime(year, month, day);
        }

        public DateTime Fix(DateTime date)
        {
            return date;
        }
    }
}