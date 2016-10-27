using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infi_Snake
{
    class InfiSnakeGame
    {

        // private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        enum Direction { Up, Down, Left, Right }

        Random _random;
        Direction _direction;
        Frame _frame;
        bool _running;
        int _w;
        int _h;
        char[,] _field;
        int _snakelen;
        List<Tuple<int, int>> _snake;
        int _delay;

        public InfiSnakeGame()
        {
            _random = new Random();
            _frame = new Frame();

            _w = Console.WindowWidth - 1;
            _h = Console.WindowHeight - 1;

            _running = false;
            _delay = 100;
            Console.CursorVisible = false;
            //loggingexample();
        }

        //public void loggingexample()
        //{
        //    if (log.IsDebugEnabled == true) { //IsDebugEnabled -> Rückgabe boolean, Abfrage ob Debug-Level geloggt wird
        //        log.Debug("EXAMPLE Debug-Message");
        //        log.DebugFormat("EXAMPLE: Length of the Snake: {0}", _snakelen);
        //        //log.DebugFormat() ist log.Debug(String.Format()) vorzuziehen, 
        //        //da hier keine exception geworfen wird.
        //    }
        //    log.Info("EXAMPLE Info-Message");
        //    log.Warn("EXAMPLE Warning");
        //    log.Error("EXAMPLE Error");
        //    log.Fatal("EXAMPLE Fatal Error");
        //}

        internal void Start()
        {
            //Create Initial Snake
            _snake = new List<Tuple<int, int>>();
            int y = _h / 2;
            int x = _w / 2 - 3;

            for (int i = 0; i < 6; i++)
                _snake.Add(Tuple.Create(x + i, y));

            //Clear empty field
            _field = new char[_w, _h];
            for (x = 0; x < _w; x++)
                for (y = 0; y < _h; y++)
                    _field[x, y] = ' ';

            _frame.Show();
            for (int i = 0; i <=15; i++)
                GeneratePoint();

            _running = true;

            _snakelen = 0;
            _direction = Direction.Up;
        }

        private void GeneratePoint()
        {
            int x = _random.Next(2, _w-1);
            int y = _random.Next(2, _h-1);
            foreach (var tu in _snake)
            {
                if (tu.Item1 == x && tu.Item2 == y)
                {
                    GeneratePoint();
                    return;
                }
            }

            _field[x, y] = 'O';
        }

        internal void Run()
        {
            while (_running == true)
            {
                HandleInput();
                MoveSnake();
                Draw();
                Thread.Sleep(_delay);
            }
        }

        private void Draw()  
        {   ScoreCount();
            foreach (var tu in _snake)
                _field[tu.Item1, tu.Item2] = 'X';

            StringBuilder sb = new StringBuilder();

            for (int y = 1; y < _h; y++)
            {
                sb.Append('*');
                for (int x = 1; x < _w; x++)
                    sb.Append(_field[x, y]);

                sb.AppendLine();
            }

            Console.SetCursorPosition(0,1);
            Console.Write(sb.ToString());

            foreach (var tu in _snake)
                _field[tu.Item1, tu.Item2] = ' ';
        }

        private bool MoveSnake()
        {
            var current = _snake.Last();
            Tuple<int, int> newPos = null;

            switch (_direction)
            {
                case Direction.Up:
                    newPos = Tuple.Create(current.Item1, current.Item2 - 1);
                    break;

                case Direction.Down:
                    newPos = Tuple.Create(current.Item1, current.Item2 + 1);
                    break;

                case Direction.Left:
                    newPos = Tuple.Create(current.Item1 - 1, current.Item2);
                    break;

                case Direction.Right:
                    newPos = Tuple.Create(current.Item1 + 1, current.Item2);
                    break;
            }

            if (newPos.Item1 == _w | newPos.Item1 == 0 | newPos.Item2 == _h | newPos.Item2 == 0)
                return EndGame();

            foreach (var tu in _snake)
                if (tu.Item1 == newPos.Item1 && tu.Item2 == newPos.Item2)
                    return EndGame();

            _snake.Add(newPos);

            if (_field[newPos.Item1, newPos.Item2].Equals('O') == true)
            {
                _snakelen++;
                if (_snakelen > 14)
                    GeneratePoint();
            }
            else
                _snake.RemoveAt(0);

            return true;
        }

        private void HandleInput()
        {
            if (Console.KeyAvailable)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Add:
                    case ConsoleKey.OemPlus:
                        _delay += 25;
                      //  log.Info("Taste für Spieltemporeduktion gedrückt");
                        break;

                    case ConsoleKey.Subtract:
                    case ConsoleKey.OemMinus:
                        _delay -= 25;
                       // log.Info("Taste für Spieltempoerhöhung gedrückt");
                        break;

                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        if(_direction != Direction.Down)
                            _direction = Direction.Up;
                       // log.Info("Richtungstaste Nach Oben gedrückt");
                        break;

                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        if (_direction != Direction.Up)
                            _direction = Direction.Down;
                       // log.Info("Richtungstaste Nach unten gedrückt");
                        break;

                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.A:
                        if (_direction != Direction.Right)
                            _direction = Direction.Left;
                       // log.Info("Linke Richtungstaste gedrückt");
                        break;

                    case ConsoleKey.RightArrow:
                    case ConsoleKey.D:
                        if (_direction != Direction.Left)
                            _direction = Direction.Right;
                       // log.Info("Rechte Richtungstaste gedrückt");
                        break;

                    case ConsoleKey.Escape:
                        _running = false;
                        break;

                }
            }
        }
        internal void ScoreCount()
        {
            Console.SetCursorPosition(Console.WindowWidth - 12, 0);
            Console.Write("Score:" + _snakelen);
        }

        private bool EndGame()
        {
            _running = false;
            Console.Clear();
            _frame.Show();
            Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight / 2);
            Console.WriteLine("Game Oger");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight / 2 + 1);
            Console.Write("Score:" + _snakelen);
            Console.ReadKey();
            return false;
        }
    }
}
