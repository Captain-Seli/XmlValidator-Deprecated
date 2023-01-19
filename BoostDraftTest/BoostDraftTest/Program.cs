using System;
using System.Collections;
using System.Runtime.Serialization;


namespace BoostDraftTest
{
    public static class XmlString
    {

        /*
         * TODO
         * Break Program into Classes with a main driver: DetermineXml(): XmlValidator Class and this main driver class
         * need to trim off leading and trailing whitespaces from the strings because those are counted as proper xml strings.
         * < counts as true for some reason - Resolve
         * Fix the git repo to make it more professional, and not look like an ape made it
         * Utilize .NET7 tools. No need for all the boilerplate stuff.
         * xml.ElemetAt(0) is the same as xml[0]. Use the array notation as this is a standard for C#
         * xml[^1] will go from the reverse order
         * adding three / will automake <summary></summary> comments
         * Tokenize needs to be fixed badly. If something doesn't tokenize, it is not valid xml and thus should fail
         * Stack approach is the way to go, endorsed by 20 year Microsfot vet! yay!
         * Add Unit Tests using NUnit that test for edge cases and everything
         * 
         * 
         */

        // Program to determine whether or not an XML string is valid or not.
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter a String: ");
            string userString = Console.ReadLine();
            DetermineXml(userString);
        }

        // Method that determins validity of the XML string.
        // This is the Driver Code
        public static bool DetermineXml(string xml)
        {
            // Tokenize the XML string and Determine proper nesting
            List<String> tokens = XmlValidator.Tokenize(xml);
            bool result = XmlValidator.DetermineNestOrder(tokens);
            Console.WriteLine(result);
            return result;
        }
    }
}


