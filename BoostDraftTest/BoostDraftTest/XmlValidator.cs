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
         * Need to trim off leading and trailing whitespace
         * Add the find tags check into Tokenizer
         * Behavior of this method should create tokens from an XML String: <Code>Hello World</Code> would create a list of tokens [<Code>, </Code>]
         * If something is not tokenized by this method, then the string is not valid XML.
         * 
         */
        public static List<string> tokenize(string xml)
        {
            List<string> tokens = new List<string>();
            int startIndex = 0;
            int endIndex = 0;


            // Trim off leading and trailing whitespace
            xml = xml.TrimStart(' ');
            xml = xml.TrimEnd(' ');
            // If the first char is not a < then this is not a valid xml string
            if (xml[0] != '<')
            {
                Console.WriteLine("False: Xml doesn't start with <");
                return tokens;
            }

            while (startIndex < xml.Length)
            {
                if (xml[startIndex] == '<')
                {
                    endIndex = xml.IndexOf('>', startIndex);
                    tokens.Add(xml.Substring(startIndex, endIndex - startIndex + 1));
                    startIndex = endIndex + 1;
                }
                else
                {
                    startIndex++;
                }
            }
            // Remove before submission
            for (int i = 0; i < tokens.Count; i++)
            {
                Console.WriteLine("Token: " + tokens[i]);
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
                // Edge Case: <a/> is valid xml
                if (token.StartsWith("<") && token.EndsWith("/>") && tokens.Count() == 1) 
                { 
                    Console.WriteLine("True: <a/> style xml");
                    isValid = true;
                    return isValid; 
                }

                // if stack count is odd, xml string cannot have matching tags, so return false (Not in the above format).
                if (tokens.Count() % 2 == 1)
                {
                    Console.WriteLine("False: Odd number of tags not in <a/> format"); // Remove before submission
                    isValid = false;
                    return isValid;
                }
                // Push the tokenized strings to the stack only if they are opening tags.
                if (token.StartsWith('<') && !(token.Contains("</")))
                {
                    tagStack.Push(token);
                }
                // When the next element in the list is a closing tag compare it to the last element in the stack. Pop if they match
                if (token.StartsWith("</"))
                {
                    string currentTag = token;
                    currentTag = currentTag.Replace("/", "");
                    Console.WriteLine("currentTag: " + currentTag);
                    Console.WriteLine("Popped: " + tagStack.Peek());   
                    if (currentTag == tagStack.Pop())
                    {
                        isValid = true;
                        Console.WriteLine("True: Tags Match");
                        return isValid;
                    }
                    // If the tags do not match, invalid XML
                    else
                    {
                        isValid= false;
                        Console.Write("False: Tags did not match.");
                        return isValid;
                    }
                }
            }
            Console.WriteLine("End of Method"); // Remove before submission
            Console.WriteLine(isValid);
            return isValid;
        }
    }
}
