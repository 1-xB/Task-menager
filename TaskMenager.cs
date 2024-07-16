namespace Task_Menager;

internal class TaskMenager
{
    private List<string> _list = new List<string>() ;

    public List<string> TaskList { get => _list; set => _list = value;}

    public bool IsRunning { get; set; }


    public TaskMenager()
    {
        IsRunning = true;
        LoadTasks();
    }
    public void Start()
    {
        while (IsRunning)
        {
            // menu
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("What do you want to do?");
            Console.ResetColor();
            Console.WriteLine("1. See a list of tasks");
            Console.WriteLine("2. Add a new task");
            Console.WriteLine("3. Mark a task as completed");
            Console.WriteLine("4. Exit");


            string userInput;
            int choice;
            
            do
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("Enter your choice: ");
                Console.ResetColor();
                userInput = Console.ReadLine();
            } while (!int.TryParse(userInput, out choice));

            switch (choice)
            {
                case 1:
                    SeeTasks();
                    break;
                case 2:
                    string task;
                    Console.WriteLine("Enter the task: ");
                    do
                    {
                        task = Console.ReadLine();
                    } while (string.IsNullOrEmpty(task));
                    AddTask(task);
                    break;
                case 3:
                    Console.WriteLine("Which task do you want to delete? (Number): ");
                    int taskNumber;
                    do
                    {
                        userInput = Console.ReadLine();
                    } while (!int.TryParse(userInput, out taskNumber) && taskNumber <= TaskList.Count);
                    MarkTaskCompleted(taskNumber);
                    break;
                case 4:
                    Console.WriteLine("Exiting...");
                    IsRunning = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            if (IsRunning)
            {
                IsRunning = BackToMenu();
            }
            SaveToFile();
            

        }
        
        
        

    }
    
    private void SeeTasks()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        if (TaskList.Count == 0)
        {
            Console.WriteLine("You have no tasks!");
        }
        else
        {
            Console.WriteLine("List of tasks: ");
            Console.ResetColor();
            for (int i = 0; i < TaskList.Count; i++)
            {
                Console.WriteLine($"{i+1}. {TaskList[i]}");
            }
        }
        
    }

    private void AddTask(string task)
    {
        TaskList.Add(task);
        SaveToFile();
    }

    private void MarkTaskCompleted(int index)
    {
        if (index <= TaskList.Count && index > 0) 
            TaskList.RemoveAt(index - 1);
        
    }

    private bool BackToMenu()
    {
        Console.WriteLine("Back to the menu? (y/n): ");
        string userInput;
        do
        {
            userInput = Console.ReadLine();
        } while (string.IsNullOrEmpty(userInput));
        
        if (userInput.ToLower().Trim() == "y" || userInput.ToLower().Trim() == "yes")
            return true;
        
        else
            return false;
        
        
    }

    private void SaveToFile()
    {
        File.WriteAllLines("tl.txt", TaskList);
        Console.WriteLine("Tasks are saved.");
    }

    private void LoadTasks()
    {
        if (File.Exists("tl.txt"))
        {
            TaskList = File.ReadAllLines("tl.txt").ToList();
        }
    }
}