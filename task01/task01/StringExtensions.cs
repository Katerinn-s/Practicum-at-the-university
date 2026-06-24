using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace task01
{
    public static class StringExtensions
    {
        public static bool IsPalindrome(this string input)
        {
            input=input.ToLower();
            string new_input="";
            if(input.Length==0)
                    return false;
            
            foreach (char c in input)
            {
                
                if (Char.IsPunctuation(c)|| Char.IsWhiteSpace(c))
                    continue;
                else
                    new_input += c;

            }
            char[] chars = new_input.ToCharArray();
            Array.Reverse(chars);
            string reverse_string = new string(chars);
            if(new_input== reverse_string)
                return true;
            else return false;
        }

    }
}
