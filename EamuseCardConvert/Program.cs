using System;

namespace EamuseCardConvert
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Eamusement Card Convert");
                Console.WriteLine("A utility to convert between card IDs and back-of-card characters.");
                Console.WriteLine();
                Console.WriteLine("Usage: eamusecardconvert <card-id> ");
                Console.WriteLine();
                Console.WriteLine("Options: ");
                Console.WriteLine("  <card-id>: 16-character card ID as stored in an eAmusement card, or");
                Console.WriteLine("             the 16-character card number as shown on the back of the");
                Console.WriteLine("             e-Amusement card.  Whichever value is supplied, the result");
                Console.WriteLine("             given is the conversion to the other.");
            }
            else
            {
                //  Get the number that we'll be converting.
                string number = args[0];

                //  If the user supplied the number with spaces between the characters
                //  and did not surround the entire thing in quotes, then we'll have more
                //  than one arg. In this case, just join it all together into one string.
                if (args.Length > 1)
                {
                    number = string.Join("", args);
                }

                try
                {
                    Console.WriteLine(CardCipher.Decode(number));
                }
                catch
                {
                    try
                    {
                        string back = CardCipher.Encode(number);
                        for (int i = 4; i < back.Length; i += 4)
                        {
                            back = back.Insert(i, " ");
                            i++;
                        }
                        Console.WriteLine(back);
                    }
                    catch
                    {
                        Console.WriteLine("Bad card ID or back-of-card characters");
                    }
                }
            }
        }
    }
}
