using Gymnasieskola.Models;
using HelperProject;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gymnasieskola
{
    internal class Menu
    {
        SchoolService service = new SchoolService();
        public void Run()
        {            
            string heading = "Gymnasieskolan";

            List<string> menuOptions = new List<string>{
                "Visa alla studenter",
                "Visa alla studenter i en klass",
                "Lägg till ny student",
                "Visa personal",
                "Visa avdelningar",
                "Lägg till ny personal", 
                "Avsluta"
            };

            ShowMenu(heading, menuOptions);

        }

        public void ShowMenu(string heading, List<string> menuOptions)
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                //Printing heding
                Console.WriteLine($"{heading.ToUpper()}\n");

                //Print menu choices
                for (int i = 0; i < menuOptions.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {menuOptions[i]}");
                }

                //User choose from menu
                Console.Write("\nVälj: ");
                string? userInput = Console.ReadLine();

                //Validate user input
                if (!String.IsNullOrEmpty(userInput))
                {
                    //Parsing to int
                    bool success = int.TryParse(userInput, out int parsedInput);
                    if (!success)
                    {
                        Console.WriteLine("Välj en siffra från menyn.");
                        Thread.Sleep(2000);
                        continue;
                    }

                    else if (parsedInput < 1 || parsedInput > menuOptions.Count)
                    {
                        Console.WriteLine("Välj en giltig siffra från menyn ");
                        Thread.Sleep(2000);
                        continue;
                    }

                    //If user choice is approved by validation the choice will be handled.
                    isRunning = HandleMenuChoice(parsedInput);
                }
            }
        }

        //Method to pass user onward depending on choice from menu
        public bool HandleMenuChoice(int userChoice)
        {
            switch (userChoice)
            {
                //[1] 
                case 1:
                    Console.Clear();
                    ShowAllStudents();
                    HelperMethods.ReturnToMenu();
                    break;

                //[2] 
                case 2:
                    Console.Clear();
                    ShowStudentsInAClass();
                    HelperMethods.ReturnToMenu();
                    break;

                //[3] 
                case 3:
                    Console.Clear();
                    AddStudent();
                    HelperMethods.ReturnToMenu();
                    break;

                //[4] 
                case 4:
                    Console.Clear();
                    ShowStaff();
                    HelperMethods.ReturnToMenu();
                    break;

                //[5] 
                case 5:
                    Console.Clear();
                    ShowDepartmentsAndCount();
                    HelperMethods.ReturnToMenu();
                    break;
                //[6] 
                case 6:
                    Console.Clear();
                    AddStaff();
                    HelperMethods.ReturnToMenu();
                    break;

                //[7] Exit program
                case 7:
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Tack för att du använder Gymnasieskolans app!");
                    Console.ResetColor();

                    Console.ReadLine();
                    return false;

                default:
                    Console.Clear();
                    Console.WriteLine("Något gick fel.");
                    HelperMethods.ReturnToMenu();
                    break;
            }
            return true;
        }

        //!? Add heading and only show one question at a time?
        public void ShowAllStudents()
        {
            Console.Clear();
            Console.WriteLine("Visa alla studenter sorterat\n");

            //Asking for user to choose how to sort
            string askFirstOrLast = "Vill du sortera på (1) förnamn eller (2) efternamn? \nVälj: ";

            int userChoice = HelperMethods.ReadInt(askFirstOrLast, 1, 2);
            bool sortFirst = userChoice == 1 ? true : false;

            string askAscOrDes = "Vill du sortera i (1) stigande eller (2) fallande ordning? \nVälj: ";

            int userChoice2 = HelperMethods.ReadInt(askAscOrDes, 1, 2);
            bool sortAsc = userChoice2 == 1 ? true : false;

            //Sorting list and sending users choice as argument
            List<Student> sortedStudents = service.OrderByNameAndAscOrDesc(sortFirst, sortAsc);

            //Heading for the list varies dependning on user choice
            string firstOrLastName = sortFirst.Equals(true) ? "förnamn" : "efternamn";
            string ascOrDesc = sortAsc.Equals(true) ? "stigande" : "fallande";
            string heading = $"Alla studenter sorterade på {firstOrLastName} i {ascOrDesc} ordning";
                        
            PrintStudentList(heading, sortedStudents);

        }

        public void PrintStudentList(string heading, IEnumerable<Student> students)
        {
            Console.Clear();
            Console.WriteLine(heading + "\n");

            foreach (var student in students)
            {
                Console.WriteLine($"{student.FirstName, -15} {student.LastName, -25} | {student.Email, -35} | {student.PhoneNr, -15}");
            }
        }

        public void ShowStudentsInAClass()
        {
            Console.Clear();
            Console.WriteLine("Visa studenter i en klass\n");

            //Fetch list of classNames
            List<string> classNames = service.GetClassNames();
            string prompt = "\nVälj klass (nr): ";
            int userChoice = ChooseFromList(classNames, prompt);

            //Get ClassId of choosen class
            List<Class> classes = service.GetClasses();
            int classId = classes[userChoice - 1].ClassId;

            var selectedCLass = service.GetClassList(classId);

            string heading = $"Students in class {selectedCLass.ClassName}";

            PrintStudentList(heading, selectedCLass.Students);
        }

        //!? Add heading? clear between?
        public void AddStudent()
        {
            Console.WriteLine("Fyll i uppgifter om student\n");

            string askFirstName = "Studentens förnamn: ";
            string firstName = HelperMethods.ReadString(askFirstName);

            string askLastName = "Studentens efternamn: ";
            string lastName = HelperMethods.ReadString(askLastName);

            string askSocialSecurityNr = "Studentens personummer (ÅÅÅÅMMDD-XXXX): ";
            string socialSecurityNr = HelperMethods.ReadString(askSocialSecurityNr);

            string askHomeAddress = "Studentens hemadress (Gata nr, postnr Postort): ";
            string homeAddress = HelperMethods.ReadString(askHomeAddress);

            string askPhoneNr = "Studentens telefonnummer: ";
            string phoneNr = HelperMethods.ReadString(askPhoneNr);

            string askEmail = "Studentens e-mail: ";
            string email = HelperMethods.ReadString(askEmail);

            //Choose class from list
            Console.Clear();            
            List<string> classNames = service.GetClassNames();
            string prompt = "\nVälj klass (nr): ";
            int userChoice = ChooseFromList(classNames, prompt);

            List<Class> classes = service.GetClasses();
            int classId = classes[userChoice - 1].ClassId;

            //Adding student
            service.AddStudentToDb(firstName, lastName, socialSecurityNr, homeAddress, phoneNr, email, classId);

            Console.WriteLine("\nStudent tillagd!");
            Thread.Sleep(1500);
        }

        public void ShowStaff()
        {
            Console.Clear();
            Console.WriteLine("Visa personal\n");

            //Fetch list of professions
            List<string> professions = service.GetProfessions();
            professions.Add("Alla yrkesroller");

            //Asking user for choice of class
            string prompt = "\nVälj vilken yrkesroll du vill visa (nr): ";
            int userChoice = ChooseFromList(professions, prompt);
                       
            string selectedProfession = professions[userChoice - 1];

            // Fetch selected staff
            var selectedStaff = service.GetStaffByProfessions(selectedProfession);

            //Printing staff
            string heading = "Personal utifrån yrkesroll";
            PrintStaffList(heading, selectedStaff);

        }
                
        public int ChooseFromList(List<string> list,string choosePrompt)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine($"{i + 1, 3}. {list[i]}");
            }

            int userChoice = HelperMethods.ReadInt(choosePrompt, 1, list.Count);

            return userChoice;
        }

        public void PrintStaffList(string heading, IEnumerable<Staff> staff)
        {
            Console.Clear();
            Console.WriteLine(heading + "\n");

            foreach (var employee in staff)
            {
                Console.WriteLine($"{employee.FirstName,-15} {employee.LastName,-25} | {employee.Profession,-20} | {employee.Email,-35} | {employee.PhoneNr,-15}");
            }
        }

        public void AddStaff()
        {          
            Console.Clear();
            Console.WriteLine("Fyll i uppgifter om personal\n");

            //Choose profession from list
            List<string> professions = service.GetProfessions();
            string prompt = "\nVälj yrkesroll (nr): ";
            int userChoice = ChooseFromList(professions, prompt);
           
            string selectedProfession = professions[userChoice - 1];

            string askFirstName = "Personals förnamn: ";
            string firstName = HelperMethods.ReadString(askFirstName);

            string askLastName = "Personals efternamn: ";
            string lastName = HelperMethods.ReadString(askLastName);

            string askSocialSecurityNr = "Personals personummer (ÅÅÅÅMMDD-XXXX): ";
            string socialSecurityNr = HelperMethods.ReadString(askSocialSecurityNr);

            string askHomeAddress = "Personals hemadress (Gata nr, postnr Postort): ";
            string homeAddress = HelperMethods.ReadString(askHomeAddress);

            string askPhoneNr = "Personals telefonnummer: ";
            string phoneNr = HelperMethods.ReadString(askPhoneNr);

            string askEmail = "Personals e-mail: ";
            string email = HelperMethods.ReadString(askEmail);
                       

            //Adding staff
            service.AddStaffToDb(selectedProfession, firstName, lastName, socialSecurityNr, homeAddress, phoneNr, email);

            Console.WriteLine("\nPersonal tillagd!");
            Thread.Sleep(1500);

        }

        public void ShowDepartmentsAndCount()
        {
            List<Department> Departments = service.GetDepartments();

            Console.WriteLine("Avdelningar (antal personal)\n");

            foreach (var department in Departments)
            {
                Console.WriteLine($"{department.DepartmentName}: {department.Staff.Count}");
            }
        }


    }
}
