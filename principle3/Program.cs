using System;

//SOLID Principle 3
// Liskov Substitution Principle : you should be able to substitute a base type for a sub type 

namespace principle3
{
    public class Rectangle
    {
        public virtual int Width{get;set;}
        public virtual int Height{get;set;}
        public Rectangle()
        {
            
        }
        public Rectangle(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }
        public override string ToString(){
            return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
        }
    }

    public class Square : Rectangle{
        public override int Width 
        {
            set {base.Width = base.Height = value; }
        }
        public override int Height {
            set {base.Height = base.Width = value;}
        }

    }
    public class Program
    {
        public static int Area(Rectangle r) => r.Height * r.Width ; 
        static void Main(string[] args)
        {
            Rectangle rc = new Rectangle(2,3);
            Console.WriteLine($"{rc} has area {Area(rc)}");

            Rectangle sq = new Square();
            sq.Width = 5;
            Console.WriteLine($"{sq} has area {Area(sq)}");

        }
    }
}
