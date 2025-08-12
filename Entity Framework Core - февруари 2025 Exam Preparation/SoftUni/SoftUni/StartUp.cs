using System.Text;
using Microsoft.EntityFrameworkCore;
using SoftUni.Data;
using SoftUni.Models;

namespace SoftUni
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            using SoftUniContext contextTest = new SoftUniContext();
            contextTest.Database.EnsureCreated();



            string rezultat = RemoveTown(contextTest);
            Console.WriteLine(rezultat);


        }


        //Problem 3
        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            var employees = context
                .Employees
                .OrderBy(e => e.EmployeeId)
                .Select
                (e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.MiddleName,
                    e.JobTitle,
                    e.Salary,
                }
                )
                .ToList();

            StringBuilder result = new StringBuilder();

            foreach (var e in employees)
            {
                result.AppendLine($"{e.FirstName} {e.LastName} {e.MiddleName} {e.JobTitle} {e.Salary:f2}");
            }


            return result.ToString().TrimEnd();

        }
        //problem 4

        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            var employees = context
                .Employees
                .OrderBy(x => x.FirstName)
                .Where(x => x.Salary > 50000)
                .Select
                (e => new
                {
                    e.FirstName,
                    e.Salary
                }
                )
                .ToList();

            StringBuilder result = new StringBuilder();

            foreach (var e in employees)
            {
                result.AppendLine($"{e.FirstName} - {e.Salary:f2}");
            }
            return result.ToString().TrimEnd();
        }

        //problem 5

        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {

            var employees = context
                .Employees
                .Where(e => e.Department.Name == "Research and Development")
                .OrderBy(e => e.Salary)
                .ThenByDescending(e => e.FirstName)
                .Select
                    (e => new
                    {
                        e.FirstName,
                        e.LastName,
                        e.Department,
                        e.Salary
                    }
                    ).ToList();

            StringBuilder result = new StringBuilder();

            foreach (var e in employees)
            {
                result.AppendLine($"{e.FirstName} {e.LastName} from {e.Department.Name} - ${e.Salary:f2}");
            }

            return result.ToString().TrimEnd();
        }

        //problem 6

        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            Address newAdress = new Address();
            newAdress.AddressText = "Vitoshka 15";
            newAdress.TownId = 4;

            var nakovEmployee = context
                .Employees
                .First(e => e.LastName == "Nakov");

            nakovEmployee.Address = newAdress;
            context.SaveChanges();

            var orderedEmployees = context
                .Employees
                .OrderByDescending(e => e.AddressId)
                .Select(e => e.Address.AddressText)
                .Take(10)
                .ToList();



            return string.Join(Environment.NewLine, orderedEmployees);


        }

        //problem 7

        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            var employees = context
                    .Employees
                    .Take(10)
                    .Select(e => new
                    {
                        e.FirstName,
                        e.LastName,
                        managerFirstName = e.Manager.FirstName,
                        managerLastName = e.Manager.LastName,
                        Project = e.EmployeesProjects
                            .Select(ep => ep.Project)
                            .Where(p =>
                                   p.StartDate.Year >= 2001 &&
                                   p.StartDate.Year <= 2003)
                            .Select(p => new
                            {

                                ProjectName = p.Name,
                                ProjectStartDate = p.StartDate,
                                ProjectEndDate = p.EndDate
                            })
                            .Take(10)
                            .ToArray()
                    });

            StringBuilder result = new StringBuilder();

            foreach (var e in employees)
            {
                result.AppendLine($"{e.FirstName} {e.LastName} - Manager: {e.managerFirstName} {e.managerLastName}");
                if (e.Project != null)
                {
                    foreach (var p in e.Project)
                    {
                        if (p.ProjectEndDate == null)
                        {
                            result.AppendLine($"--{p.ProjectName} - {p.ProjectStartDate.ToString("M/d/yyyy h:mm:ss tt")} - {"not finished"}");
                        }
                        else
                        {
                            result.AppendLine($"--{p.ProjectName} - {p.ProjectStartDate.ToString("M/d/yyyy h:mm:ss tt")} - {p.ProjectEndDate.Value.ToString("M/d/yyyy h:mm:ss tt")}");
                        }

                    }
                }
            }
            return result.ToString().TrimEnd();
        }


        //problem 8
        public static string GetAddressesByTown(SoftUniContext context)
        {
            var adresses = context
                .Addresses
                .OrderByDescending(a => a.Employees.Count)
                .ThenBy(a => a.Town.Name)
                .ThenBy(a => a.AddressText)
                .Select
                (a => new
                {
                    AdressText = a.AddressText,
                    TownName = a.Town.Name,
                    EmployeeCounte = a.Employees.Count


                }
                ).Take(10)
                .ToList();

            StringBuilder result = new StringBuilder();
            foreach (var a in adresses)
            {
                result.AppendLine($"{a.AdressText}, {a.TownName} - {a.EmployeeCounte} employees");
            }
            return result.ToString().TrimEnd();
        }

        //problem 9
        public static string GetEmployee147(SoftUniContext context)
        {
            var employee = context.Employees
                .Where(e => e.EmployeeId == 147)
                .Select(e => new
                {
                    EmployeeFirstName = e.FirstName,
                    EmployeeLastName = e.LastName,
                    EmployeeJobTitle = e.JobTitle,

                    Projects = e.EmployeesProjects
                        .Select(ep => new
                        {
                            employeeProjectName = ep.Project.Name

                        }
                        ).OrderBy(ep => ep.employeeProjectName)
                        .ToList()


                }).FirstOrDefault();

            StringBuilder result = new StringBuilder();

            result.AppendLine(
                $"{employee.EmployeeFirstName} {employee.EmployeeLastName} - {employee.EmployeeJobTitle}");

            foreach (var p in employee.Projects)
            {
                result.AppendLine($"{p.employeeProjectName}");


            }
            return result.ToString().TrimEnd();
        }

        //problem 10
        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            var departmm = context
                .Departments
                .Where(x => x.Employees.Count > 5)
                .OrderBy(x => x.Employees.Count)
                .ThenBy(x => x.Name)
                .Select(d => new
                {

                    deprtmentName = d.Name,
                    mngrFirstName = d.Manager.FirstName,
                    mgrLastName = d.Manager.LastName,
                    Employees = d.Employees
                        .Select(e => new
                        {

                            employeeFirstName = e.FirstName,
                            employeeLastName = e.LastName,
                            employeeJobTitle = e.JobTitle,
                        }).OrderBy(e => e.employeeFirstName)
                        .ThenBy(e => e.employeeLastName)
                        .ToList()
                }).ToList();


            StringBuilder result = new StringBuilder();
            foreach (var d in departmm)
            {
                result.AppendLine($"{d.deprtmentName} – {d.mngrFirstName} {d.mgrLastName}");
                foreach (var e in d.Employees)
                {
                    result.AppendLine($"{e.employeeFirstName} {e.employeeLastName} - {e.employeeJobTitle}");
                }
            }

            return result.ToString().TrimEnd();
        }

        //problem 11
        public static string GetLatestProjects(SoftUniContext context)
        {
            var projecttts = context.Projects
                .OrderByDescending(x => x.StartDate.Date)


                .Select(pr => new
                {
                    projectName = pr.Name,
                    projectDescript = pr.Description,
                    projectStarrt = pr.StartDate
                }).Take(10)
                .OrderBy(x => x.projectName)
                .ToList();

            StringBuilder result = new StringBuilder();

            foreach (var p in projecttts)
            {

                result.AppendLine($"{p.projectName}");

                result.AppendLine($"{p.projectDescript}");

                result.AppendLine($"{p.projectStarrt.ToString("M/d/yyyy h:mm:ss tt")}");
            }
            return result.ToString().TrimEnd();
        }

        //Problem 12     TUKA SUS MODIFICIRANETO NA CENATA SUM OBYRKAN
        public static string IncreaseSalaries(SoftUniContext context)
        {
            var allEmployees = context.Employees
                .Where(
                    e =>
                        e.Department.Name == "Engineering" ||
                        e.Department.Name == "Tool Design" ||
                        e.Department.Name == "Marketing" ||
                        e.Department.Name == "Information Services"
                ).ToList();
            foreach (var ep in allEmployees)
            {
                ep.Salary *= 1.12m;
            }
            context.SaveChanges();
            var updatedEmployees = allEmployees
                .Select(e => new

                {
                    EmployeeFirstName = e.FirstName,
                    EmployeeLastName = e.LastName,
                    EmployeeSalary = e.Salary
                }).OrderBy(x => x.EmployeeFirstName)
                .ThenBy(x => x.EmployeeLastName)
                .ToList();



            StringBuilder resut = new StringBuilder();

            foreach (var e in updatedEmployees)
            {
                resut.AppendLine($"{e.EmployeeFirstName} {e.EmployeeLastName} (${e.EmployeeSalary:f2})");
            }
            return resut.ToString().TrimEnd();


        }

        //Problem 13
        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            var employees = context.Employees
                .Where(e => e.FirstName.StartsWith("Sa"))
                .Select(e => new
                {
                    EmployeeFirstName = e.FirstName,
                    EmployeeLastName = e.LastName,
                    EmployeeJobTitle = e.JobTitle,
                    EmployeeSalary = e.Salary,
                })
                .OrderBy(e => e.EmployeeFirstName)
                .ThenBy(e => e.EmployeeLastName)
                .ToList();

            StringBuilder result = new StringBuilder();

            foreach (var e in employees)
            {
                result.AppendLine(
                    $"{e.EmployeeFirstName} {e.EmployeeLastName} - {e.EmployeeJobTitle} - (${e.EmployeeSalary:f2})");
            }

            return result.ToString().TrimEnd();
        }

        //Problem 14   TRIENE S RELACII

        public static string DeleteProjectById(SoftUniContext context)
        {
            var projectToDelete = context.Projects.Find(2);

            var projectsToDelete = context.EmployeesProjects
                .Where(p => p.Project.Equals(projectToDelete))
                .ToList();

            context.EmployeesProjects.RemoveRange(projectsToDelete);

            context.Projects.Remove(projectToDelete);
            context.SaveChanges();



            StringBuilder result = new StringBuilder();
            foreach (var p in context.Projects)
            {
                result.AppendLine($"{p.Name}");
            }

            return result.ToString().TrimEnd();
        }

        //Problem 15

        public static string RemoveTown(SoftUniContext context)
        {
            var townToBeRemoved = context.Towns.Where(t => t.Name == "Seattle").FirstOrDefault();

            var adressesToBeRemoved = context.Addresses
                .Where(a => a.Town.Equals(townToBeRemoved))
                .ToList();

            foreach (var employee in context.Employees)
            {
                if (adressesToBeRemoved.Contains(employee.Address))
                {
                    employee.Address = null;
                }
            }

            context.Addresses.RemoveRange(adressesToBeRemoved);
            context.Towns.Remove(townToBeRemoved);
            context.SaveChanges();

            StringBuilder result = new StringBuilder();

            result.AppendLine($"{adressesToBeRemoved.Count} addresses in {townToBeRemoved.Name} were deleted");
            return result.ToString().TrimEnd();
        }
    }
}
