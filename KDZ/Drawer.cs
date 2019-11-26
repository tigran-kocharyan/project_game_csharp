using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDZ
{
    public class Drawer
        //This class is supposed to make the CLI perfomance much better by changing the color of messages.
    {
        static public void RedConsole(string message)
        // The function takes the message to the CLI and makes it Red for a good perfomance.
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        static public void GreenConsole(string message)
        // The function takes the message to the CLI and makes it Green for a good perfomance.
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        static public void MagentaConsole(string message)
        // The function takes the message to the CLI and makes it Magenta for a good perfomance.
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
