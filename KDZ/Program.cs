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
        static public Random random = new Random();
        static public int ReadInt(string message, int minValue = int.MinValue, int maxValue = int.MaxValue)
        {
            int number;
            do
            {
                Drawer.CyanConsole(message);
            } while (!int.TryParse(Console.ReadLine(), out number) || number < minValue || number > maxValue);
            return number;
        }

        static public List<Gun> BuyGuns(List<Gun> arsenal, int money)
        {
            int number;
            Drawer.GreenConsole(Environment.NewLine + "Теперь пришло время купить немного ПУШЕК!" + Environment.NewLine);
            Drawer.WhiteConsole("Вот тебе краткий список, введи номер оружия для приобретения:\n" +
                "1) Пистолет - 10 монет.\n2) Автоматическая винтовка - 15 монет.\n3) Пулемет - 20 монет.");

            while (money >= 10)
            {
                Console.WriteLine(Environment.NewLine + $"На данный момент у тебя {money} монет, продолжай покупки! (^_~)");
                number = ReadInt("Введите номер оружия в пределах 1 и 3: ", 1, 3);

                switch (number)
                {
                    case 1:
                        money -= 10;
                        Pistol pistol = new Pistol(random.Next(10, 16), random.Next(5, 9));
                        Drawer.GreenConsole($"Поздравляю, ты купил пистолет с {pistol.Patrons} патронами в обойме" +
                            $" и дамагом {pistol.Damage}");
                        arsenal.Add(pistol);
                        break;
                    case 2:
                        if (money >= 15)
                        {
                            money -= 15;
                            AutomaticWeapon automaticGun = new AutomaticWeapon(random.Next(13, 19), random.Next(9, 15), random.NextDouble() * 0.3 + 0.3);
                            Drawer.GreenConsole($"Поздравляю, ты купил винтовку с {automaticGun.Patrons} патронами в обойме" +
                                $" и дамагом {automaticGun.Damage}. Твой шанс промазать с ней равен {automaticGun.Coef:F3}, везунчик!");
                            arsenal.Add(automaticGun);
                            break;
                        }
                        else
                        {
                            Drawer.RedConsole("Тебе катастрофично не хватает монет на это!");
                            break;
                        }
                    case 3:
                        if (money >= 20)
                        {
                            money -= 20;
                            Machinegun machinegun = new Machinegun(random.Next(10, 26), random.Next(8, 17), random.NextDouble()*0.1+0.3, random.Next(2, 5), random.NextDouble()*0.2+0.1);
                            Drawer.GreenConsole($"Поздравляю, ты купил пулемёт с {machinegun.Patrons} патронами в обойме" +
                                $" и дамагом {machinegun.Damage}. Твой шанс промазать с ним равен {machinegun.Coef:F3}, везунчик!");
                            arsenal.Add(machinegun);
                            break;
                        }
                        else
                        {
                            Drawer.RedConsole(Environment.NewLine + "Тебе катастрофично не хватает монет на это!");
                            break;
                        }
                    default:
                        Drawer.RedConsole("Попробуйте ввести другие данные!");
                        break;
                }
            }
            return arsenal;
        }

        static void Main(string[] args)
        {
            Drawer.MagentaConsole("\t\t\t\t\t<<НАЧАЛО ТВОЕГО ВЕЛИКОГО ПУТИ>>" + Environment.NewLine);
            do
            {
                int money = ReadInt("Для начала, введи количество монет для покупки оружия: ", 1);
                List<Gun> arsenal = new List<Gun>();

                arsenal = BuyGuns(arsenal, money);

                Drawer.DarkGrayConsole("Введите ENTER для продолжения, в противном случае," +
                    " нажмите на любую клавишу");
            } while (Console.ReadKey(true).Key == ConsoleKey.Enter);
        }
    }
}
