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
        public enum Kombinacii { сard = 1, couple, twopairs, triple, straight, flush, fullhouse, four_of_a_kind, straight_flush, royal_flush }// для определения победителя
        public enum Mast { peaks = 1, heart, diamonds, baptize }
        public enum Znachenie { two = 1, three, four, five, sex, seven, eight, nine, ten,  jack, lady, king, ace }
        public int[] yours;
        public int[] ontable;
        public int balance;
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

            for (int i = 0; i < ontable.Length / 2; i++)
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
            if (CountZnach == 1)
            {
                vashakombin = (int)Kombinacii.couple;
            }
            if (CountZnach == 2)
            {
                vashakombin = (int)Kombinacii.twopairs;
            }
            if (CountZnach == 5)
            {
                vashakombin = (int)Kombinacii.fullhouse;
            }
            if (CountZnach == 3)
            {
                vashakombin = (int)Kombinacii.triple;
            }
            if (CountZnach == 4)
            {
                vashakombin = (int)Kombinacii.four_of_a_kind;
            }
            if (CountMast == 5)
            {
                vashakombin = (int)Kombinacii.flush;
            }
            int[] array = new int[5];
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
                vashakombin = (int)Kombinacii.royal_flush;
            }
            if (count == 4 && proverka1)
            {
                vashakombin = (int)Kombinacii.straight_flush;
            }
            if (count == 4)
            {
                vashakombin = (int)Kombinacii.straight;
            }
        }
        public void BALANCE()
        {
            Console.WriteLine("place your bets");
            Program.Write($"Enter your bet\nRemember, your starting balance is={balance}", 5, 5);
            int stavka;
            try
            {
                stavka = int.Parse(Console.ReadLine());
                if (stavka > balance)
                    throw new Exception();
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid value entered\ndefault value will be assigned");
                stavka = 10;
            }
            newbalance += stavka;
            if (newbalance > balance)
            {
                newbalance = balance - 20;
            }
            Console.WriteLine($"Your current bid ==={newbalance}");
            Console.WriteLine("Dealer > Bets made.No more bids");
        }
        public void W()
        {
            Console.WriteLine((Kombinacii)vashakombin);
        }
    }
    class Program
    {
        public static void Write(string str, int left = 0, int top = 0, ConsoleColor fore = ConsoleColor.White, ConsoleColor back = ConsoleColor.Black)
        {
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
                Write("> Begin", 5, 5, ConsoleColor.White, (hoverRow == 0) ? ConsoleColor.Green : ConsoleColor.Black);
                Write(">Rules", 0, 0, ConsoleColor.White, (hoverRow == 1) ? ConsoleColor.Green : ConsoleColor.Black);
                Write("> About", 0, 0, ConsoleColor.White, (hoverRow == 2) ? ConsoleColor.Green : ConsoleColor.Black);
                switch (selectedRow)
                {
                    case -1:
                        {
                            Write("Use the up or down arrows to navigate ", 2);
                            Write("When you are finished, press Enter");
                            break;
                        }
                    case 1:
                        {
                            Write("2 cards are dealt to a person", 2, 2);
                            Write("5 cards are laid out on the table");
                            Write("First 3 then the 4th, and then the 5th");
                            Write("By raising the bet, the person continues the game");
                            Write("Challenge-win");
                            Write("P.S. this game requires a person to have knowledge of higher mathematics to guess the probability of getting one or another combination");
                            Write("everything will be simpler here");
                            Write("Good luck!");
                            break;
                        }
                    case 2:
                        {

                            Write("Welcome to the poker game TEXAS HOLDEM", 2, 2);
                            Write("In 1900, in a small town in the state of Texas, there was a game demanded by a huge number of poker fans. \n\t\t According to the story, Texas Hold'em was invented by gold miners who crave adventure and wealth.");
                            Write("Texas Hold'em differs from many other types of poker in that \n\t\t in addition to cards on the player’s hands there are also general cards that everyone at the table can use. \n\t\t There is several rounds: Preflop, Flop, Turn, River. \n\t\tEvery round is accompanied by bidding: players make bets that make up the common bank. \n\t\tTo break the bank is the goal of each player. \n\t\tWhat there are community cards in the game, which makes it more interesting. ");
                            Write("Good luck", 2, 2);
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
                Write("Are we continuing ? ", 5, 5);
                Write("> YES", 0, 0, ConsoleColor.White, (hoverRow == 0) ? ConsoleColor.Green : ConsoleColor.Black);
                Write(">NO", 0, 0, ConsoleColor.White, (hoverRow == 1) ? ConsoleColor.Green : ConsoleColor.Black);

                switch (selectedRow)
                {
                    case -1:
                        {
                            Write("Use the up or down arrows to navigate ", 2);
                            Write("When you are finished, press Enter");
                            break;
                        }
                    case 1:
                        {
                            Write("You lose");
                            Write(" Thanks for playing!");
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
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
     
            Thread.Sleep(3000);
            Console.Clear();
            Intro();
            ShowMenu();
            Karta karta = new Karta();//1-ый игрок
            Karta karta1 = new Karta();//2-ой игрок
            Console.Write("Your cards");
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
                Console.WriteLine("You win");
                karta.balance += karta.newbalance;
                karta1.balance -= karta.newbalance;
            }
            if (karta.vashakombin < karta1.vashakombin)
            {
                Console.WriteLine("You lose");
                karta.balance -= karta.newbalance;
                karta1.balance += karta.newbalance;
            }
            Write($"Your balance after game ==={karta.balance}");
            Write($"The balance of the second player after game ===={karta1.balance}");
            Write("Second player cards: ");
            karta1.Vivod1();
            karta.W();
            karta1.W();
            Console.Read();
        }
    }
}