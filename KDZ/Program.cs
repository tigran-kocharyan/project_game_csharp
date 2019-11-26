using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gun_Library;

namespace KDZ
{
    class Program
    {
        static public int ReadInt(string message, int minValue = int.MinValue, int maxValue = int.MaxValue)
        {
            int number;
            do
            {
                Drawer.GreenConsole(message);
            } while (!int.TryParse(Console.ReadLine(), out number) || number < minValue || number > maxValue);
            return number;
        }

        static void Main(string[] args)
        {
            Drawer.MagentaConsole("\t\t\t\t\t<<НАЧАЛО ТВОЕГО ВЕЛИКОГО ПУТИ>>" + Environment.NewLine);
            do
            {
                int number = ReadInt("Введите количество монет для покупки оружия: ", 1, 100);
                List<Gun> numbers = new List<Gun>();



                Drawer.RedConsole("Введите ENTER для продолжения, в противном случае, нажмите на любую клавишу");
            } while (Console.ReadKey(true).Key == ConsoleKey.Enter);
        }
    }
}
