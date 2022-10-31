using System;

namespace Vokabeltrainer.Menus.SelectOption
{
    internal class SelectOptionMenu
    {
        private Option[] Options { get; set; }
        private int Index { get; set; }
        private string Message { get; set; }

        public SelectOptionMenu(Option[] options, string message)
        {
            Options = options;
            Message = message;
            Index = 0;
            DisplayMenu();
        }

        public SelectOptionMenu(SelectOptionTemplate template)
        {
            Options = template.Options;
            Message = template.Message;
            Index = 0;
            DisplayMenu();
        }

        public SelectOptionMenu(DynamicSelectOptionTemplate template)
        {
            Options = template.GetOptions().ToArray();
            Message = template.Message;
            Index = 0;
            DisplayMenu();
        }

        private void DisplayMenu()
        {
            ConsoleKeyInfo key;
            do
            {
                PrintMenu();
                key = Console.ReadKey();
                if(key.Key == ConsoleKey.Enter)
                {
                    Options[Index].Action();
                    break;
                }
                else if(key.Key == ConsoleKey.DownArrow)
                {
                    if(Index < Options.Length - 1)
                        Index++;
                    else
                        Index = 0;
                }
                else if(key.Key == ConsoleKey.UpArrow)
                {
                    if(Index != 0)
                        Index--;
                    else
                        Index = Options.Length - 1;
                }
            } while (key.Key != ConsoleKey.Escape);
        }

        private void PrintMenu()
        {
            Console.Clear();
            Console.WriteLine(Message);
            PrintOptions();
        }

        private void PrintOptions()
        {
            foreach(Option option in Options)
            {
                Console.WriteLine();
                if(option == Options[Index])
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("> ");
                }
                else
                {
                    Console.Write("  ");
                }
                Console.Write(option.Text);
                Console.ResetColor();
            }
        }
    }
}
