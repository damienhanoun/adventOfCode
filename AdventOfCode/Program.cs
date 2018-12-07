using StructureMap;
using System;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var containers = new Container(c => c.Scan(scanner =>
            {
                scanner.AssemblyContainingType<Day>();
                scanner.AddAllTypesOf<Day>().NameBy(x => x.Name);
            }));
            foreach (var day in containers.GetAllInstances<Day>().Reverse())
                day.GiveAnswer();
            Console.Read();
        }
    }
}
