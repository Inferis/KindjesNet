using System;

namespace Inferis.KindjesNet.Core.Managers
{
    public interface IDateFixer
    {
        DateTime Fix(ref int year, ref int month, ref int day);
        DateTime Fix(int year, int month, int day);
        DateTime Fix(DateTime date);
    }
}