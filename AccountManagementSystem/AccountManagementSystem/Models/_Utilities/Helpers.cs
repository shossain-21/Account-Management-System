using System;

namespace AccountManagementSystem.Models._Utilities 
{
    public static class Helpers
    {
        public static string FixNumber(this double amount)
        {
            if (amount == 0)
            {
                return "";
            }
            else
            {
                return amount.ToString("N2");
            }
        }

        public static string FixDate(this DateTime date)
        {
            if (date == default)
            {
                return "";
            }
            else
            {
                return $"{date.ToString("d")}";
            }
        }

        public static string FixNumber(this decimal amount)
        {
            if (amount == 0)
            {
                return "";
            }
            else
            {
                return amount.ToString("N2");
            }
        }
    }
}