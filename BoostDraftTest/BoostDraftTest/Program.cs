using System;
using System.Collections;
using System.Runtime.Serialization;


namespace BoostDraftTest
{
    public static class XmlString
    {

        // Program to determine whether or not an XML string is valid or not.
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter a String: ");
            string userString = Console.ReadLine();
            DetermineXml(userString);
        }
        // Method that determins validity of the XML string.
        public static bool DetermineXml(string xml)
        {
            //Find tags
            if (xml.ElementAt(0) != '<')
            {
                Console.WriteLine("False");
                return false;
            }
            // Tokenize the XML string and Determine proper nesting
            List<String> tokens = tokenize(xml);
            bool result = determineNestOrder(tokens);
            //Return value
            
            return result;
        }

        // Helper methods

        //Tokenize each tag within the XML string
        private static List<String> tokenize(String xml)
        {
            List<String> tokens = new List<String>();
            int start = xml.IndexOf('<');
            int position;
            // Extract tokens from the string.
            do
            {
                position = xml.IndexOf('>', start);
                if (position >= 0)
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
            return tokens;
        }

        // Determine the proper nesting order of the XML tokens.
        public static bool determineNestOrder(List<String> tokens)
        {
            // Input: <Design><Code>hello world</Code></Design> Output: True
            // Input: <Design><Code>hello world</Code></Design><People> Output: False

            // Things about the tags:
            // every opening tag has a closing tag
            // each open tag has to exactly string match the closing tag except for the '/' that denotes a close tag
            // all we need to do is immediately fail strings with odd numbers of tags and check that each substring in the first half of tags exists in order using math

            bool isNestedProperly = false;

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
                String subStr = tokens[i].Trim(new Char[] { '<', '>' });

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


