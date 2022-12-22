using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vokabeltrainer.Management;
using Vokabeltrainer.Menus;
using Vokabeltrainer.Menus.SelectOption;
using Vokabeltrainer.Vocabs;
using Vokabeltrainer.VocabCollections;

namespace Vokabeltrainer.Menus
{
    internal class EditVocabMenu : Menu
    {
        public override void DisplayMenu()
        {
            if (SubjectManager.GetSubjectNames().Count() != 0)
            {
                new SelectOptionMenu(SelectOptionTemplates.SubjectMenu);
                new SelectOptionMenu(SelectOptionTemplates.LectionMenu);
                new SelectOptionMenu(SelectOptionTemplates.VocabMenu);
                Console.Clear();
                Console.WriteLine("Vokabel bearbeiten");
                Console.WriteLine();
                Console.WriteLine("Schreibweisen");
                Console.WriteLine("-------------");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("{0}&Form&Deutsch", SubjectManager.CurrentSubject.Name);
                Console.WriteLine("{0}&Deutsch", SubjectManager.CurrentSubject.Name);
                Console.ResetColor();
                Console.ResetColor();
                Console.WriteLine("Befehle");
                Console.WriteLine("-------");
                Console.ForegroundColor = ConsoleColor.Green;
                //Console.WriteLine("cancel - Eingabe beenden");
                Console.WriteLine("delete - Vokabel löschen");
                Console.ResetColor();
                Console.Write("Vokabel: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(VocabManager.CurrentVocab.ToString());
                Console.ResetColor();
                InputVocab();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Es sind noch keine Vokabeln vorhanden!");
                Console.WriteLine("Bitte gebe zunächst Vokabeln ein!");
                Console.ReadKey();
                Console.ResetColor();
            }
            new SelectOptionMenu(SelectOptionTemplates.StartMenu);
        }

        private void InputVocab()
        {
            string input;
            while (true)
            {
                Console.Write("Eingabe: ");
                input = Console.ReadLine();
                /*if(input == "delete")
                {
                    Lection lection = SubjectManager.CurrentSubject.GetLectionOf(VocabManager.CurrentVocab);
                    lection.Remove(VocabManager.CurrentVocab);
                    lection.Save();
                    break;
                }*/
                if(input == "cancel")
                {
                    break;
                }
                else if (input.IsVocabString())
                {
                    SubjectManager.CurrentSubject.ChangeVocab(VocabManager.CurrentVocab, new Vocab(input));
                    SubjectManager.CurrentSubject.Save();
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("FEHLER: Ungültige Eingabe!");
                    Console.ResetColor();
                }
            }
        }
    }
}
