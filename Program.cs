using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SafeCracker
{
    internal class Value
    {
        private static string errorMessage = "Error while parsing the value of the bet! Make sure it is not a negative whole number!";
        private static Position errorPos;
        private static Position inputPos;
        private static Position winnerPos = new Position(0, 9);
        private static Position[] prizePos = { new Position(0, 10), new Position(0, 11), new Position(0, 12), new Position(0, 13) };

        public static Position ErrorPos { get => errorPos; set => errorPos = value; }
        public static Position InputPos { get => inputPos; set => inputPos = value; }
        public static Position[] PrizePos { get => prizePos; set => prizePos = value; }
        internal static Position WinnerPos { get => winnerPos; set => winnerPos = value; }

        public static void clearValueError()
        {
            Console.SetCursorPosition(errorPos.X, errorPos.Y);
            int number = errorMessage.Length;
            for (int i = 0; i < number; i++)
            {
                Console.Write(" ");
            }
            Console.SetCursorPosition(errorPos.X, errorPos.Y);
            for (int i = 0; i < number; i++)
            {
                Console.Write("\b");
            }
            Console.SetCursorPosition(inputPos.X, inputPos.Y);
        }

        public static void showValueError(string value)
        {
            Console.Write(errorMessage);
            Console.SetCursorPosition(inputPos.X, inputPos.Y);
            for (int i = 0; i < value.Length; i++)
            {
                Console.Write(" ");
            }
            for (int i = 0; i < value.Length; i++)
            {
                Console.Write("\b");
            }
            Console.SetCursorPosition(inputPos.X, inputPos.Y);
        }

        public static int Parse(string value)
        {
            try
            {
                if (int.Parse(value) <= 0)
                {
                    throw new Exception();
                }
            }
            catch
            {
                showValueError(value);
                return -1;
            }
            clearValueError();
            for (int i = 0; i < value.Length; i++)
            {
                Console.Write(" ");
            }
            for (int i = 0; i < value.Length; i++)
            {
                Console.Write("\b");
            }
            Console.SetCursorPosition(inputPos.X, inputPos.Y);
            return int.Parse(value);
        }
    }
    internal class Position
    {
        public int X;
        public int Y;

        public Position(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }
    }
    internal class Animation
    {
        Position[] numberPos;
        Position[] safePos;
        public Animation()
        {
            numberPos = new Position[9];
            numberPos[0] = new Position(1, 6);
            numberPos[1] = new Position(4, 6);
            numberPos[2] = new Position(7, 6);
            numberPos[3] = new Position(10, 6);
            numberPos[4] = new Position(13, 6);
            numberPos[5] = new Position(16, 6);
            numberPos[6] = new Position(19, 6);
            numberPos[7] = new Position(22, 6);
            numberPos[8] = new Position(25, 6);

            safePos = new Position[9];
            safePos[0] = new Position(9, 1);
            safePos[1] = new Position(15, 1);
            safePos[2] = new Position(21, 1);
            safePos[3] = new Position(9, 2);
            safePos[4] = new Position(15, 2);
            safePos[5] = new Position(21, 2);
            safePos[6] = new Position(9, 3);
            safePos[7] = new Position(15, 3);
            safePos[8] = new Position(21, 3);
        }
        public void drawREADME()
        {
            Console.SetCursorPosition(0, 0);
            Console.Out.WriteLine("README");
            Console.Out.WriteLine("1. To exit the program type 'exit' instead of a bet number");
            Console.Out.WriteLine("2. The game has 9 lockers inside a safe; After typing a bet, 4 lockers will be " +
                "open at random\nThese lockers contain a multiplier and an object (Ring, Horseshoe or Treasure)\n" +
                "In order to win you must open 2 identical lockers! Good luck!");
            Console.Out.WriteLine("3. There are a total of 3 identical lockers in each safe");
            Console.Out.WriteLine("To get to the actual game, press enter!");
        }
        public void drawTable()
        {
            Console.SetCursorPosition(0, 0);
            Console.Out.WriteLine("#################################");
            Console.Out.WriteLine("####### [001] [002] [003] #######");
            Console.Out.WriteLine("####### [004] [005] [006] #######");
            Console.Out.WriteLine("####### [007] [008] [009] #######");
            Console.Out.WriteLine("#################################");
            Console.Out.WriteLine("[1][2][3][4][5][6][7][8][9]######");
            Console.Out.WriteLine("#################################");
            Console.Out.Write("Please enter your betting value: ");
        }

        private void quickSelect(int number)
        {
            Thread.Sleep(250);
            Console.SetCursorPosition(numberPos[number - 1].X, numberPos[number - 1].Y);
            Console.Write("^");
            Thread.Sleep(250);
            Console.SetCursorPosition(numberPos[number - 1].X, numberPos[number - 1].Y);
            Console.Write("#");
        }

        private void longSelect(int number)
        {
            Console.SetCursorPosition(numberPos[number - 1].X, numberPos[number - 1].Y);
            Console.Write("^");
            Thread.Sleep(1500);
            Console.SetCursorPosition(numberPos[number - 1].X, numberPos[number - 1].Y);
            Console.Write("#");
        }

        private void between2animation(int n1, int n2)
        {
            if (n1 > n2)
            {
                for (int i = n1; i >= n2; i--)
                {
                    this.quickSelect(i);
                }
            }
            else
            {
                for (int i = n1; i <= n2; i++)
                {
                    this.quickSelect(i);
                }
            }
        }

        public void animateNumber(int number)
        {
            Random random = new Random();
            int[] numbers = new int[3];
            bool regenerate = false;
            do
            {
                numbers[0] = random.Next(1, 10);
                numbers[1] = random.Next(1, 10);
                numbers[2] = random.Next(1, 10);
                if (Math.Abs(numbers[0] - numbers[1]) == 1 ||
                    Math.Abs(numbers[0] - numbers[2]) == 1 ||
                    Math.Abs(numbers[1] - numbers[2]) == 1)
                {
                    regenerate = true;
                }
            }
            while (numbers.Distinct().Count() != numbers.Length && !numbers.Contains(number) && !regenerate
            && !numbers.Contains(number + 1) && !numbers.Contains(number - 1));
            for (int i = 0; i < 2; i++)
            {
                this.between2animation(numbers[i], numbers[i + 1]);
            }
            this.between2animation(numbers[numbers.Length - 1], number);
            this.longSelect(number);
        }

        public void animateNumbers(int[] numbers)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                animateNumber(numbers[i]);
                Console.SetCursorPosition(safePos[numbers[i] - 1].X, safePos[numbers[i] - 1].Y);
                Console.Write(Generator.Symbols[numbers[i]].ToString());
                Console.SetCursorPosition(Value.PrizePos[i].X, Value.PrizePos[i].Y);
                Console.Write(Generator.Symbols[numbers[i]].getPrize(Generator.Numbers[i]));
                for (int p = 0; p <= i - 1; p++)
                {
                    for (int q = p + 1; q <= i; q++)
                    {
                        if (Generator.Symbols[numbers[p]].CompareTo(Generator.Symbols[numbers[q]]) == 0)
                        {
                            Console.SetCursorPosition(Value.WinnerPos.X, Value.WinnerPos.Y);
                            Console.Write("WINNER! PRIZE = {0}$ ", Generator.Bet * Generator.Symbols[numbers[p]].Multiplier);
                            return;
                        }
                    }
                }
            }
            Console.SetCursorPosition(Value.InputPos.X, Value.InputPos.Y);
        }
    }
    internal class Symbol : IComparable
    {
        string name;
        string symbol;
        int multiplier;

        public int Multiplier { get => multiplier; set => multiplier = value; }

        public Symbol(int number, int multiplier)
        {
            switch (number)
            {
                case 0:
                    this.name = "Treasure";
                    this.symbol = "X";
                    this.multiplier = multiplier;
                    break;
                case 1:
                    this.name = "Ring";
                    this.symbol = "O";
                    this.multiplier = multiplier;
                    break;
                case 2:
                    this.name = "Horseshoe";
                    this.symbol = "C";
                    this.multiplier = multiplier;
                    break;
                default:
                    this.multiplier = -1;
                    break;
            }
        }
        public override string ToString()
        {
            if (multiplier < 10)
            {
                return String.Format("0{0}{1}", multiplier, symbol);
            }
            else
            {
                return String.Format("{0}{1}", multiplier, symbol);
            }
        }
        public string getPrize(int number)
        {
            return String.Format("NUMBER: {0}; {1} x {2}", number, multiplier, name);
        }

        public int CompareTo(object obj)
        {
            var compareTo = (Symbol)obj;
            if (compareTo.multiplier == this.multiplier && compareTo.name == this.name)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
    }
    internal class Generator
    {
        static Symbol[] symbols;
        static int[] numbers;
        static int bet;

        public static void generate()
        {
            Random random = new Random();
            symbols = new Symbol[9];
            symbols[0] = symbols[1] = symbols[2] = new Symbol(random.Next(0, 3), random.Next(5, 10));
            for (int i = 3; i < symbols.Length; i++)
            {
                symbols[i] = new Symbol(random.Next(0, 3), random.Next(5, 10));
            }
            for (int i = 0; i < 3; i++)
            {
                int newPosition = random.Next(0, symbols.Length);
                var aux = symbols[i];
                symbols[i] = symbols[newPosition]; ;
                symbols[newPosition] = aux;
            }
            numbers = new int[4];
            do
            {
                numbers[0] = random.Next(1, 9);
                numbers[1] = random.Next(1, 9);
                numbers[2] = random.Next(1, 9);
                numbers[3] = random.Next(1, 9);
            } 
            while (numbers.Distinct().Count() != numbers.Length);
        }
        public static int[] Numbers { get => numbers; set => numbers = value; }
        internal static Symbol[] Symbols { get => symbols; set => symbols = value; }
        public static int Bet { get => bet; set => bet = value; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Animation animation = new Animation();
            animation.drawREADME();
            Console.ReadLine();
            Console.Clear();
            animation.drawTable();
            Value.InputPos = new Position(Console.CursorLeft, Console.CursorTop);
            string input = Console.ReadLine();
            Value.ErrorPos = new Position(Console.CursorLeft, Console.CursorTop);
            while (input.CompareTo("exit") != 0)
            {
                int value = Value.Parse(input);
                if (value > 0)
                {
                    Generator.Bet = value;
                    Generator.generate();
                    animation.animateNumbers(Generator.Numbers);
                    Thread.Sleep(5000);
                    Console.Clear();
                    animation.drawTable();
                }
                input = Console.ReadLine();
            }
        }
    }
}
