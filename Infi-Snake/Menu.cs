using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infi_Snake
{
    class Menu
    {
        Frame _frame;
        InfiSnakeGame _r;

        public Menu()
        {
            _frame = new Frame();
            _r = new InfiSnakeGame();
        }

        internal void StartUp()
        {
            _frame.Show();
            Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2);
            Console.Write("Infi-Snake");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 14, Console.WindowHeight / 2 + 10);
            Console.Write("Press any key to continue");
            Console.ReadKey();
        }

        bool Ask()
        {
            bool _answer = false;
            Console.Clear();
            _frame.Show();
            Console.SetCursorPosition(Console.WindowWidth/2 - 7 ,Console.WindowHeight / 2);
            Console.Write("Restart? Y/N");
            switch(Console.ReadKey(true).Key)
                {
                case ConsoleKey.Y:
                    _answer = true;
                    break;
                }
                
            return _answer;
        }


        internal void Main()
        {

            StartUp();
        myloop:
            _r.Start();
            _r.Run();

            if (Ask() == true)
                goto myloop;
        }

    }
}
