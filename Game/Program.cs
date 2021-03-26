using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Game
{
    class Program
    {
        interface IMoveable
        {
            void MoveDown();
            void MoveUp();
            void MoveRight();
            void MoveLeft();
        }
        interface IDestroyer
        {
            void Shoot(MapElement[,] Elements);
        }
        abstract class MapElement
        {
            public char drawChar;
            public Point location { get; set; }

            public MapElement(char drawChar, Point location)
            {
                this.drawChar = drawChar;
                this.location = location;
            }
            public void Draw() { Console.Write("[{0}]", drawChar); }
            public virtual void MoveDown()
            {
                if (location.Y < 19)
                    location.Offset(0, -1);
            }

            public virtual void MoveLeft()
            {
                if (location.X > 1)
                    location.Offset(-1, 0);
            }

            public virtual void MoveRight()
            {
                if (location.X < 19)
                    location.Offset(-1, 0);
            }

            public virtual void MoveUp()
            {
                if (location.Y > 0)
                    location.Offset(0, -1);
            }
            public virtual void Shoot(MapElement[,] Elements) { }

        }
        class Rock : MapElement
        {
            public Rock(char drawChar, Point location) : base(drawChar, location) { }
        }
        class Monster : MapElement, IMoveable
        {
            public Monster(char drawChar, Point location) : base(drawChar, location) { }

            public override void MoveDown()
            {
                base.MoveDown();
            }
            public override void MoveUp()
            {
                base.MoveUp();
            }
            public override void MoveLeft()
            {
                base.MoveLeft();
            }
            public override void MoveRight()
            {
                base.MoveRight();
            }
        }
        class RockDestroyer : Monster, IDestroyer
        {
            public RockDestroyer(char drawChar, Point location) : base(drawChar, location) { }
            public override void MoveDown()
            {
                base.MoveDown();
            }
            public override void MoveUp()
            {
                base.MoveUp();
            }
            public override void MoveLeft()
            {
                base.MoveLeft();
            }
            public override void MoveRight()
            {
                base.MoveRight();
            }
            public override void Shoot(MapElement[,] Elements)
            {
                Elements[location.X - 1, location.Y] = null;
            }

        }
        class Player : MapElement, IMoveable, IDestroyer
        {
            public Player(char drawChar, Point location) : base(drawChar, location) { }
            public override void MoveDown()
            {
                base.MoveDown();
            }
            public override void MoveUp()
            {
                base.MoveUp();
            }
            public override void MoveLeft()
            {
                base.MoveLeft();
            }
            public override void MoveRight()
            {
                base.MoveRight();
            }
            public override void Shoot(MapElement[,] Elements)
            {
                Elements[location.X + 1, location.Y] = null;
            }
        }
        static void Main(string[] args)
        {
            
                Random r = new Random();
                MapElement[,] Elements = new MapElement[20, 20];
                int Xplayer = 0; int Yplayer = 10;
                Elements[Xplayer, Yplayer] = new Player('X', new Point(Xplayer, Yplayer));
                for (int i = 1; i < 20; i++)
                {
                    int y = r.Next(0, 19);
                    Elements[i, y] = new Rock('O', new Point(i, y));
                    y = r.Next(0, 19);
                    Elements[i, y] = new Rock('O', new Point(i, y));
                    y = r.Next(0, 19);
                    Elements[i, y] = new Rock('O', new Point(i, y));
                }
                for (int i = 1; i < 20; i++)
                {
                    int y = r.Next(0, 19);
                    Elements[i, y] = new Monster('M', new Point(i, y));
                }
                for (int i = 3; i < 20; i = i + 2)
                {
                    int y = r.Next(0, 19);
                    Elements[i, y] = new RockDestroyer('D', new Point(i, y));
                    if (Elements[i - 1, y] is Rock)
                        Elements[i - 1, y] = null;
                }
            while (true)
            {
                Console.Clear();
                Reprint(Elements);                                                                                                                                        
                Console.WriteLine("\nBeweeg met pijlen of schiet met S.");
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        if (Elements[Xplayer, Yplayer - 1] != null)
                        {
                            Console.WriteLine("You ve hited a {0}; you lost!.", Elements[Xplayer, Yplayer - 1].ToString().Split('.')[1]);
                            Console.ReadLine();
                            break;
                        }
                        Elements[Xplayer, Yplayer] = null;
                        Yplayer--;
                        Elements[Xplayer, Yplayer] = new Player('X', new Point(Xplayer, Yplayer));
                        next(Elements[Xplayer, Yplayer].location, Elements);
                        Console.Clear();
                        Reprint(Elements);
                        break;
                    case ConsoleKey.DownArrow:
                        if (Elements[Xplayer, Yplayer + 1] != null)
                        {
                            Console.WriteLine("You ve hited a {0}, you lost!.", Elements[Xplayer, Yplayer + 1].ToString().Split('.')[1]);
                            Console.ReadLine();
                            break;
                        }
                        Elements[Xplayer, Yplayer] = null;
                        Yplayer++;
                        Elements[Xplayer, Yplayer] = new Player('X', new Point(Xplayer, Yplayer));
                        next(Elements[Xplayer, Yplayer].location, Elements);
                        Console.Clear();
                        Reprint(Elements);
                        break;
                    case ConsoleKey.LeftArrow:
                        if (Elements[Xplayer - 1, Yplayer] != null)
                        {
                            Console.WriteLine("You ve hited a {0}, you lost!.", Elements[Xplayer - 1, Yplayer].ToString().Split('+')[1]);
                            Console.ReadLine();
                            break;
                        }
                        Elements[Xplayer, Yplayer] = null;
                        Xplayer--;
                        Elements[Xplayer, Yplayer] = new Player('X', new Point(Xplayer, Yplayer));
                        next(Elements[Xplayer, Yplayer].location, Elements);
                        Console.Clear();
                        Reprint(Elements);
                        break;
                    case ConsoleKey.RightArrow:
                        if (Xplayer == 18)
                        {
                            Console.WriteLine("You win!!");
                            Console.ReadLine();
                            break;
                        }
                        if (Elements[Xplayer + 1, Yplayer] != null)
                        {
                            Console.WriteLine("You ve hited a {0}, you lost!.", Elements[Xplayer + 1, Yplayer].ToString().Split('+')[1]);
                            Console.ReadLine();
                            break;
                        }
                        Elements[Xplayer, Yplayer] = null;
                        Xplayer++;
                        Elements[Xplayer, Yplayer] = new Player('X', new Point(Xplayer, Yplayer));
                        next(Elements[Xplayer, Yplayer].location, Elements);
                        Console.Clear();
                        Reprint(Elements);
                        break;
                    case ConsoleKey.S:
                        Elements[Xplayer + 1, Yplayer] = null;
                        Console.Clear();
                        Reprint(Elements);
                        break;
                }
            }
        }
        static void next(Point player, MapElement[,] Elements)
        {
            foreach (var item in Elements)
                if (item is Monster && !(item is RockDestroyer))
                { 
                    Random rd = new Random();
                    List<Point> buren = new List<Point>();
                    if(item.location.X>0 )
                        buren.Add(  new Point(item.location.X - 1, item.location.Y));
                    if (item.location.Y > 0)
                        buren.Add(new Point(item.location.X, item.location.Y - 1));
                    if (item.location.Y < 19)
                        buren.Add(new Point(item.location.X, item.location.Y + 1));
                    if (item.location.X <19)
                        buren.Add(new Point(item.location.X + 1, item.location.Y));
                    Elements[item.location.X, item.location.Y] = null;
                    Point Next;
                    do
                        Next = buren[rd.Next(0, buren.Count-1)];
                    while (Elements[Next.X, Next.Y] != null);
                    Elements[Next.X, Next.Y] = new Monster('M', Next);
                }
            foreach (var item in Elements)
                if (item is RockDestroyer)
                {
                    Random rd = new Random();
                    List<Point> buren = new List<Point>();
                    if (item.location.X > 0)
                        buren.Add(new Point(item.location.X - 1, item.location.Y));
                    if (item.location.Y > 0)
                        buren.Add(new Point(item.location.X, item.location.Y - 1));
                    if (item.location.Y < 19)
                        buren.Add(new Point(item.location.X, item.location.Y + 1));
                    if (item.location.X < 19)
                        buren.Add(new Point(item.location.X + 1, item.location.Y));
                    Elements[item.location.X, item.location.Y] = null;
                    Point Next;
                    do
                        Next = buren[rd.Next(0, buren.Count - 1)];
                    while (Elements[Next.X, Next.Y] != null);
                    Elements[Next.X, Next.Y] = new RockDestroyer('D', Next);
                    if (Next.X > 0 && Elements[Next.X - 1, Next.Y] is Rock)
                    {
                        Elements[Next.X - 1, Next.Y] = null;
                        Console.Clear();
                        Reprint(Elements);
                    }
                    if (Next.X > 0 && Elements[Next.X - 1, Next.Y] is Player)
                    {
                        Console.WriteLine("You ve been shooted by a destroyer, you lost!.");
                        Console.ReadLine();
                    }
                }
        }
         static void Reprint(MapElement[,] elements)
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                    if (elements[j, i] != null)
                    {
                        if (elements[j, i] is Player)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            elements[j, i].Draw();
                        }
                        else if(elements[j, i]is RockDestroyer)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            elements[j, i].Draw();
                        }
                        else if (elements[j, i] is Monster && !(elements[j, i] is RockDestroyer))
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            elements[j, i].Draw();
                        }
                        else if (elements[j, i] is Rock)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            elements[j, i].Draw();
                        }
                    }
                    else
                        Console.Write("   ");
                Console.WriteLine();
                Console.ResetColor();
            }
        }
    }
}
