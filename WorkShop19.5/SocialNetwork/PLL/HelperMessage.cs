using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.PLL
{
    public class HelperMessage
    {
        public static void Show(bool IsSuccses, string Message)
        {
            ConsoleColor consoleColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            if (IsSuccses)
                Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(Message);
            Console.ForegroundColor = consoleColor;
        }
    }
}
