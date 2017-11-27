using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTCI_1._5_Compression
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintHeaderMsg(1, 5, "Compression");

            string string1 = "aabcccccaaaqqqqqqqqqqqqq";
            Console.WriteLine("Original:        " + string1);
            Console.WriteLine();

            compress_concat(string1);

            compress_stringbuilder(string1);

            compress_array(string1);

            Console.ReadLine();
        }

        private static void compress_array(string string1)
        {
            Stopwatch sw = Stopwatch.StartNew();
            
            if (string1.Length == 0)
                return;

            int l = getCompressedLength(string1);

            char[] charArray = new char[l];
            int array_cursor = 0;

            int count = 1;
            char lastchar = string1.ElementAt(0); // prime with first character

            for (int string_cursor = 1; string_cursor < string1.Length; ++string_cursor)
            {
                if (string1.ElementAt(string_cursor) == lastchar)
                {
                    ++count;
                }
                else
                {
                    //copy the character to the array
                    charArray[array_cursor++] = lastchar;

                    //copy the count to the array
                    if (count < 10)
                        charArray[array_cursor++] = char.Parse( count.ToString() );
                    else if (count < 100)
                    {
                        int temp = count / 10;
                        charArray[array_cursor++] = char.Parse(temp.ToString());
                        charArray[array_cursor++] = char.Parse((count - (temp * 10)).ToString());
                    }
                    
                    lastchar = string1.ElementAt(string_cursor);
                    count = 1;
                }
            }
            
            charArray[array_cursor++] = lastchar;
            
            if (count < 10)
                charArray[array_cursor++] = char.Parse(count.ToString());
            else if (count < 100)
            {
                int temp = count / 10;
                charArray[array_cursor++] = char.Parse(temp.ToString());
                charArray[array_cursor++] = char.Parse((count - (temp * 10)).ToString());
            }

            sw.Stop();
            
            Console.WriteLine(    "Array:           " + new string(charArray));
            if ( charArray.Length < string1.Length)
                Console.WriteLine("                 Compressed string is smaller than the original!");
            else
                Console.WriteLine("                 Compressed string is NOT smaller than the original.");
            Console.WriteLine("                 " + sw.ElapsedTicks + " ticks");
            Console.WriteLine("                 82 Bytes");

            Console.WriteLine();
        }

        private static void compress_stringbuilder(string string1)
        {
            Stopwatch sw = Stopwatch.StartNew();

            if (string1.Length == 0)
                return;

            int l = getCompressedLength(string1);

            StringBuilder compressed = new StringBuilder(l);     
                        
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

            sw.Stop();

            Console.WriteLine(    "StringBuilder:   " + compressed);
            if (compressed.Length < string1.Length)
                Console.WriteLine("                 Compressed string is smaller than the original!");
            else
                Console.WriteLine("                 Compressed string is NOT smaller than the original.");
            Console.WriteLine("                 " + sw.ElapsedTicks + " ticks");
            Console.WriteLine("                 166 Bytes");
            
            Console.WriteLine();
        }

        private static int getCompressedLength(string string1)
        {
            if (string1.Length == 0)
                return 0;
            else if (string1.Length == 1)
                return 2;

            int length = 0;
            int count_char = 1;
            char lastchar = string1.ElementAt(0); // prime with first character

            for (int i = 1; i < string1.Length; ++i)
            {
                if (string1.ElementAt(i) == lastchar)
                {
                    ++count_char;
                }
                else
                {
                    ++length; // 1 for the character

                    if (count_char < 10) // now add places for the count
                        ++length;
                    else if (count_char < 100)
                        length += 2;
                    else if (count_char < 1000)
                        length += 3;

                    lastchar = string1.ElementAt(i);
                    count_char = 1;
                }
            }

            ++length; // 1 for the character

            if (count_char < 10)
                ++length;
            else if (count_char < 100)
                length += 2;
            else if (count_char < 1000)
                length += 3;

            return length;

            
        }

        private static void compress_concat(string string1)
        {
            Stopwatch sw = Stopwatch.StartNew();

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

            sw.Stop();

            Console.WriteLine(    "Concat:          " + compressed);
            if (compressed.Length < string1.Length)
                Console.WriteLine("                 Compressed string is smaller than the original!");
            else
                Console.WriteLine("                 Compressed string is NOT smaller than the original.");
            Console.WriteLine("                 " + sw.ElapsedTicks + " ticks");
            Console.WriteLine("                 2632 Bytes");
            
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
