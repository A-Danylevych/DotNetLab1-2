using System;
using BinaryTree;

namespace DotNetLab
{
    internal static class Program
    {
        private static void Main()
        {
            BinaryTree<int> tree = new();
            Console.WriteLine("Binary Tree\n1. Create new tree\n2. Add\n3. Remove\n4. Add notifier\n" +
                "5. Remove notifier\n6. Contains\n7. Show all\n8. Quit");
            var run = true;
            while (run)
            {
                switch (Console.ReadLine())
                {
                    case "1":
                        tree.Clear();
                        break;
                    case "2":
                        var number = ReadValidNumber();
                        tree.Add(number);
                        break;
                    case "3":
                        number = ReadValidNumber();
                        tree.Add(number);
                        break;
                    case "4":
                        tree.Notify += ShowMessage;
                        break;
                    case "5":
                        tree.Notify-= ShowMessage;
                        break;
                    case "6":
                        number = ReadValidNumber();
                        var contains = tree.Contains(number);
                        Console.WriteLine(contains);
                        break;
                    case "7":
                        foreach(var i in tree)
                        {
                            Console.Write(i + " ");
                        }
                        break;
                    case "8":
                        run = false;
                        break;
                    default:
                        Console.WriteLine("Something wrong, try again");
                        break;
                }
            }
        }

        private static void ShowMessage<T>(object sender, TreeEventArgs<T> e)
        {
            Console.WriteLine("Operation: " + e.Operation);
            Console.WriteLine("Message: " + e.Message);
            Console.WriteLine("Value: " + e.Value);
        }

        private static int ReadValidNumber()
        {
            string? input;
            int value;
            do
            {
                Console.WriteLine("Write your number");
                input = Console.ReadLine();
            }
            while (!int.TryParse(input, out value));
            return value;
        }
    }
}