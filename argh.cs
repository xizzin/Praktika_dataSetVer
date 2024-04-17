using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prOneDataSetVer
{
    internal class argh
    {
        public static bool isNotPermittedIn(char ch)
        {
            bool result = ch == '*' ||
                ch == '-' ||
                ch == '!' ||
                ch == '@' ||
                ch == '"' ||
                ch == '#' ||
                ch == '№' ||
                ch == '£' ||
                ch == '^' ||
                ch == '(' ||
                ch == ')' ||
                ch == '?' ||
                ch == '{' ||
                ch == '}' ||
                ch == '[' ||
                ch == ']' ||
                ch == '+' ||
                ch == ':' ||
                ch == ';' ||
                ch == '/' ||
                ch == '~' ||
                ch == '|' ||
                ch == '.' ||
                ch == ',' ||
                ch == '$' ||
                ch == '0' ||
                ch == '1' ||
                ch == '2' ||
                ch == '3' ||
                ch == '4' ||
                ch == '5' ||
                ch == '6' ||
                ch == '7' ||
                ch == '8' ||
                ch == '9';
            return result;
        }
    }
}
