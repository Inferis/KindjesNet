using System;

namespace Inferis.KindjesNet.Core.Managers
{
    public class DateFixer : IDateFixer
    {
        public DateTime Fix(ref int year, ref int month, ref int day)
        {
            throw new NotImplementedException();
        }

        public DateTime Fix(int year, int month, int day)
        {
            throw new NotImplementedException();
        }

        public DateTime Fix(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}