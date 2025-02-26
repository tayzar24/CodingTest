using System;
using System.Collections.Generic;
using System.Text;

namespace OldPhonePad
{
    class Program
    {
        public static string OldPhonePad(string input)
        {
            if (string.IsNullOrEmpty(input) || input[^1] != '#')
                return ""; 

            input = input.TrimEnd('#'); 

            StringBuilder output = new StringBuilder();
            StringBuilder currentSequence = new StringBuilder();

            
            string[] keyMap =
            {
            " ",    // 0
            "",     // 1 
            "ABC",  // 2
            "DEF",  // 3
            "GHI",  // 4
            "JKL",  // 5
            "MNO",  // 6
            "PQRS", // 7
            "TUV",  // 8
            "WXYZ"  // 9
        };

            char lastKey = '\0';

            foreach (char c in input)
            {
                if (c == '*' && string.IsNullOrEmpty(currentSequence.ToString()))
                {
                    if (output.Length > 0)
                        output.Length--;
                    continue;
                }

                if (c == ' ') 
                {
                    if (currentSequence.Length > 0)
                    {
                        output.Append(ProcessKeySequence(currentSequence.ToString(), keyMap));
                        currentSequence.Clear();
                    }
                    continue;
                }

                if (char.IsDigit(c)) 
                {
                    if (c != lastKey && currentSequence.Length > 0)
                    {
                        output.Append(ProcessKeySequence(currentSequence.ToString(), keyMap));
                        currentSequence.Clear();
                    }

                    currentSequence.Append(c);
                    lastKey = c;
                }
            }

            if (currentSequence.Length > 0)
            {
                output.Append(ProcessKeySequence(currentSequence.ToString(), keyMap));
            }

            return output.ToString();
        }

        private static char ProcessKeySequence(string sequence, string[] keyMap)
        {
            int key = sequence[0] - '0';
            if (key < 0 || key >= keyMap.Length || keyMap[key] == "")
                return '?';

            int index = (sequence.Length - 1) % keyMap[key].Length;
            return keyMap[key][index];
        }

        public static void Main()
        {
            Console.WriteLine("Input : 33# => " + OldPhonePad("33#"));
            Console.WriteLine("Input : 227*# => " + OldPhonePad("227*#"));
            Console.WriteLine("Input : 4433555 555666# =>" + OldPhonePad("4433555 555666#"));
            Console.WriteLine("Input : 8 88777444666*664# =>" + OldPhonePad("8 88777444666*664#"));
            Console.WriteLine("Input : 8 2 999 9999 2 777 6 999 2 8 66 666 33# =>" + OldPhonePad("8 2 999 9999 2 777 6 999 2 8 66 666 33#"));
            while (true)
            {
                Console.WriteLine("Enter  text and press Enter");
                string userInput = Console.ReadLine();

                if (!string.IsNullOrEmpty(userInput))
                {
                    string message = "Input : {0} => {1}";
                    Console.WriteLine(string.Format(message, userInput, OldPhonePad(userInput)));
                }
            }
        }

    }
}
