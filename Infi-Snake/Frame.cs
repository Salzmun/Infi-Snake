using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Infi_Snake
{
    class Frame
    {
        internal void Show()
        {
            top();
            left();
            right();
            bottom();

            Console.SetCursorPosition(0, 0);
            
        }

        void top()
        {
            int x = 1;
            while (x <= Console.WindowWidth)
            {
                Console.Write("*");
                x++;
            }
        }

        void left()
        {
            for (int y = 1; y < Console.WindowHeight; y++)
            {
                Console.WriteLine("*");
            }
        }

        void right()
        {
            int y = 0;
            do
            {
                Console.SetCursorPosition(Console.WindowWidth - 1, y++);
                Console.Write('*');
            }
            while (y < Console.WindowHeight);
        }

        void bottom()
        {
            int i = 0;
        myloop:
            if (i < Console.WindowWidth)
            {
                Console.SetCursorPosition(i, Console.WindowHeight - 1);
                Console.Write('*');
                i++;
                goto myloop;
            }
        }
    }
}
