using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Vokabeltrainer.Management;
using Vokabeltrainer.Menus.SelectOption;
using Vokabeltrainer.Vocabs;

namespace Vokabeltrainer.Menus
{
    internal class FeedVocabMenu : Menu
    {
        public override void DisplayMenu()
        {
            new SelectOptionMenu(SelectOptionTemplates.SubjectMenuCreate);
            new SelectOptionMenu(SelectOptionTemplates.LectionMenuCreate);
            StartFeeding();
            Console.WriteLine("Lektion wird gespeichert...");
            try
            {
                LectionManager.CurrentLection.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Die Lektion konnte nicht fehlerfrei gespeichert werden!");
                Console.ReadKey();
            }
            new SelectOptionMenu(SelectOptionTemplates.StartMenu);
        }

        private void StartFeeding()
        {
            Console.Clear();
            Console.WriteLine("Vokabeln eingeben");
            Console.WriteLine();
            Console.WriteLine("Schreibweisen");
            Console.WriteLine("-------------");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("{0}&Form&Deutsch", SubjectManager.CurrentSubject.Name);
            Console.WriteLine("{0}&Deutsch", SubjectManager.CurrentSubject.Name);
            Console.ResetColor();
            Console.WriteLine("Befehle");
            Console.WriteLine("-------");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("cancel - Eingabe beenden");
            Console.WriteLine("edit --rmlast - Letzte Vokabel löschen");
            Console.WriteLine("clear - Konsole neu laden");
            Console.WriteLine("show --all - Letzte Vokabeln anzeigen");
            Console.WriteLine("edit --rmall - Alle Vokabeln löschen");
            Console.ResetColor();
            Console.WriteLine();
            InputVocabs();
        }

        private void InputVocabs()
        {
            string input;
            while (true)
            {
                Console.Write("Eingabe: ");
                input = Console.ReadLine();
                if(input.ToLower() == "cancel")
                {
                    break;
                }
                else if(input.ToLower() == "edit --rmlast" && LectionManager.CurrentLection.Count != 0)
                {
                    LectionManager.CurrentLection.Remove(LectionManager.CurrentLection.Last());
                }
                else if(input.ToLower() == "edit --rmall")
                {
                    LectionManager.CurrentLection.Clear();
                }
                else if(input.ToLower() == "clear")
                {
                    StartFeeding();
                    break;
                }
                else if(input.ToLower() == "show --all")
                {
                    foreach(Vocab vocab in LectionManager.CurrentLection)
                    {
                        Console.WriteLine(vocab.ToString());
                    }
                }
                else if (input.IsVocabString())
                {
                    Vocab newVocab = new Vocab(input);
                    LectionManager.CurrentLection.Add(newVocab);
                    Console.WriteLine(newVocab.ToString());
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
