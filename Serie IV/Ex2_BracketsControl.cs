using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie_IV
{
    public static class BracketsControl
    {
        public static bool BracketsControls(string brackets)
        {
            Stack<char> stack = new Stack<char>();

            foreach (char bracket in brackets)
            {
                if (IsOpeningBracket(bracket))
                {
                    stack.Push(bracket);
                }
                else if (IsClosingBracket(bracket))
                {
                    if (stack.Count == 0 || !AreMatchingBrackets(stack.Pop(), bracket))
                    {
                        return false;
                    }
                }
            }
            return stack.Count == 0;
        }

        private static bool IsOpeningBracket(char bracket)
        {
            return bracket == '(' || bracket == '[' || bracket == '{' || bracket == '<';
        }

        private static bool IsClosingBracket(char bracket)
        {
            return bracket == ')' || bracket == ']' || bracket == '}' || bracket == '>';
        }

        private static bool AreMatchingBrackets(char opening, char closing)
        {
            return (opening == '(' && closing == ')') ||
                   (opening == '[' && closing == ']') ||
                   (opening == '{' && closing == '}') ||
                   (opening == '<' && closing == '>');
        }
    }
}
