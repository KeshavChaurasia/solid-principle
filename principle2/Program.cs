using System;
using System.Collections.Generic;
// SOLID Princple 2: Open-Close Principle
// The Classes should be open for extension by closed for modification
namespace principle2
{
    public enum Color {
        Green,Red,Blue
    }

    public enum Size{
        Small,Medium,Large
    }

    public class Product
    {
        public string Name;
        public Color Color;
        public Size Size;
        public Product(string name, Color color, Size size)
        {
            Name = name;
            Color = color;
            Size = size;
        }
    }

    public class ProductFilter
    {
        public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size){
            foreach(var p in products)
                if(p.Size == size)
                    yield return p;
        }
        // you see the productfilter we need to modify it everytime we need new type of filters.. this is bad and we should not do this cause we need to modify.. This should be closed
        public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color){
            foreach(var p in products)
                if(p.Color == color)
                    yield return p;
        }
        //you see everytime I need different filter I need to modify the class.. this is bad. this is very bad
        public IEnumerable<Product> FilterByColorAndSize(IEnumerable<Product> products, Color color, Size size){
            foreach(var p in products)
                if(p.Color == color && p.Size == size)
                    yield return p;
        }


    }

    //So lets extend this and make it better and by creating generic filter.. we are open to this..
    public interface ISpecification<T>
    {
        bool IsSatisfied();
    }



    public class Program
    {
        static void Main(string[] args)
        {
            var apple = new Product("Apple",Color.Green,Size.Small);
            var tree = new Product("Tree",Color.Green,Size.Large);
            var bread = new Product("Apple",Color.Blue,Size.Medium);
            var house = new Product("House",Color.Blue,Size.Large);

            Product[] products = {apple,tree,bread,house};
            var pf = new ProductFilter();
            Console.WriteLine("Green Products (old):");
            foreach(var p in pf.FilterByColor(products,Color.Green))
            {
                Console.WriteLine($" - {p.Name}");
            }
            
        }
    }
}
