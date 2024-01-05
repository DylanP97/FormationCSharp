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

            foreach (char character in brackets)
            {
                if (IsOpeningBracket(character))
                {
                    stack.Push(character);
                }
                else if (IsClosingBracket(character))
                {
                    if (stack.Count == 0 || !AreMatchingBrackets(stack.Pop(), character))
                    {
                        return false;
                    }
                }
            }
            return stack.Count == 0;
        }

        private static bool IsOpeningBracket(char character)
        {
            return character == '(' || character == '[' || character == '{' || character == '<';
        }

        private static bool IsClosingBracket(char character)
        {
            return character == ')' || character == ']' || character == '}' || character == '>';
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
