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
    }
}