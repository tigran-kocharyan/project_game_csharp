using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gun_Library;
using System.Threading;

namespace KDZ
{
    class Program
    {
        // Static methods and fields of the program.
        static public Random random = new Random();
        static int money;
        static int patronsSum = 0;
        static int squadIndex;
        static int gunIndex;
        static string answerGun;
        static int currentDamage;

        /// <summary>
        /// The function shows the message and returns the number from the Console.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        static public int ReadInt(string message, int minValue = int.MinValue, int maxValue = int.MaxValue)
        {
            int number;
            do
            {
                Drawer.CyanConsole(message);
            } while (!int.TryParse(Console.ReadLine(), out number) || number < minValue || number > maxValue);
            return number;
        }

        /// <summary>
        /// This function takes the message and has almost the same functional as ReadInt, but with some properties for Health.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        static public int ReadHealth(string message, int minValue = int.MinValue, int maxValue = int.MaxValue)
        {
            int number;
            do
            {
                Drawer.CyanConsole(message);
            } while (!int.TryParse(Console.ReadLine(), out number) || number < minValue || number > maxValue || number % minValue != 0);
            return number;
        }

        /// <summary>
        /// Takes the input from console and transforms the data.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static public Mob ReadSquad(string input)
        {
            int entity, health;
            string[] paramsSquad = input.Split('-');
            if (paramsSquad.Length != 2)
            {
                return null;
            }
            if (!int.TryParse(paramsSquad[0], out entity))
            {
                return null;
            }
            if (!int.TryParse(paramsSquad[1], out health))
            {
                return null;
            }
            if (entity <= 0 || health <= 0)
            {
                return null;
            }
            if (health % entity != 0)
            {
                return null;
            }
            return new Mob(entity, health);
        }

