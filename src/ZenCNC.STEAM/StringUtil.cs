using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenCNC.STEAM
{
    public class StringUtil
    {
        public static string Padding(string s, int num, char padChar, bool left)
        {
            s = s.Trim();
            string result = string.Empty;
            string padStr = string.Empty;
            for (int i = 0; i < num - s.Length; i++)
            {
                padStr += padChar;
            }
            if(left)
            {
                result = s + padStr;
            }
            else
            {
                result = padStr + s;
            }
            return result;
        }
    }
}

