using System;
using System.Collections.Generic;
using System.Linq;

// SOLID PRINCIPLE 5: Dependency Inversion Principle
// It states that high level parts of the system should not depend on low level parts of the systems directly but instead depend upon the abstraction. When the high level part is dependent using abstraction we can change the private low level parts however we like because it isn't consumed in high level part

namespace principle5
{
    public enum Relationship
    {
        Parent,
        Child,
        Sibling
    }
    public class Person
    {
        public string Name;
    }


    //low level part of the system
    // this relationship will implement from IRelationshipBrowser
    public class Relationships : IRelationshipBrowser
    {
        private List<(Person, Relationship,Person)> relations = new List<(Person, Relationship, Person)>()   ;
        public void AddParentAndChild(Person parent, Person child){
            relations.Add((parent,Relationship.Parent,child));
            relations.Add((child,Relationship.Child, parent));
        }

        public IEnumerable<Person> FindAllChildrenOf(string Name) => relations.Where(
                x => x.Item1.Name == Name &&
                x.Item2 == Relationship.Parent
            ).Select(r => r.Item3);
    }

    //now we need to see all the child of any parent to do that our Research class needs to access relations list which is on low level so let's make the abstraction that only exposes the list
    public interface IRelationshipBrowser{
        IEnumerable<Person> FindAllChildrenOf(string Name);
    }

    public class Research
    {
        //research is high level part of the system which only depends upon the interface of low level part. Here we are injecting the dependency via constructor. This is dependcy injection.
        
        public Research(IRelationshipBrowser browser)
        {
          foreach(var p in browser.FindAllChildrenOf("John")){
              Console.WriteLine($"John has a child named {p.Name}");
          }

        }
        static void Main(string[] args)
        {
            var parent = new Person(){Name ="John"};
            var child1 = new Person(){Name ="Keshav"};
            var child2 = new Person(){Name ="Suhrit"};
            var child3 = new Person(){Name ="Simran"};



            var relationships = new Relationships();
            relationships.AddParentAndChild(parent,child1);
            relationships.AddParentAndChild(parent,child2);
            relationships.AddParentAndChild(parent,child3);

            new Research(relationships);

        }
    }
}
