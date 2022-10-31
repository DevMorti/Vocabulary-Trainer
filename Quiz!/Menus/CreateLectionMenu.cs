using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vokabeltrainer.Management;

namespace Vokabeltrainer.Menus
{
    internal class CreateLectionMenu : Menu
    {
        public override void DisplayMenu()
        {
            Console.WriteLine("Lektion erstellen");
            Console.WriteLine();
            Console.WriteLine("Gebe Namen der Lektion ein!");
            string input = InputName();
            try
            {
                LectionManager.CreateLection(input, SubjectManager.CurrentSubject.Name);
                Console.WriteLine("Die Lektion wurde erfolgreich erstellt!");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }

            Console.ReadKey();
        }
        public string InputName()
        {
            string input;
            bool isRightInput;
            do
            {
                isRightInput = true;
                Console.Write("Eingabe: ");
                input = Console.ReadLine();
                if (input.Where(character => !char.IsLetterOrDigit(character) && character != ' ').Count() > 0 || LectionManager.CheckIfLectionExists(input, SubjectManager.CurrentSubject.Name))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("FEHLER: Ungültige Eingabe!");
                    Console.ResetColor();
                    isRightInput = false;
                }
            } while (!isRightInput);
            return input;
        }
    }
}
