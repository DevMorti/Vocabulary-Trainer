using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vokabeltrainer.Management;

namespace Vokabeltrainer.Menus
{
    internal class CreateSubjectMenu : Menu
    {
        public override void DisplayMenu()
        {
            Console.WriteLine("Fach erstellen");
            Console.WriteLine();
            Console.WriteLine("Gebe den Fachnamen ein!");
            string input = InputName();
            try
            {
                SubjectManager.CreateSubject(input);
                Console.WriteLine("Das Fach wurde erfolgreich erstellt!");
            }
            catch(Exception ex)
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
                if (input.Where(character => !char.IsLetterOrDigit(character)).Count() > 0 || SubjectManager.CheckIfSubjectExists(input))
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
