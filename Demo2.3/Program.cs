using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo2._3
{
    class Shape//图形
    {
    }
    class Rectangle : Shape//长方形
    {
        public double Height;
        public double Width;
    }
    class Program
    {
        public static void Display(Shape o)
        {
            Console.WriteLine("shape!");
        }        
        public static Rectangle GetRectangle()
        {
           return new Rectangle();
        }
        static void Main(string[] args)
        {
            var r = new Rectangle() {Height = 2.5, Width = 5};
            Display(r);//in 抗变（逆变）


            Shape s = GetRectangle();//协变 out
            
            Display(s);
        }
    }
}
