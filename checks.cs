using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktika_5DataSetVer
{
    internal class checks
    {
        public static bool isNotPermittedInString(char ch)
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

        public static int LoggedWorker = 0;
    }
}
