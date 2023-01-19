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

            // Trim off leading and trailing whitespace
            xml = xml.TrimStart(' ');
            xml = xml.TrimEnd(' ');
            Console.WriteLine(xml); // Remove before submission
            // If the first char is not a < then this is not a valid xml string
            if (xml[0] != '<')
            {
                Console.WriteLine("False: Xml doesn't start with <");
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
        public static bool DetermineNestOrder(List<string> tokens)
        {
            // Input: <Design><Code>hello world</Code></Design> Desired Output: True Current Output: False
            // Input: <Design><Code>hello world</Code></Design><People> Desired Output: False Current Output: False

            // Things about the tags:
            // every opening tag has a closing tag
            // each open tag has to exactly string match the closing tag except for the '/' that denotes a close tag
            // all we need to do is immediately fail strings with odd numbers of tags and check that each substring in the first half of tags exists in order using math
            Stack<string> tagStack = new Stack<string>();
            bool isValid = true;
            // If tokens is empty, then there's no valid xml string
            if (tokens.Count == 0)
            {
                Console.WriteLine("False: Empty Tokens List"); // Remove before submission
                isValid= false;
                return isValid;
            }

            foreach (string token in tokens)
            {
                // Edge Case: <a/> is valid xml but <a/><a/> is NOT valid
                if (token.StartsWith("<") && token.EndsWith("/>") && tokens.Count() == 1) 
                { 
                    Console.WriteLine("True: <a/> style xml");
                    isValid = false;
                    return isValid; 
                }

                if (token.StartsWith('<'))
                {
                    tagStack.Push(token.Substring(1));
                }
                else if (token.StartsWith("</"))
                {
                    string currentTag = token.Substring(2);
                    if (tagStack.Count == 0 || currentTag != tagStack.Pop())
                    {
                        Console.WriteLine("False: Stack is empty or the tags don't match"); // Remove before submission
                        isValid = false;
                        return isValid;
                    }
                }

                // if it's odd, it cannot have matching tokens, so return false.
                if (tokens.Count() % 2 == 1)
                {
                    Console.WriteLine("False: Odd number of tags not in <a/> format"); // Remove before submission
                    isValid = false;
                    return isValid;
                }
            }
            Console.WriteLine("End of Method"); // Remove before submission
            Console.WriteLine(isValid);
            return isValid;
        }
    }
}
