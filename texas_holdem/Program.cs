using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace POKER
{
    class Karta
    {
        public enum Kombinacii { карта = 1, пара, двепары, тройка, стрит, флеш, фулхауз, каре, стритфлеш, ройалфлеш }// для определения победителя
        public enum Mast { peaks = 1, heart, bubi, baptize }
        public enum Znachenie { two = 1, three, four, five, six, seven, eight, nine, ten, jack, lady, king, ace }
        public int[] yours;//карты у человека на руках
        public int[] ontable;//карты, которые будут выкладываться на столе
        public int balance;//начальный баланс игрока
        public int CountMast;
        public int CountZnach;
        public int newbalance = 0;
        public int vashakombin;
        public Karta()
        {
            yours = new int[4];
            ontable = new int[10];
            int i = 0;

            for (i = 0; i < yours.Length / 2; i++)
            {
                yours[i] = random.Next((int)Mast.peaks, (int)Mast.baptize);
            }
            int y = yours.Length / 2;
            for (; y < yours.Length; y++)
            {
                yours[y] = random.Next((int)Znachenie.two, (int)Znachenie.ace);
            }
            for (int t = 0; t < ontable.Length / 2; t++)
            {
                ontable[t] = random.Next((int)Mast.peaks, (int)Mast.baptize);
            }

            int r = ontable.Length / 2;
            for (; r < ontable.Length; r++)
            {
                ontable[r] = random.Next((int)Znachenie.two, (int)Znachenie.ace);
            }
            balance = 100;//начальный баланс =100
            CountMast = 0;
            CountZnach = 0;
        }
        public static Random random = new Random();
        public void Vivod1()
        {
            Console.SetCursorPosition(15, Console.CursorTop + 5);
            Console.WriteLine($"\t|{(Znachenie)yours[2]}|\n\t\t|{(Mast)yours[0]}|");
            Console.WriteLine("_________________________");
            Console.WriteLine($"\t\t|{(Znachenie)yours[3]}|\n\t\t|{(Mast)yours[1]}|");
            bool contine = false;
            do
            {
                ConsoleKey ki = Console.ReadKey().Key;
                if (ki != ConsoleKey.Enter)
                {
                    Console.WriteLine(" Для продолжения нажмите Enter");
                }
                else
                {
                    contine = true;
                }
            }
            while (!contine);
        }
        public void Vivod(int r)
        {
            BALANCE();
            Console.SetCursorPosition(15, Console.CursorTop + 5);
            Console.WriteLine("Ваши карты");
            Console.WriteLine($"\t{(Znachenie)yours[2]}|       {(Znachenie)yours[3]}");
            Console.WriteLine($"\t{(Mast)yours[0]}|       {(Mast)yours[1]}");
            Console.WriteLine();
            Console.WriteLine("____________________________________");
            for (int i = 0; i < (ontable.Length / 2) - r; i++)
            {
                Console.Write((Mast)ontable[i] + " |");
            }
            Console.WriteLine();
            int e = (ontable.Length / 2) + r;
            for (; e < ontable.Length; e++)
            {
                Console.Write((Znachenie)ontable[e] + " |");
            }
            Console.WriteLine("\n____________________________________");
            bool exit = false;
            do
            {
                ConsoleKey ki = Console.ReadKey().Key;
                if (ki != ConsoleKey.Enter)
                {
                    Console.WriteLine(" Для продолжения нажмите Enter");
                }
                else
                {
                    exit = true;
                }
            }
            while (!exit);
            Program.Prodolzhenie();
        }
        public void Nahozhdenie()
        {
            // Console.WriteLine("\n\n\tДанную часть вы можете использовать как подсказку для своих выводах о возможных исходах игры"); 
            for (int i = 0; i < ontable.Length / 2; i++)//сначала проверим масти
            {
                for (int r = 0; r < yours.Length / 2; r++)
                {
                    if (ontable[i] == yours[r])
                    {
                        CountMast++;
                    }
                }
            }
            int t = ontable.Length / 2;
            int m = yours.Length / 2;
            for (; t < ontable.Length; t++)
            {
                for (; m < yours.Length; m++)
                {
                    if (ontable[t] == yours[m])
                    {
                        CountZnach++;
                    }
                }
            }
        }
        public void End()
        {
            if (CountZnach == 1)//пары
            {
                vashakombin = (int)Kombinacii.пара;
            }
            if (CountZnach == 2)
            {
                vashakombin = (int)Kombinacii.двепары;
            }
            if (CountZnach == 5)//фулхауз
            {
                vashakombin = (int)Kombinacii.фулхауз;
            }
            if (CountZnach == 3)//тройка
            {
                vashakombin = (int)Kombinacii.тройка;
            }
            if (CountZnach == 4)//каре
            {
                vashakombin = (int)Kombinacii.каре;
            }
            if (CountMast == 5)//флеш
            {
                vashakombin = (int)Kombinacii.флеш;
            }
            int[] array = new int[5];//количество карт на столе
            Array.Copy(ontable, 5, array, 0, 4);
            array[4] = yours[2];
            Poranaarm(array);
            Array.Copy(ontable, 5, array, 0, 4);
            array[4] = yours[3];
            Poranaarm(array);
            Array.Copy(ontable, 6, array, 1, 4);
            array[0] = yours[2];
            Poranaarm(array);
            Array.Copy(ontable, 6, array, 1, 4);
            array[0] = yours[3];
        }
        public void Poranaarm(int[] array)
        {

            Array.Sort(array);
            int count = 0;
            bool proverka = false;
            bool proverka1 = false;
            for (int i = 0; i < array.Length - 1; i++)
            {
                if (array[i] + 1 == array[i + 1])
                {
                    count++;
                }
            }
            if (array[0] == 9)//10  в начале ?
            {
                proverka = true;
            }
            if (array[0] == 8)//9 в начале ?
            {
                proverka1 = true;
            }
            if (count == 4 && proverka)
            {
                vashakombin = (int)Kombinacii.ройалфлеш;
            }
            if (count == 4 && proverka1)
            {
                vashakombin = (int)Kombinacii.стритфлеш;
            }
            if (count == 4)
            {
                vashakombin = (int)Kombinacii.стрит;
            }
        }
        public void BALANCE()
        {
            Console.WriteLine(" Делайте ваши ставки");
            Program.Write($"Введите свою ставку\nНапоминаем, ваш начальный баланс равен={balance}", 5, 5);
            int stavka;
            try
            {
                stavka = int.Parse(Console.ReadLine());
                if (stavka > balance)
                    throw new Exception();
            }
            catch (Exception)
            {
                Console.WriteLine("Было введено недопустимое значение\nБудет присвоено значение по умолчанию");
                stavka = 10;
            }
            newbalance += stavka;
            if (newbalance > balance)
            {
                newbalance = balance - 20;
            }
            Console.WriteLine($"Ваша текущая ставка==={newbalance}");
            Console.WriteLine("Дилер>Ставки сделаны. Ставок больше нет");
        }
        public void W()
        {
            Console.WriteLine((Kombinacii)vashakombin);
        }
    }
    class Program
    {
        public static void Write(string str, int left = 0, int top = 0, ConsoleColor fore = ConsoleColor.White, ConsoleColor back = ConsoleColor.Black)
        {//left -слева направо 
         // top-сверху вниз
            Console.BackgroundColor = back;
            Console.ForegroundColor = fore;
            Console.SetCursorPosition(25, Console.CursorTop + top);
            Console.Write(str + "\n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void Intro()
        {
            Thread.Sleep(1000);
            Write("╔════╦═══╦══╗╔══╦══╦══╗       ╔╗╔╦══╦╗ ╔══╗╔═══╦╗  ╔╗", 5, 5, ConsoleColor.Green);
            Write("╚═╗╔═╣╔══╩═╗║║╔═╣╔╗║╔═╝       ║║║║╔╗║║ ║╔╗╚╣╔══╣╚╗╔╝║", 0, 0, ConsoleColor.Green);
            Write("  ║║ ║╚══╗ ║╚╝║ ║╚╝║╚═╗       ║╚╝║║║║║ ║║╚╗║╚══╣ ╚╝ ║", 0, 0, ConsoleColor.Green);
            Write("  ║║ ║╔══╝ ║╔╗║ ║╔╗╠═╗║       ║╔╗║║║║║ ║║ ║║╔══╣║╗╔║║", 0, 0, ConsoleColor.Green);
            Write("  ║║ ║╚══╦═╝║║╚═╣║║╠═╝║       ║║║║╚╝║╚═╣╚═╝║╚══╣║╚╝║║", 0, 0, ConsoleColor.Green);
            Write("  ╚╝ ╚═══╝══╝╚══╝╝╚╝══╝       ╚╝╚╝══╝══╚═══╝═══╚╝  ╚╝", 0, 0, ConsoleColor.Green);
            Thread.Sleep(500);
            Write("powered  by technolog", 2, 2);
            Thread.Sleep(2000);
        }
        static void ShowMenu()
        {
            bool startedButtonPressed = false;
            int hoverRow = 0;
            int selectedRow = -1;
            ConsoleKeyInfo keyinfo;
            do
            {
                Console.Clear();
                Write("> Start", 5, 5, ConsoleColor.White, (hoverRow == 0) ? ConsoleColor.Green : ConsoleColor.Black);
                Write("> Rules", 0, 0, ConsoleColor.White, (hoverRow == 1) ? ConsoleColor.Green : ConsoleColor.Black);
                Write("> About", 0, 0, ConsoleColor.White, (hoverRow == 2) ? ConsoleColor.Green : ConsoleColor.Black);
                switch (selectedRow)
                {
                    case -1:
                        {
                            Write("Use up or down arrows for navigation ", 2);
                            Write("Upon completion, select Enter");
                            break;
                        }
                    case 1:
                        {
                            Write("человеку раздается по 2 карты", 2, 2);
                            Write("5 карт выкладываются на стол");
                            Write("Сначала 3 потом 4-ая, а потом уже 5-ая");
                            Write("Посредством поднятия ставки, человек продолжает игру");
                            Write("Задача- выиграть");
                            Write(" P.S.данная игра требует от человека знаний высшей математики для угадывания  вероятности попадания той или иной комбинации ");
                            Write("здесь все будет попроще");
                            Write("Желаю удачи!");
                            break;
                        }
                    case 2:
                        {

                            Write("Добро пожаловать в игру покер TEXAS HOLDEM", 2, 2);
                            Write("В  1900  в небольшом городке в штате Техас, появилась игра, востребованная у огромного количества любителей покера.\n\t\t Согласно истории, Техасский Холдем придумали золотоискатели, жаждущие приключений и богатства.");
                            Write("Техасский Холдем отличается от многих других видов покера тем,\n\t\t что помимо карт на руках игрока есть еще и общие карты, которые может использовать каждый сидящий за столом.\n\t\t В «Техасе» есть несколько раундов: Префлоп, Флоп, Терн, Ривер.\n \t\tКаждый раунд сопровождается торгами: игроки делают ставки, которые составляют общий банк. \n\t\tСорвать банк - цель каждого игрока.\n \t\tТо, что в игре есть общие карты, делает ее более интересной. ");
                            Write("Желаю удачи", 2, 2);
                            break;
                        }
                }
                keyinfo = Console.ReadKey();
                switch (keyinfo.Key)
                {
                    case ConsoleKey.UpArrow://если вверх
                        {
                            hoverRow = (hoverRow - 1 >= 0) ? hoverRow - 1 : 2;
                            break;
                        }
                    case ConsoleKey.DownArrow://если вниз
                        {
                            hoverRow = (hoverRow + 1 <= 2) ? hoverRow + 1 : 0;
                            break;
                        }
                    case ConsoleKey.Enter:
                        {
                            selectedRow = hoverRow;
                            startedButtonPressed = selectedRow == 0;
                            break;
                        }
                }
            }
            while (!startedButtonPressed);
            Console.Clear();
        }
        public static void Prodolzhenie()
        {
            bool startedButtonPressed = false;
            int hoverRow = 0;
            int selectedRow = -1;
            ConsoleKeyInfo keyinfo;
            do
            {
                Console.Clear();
                Write("Продолжаем?", 5, 5);
                Write("> ДА", 0, 0, ConsoleColor.White, (hoverRow == 0) ? ConsoleColor.Green : ConsoleColor.Black);
                Write(">НЕТ", 0, 0, ConsoleColor.White, (hoverRow == 1) ? ConsoleColor.Green : ConsoleColor.Black);

                switch (selectedRow)
                {
                    case -1:
                        {
                            Write("Используй стрелки вверх или вниз для навигации ", 2);
                            Write("Завершив свой выбор, нажми Enter");
                            break;
                        }
                    case 1:
                        {
                            Write("Вs проиграли");
                            Write("  Спасибо за игру!");
                            Thread.Sleep(1000);
                            Environment.Exit(0);
                            break;
                        }
                }
                keyinfo = Console.ReadKey();
                switch (keyinfo.Key)
                {
                    case ConsoleKey.UpArrow://если вверх
                        {
                            hoverRow = (hoverRow - 1 >= 0) ? hoverRow - 1 : 1;
                            break;
                        }
                    case ConsoleKey.DownArrow://если вниз
                        {
                            hoverRow = (hoverRow + 1 <= 2) ? hoverRow + 1 : 0;
                            break;
                        }
                    case ConsoleKey.Enter:
                        {
                            selectedRow = hoverRow;
                            startedButtonPressed = selectedRow == 0;
                            break;
                        }
                }
            }
            while (!startedButtonPressed);
            Console.Clear();
        }
        public static void Obuchenie()
        {
            Console.Clear();
            Write("Обучение");
            Write("И так");
            Write(" После вывода приветственного сообщения будет выводится меню");
            Write(" Вы можете прочитать правила, об игре или сразу начать");
            Write(" (начать) сначала выводятся ваши карты и посредством нажатия Enter  и ответа <Да> продолжайте игру");
            Write(" В покере проходят 3 фазы когда человеку нужно сделать ставку");
            Write(" Ставка делается после вопроса о продолжении ( для того чтобы обдумать нужно ли продолжать игру или нет)");
            Write("Вам не придется думать о вашей комбинации(я позаботился об этом");
            Write("под конец на экране появятся карты противника ");
            Write("И надпись о победе, поражении или ничьей(ничья не выводится)");
            Write(" Потом появится ваша комбинация, а потом противника");
            Write(" Если же не будет никакой комбинации, появится 0");
            bool exit = false;
            do
            {
                ConsoleKey ki = Console.ReadKey().Key;
                if (ki != ConsoleKey.Enter)
                {
                    Console.WriteLine(" Для продолжения нажмите Enter");
                }
                else
                {
                    exit = true;
                }
            }
            while (!exit);
        }
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.WriteLine("Вы хотите пройти обучение?\nВведите 1, если хотите пройти и 0, если нет");
            int reshenie = int.Parse(Console.ReadLine());
            if (reshenie == 1)
            {
                Console.WriteLine("Отлично!");
            }
            else
            {
                Console.WriteLine("Все же советуется пройти обучение)");
            }
            Thread.Sleep(3000);
            Console.Clear();
            Intro();
            Obuchenie();
            ShowMenu();
            Karta karta = new Karta();//1-ый игрок
            Karta karta1 = new Karta();//2-ой игрок
            Console.Write("Ваши карты");
            karta.Vivod1();
            Prodolzhenie();
            for (int i = 5; i > 2; i--)//вывод сначала 3-х карт, потом 4-х карт, потом 5-ти карт
            {
                karta.Vivod(i - 3);
            }
            karta.Nahozhdenie();
            karta1.Nahozhdenie();
            karta.End();
            karta1.End();
            if (karta.vashakombin > karta1.vashakombin)
            {
                Console.WriteLine("Вы победили");
                karta.balance += karta.newbalance;
                karta1.balance -= karta.newbalance;
            }
            if (karta.vashakombin < karta1.vashakombin)
            {
                Console.WriteLine("Вы проиграли");
                karta.balance -= karta.newbalance;
                karta1.balance += karta.newbalance;
            }
            Write($"Ваш баланс после игры ==={karta.balance}");
            Write($"Баланс второго игрока после===={karta1.balance}");
            Write("Карты второго игрока:");
            karta1.Vivod1();
            karta.W();
            karta1.W();
            Console.Read();
        }
    }
}