using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;
using Core;

namespace ConsoleInterface
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    var b = new Book();
        //    b.Name = "Скотный двор";
        //    b.Year = 1945;
        //    b.ID_Author = 2;
        //    b.ID_Department = 2;
        //    b.Quantity = 10;
        //
        //    DataAccess.AddBook(b);
        //
        //
        //    foreach (var i in DataAccess.GetBooks())
        //    {
        //        Console.WriteLine(i.ID_Book + " " + i.Name);
        //    }
        //
        //
        //}

        static void Main(string[] args)
        {
            while (true)
            {
                var command = Console.ReadLine();
                if (command.Trim().ToLower() == "exit")
                    return;
                Execute(command);
            }
        }
        private static void Execute(string command)
        {
            var arguments = command.Trim().Split();
            switch (arguments[0])
            {
                case "add":
                    Add(arguments);
                    break;
                case "get":
            //        Get(arguments);
            //        break;
                case "getall":
                    GetAll(arguments);
                    break;
                case "update":
                    Update(arguments);
                    break;
                case "delete":
                    Delete(arguments);
                    break;
                default:
                    Console.WriteLine($"Unknown command");
                    break;
            }

        }

        private static void Add(string[] args)
        {
            switch (args[1])
            {
                case "author":
                    DataAccess.AddAuthor
                    (
                        new Author()
                        {
                            Surname = args[2],
                            Name = args[3],
                            BirthDate = Convert.ToDateTime(args[4]),
                            DeathDate = Convert.ToDateTime(args[5]),
                        }
                    );
                    break;
                case "book":
                    DataAccess.AddBook(new Book() {
                        Name = args[2],
                        Year = Convert.ToInt32(args[3]),
                        ID_Author = Convert.ToInt32(args[4]),
                        ID_Department = Convert.ToInt32(args[5]),
                        Quantity = Convert.ToInt32(args[6])
                    });
                    break;
                case "department":
                    DataAccess.AddDepartment
                    (
                        new Department()
                        {
                            Name = args[2],
                        }
                    );
                    break;
                default:
                    Console.WriteLine($"Unknown command");
                    break;
            }
        }
        //private static void Get(string[] args)
        //{
        //    switch (args[1])
        //    {
        //        case "book":
        //    }
        //}
        private static void GetAll(string[] args)
        {
            switch (args[1])
            {
                case "books":
                    foreach (var a in DataAccess.GetBooks())
                        Console.WriteLine($"{a.ID_Book} {a.Name} " +
                            $"{DataAccess.connection.Query<Author>(@$"select Surname, Name 
                                                                      from Author   
                                                                      where ID_Author = {a.ID_Author}").FirstOrDefault().Surname} " + 
                            $"{DataAccess.connection.Query<Department>(@$"select Name 
                                                                      from Department   
                                                                      where ID_Department = {a.ID_Department}").FirstOrDefault().Name} " +
                            $"{a.Quantity}"
                             );
                    break;
                case "departments":
                    foreach (var a in DataAccess.GetDepartments())
                        Console.WriteLine($"{a.ID_Department} {a.Name}");
                    break;
                case "authors":
                    foreach (var a in DataAccess.GetAuthors())
                        Console.WriteLine($"{a.ID_Author} {a.Surname} {a.Name} {a.BirthDate} {a.DeathDate}");
                    break;
                //case "food":
                //    foreach (var f in FoodStorage.GetFood())
                //        Console.WriteLine($"{f.FoodID} {f.FoodName} {f.Weight}");
                //    break;
                //case "animal":
                //    Console.WriteLine(AviaryStorage.GetAnimal(int.Parse(args[2])).Name);
                //    break;
                //case "homelessanimals":
                //    foreach (var a in AviaryStorage.GetHomelessAnimals())
                //        Console.WriteLine($"{a.AnimalID} {a.Name}");
                //    break;
                //case "diets":
                //    foreach (var d in FoodStorage.GetDiets(new Animal()
                //    {
                //        AnimalID = int.Parse(args[2])
                //    }))
                //        Console.WriteLine($"{d.AnimalID} {d.Date} {d.FoodID} {d.Weight}");
                //    break;
                default:
                    Console.WriteLine($"Unknown command");
                    break;
            }
        }
        private static void Delete(string[] args)
        {
            switch (args[1])
            {
                case "book":
                    DataAccess.DeleteBook(int.Parse(args[2]));
                    break;
                default:
                    Console.WriteLine("Unknown command");
                    break;
            }
        }
        private static void Update(string[] args)
        {
            switch (args[1])
            {
                case "book":
                    DataAccess.UpdateBook(int.Parse(args[2]), new Book() { Name = args[3], 
                                                                           Year = Convert.ToInt32(args[4]),
                                                                           ID_Author = Convert.ToInt32(args[5]),
                                                                           ID_Department = Convert.ToInt32(args[6]),
                                                                           Quantity = Convert.ToInt32(args[7])});
                    break;
                default:
                    Console.WriteLine("Unknown command");
                    break;
            }
        }
    }
}
