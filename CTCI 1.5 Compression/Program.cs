using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTCI_1._5_Compression
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintHeaderMsg(1, 5, "Replace Spaces");

            string string1 = "aabcccccaaa";
            Console.WriteLine("Original:        " + string1);
            Console.WriteLine();

            compress_concat(string1);

            compress_stringbuilder(string1);

           // compress_array(string1);

            Console.ReadLine();
        }

        private static void compress_array(string string1)
        {
            throw new NotImplementedException();
        }

        private static void compress_stringbuilder(string string1)
        {
            if (string1.Length == 0)
                return;

            StringBuilder compressed = new StringBuilder(string1.Length * 2);     
                        
            int count = 1;
            char lastchar = string1.ElementAt(0); // prime with first character

            for (int i = 1; i < string1.Length; ++i)
            {
                if (string1.ElementAt(i) == lastchar)
                {
                    ++count;
                }
                else
                {
                    // string concatenation is slow!
                    compressed.Append( lastchar + count.ToString() );

                    lastchar = string1.ElementAt(i);
                    count = 1;
                }
            }
            compressed.Append( lastchar + count.ToString() ); // handle last character

            Console.WriteLine(    "StringBuilder:   " + compressed);
            if (compressed.Length < string1.Length)
                Console.WriteLine("                 Compressed string is smaller than the original!");
            else
                Console.WriteLine("                 Compressed string is NOT smaller than the original.");

            Console.WriteLine();
        }

        private static void compress_concat(string string1)
        {
            if (string1.Length == 0)
                return;

            string compressed = "";
            int count = 1;
            char lastchar = string1.ElementAt(0); // prime with first character
            
            for (int i = 1; i < string1.Length; ++i)
            {
                if (string1.ElementAt(i) == lastchar)
                {
                    ++count;
                }
                else
                {
                    // string concatenation is slow!
                    compressed += lastchar + count.ToString();

                    lastchar = string1.ElementAt(i);
                    count = 1;
                }                
            }
            compressed += lastchar + count.ToString(); // handle last character

            Console.WriteLine(    "Concat:          " + compressed);
            if (compressed.Length < string1.Length)
                Console.WriteLine("                 Compressed string is smaller than the original!");
            else
                Console.WriteLine("                 Compressed string is NOT smaller than the original.");

            Console.WriteLine();
        }

        private static void PrintHeaderMsg(int chapter, int problem, string title)
        {
            Console.WriteLine("Cracking the Coding Interview");
            Console.WriteLine("Chapter " + chapter + ", Problem " + chapter + "." + problem + ": " + title);
            Console.WriteLine();
        }
    }
}
