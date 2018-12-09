using System;


//SOLID Principle 4: Interface Segregation Principle
// this principle says that instead of making one big interface for all methods.. let's make a small interface with proper implementations and your larger interface can implement from smaller interface

namespace principle4
{
    public class Document{

    }

    //this is a huge interface with all print, scan and fax .. this is okay for multifunction printer
    public interface IMachine{
        void Print(Document document);
        void Scan(Document document);
        void Fax(Document document);

    }

    public class MultiFunctionPrinter : IMachine
    {
        public void Fax(Document document)
        {
            //this is implemented
        }

        public void Print(Document document)
        {
            //this is implemented
        }

        public void Scan(Document document)
        {
            //this is implemented
        }
    }


    // but for old fashion printer we can't use this interface because only print is available but fax and scan are not implemented so .. the best thing to do would be to make smaller interface as such

    public interface IPrinter
    {
        void Print(Document document);
    }

    public interface IScanner 
    {
        void Scan(Document document);
    }

    public interface IFaxMachine
    {
        void Fax(Document document);
    }


    //so let's make the old fashion Printer 
    public class OldFashionPrinter : IPrinter
    {
        public void Print(Document document)
        {
            //this is better and now only this method needs to be implemented
        }
    }

    public class PhotoCopier : IPrinter, IScanner
    {
        public void Print(Document document)
        {
            // this is doable
        }

        public void Scan(Document document)
        {
            // this is doable
        }
    }


    // this is better approach. 
    public class BetterMultiFunctionPrinter : IPrinter, IScanner, IFaxMachine
    {
        public void Fax(Document document)
        {
            //this is doable
        }

        public void Print(Document document)
        {
            //this is doable
        }

        public void Scan(Document document)
        {
            //this is doable
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
