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
    //Ispecification implements specification pattern that whether or not certain criteria is implemented or not 
    public interface ISpecification<T>
    {
        bool IsSatisfied(T t);
    }

    //this ifilter filters from items with T type of specs and returne IEnumerable<T>
    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> specs);
    }

    //so lets make color specification
    public class ColorSpecification : ISpecification<Product>
    {
        private Color color;
        public ColorSpecification(Color color)
        {
            this.color = color;
        }
        public bool IsSatisfied(Product t)
        {
            return t.Color == this.color;
        }
    }

    //this size specification takes in Product and checks if the size is satisfied by the condition provided in the constructor

    public class SizeSpecification : ISpecification<Product>
    {
        private Size size;
        public SizeSpecification(Size size){
            this.size = size;
        }
        
        public bool IsSatisfied(Product t){
            return t.Size == this.size;
        }
    }

    public class AndSpecification<T> : ISpecification<T>
    {
        private ISpecification<T> first, second;
        public AndSpecification(ISpecification<T> first, ISpecification<T> second){
            this.first = first ?? throw new ArgumentNullException(paramName:nameof(first));
            this.second = second ?? throw new ArgumentNullException(paramName:nameof(second));
        }

        public bool IsSatisfied(T t)
        {
            return first.IsSatisfied(t) && second.IsSatisfied(t); 
        }
    }


    public class BetterFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> specs)
        {
            foreach(var i in items){
                if(specs.IsSatisfied(i)){
                    yield return i;
                }
            }
        }
    }



    // finally  we shouldn't be able to modify the class that's already built but add more class and make it more extensible 
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

            var bf = new BetterFilter();
            Console.WriteLine("Green Products (new):");
            foreach(var p in bf.Filter(products,new ColorSpecification(Color.Green))){
                Console.WriteLine($" - {p.Name}");
            }

            Console.WriteLine("Large and blue (new):");
            foreach(var p in bf.Filter(products, new AndSpecification<Product>(new ColorSpecification(Color.Blue), new SizeSpecification(Size.Large)))){
                Console.WriteLine($" - {p.Name}");

            }

        }
    }
}
