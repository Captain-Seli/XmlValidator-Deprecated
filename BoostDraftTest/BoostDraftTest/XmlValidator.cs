using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoostDraftTest
{
    internal class XmlValidator
    {
        /* This file needs many fixes
         * Do not use Console.WriteLine() in final solution, only for debugging
         * Use the Stack approach for the nest order
         * tokenize should determine whether or not an XML fails immediately
         * Add Unit Tests
         * Utilize the tools of .NET7
         * Use string and not String
         * 
         */

   
        /* Tokenize each tag within the XML string
         * This method is broken
         * Need to trim off leading and trailing whitespace
         * Add the find tags check into Tokenizer
         * Behavior of this method should create tokens from an XML String: <Code>Hello World</Code> would create a list of tokens [<Code>, </Code>]
         * If something is not tokenized by this method, then the string is not valid XML.
         * 
         */
        public static List<string> tokenize(string xml)
        {
            List<string> tokens = new List<string>();
            int start = xml.IndexOf('<');
            int position;
           
            xml = xml.TrimStart(' ');
            xml = xml.TrimEnd(' ');
            Console.WriteLine(xml); // Remove before submission
            if (xml[0] != '<')
            {
                Console.WriteLine("False");
                return tokens;
            }
            if (xml.Length > 1)
            {
                // Extract tokens from the string.
                do
                {
                    position = xml.IndexOf('>', start);
                    if (position >= 0) // This line makes '<' a valid xml string
                    {
                        tokens.Add(xml.Substring(start, position - start + 1).Trim());
                        start = position + 1;
                        // Strip out words Hello world</Code> -> ><
                        int index = xml.IndexOf('<', start);

                        if (index != -1)
                        {
                            start = index;
                        }
                    }
                } while (position > 0);
            }
            return tokens;
        }

        // Determine the proper nesting order of the XML tokens.
        /* Redesign to utilize a Stack
         * 
         */
        public static bool determineNestOrder(List<string> tokens)
        {
            // Input: <Design><Code>hello world</Code></Design> Output: True
            // Input: <Design><Code>hello world</Code></Design><People> Output: False

            // Things about the tags:
            // every opening tag has a closing tag
            // each open tag has to exactly string match the closing tag except for the '/' that denotes a close tag
            // all we need to do is immediately fail strings with odd numbers of tags and check that each substring in the first half of tags exists in order using math

            bool isNestedProperly = false;
            Stack<string> tagStack = new Stack<string>();

            // if it's odd, it cannot have matching tokens, so return false.
            if (tokens.Count() % 2 == 1)
            {
                Console.WriteLine(isNestedProperly);
                return isNestedProperly;
            }

            // The amount of tags is equal on both sides so as the index increases, it must match the similarly decreasing index
            // Example:
            /*
             * <1><2><3><text></3></2></1>
             * |  |  |        |   |   |
             * 0  1  2        3   4   5
             * 
             * [0] matches [5]
             * [1] matches [4]
             * [2] matches [3]
             * 
             */

            // Break the token list in half to compare for the above
            for (int i = 0; i < tokens.Count / 2; i++)
            {
                // Peform two checks:
                // 1. The substrings match
                // 2. The closing tag is in the second half of the tokens list

                // get the substring of the token
                string subStr = tokens[i].Trim(new Char[] { '<', '>' });

                //Check that there's no '/' in the opening tag
                if (subStr.Contains('/'))
                {
                    Console.WriteLine(isNestedProperly);
                    return isNestedProperly;
                }

                // Check for a match and '/' in corresponding close tag
                if (!(tokens[tokens.Count - (i + 1)].Contains(subStr)) || !(tokens[tokens.Count - (i + 1)].Contains('/')))
                {
                    Console.WriteLine(isNestedProperly);
                    return isNestedProperly;
                }
            }
            isNestedProperly = true;
            Console.WriteLine(isNestedProperly);
            return isNestedProperly;
        }
    }
}
