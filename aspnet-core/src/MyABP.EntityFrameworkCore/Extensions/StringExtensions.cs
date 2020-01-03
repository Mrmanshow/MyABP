using System;
using System.Collections.Generic;
using System.Text;

namespace MyABP.Extensions
{
    public static class StringExtensions
    {
        public static string HumpToUnderline(this string hump)
        {
            if (string.IsNullOrWhiteSpace(hump))
            {
                return "";
            }

            string underline = "";
            foreach (var item in hump)
            {
                if (item >= 'A' && item <= 'Z')
                {
                    underline += "_" + item;
                }
                else
                {
                    underline += item;
                }
            }

            if (underline.IndexOf('_') == 0)
            {
                underline = underline.Substring(1);
            }

            return underline.ToLower();
        }
    }
}
