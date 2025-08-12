using EFCoreIntro.Models;

namespace EFCoreIntro
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SoftUniContext context = new SoftUniContext();



            




            var ToBeRemoved = context.Employees.FirstOrDefault(x => x.FirstName == "Svetlin");

            context.Entry(ToBeRemoved).Reload();
            context.Employees.Add(ToBeRemoved);
            context.SaveChanges();







            var employees = context.Employees.ToList();
            foreach (var e in employees)
            {
                Console.WriteLine(e.FirstName);
                Console.WriteLine(e.LastName);
                Console.WriteLine("------------------------------------------");
            }
        }
    }
}
