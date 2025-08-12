// See https://aka.ms/new-console-template for more information


using System.Diagnostics;
using System.Net.Http.Headers;
using EFDBFirst;
using Microsoft.EntityFrameworkCore;

SoftUniContext currentContext = new SoftUniContext();

var project = new Project()
{
    StartDate = DateTime.Now
};



currentContext.SaveChanges();





var allProjects = currentContext.Projects
    .Select(x => new
        {
            x.Name,
            x.Description
        }
    )
    .ToList();


foreach (var p in allProjects)
{
    Console.WriteLine($"{p.Name}  {p.Description}");
}

