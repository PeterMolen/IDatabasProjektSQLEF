using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDatabasProjektSQLEF.Models
{
    public partial class HogwartzContext : DbContext // make it partial to hogwartzContext 
    {
        //properties to make an instans
        public string Username { get; set; }
        public string Password { get; set; }
        public string UserRole { get; set; }
        public int ID { get; set; }

        // constructor
        public HogwartzContext(string username, string password, string userRole, int id)
        {
            Username = username;
            Password = password;
            UserRole = userRole;
            ID = id;

        }

        //first method, run the show (menu)
        public void RunTheShow()
        {
            Loading();
            Console.OutputEncoding = System.Text.Encoding.Unicode; //to se "special" symbols in console. specific : ϟ
            string[] menuOptions = { //display what you will see in the method (Categories)
                "       • Proffesor Overview\t\t",
                "       • Student Overview\t\t",
                "       • House Overview\t\t",
                "       • Class list\t\t",
                "       • Admin login\t\t",
                "       • End\t\t" };
            int menuSelected = 0;

            while (true)  //loop the menu
            {
                Console.Clear(); // Clear console for a cleaner display
                Console.ForegroundColor = ConsoleColor.DarkGreen; // color logo
                // Logo
                Console.WriteLine(@"
 _    ,                     __,                     
( |__|   _  _      _  _|__ / _    _  _  _  _'    _ 
  |  |_)(_)(_)\/\/(_|| |_/_\__)\/|||| )(_|_)||_||||
           _/                  /
             ▄▄▀█▄───▄───────▄
             ▀▀▀██──███─────███
             ░▄██▀░█████░░░█████░░
             ███▀▄███░███░███░███░▄
             ▀█████▀░░░▀███▀░░░▀██▀
              ,  By Peter Molen ,
            _| _ |_ _ |_  _  _ | 
           (_|(_||_(_||_)(_|| )|<");
                Console.ResetColor();

                Console.WriteLine("");
                // how to Display menu
                Console.ForegroundColor = ConsoleColor.Yellow;
                string textToFrame = "Move the ligthtning with Keyboard";
                string textToFrame2 = "       then press enter";

                Console.WriteLine("     ╔" + new string('═', textToFrame.Length + 2) + "╗");
                Console.WriteLine("     ║ " + textToFrame + " ║");
                Console.WriteLine("     ║ " + textToFrame2 + "           ║");
                Console.WriteLine("     ╚" + new string('═', textToFrame.Length + 2) + "╝");
                Console.ResetColor();
                Console.WriteLine("");

                // Display menu options with arrow pointing to the selected option
                for (int i = 0; i < menuOptions.Length; i++) // iteration to move the cursor
                {
                    if (i == menuSelected)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(menuOptions[i] + "ϟ"); // the keyborad cursor as a lightning
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(menuOptions[i]);
                        Console.ResetColor();
                    }
                }
                Console.ResetColor();

                ConsoleKeyInfo keyInfo = Console.ReadKey();
                ConsoleKey keyPressed = keyInfo.Key;

                // Update menu selection based on arrow key input
                // to move the arrow key, upp, down, or press enter
                if (keyPressed == ConsoleKey.DownArrow && menuSelected + 1 != menuOptions.Length)
                {
                    menuSelected++;
                }
                else if (keyPressed == ConsoleKey.UpArrow && menuSelected != 0)
                {
                    menuSelected--;
                }
                else if (keyPressed == ConsoleKey.Enter)
                {
                    // Perform action based on selected menu option // if press enter
                    switch (menuSelected)
                    {
                        case 0:  // refere to a method
                            ProffesorOverview();
                            break;
                        case 1:
                            StudentOverview();
                            break;
                        case 2:
                            HouseOverwiev();
                            break;
                        case 3:
                            ClassList();
                            break;
                        case 4:
                            AdminLogin();
                            break;
                        case 5:
                            EndProgram();
                            break;
                    }

                    // Reset console cursor visibility
                    Console.CursorVisible = true;

                    // Exit the loop
                    break;
                }
            }
        }

        public void ProffesorOverview()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(@"  
(\ 
\'\ 
 \'\     __________  
 / '|   ()_________)
 \ '/    \ professor \
   \       \ Overwiev \
   /        \          \
  ==).       \__________\  
 (__)        ()__________)");
            Console.ResetColor();

            Console.WriteLine("");
            // Add your code for the first choice here
            HogwartzContext dbContext = new HogwartzContext();

            // EF method list of all professors
            var query = from Proffesor in dbContext.Proffesors
                        join occupation in dbContext.Occupations on Proffesor.ProffesorId equals occupation.FkProffesorId
                        join proffesion in dbContext.Proffesions on occupation.FkProffesionId equals proffesion.ProffesionId
                        orderby Proffesor.ProffStartDate // order by title
                        select new
                        {
                            FirstName = Proffesor.FirstName,
                            LastName = Proffesor.LastName,
                            Title = proffesion.Title,
                            ProfStartDate = Proffesor.ProffStartDate,
                            YearsWorked = EF.Functions.DateDiffYear(Proffesor.ProffStartDate, DateTime.Now)
                        };

            foreach (var item in query)
            {
                Console.WriteLine($"Name: {item.FirstName} {item.LastName}, " +
                                  $"Title: {item.Title}, " +
                                  $"Start Date: {item.ProfStartDate} Years worked: {item.YearsWorked}");

            }

            //method to count all profesor (those who works as teachers)
            var professorCount = (from professor in dbContext.Proffesors
                                  join occupation in dbContext.Occupations on professor.ProffesorId equals occupation.FkProffesorId
                                  join profession in dbContext.Proffesions on occupation.FkProffesionId equals profession.ProffesionId
                                  where profession.Title == "Proffesor"
                                  select professor).Count();

            Console.Write($"The current number that works as a professor in Hogwartz are: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{professorCount}");
            Console.ResetColor();


            Console.WriteLine("");
            Console.WriteLine("");
            ReturnToMenu();
            Console.ReadLine(); // Wait for user input
        }

        public void StudentOverview()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(@"
          _
      /b_,dM\__,_
    _/MMMMMMMMMMMm,
   _YMMMMMMMMMMMM(
  `MMMMMM/   /   \   
   MMM|  __  / __/  
   YMM/_/# \__/# \    
   (.   \__/  \__/     
     )       _,  |    
_____/\     _   /       
    \  `._____,'
     `..___(__
              ``-.
                  \
                   )

░S░t░u░d░e░n░t░ ░o░v░e░r░v░i░e░w░");
            Console.ResetColor();
            Console.WriteLine("");

            //method to show student list
            try
            {
                using (var context = new HogwartzContext())
                {
                    // Explicitly load students and related data
                    //context.Students.Load();
                    //context.HouseMembers.Load();
                    //context.Houses.Load();

                    var query = from student in context.Students
                                join housemember in context.HouseMembers on student.StudentId equals housemember.FkStudentId
                                join houses in context.Houses on housemember.FkHouseId equals houses.HousesId
                                orderby student.StudentId
                                select new
                                {
                                    StudentID = student.StudentId,
                                    FirstName = student.StudentFirstName,
                                    LastName = student.StudentLastName,
                                    HouseName = houses.HouseName
                                };

                    foreach (var student in query)
                    {
                        Console.Write($"ID: ");
                        Console.Write($"[{student.StudentID}] ");
                        Console.Write($"Name: ");
                        Console.Write($"{student.FirstName} {student.LastName}, ");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write($"House: ");
                        Console.ResetColor();
                        Console.WriteLine($"{student.HouseName} ");
                    }

                    Console.WriteLine("");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            ReturnToMenu();
        }

        public void ClassList()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(@"
      _ _
 .-._|C|.|
 |D| |H|M|
 |A|F|A|A|<\
 |D|L|R|G| \\
 |A|Y|M|I|  \\     
 |_|_|S|C|   \>     
nnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnn
░C░l░a░s░s░ ░l░i░s░t░");
              
            Console.ResetColor();
            Console.WriteLine("");


            // view all classes list
            HogwartzContext dbContext = new HogwartzContext();

            var Query = from Class in dbContext.Classes
                        orderby Class.ClassId
                        select new
                        {
                            ClassID = Class.ClassId,
                            ClassName = Class.ClassName,
                            ClassInfo = Class.ClassInfo
                        };

            foreach (var item in Query)
            {


                Console.Write($"Class ID: ");
                Console.Write($"[{item.ClassID}] ");
                Console.Write($"Name: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{item.ClassName}, ");
                Console.ResetColor();
                Console.Write($"Info: ");
                Console.ResetColor();
                Console.WriteLine($"{item.ClassInfo}, ");

            }

            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Active courses:");
            Console.ResetColor();

            // method of all active courses
            HogwartzContext dbContext2 = new HogwartzContext();

            var query = from student in dbContext.Students
                        join enrollment in dbContext.Enrollments on student.StudentId equals enrollment.FkStudentId
                        join Class in dbContext.Classes on enrollment.FkClassId equals Class.ClassId
                        orderby Class.ClassId
                        select new
                        {
                            FirstName = student.StudentFirstName,
                            LastName = student.StudentLastName,
                            ClassID = Class.ClassId,
                            CLassName = Class.ClassName
                        };

            foreach (var result in query)
            {
                Console.Write($"Student Name: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{result.FirstName} {result.LastName}, ");
                Console.ResetColor();
                Console.Write($"Class ID: ");
                Console.Write($"[{result.ClassID}], ");

                Console.Write($"Class Name: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{result.CLassName}, ");
                Console.ResetColor();
            }

            Console.WriteLine("");
            ReturnToMenu();
            Console.ReadLine(); // Wait for user input
        }

        public void HouseOverwiev()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(@"  
  |`-. _/\_.-`|
  |           |
  |___________|
  |__| █░█ |__|
  |__|_█▀█_|__|
  \           /
   \         /
    \       /
     '.   .'
       `,`
  ░H░o░u░s░e░s░");
            Console.ResetColor();

            HogwartzContext dbContext = new HogwartzContext();
            var Query = from House in dbContext.Houses
                        select new
                        {
                            HouseName = House.HouseName,
                            HouseAttributes = House.HouseAttributes,
                            HouseAnimal = House.HouseAnimal,
                            HouseGhost = House.HouseGhost,
                            HouseHead = House.HouseHead,
                            HouseFounder = House.HouseFounder,
                            HouseCommonRoom = House.HouseCommonRoom
                        };

            foreach (var house in Query)
            {

                Console.WriteLine("");
                Console.WriteLine($"House name: {house.HouseName}, " +
                  $"House attributes: {house.HouseAttributes}, " +
                  $"House animal: {house.HouseAnimal}");

                Console.WriteLine($"House ghost: {house.HouseGhost}, " +
                  $"House head: {house.HouseHead}, " +
                  $"House founder: {house.HouseFounder}");
                Console.WriteLine($"House Common room: {house.HouseCommonRoom}, ");

            }

            Console.WriteLine("");
            ReturnToMenu();
        }


        public void AdminLogin()
        {
            Console.Clear();
            Admin();

            // Add your code for the third choice here
            Console.ReadLine(); // Wait for user input
        }

        static void EndProgram()
        {
            Console.Clear();
            Console.WriteLine("\nAvsluta programmet!");
            // Add your code to clean up or end the program here
            Console.ReadLine(); // Wait for user input
        }

        public void Admin()
        {

            // Local variables acting as properties
            // Local variables acting as properties
            string username;
            string password;

            // List acting as a database
            List<HogwartzContext> users = new List<HogwartzContext>
        {
            new HogwartzContext("dumbledore", "123", "Admin", 1),
        };

            // Login method with parameters
            bool Login(string u, string p)
            {
                foreach (HogwartzContext user in users)
                {
                    if (user.Username == u && user.Password == p)
                    {
                        Console.WriteLine("Login successful!");
                        if (user.UserRole == "Admin")
                        {
                            AdminMenu();
                            // Do xyz
                        }
                        return true;
                    }
                }
                Console.WriteLine("Login failed. Incorrect username or password.");
                return false;
            }


            bool isSuccess;
            int attempts = 0;

            do
            {
                Console.OutputEncoding = System.Text.Encoding.Unicode;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(@"
╭──────────.₊•..─╮
  Admin Login
╰─..•₊.──────────╯");
                Console.ResetColor();
                Console.WriteLine("");
                Console.Write("Enter username: ");
                username = Console.ReadLine();
                Console.Write("Enter password: ");
                password = Console.ReadLine();

                // Use the login method
                isSuccess = Login(username, password);

                if (!isSuccess)
                {
                    attempts++;
                    Console.WriteLine($"Login attempt {attempts} failed.");
                }

                if (attempts >= 3)
                {
                    Console.WriteLine("Too many login attempts. Exiting.");
                    Environment.Exit(0);
                }

            } while (!isSuccess);
        }

        public void AdminMenu()
        {
            Loading();
            Console.OutputEncoding = System.Text.Encoding.Unicode; //to se "special" symbols in console. specific : ♥
            string[] menuOptions = {
                "                           • Add Professor\t\t",
                "                           • Add Student\t\t",
                "                           • Set gradeZ\t\t",
                "                           • View grades\t\t",
                "                           • Financials\t\t",
                "                           • Return to menu\t\t",
                "                           • End\t\t"};
            int menuSelected = 0;

            while (true)
            {
                Console.Clear(); // Clear console for a cleaner display
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(@"  
        __________-------____                 ____-------__________
          \------____-------___--__---------__--___-------____------/
           \//////// / / / / / \   _-------_   / \ \ \ \ \ \\\\\\\\/
             \////-/-/------/_/_| /___   ___\ |_\_\------\-\-\\\\/
               --//// / /  /  //|| (O)\ /(O) ||\\  \  \ \ \\\\--
                    ---__/  // /| \_  /V\  _/ |\ \\  \__---
                         -//  / /\_ ------- _/\ \  \\-
                           \_/_/ /\---------/\ \_\_/
                               ----\   |   /----
                                    | -|- |
                                   /   |   \
                                   ---- \___|
                                  ,                
                             _  _| _ . _  _  _  _    
                            (_|(_|||||| )|||(-`| )|_|");
                Console.ResetColor();
                // Display menu
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Yellow;
                string textToFrame = "Move the ligthtning with Keyboard";
                string textToFrame2 = "       then press enter";
                Console.WriteLine("                      ╔" + new string('═', textToFrame.Length + 2) + "╗");
                Console.WriteLine("                      ║ " + textToFrame + " ║");
                Console.WriteLine("                      ║ " + textToFrame2 + "           ║");
                Console.WriteLine("                      ╚" + new string('═', textToFrame.Length + 2) + "╝");
                Console.ResetColor();
                Console.WriteLine("");

                // Display menu options with arrow pointing to the selected option
                for (int i = 0; i < menuOptions.Length; i++)
                {
                    if (i == menuSelected)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(menuOptions[i] + "ϟ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine(menuOptions[i]);
                        Console.ResetColor();
                    }
                }
                Console.ResetColor();

                ConsoleKeyInfo keyInfo = Console.ReadKey();
                ConsoleKey keyPressed = keyInfo.Key;

                // Update menu selection based on arrow key input
                if (keyPressed == ConsoleKey.DownArrow && menuSelected + 1 != menuOptions.Length)
                {
                    menuSelected++;
                }
                else if (keyPressed == ConsoleKey.UpArrow && menuSelected != 0)
                {
                    menuSelected--;
                }
                else if (keyPressed == ConsoleKey.Enter)
                {
                    // Perform action based on selected menu option
                    switch (menuSelected)
                    {
                        case 0:
                            AddProfessor();
                            break;
                        case 1:
                            AddStudent();
                            break;
                        case 2:
                            SetGradeZ();
                            break;
                        case 3:
                            ViewGrades();
                            break;
                        case 4:
                            Financials();
                            break;
                        case 5:
                            ReturnToMenu();
                            break;
                        case 6:
                            EndAdminProgram();
                            break;
                    }

                    // Reset console cursor visibility
                    Console.CursorVisible = true;

                    // Exit the loop
                    break;
                }
            }
        }

        public void AddProfessor()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(@"
      __...--~~~~~-._   _.-~~~~~--...__
    //               `V'               \\ 
   //      ADD        |    Professor    \\ 
  //__...--~~~~~~-._  |  _.-~~~~~~--...__\\ 
 //__.....----~~~~._\ | /_.~~~~----.....__\\
====================\\|//====================
                     `---`");
            Console.ResetColor();
            // Add your code for the first choice here
            // add Proffesor FirstName, Lastname, ProffStartDate samt t.ex proffesorID
            // och sätta vilken typ av klass dom ska undervisa?
            // eller sätta 3 i proffesion att dom är en proffesor?
            HogwartzContext dbContext = new HogwartzContext();
            
            // Display available proffesions for the proffesor to choose
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Available Professions:");
            Console.ResetColor();

            var proffesions = dbContext.Proffesions.ToList();
            foreach (var item in proffesions)
            {
                Console.WriteLine($"Proffesion ID: {item.ProffesionId}, Title: {item.Title}");
            }

            // Get the Proffesion ID from the user
            Console.Write("Enter the Profession ID for the professor: ");
            int professionId = int.Parse(Console.ReadLine());

            // Get proffeosor input from user
            Console.Write("Enter Professor First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter Professor Last Name: ");
            string lastName = Console.ReadLine();

            // Create a new Professor
            Proffesor newProfessor = new Proffesor()
            {
                FirstName = firstName,
                LastName = lastName,
                ProffStartDate = DateTime.Now
            };

            //Add the student to the Students table
            dbContext.Proffesors.Add(newProfessor);
            dbContext.SaveChanges();

            // Create a new TItle record to associate the proffeosr with the chosen Profession
            Occupation newOccupation = new Occupation()
            {
                FkProffesorId = newProfessor.ProffesorId,
                FkProffesionId = professionId
            };

            // Add the HouseMember to the HouseMembers table
            dbContext.Occupations.Add(newOccupation);
            dbContext.SaveChanges();


            Console.WriteLine("Proffesor, startDate and title added successfully!");

            Console.WriteLine("");
            ReturnToAdminMenu();


        }

        public void AddStudent()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(@"
   _____
  /     \          
/- (*) |*)\      * *
|/\.  _>/\|     _    *
    \__/       //     * *   
   _| |_      //       * * *
  /|\__|\\  ,//        * * * 
 |/|   | \\/__\        * * * *  
 |||   |
 /_\| ||
   |7 |7
   /\ \ \  
  ^^^^ ^^^
░A░d░d░ ░s░t░u░d░e░n░t░");
            Console.ResetColor();
            // Add your code for the first choice here
            // add spara ner nya studenter, StudentFirtName och StudentLastName & välj vilken klass dom ska gå i + hus
            // add sätt betyg och välj vilket lärare (ID 1-13) som satt det. betyget ska också ha ett datum då det sätts

            HogwartzContext dbContext = new HogwartzContext();
            // Display available houses for the user to choose
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Available Houses:");
            Console.ResetColor();

            var houses = dbContext.Houses.ToList();
            foreach (var house in houses)
            {
                Console.WriteLine($"House ID: {house.HousesId}, House Name: {house.HouseName}");
            }

            // Get the house ID from the user
            Console.Write("Enter the House ID for the student: ");
            int houseId = int.Parse(Console.ReadLine());

            // Display available classes for the user to choose
            Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Available Classes:");
                Console.ResetColor();
                var classes = dbContext.Classes.ToList();
                foreach (var allClasses in classes)
                {
                    Console.WriteLine($"Class ID: {allClasses.ClassId}, Class Name: {allClasses.ClassName}");
                }

                // Get the class ID from the user
                Console.Write("Enter the Class ID for the student: ");
                int classId = int.Parse(Console.ReadLine());

                // Get student information from the use
                Console.Write("Enter Student First Name: ");
                string firstName = Console.ReadLine();

                Console.Write("Enter Student Last Name: ");
                string lastName = Console.ReadLine();

                // Create a new student
                Student newStudent = new Student()
                {
                    StudentFirstName = firstName,
                    StudentLastName = lastName
                };

                //Add the student to the Students table
                dbContext.Students.Add(newStudent);
                dbContext.SaveChanges();
                
               
                // Create a new enrollment record to associate the student with the chosen class
                Enrollment newEnrollment = new Enrollment()
                {
                    FkStudentId = newStudent.StudentId,
                    FkClassId = classId
                };

                //Add the enrollment to the Enrollments table
                dbContext.Enrollments.Add(newEnrollment);
                dbContext.SaveChanges();

                // Create a new HouseMember record to associate the student with the chosen house
                HouseMember newHouseMember = new HouseMember()
                {
                    FkStudentId = newStudent.StudentId,
                    FkHouseId = houseId
                };

                // Add the HouseMember to the HouseMembers table
                dbContext.HouseMembers.Add(newHouseMember);
                dbContext.SaveChanges();

                Console.WriteLine("Student added and assigned to the selected class and house successfully!");

                Console.WriteLine("");
                ReturnToAdminMenu();
        }

        public void SetGradeZ()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(@"
      ______ ______
    _/      Y      \_
   // Set   |  MVG+ \\
  // GradeZ |   A+   \\  
 //________.|.________\\    
`----------`-'----------'");
            Console.ResetColor();
            // Vi vill kunna spara ner betyg för en elev i varje kurs de läst och
            // vi vill kunna se vilken lärare som satt betyget. Betyg ska också ha ett datum då de satts

            HogwartzContext dbContext = new HogwartzContext();
            // Display available grades for the user to choose
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Available Grades:");
            Console.ResetColor();
            var grades = dbContext.Grades.ToList();
            foreach (var grade in grades)
            {
                Console.WriteLine($"Grade ID: {grade.GradesId}, Grade Info: {grade.GradesInfo}");
            }

            // Get the grade ID from the user
            Console.Write("Enter the Grade ID (1-5): ");
            int gradeId = int.Parse(Console.ReadLine());

            // Display available professors for the user to choose
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Available Professors:");
            Console.ResetColor();
            var professors = dbContext.Proffesors.ToList();
            foreach (var professor in professors)
            {
                Console.WriteLine($"Professor ID: {professor.ProffesorId}, Name: {professor.FirstName} {professor.LastName}");
            }

            // Get the professor ID from the user
            Console.Write("Enter Professor ID: ");
            int professorId = int.Parse(Console.ReadLine());

            // Display available classes for the user to choose
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Available Classes:");
            Console.ResetColor();
            var classes = dbContext.Classes.ToList();
            foreach (var allClasses in classes)
            {
                Console.WriteLine($"Class ID: {allClasses.ClassId}, Class Name: {allClasses.ClassName}");
            }

            // Get the class ID from the user
            Console.Write("Enter the Class ID for the student: ");
            int classId = int.Parse(Console.ReadLine());

            // Display students in the selected class
            var studentsInClassQuery = from student in dbContext.Students
                                       join enrollment in dbContext.Enrollments on student.StudentId equals enrollment.FkStudentId
                                       where enrollment.FkClassId == classId
                                       select new
                                       {
                                           StudentID = student.StudentId,
                                           FirstName = student.StudentFirstName,
                                           LastName = student.StudentLastName
                                       };

            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Students in Class ID: {classId}");
            Console.ResetColor();
            foreach (var studentInfo in studentsInClassQuery)
            {
                Console.WriteLine($"Student ID: {studentInfo.StudentID}, Name: {studentInfo.FirstName} {studentInfo.LastName}");
            }

            // Get the student ID from the user
            Console.Write("Enter the Student ID to set the grade: ");
            int studentId = int.Parse(Console.ReadLine());

            // Set the grade for the student using LINQ
            var setGradeQuery = from enrollment in dbContext.Enrollments
                                where enrollment.FkClassId == classId && enrollment.FkStudentId == studentId
                                select enrollment;

            var enrollmentToUpdate = setGradeQuery.FirstOrDefault();

            if (enrollmentToUpdate != null)
            {
                // Create a new SetGrade record to set the grade for the student
                SetGrade newSetGrade = new SetGrade()
                {
                    FkProffesorId = professorId,
                    FkGradesId = gradeId,
                    FkClassId = classId,
                    FkStudentId = studentId
                };

                // Add the SetGrade record to the SetGrades table
                dbContext.SetGrades.Add(newSetGrade);

                // Update GradeDateSet in the Grades table
                var gradeToUpdate = dbContext.Grades.FirstOrDefault(g => g.GradesId == gradeId);
                if (gradeToUpdate != null)
                {
                    gradeToUpdate.GradeDateSet = DateTime.Now;
                }

                dbContext.SaveChanges();

                Console.WriteLine($"Grade {gradeId} set for Student ID: {studentId} in Class ID: {classId} by Professor ID: {professorId}");
            }
            else
            {
                Console.WriteLine($"Student with ID {studentId} not found in the specified class.");
            }

            ReturnToAdminMenu();


        }

        public void ViewGrades()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(@"
      ______ ______
    _/      Y      \_
   // View  |  MVG+ \\
  // Grades |   A+   \\  
 //________.|.________\\    
`----------`-'----------'");
            Console.ResetColor();

            try
            {
                using (var context = new HogwartzContext())
                {
                    var query = from grade in context.Grades
                                join setGrade in context.SetGrades on grade.GradesId equals setGrade.FkGradesId
                                join proffesor in context.Proffesors on setGrade.FkProffesorId equals proffesor.ProffesorId
                                join student in context.Students on setGrade.FkStudentId equals student.StudentId
                                orderby student.StudentId
                                select new
                                {
                                    StudentID = student.StudentId,
                                    StudentFirstName = student.StudentFirstName,
                                    StudentLastName = student.StudentLastName,
                                    GradesInfo = grade.GradesInfo,
                                    GradeDateSet = grade.GradeDateSet,
                                    FirstName = proffesor.FirstName,
                                    LastName = proffesor.LastName,
                                };

                    foreach (var item in query)
                    {
                        Console.Write($"ID: ");
                        Console.Write($"[{item.StudentID}] ");
                        Console.Write($"Name: ");
                        Console.Write($"{item.StudentFirstName} {item.StudentLastName}, ");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write($"Grades: {item.GradesInfo} ");
                        Console.ResetColor();
                        Console.Write($"Set by: ");
                        Console.WriteLine($"{item.FirstName} {item.LastName}, Date: {item.GradeDateSet}");
                    }

                    Console.WriteLine("");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            Console.WriteLine("");
            ReturnToAdminMenu();
        }



        public void Financials()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(@"


        _.-'~~`~~'-._
     .'`  L   L   E  `'.
    / A       ,,,     O  \
  /`      /-'`   '`-\     `\
 ; G     '  ░▄▄▄▄░   '   N  ;
;      /    ▀▀▄██►     \     ;
|     |     ▀▀███►      |     |
|     |    ░ ▀███►░█►   |     |
;      \    ▒▄████▀▀   /      ;
 ;  	'	      '      ;
  \       \          /      /
   `\   ()  '`-,,,-'` ()  /`
     '._       1      _ .'  
         `'-..,,,..-'` 	
     ░F░i░n░a░n░c░i░a░l░s░
");
            Console.ResetColor();
            // Monthly Salary list
            HogwartzContext dbContext = new HogwartzContext();
            var query = from Proffesion in dbContext.Proffesions
                        select new
                        {
                            Title = Proffesion.Title,
                            MonthlySalary = Proffesion.MonthlySalary
                        };

            foreach (var salary in query)
            {
                Console.Write($"Department: ");
                Console.Write($"{salary.Title}, ");
                Console.Write($"Monthly Salary: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{salary.MonthlySalary}");
                Console.ResetColor();
            }
            // Average total salary
            HogwartzContext dbContext2 = new HogwartzContext();
            var query2 = (from Proffesion in dbContext.Proffesions
                          join Occupation in dbContext.Occupations on Proffesion.ProffesionId equals Occupation.FkProffesionId
                          join Proffesor in dbContext.Proffesors on Occupation.FkProffesorId equals Proffesor.ProffesorId
                          select Proffesion.MonthlySalary).Average();

            Console.WriteLine("");
            Console.Write("Overall AVG Salary : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($" {query2}");
            Console.ResetColor();

            Console.WriteLine("");
            Console.WriteLine("");
            ReturnToAdminMenu();
        }


        public void ReturnToMenu() // method ro retunr to menu
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("[Press any key to return to the menu]");
            Console.ResetColor();
            Console.ReadKey(true);
            RunTheShow();
        }

        public void ReturnToAdminMenu() // method to retunn to admin menu
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Press any key to return to Admin menu");
            Console.ResetColor();
            Console.ReadKey(true);
            AdminMenu();
        }



        static void EndAdminProgram() // end program
        {
            Console.Clear();
            Console.WriteLine("\nAvsluta programmet!");
            // Add your code to clean up or end the program here
            Console.ReadLine(); // Wait for user input
        }

        public void Loading() // method for a loading screen
        {
            Console.CursorVisible = false;
            Console.WindowWidth = 110;
            Console.WindowHeight = 50;

            for (int i = 0; i <= 5; i++) // Reduced the number of iterations
            {
                Console.Clear();
                // ASCII art movement
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine(new string(' ', i) + "──▄█▀█▄─────────██");
                Console.WriteLine(new string(' ', i) + "▄████████▄───▄▀█▄▄▄▄");
                Console.WriteLine(new string(' ', i) + "██▀▼▼▼▼▼─▄▀──█▄▄");
                Console.WriteLine(new string(' ', i) + "█████▄▲▲▲─▄▄▄▀───▀▄");
                Console.WriteLine(new string(' ', i) + "██████▀▀▀▀─▀────────▀▀");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("Loading... [" + new string('=', i) + new string(' ', 5 - i) + "] " + (i * 20) + "%");
                Console.ResetColor();
                Thread.Sleep(200); // Adjust the sleep duration


            }

            Console.Clear();
            Console.CursorVisible = true;
        }
    }
}
