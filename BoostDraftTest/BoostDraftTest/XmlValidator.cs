using System.Runtime.CompilerServices;

namespace BoostDraftTest
{
    internal class XmlValidator
    {
        /*
         * Do not use Console.WriteLine() in final solution, only for debugging
         * Use the Stack approach for the nest order
         * tokenize should determine whether or not an XML fails immediately
         * Add Unit Tests
         * Utilize the tools of .NET7
         * Use string and not String
         * 
         * BUGS:
         * <Code>hello</Code>> produces true
         * 
         */


        /* Tokenize each tag within the XML string
         * Behavior of this method should create tokens from an XML String: <Code>Hello World</Code> would create a list of tokens [<Code>, </Code>]
         * If something is not tokenized by this method, then the string is not valid XML.
         * Line 50: System.ArgumentOutOfRangeException: 'Length cannot be less than zero.
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
                Console.WriteLine("False: Xml doesn't start with <"); // Remove before submission
                return tokens;
            }

            while (startIndex < xml.Length)
            {
                startIndex = xml.IndexOf("<", endIndex);
                if (startIndex == -1)
                {
                    return null;
                }
                endIndex = xml.IndexOf(">", startIndex);
                if (endIndex == -1) { return null; }
                if(endIndex - startIndex == 1) { return null; }
                tokens.Add(xml.Substring(startIndex, endIndex - startIndex + 1));
                startIndex = endIndex + 1;
            }
            return tokens;
        }

        public static bool DetermineNestOrder(List<string> tokens)
        {
            // Input: <Design><Code>hello world</Code></Design> Desired Output: True Current Output: False
            // Input: <Design><Code>hello world</Code></Design><People> Desired Output: False Current Output: False

            // Things about the tags:
            // every opening tag has a closing tag
            // each open tag has to exactly string match the closing tag except for the '/' that denotes a close tag
            // all we need to do is immediately fail strings with odd numbers of tags and check that each substring in the first half of tags exists in order using math
            Stack<string> tagStack = new Stack<string>();
            // If tokens is empty, then there's no valid xml string
            if (tokens != null)
            {
                if (tokens.Count == 0)
                {
                    Console.WriteLine("False: Empty Tokens List"); // Remove before submission
                    return false;
                }

                if (tokens.Count == 1) { return SelfClosingCheck(tokens[0]); }

                string lastToken = tokens.Last().Replace("/", "");
                string firstToken = tokens.First();

                // Check for Root tags
                if (firstToken == lastToken)
                {
                    foreach (string token in tokens)
                    {
                        // Push the tokenized strings to the stack only if they are opening tags.
                        if (!(token.Contains("</")) && !(SelfClosingCheck(token)))
                        {
                            tagStack.Push(token);
                        }
                        // When the next element in the list is a closing tag compare it to the last element in the stack. Pop if they match
                        if (token.StartsWith("</"))
                        {
                            // Closing tag but nothing left on Stack
                            if (tagStack.Count() == 0)
                            {
                                return false;
                            }
                            string currentTag = token;
                            currentTag = currentTag.Replace("/", "");
                            Console.WriteLine("currentTag: " + currentTag); // Remove before submission
                            Console.WriteLine("Popped: " + tagStack.Peek()); // Remove before submission
                            // If the tags do not match, invalid XML
                            if (currentTag != tagStack.Pop())
                            {
                                Console.WriteLine("False: Tags Do Not Match"); // Remove before submission
                                return false;
                            }
                        }
                    }
                    if (tagStack.Count > 0) { return false; }
                    else { return true; }
                }
                else { return false; }
            }
            else { return false; }
        }


        private static bool SelfClosingCheck(string token)
        {
            if (token.StartsWith("<") && token.EndsWith("/>"))
            {
            return true;
            }
            else
            {
                return false;
            }
        }
    }
}
