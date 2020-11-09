using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ToDo
{
    class Program
    {
        class Activity
        {
            public string date;
            public string state;
            public string title;

            public Activity(string inputDate, string inputState, string inputTitle)
            {
                if (String.IsNullOrEmpty(inputDate))
                {
                    date = "--";
                }
                else
                {
                    date = inputDate;
                }
                state = inputState;
                title = inputTitle;

            }

            public Activity(string inputDate, string inputTitle)
            {
                if (String.IsNullOrEmpty(inputDate))
                {
                    date = "--";
                }
                else
                {
                    date = inputDate;
                }
                state = "v";
                title = inputTitle;

            }

            public override string ToString()
            {
                return String.Format("{0,-6} {1} {2}", date.PadLeft(4), state, title);
            }

            public string SaveFields()
            {
                return String.Format("{0}#{1}#{2}{3}", date, state, title, Environment.NewLine);
            }

        }
        static void Main(string[] args)
        {
            // Greetings:
            Console.WriteLine("Hello and welcome to todo list!");
            Console.WriteLine("To quit type 'quit', for help type 'help'!");
            // Declarations:
            string command;
            string[] commandWord;
            string pathToCurrentList = "";
            List<Activity> todoList = new List<Activity>();
           
            do
            {
                Console.Write("> ");
                command = Console.ReadLine();
                commandWord = command.Split(' ');
                //    OM commandot är load: läs filen
                if (command == "quit")
                {
                    Console.WriteLine("Bye!");
                }
                else if (commandWord[0] == "load")
                {
                    Console.WriteLine("Reading file {0}", commandWord[1]);
                    todoList = ReadTodoListFile(commandWord[1]);
                    pathToCurrentList = commandWord[1];
                }
                else if (commandWord[0] == "save")
                {
                    if (commandWord.Length == 1)
                    {
                        SaveTodoList(todoList, pathToCurrentList);
                    }
                    else
                    {
                        SaveTodoList(todoList, commandWord[1]);
                    }
                    
                }
                else if (commandWord[0] == "find")
                {
                    string[] dirs = Directory.GetFiles(@"c:\users\frage", "*.lis");
                    foreach (string dir in dirs)
                    {
                        Console.WriteLine(dir);
                    }

                }
                else if (commandWord[0] == "print")
                {
                    PrintList(todoList);
                }
                else if (commandWord[0] == "add")
                {
                    todoList.Add(new Activity(commandWord[1], commandWord[2]));
                }
                else if (commandWord[0] == "move")
                {
                    int index = int.Parse(commandWord[1]);
                    Activity temp = todoList[index];
                    if (commandWord[2] == "up")
                    {
                        todoList.RemoveAt(index);
                        todoList.Insert(index - 1, temp);
                    }
                    else if (commandWord[2] == "down")
                    {
                        todoList.RemoveAt(index);
                        todoList.Insert(index + 1, temp);
                    }
                   
                }
                else
                {
                    Console.WriteLine("Unknown command: {0}", command);
                }
            } while (command != "quit");
        }

        private static List<Activity> ReadTodoListFile(string fileName)
        {
            List<Activity> todoList = new List<Activity>();
            string[] lines = File.ReadAllLines(fileName);
            foreach (string line in lines)
            {
                string[] i = line.Split('#');
                todoList.Add(new Activity(i[0], i[1], i[2]));
            }
            return todoList;
        }

        static void SaveTodoList(List<Activity> act, string fileName)
        {
            //clear
            File.WriteAllText(fileName, String.Empty);

            for (int i = 0; i < act.Count(); i++)
            {
                File.AppendAllText(fileName, act[i].SaveFields());
            }

        }

        static void PrintList(List<Activity> act)
        {
            Console.WriteLine("N  datum  S titel");
            Console.WriteLine("-------------------");
            for (int i = 0; i < act.Count(); i++)
            {
                Console.Write(i +": ");
                Console.WriteLine(act[i].ToString());
            }
        }
    }
}