        /// <summary>
        /// The function takes List Arsenal and fills it with the guns what purchased with the money.
        /// </summary>
        /// <param name="arsenal"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        static public List<Gun> BuyGuns(List<Gun> arsenal, int money)
        {
            int number;
            Drawer.GreenConsole(Environment.NewLine + "Теперь пришло время купить немного ПУШЕК!" + Environment.NewLine);
            Drawer.WhiteConsole("Вот тебе краткий список, введи номер оружия для приобретения:" + Environment.NewLine +
                "1) Пистолет - 10 монет." + Environment.NewLine + "2) Автоматическая винтовка - 15 монет." + Environment.NewLine + "3) Пулемет - 20 монет." + Environment.NewLine);

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
                        patronsSum += pistol.Patrons;
                        break;
                    case 2:
                        if (money >= 15)
                        {
                            money -= 15;
                            AutomaticWeapon automaticGun = new AutomaticWeapon(random.Next(13, 19), random.Next(9, 15), random.NextDouble() * 0.3 + 0.3);
                            Drawer.GreenConsole($"Поздравляю, ты купил винтовку с {automaticGun.Patrons} патронами в обойме" +
                                $" и дамагом {automaticGun.Damage}. Твой шанс промазать с ней равен {automaticGun.Coef:F3}, везунчик!");
                            arsenal.Add(automaticGun);
                            patronsSum += automaticGun.Patrons;
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
                            Machinegun machinegun = new Machinegun(random.Next(10, 26), random.Next(8, 17), random.NextDouble() * 0.1 + 0.3, random.Next(2, 5), random.NextDouble() * 0.2 + 0.1);
                            Drawer.GreenConsole($"Поздравляю, ты купил пулемёт с {machinegun.Patrons} патронами в обойме" +
                                $" и дамагом {machinegun.Damage}. Твой шанс промазать с ним равен {machinegun.Coef:F3}, везунчик!");
                            arsenal.Add(machinegun);
                            patronsSum += machinegun.Patrons;
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
            Drawer.YellowConsole(Environment.NewLine + "\t\t\t\t <<АРСЕНАЛ ВАШЕГО ОРУЖИЯ БЫЛ УСПЕШНО СОЗДАН>>" + Environment.NewLine);
            return arsenal;
        }

        /// <summary>
        /// This function fill the List mobs with different squads of mobs.
        /// </summary>
        /// <param name="mobs"></param>
        /// <param name="crews"></param>
        /// <returns></returns>
        static public List<Mob> CreateSquads(List<Mob> mobs, int crews)
        {
            Drawer.DarkYellowConsole(Environment.NewLine + "НЕБОЛЬШАЯ ПАМЯТКА: Количество монстров в отряде должно быть >= 1, " +
                    $"тебе же нужно с кем-то воевать (^_~)");
            Drawer.DarkYellowConsole("Так же количество жизней у всего отряда должно быть >= " +
                $"количеству существ в текущем отряде и поровну делиться между всеми его монстрами" + Environment.NewLine);
            Drawer.DarkYellowConsole("Начни вводить в виде {Кол-во Мобов}-{Жизни Отряда}");

            for (int i = 0; i < crews; i++)
            {
                Mob squad = null;
                while (squad == null)
                {
                    Drawer.CyanConsole($"Введите параметры отряда {i + 1}");
                    squad = ReadSquad(Console.ReadLine());
                }
                mobs.Add(squad);
            }
            return mobs;
        }

        /// <summary>
        /// Imitates the defence with guns versus the mobs.
        /// </summary>
        /// <param name="arsenal"></param>
        /// <param name="mobs"></param>
        static public void Defence(List<Gun> arsenal, List<Mob> mobs)
        {
            // The loop for processing till the guns aren't empty and mobs are alive.
            while (mobs.Count() != 0 && arsenal.Count() != 0 && patronsSum != 0)
            {
                // Making copies of the list elements.
                gunIndex = random.Next(0, arsenal.Count());
                squadIndex = random.Next(0, mobs.Count());
                Gun randomShootableGun = arsenal[gunIndex];
                Mob randomAliveSquad = mobs[squadIndex];
                currentDamage = randomShootableGun.Shoot();

                if (currentDamage != 0)
                {
                    randomAliveSquad.ReceiveDamage(randomShootableGun.Shoot());
                    Drawer.DarkGrayConsole($"Отряд под номером {squadIndex + 1} получил урон в размере " +
                        $"{randomShootableGun.Damage} от {randomShootableGun.GetType().Name}, в котором осталось " +
                        $"{randomShootableGun.Patrons - 1} патронов и так как у самого слабого монстра из этого отряда жизней было " +
                        $"{randomAliveSquad.MaxDamage}, теперь его жизни равны {randomAliveSquad.Health}.");
                }
                if (currentDamage == 0)
                {
                    Drawer.WhiteConsole($"Отряд под номером {squadIndex + 1} должен был получить урон, но ты промазал :(");
                }

                randomShootableGun.Patrons -= 1;
                // If the squad is dead removes it.
                if (randomAliveSquad.IsDead)
                {
                    mobs.RemoveAt(squadIndex);
                    Drawer.RedConsole(Environment.NewLine + $"\t\t\t  УРА! Отряд монстров под номером {squadIndex + 1} уничтожен!!!" + Environment.NewLine);
                }
                // If the gun is broken removes it.
                if (randomShootableGun.IsBroken)
                {
                    Drawer.RedConsole(Environment.NewLine + $"\t\t     К сожалению, оружие под индексом {gunIndex + 1} сломалось из-за неисправности :(");
                    arsenal.RemoveAt(gunIndex);
                }
                if (randomShootableGun.Patrons == 0)
                {
                    if (arsenal.Count == 1)
                    {
                        Drawer.RedConsole("\t\t\t    К сожалению, в последнем оружие закончились патроны :(" + Environment.NewLine);
                        arsenal.RemoveAt(0);
                        break;
                    }
                    Drawer.WhiteConsole(Environment.NewLine + "В одном из оружий кончились патроны," +
                        " хочешь ли ты добавить туда патроны?"
                        + Environment.NewLine + "1) Нет, перейти дальше." + Environment.NewLine + "2) Да, попробовать, " +
                        "с вероятностью 50% потерять другое оружие");
                    int answerGun = ReadInt("Твой ответ: (введи либо 1, либо 2)", 1, 2);
                    // Remove gun by index.
                    if (answerGun == 1)
                    {
                        arsenal.RemoveAt(gunIndex);
                    }
                    else
                    {
                        Gun gunCopy = randomShootableGun;
                        arsenal.RemoveAt(gunIndex);
                        Drawer.WhiteConsole("Теперь тебе нужно выбрать, чем пожертвовать и потом ты испытаешь удачу!");
                        for (int i = 0; i < arsenal.Count; i++)
                        {
                            Drawer.YellowConsole($"{i + 1}. {arsenal[i].GetType().Name} с {arsenal[i].Patrons} патронами " +
                                $"и уроном {arsenal[i].Damage}");
                        }
                        int deleteAnotherIndex = ReadInt($"Твой ответ? (от 1 до {arsenal.Count()})", 1, arsenal.Count());
                        if (random.NextDouble() < 0.5)
                        {
                            Drawer.WhiteConsole(Environment.NewLine + "К сожалению, удача не на твоей стороне, " +
                            "оружие осталось без патронов и ты теряешь поставленное на кон оружие :(" + Environment.NewLine);
                            if (arsenal.Count() >= 1)
                            {
                                Drawer.RedConsole($"\t\t\t\t    Оружие под индексом {deleteAnotherIndex} будет удалено!");
                                arsenal.RemoveAt(deleteAnotherIndex - 1);
                            }
                        }
                        else
                        {
                            Drawer.WhiteConsole(Environment.NewLine + "Фортуна на твоей стороне."
                                + Environment.NewLine + "Получи 5 патронов!!!");
                            gunCopy.Patrons += 5;
                            arsenal.Add(gunCopy);
                        }
                    }
                }
                Drawer.GreenConsole("\t\t\t\t Стадия Обороны завершилась, ждем следующей!" + Environment.NewLine);
                Thread.Sleep(500);
            }
            // In case of WIN, it displays the output in the Console.
            if (arsenal.Count == 0 && mobs.Count == 0)
            {
                Drawer.GreenConsole("\t\t\t\t\t\t<<ПОБЕДА!>>");
            }
            // In case of WIN, it displays the output in the Console.
            if (arsenal.Count != 0 && mobs.Count == 0)
            {
                Drawer.GreenConsole("\t\t\t\t\t\t<<ПОБЕДА!>>");
            }
            // In case of LOSE, it displays the output in the Console.
            if (arsenal.Count == 0 && mobs.Count != 0)
            {
                Drawer.RedConsole("\t\t\t\t\t\t<<ПОРАЖЕНИЕ!>>");
            }
        }

        /// <summary>
        /// The Main function of the program with all operations.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Drawer.MagentaConsole("\t\t\t\t\t<<НАЧАЛО ТВОЕГО ВЕЛИКОГО ПУТИ>>" + Environment.NewLine);
            do
            {
                Console.OutputEncoding = Encoding.UTF8;
                // Creating the arsenal of the hero.
                money = ReadInt("Для начала, введи количество монет для покупки оружия: ", 10);
                List<Gun> arsenal = new List<Gun>();
                arsenal = BuyGuns(arsenal, money);

                // This part is responsible for making the enemies' crews.
                int crews = ReadInt("Теперь пора ввести количество отрядов противников!", 1, 10000000);
                List<Mob> mobs = new List<Mob>(crews);
                mobs = CreateSquads(mobs, crews);

                // The starting point of the defence.
                Drawer.YellowConsole(Environment.NewLine + "\t\t\t\t       <<ДА НАЧНЕТСЯ ВЕЛИКАЯ ОБОРОНА!>>" + Environment.NewLine);
                Defence(arsenal, mobs);

                Drawer.DarkGrayConsole("Введите ENTER для продолжения, в противном случае," +
                    " нажмите на любую клавишу");
            } while (Console.ReadKey(true).Key == ConsoleKey.Enter);
        }
    }
}
