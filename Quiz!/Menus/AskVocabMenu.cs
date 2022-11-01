using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vokabeltrainer.Menus.SelectOption;
using Vokabeltrainer.Vocabs;
using Vokabeltrainer.Management;

namespace Vokabeltrainer.Menus
{
    internal class AskVocabMenu : Menu
    {
        public override void DisplayMenu()
        {
            new SelectOptionMenu(SelectOptionTemplates.SubjectMenu);
            new SelectOptionMenu(SelectOptionTemplates.AskingDirectionMenu);
            Console.Clear();
            RequestLoop();
            SubjectManager.CurrentSubject.Save();
            new SelectOptionMenu(SelectOptionTemplates.StartMenu);
        }

        public void RequestLoop()
        {
            Queue<VocabRequest> requests = RequestManager.CurrentRequest.Requests;
            while(requests.Count > 0)
            {
                Console.Clear();
                VocabRequest vocab = requests.Dequeue();
                Console.WriteLine($"Abfrage -- {SubjectManager.CurrentSubject.Name} -- Noch {requests.Count + 1} Vokabeln");
                Console.WriteLine("---------------------------------------");
                Console.WriteLine();
                Console.Write("Wort: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(vocab.GetQuestion());
                string input = InputAnswer();
                Console.ResetColor();
                bool isRightInput = vocab.IsRightInput(input);
                vocab.LogInput(vocab.IsRightInput(input));
                SubjectManager.CurrentSubject.ChangeVocab(vocab);
                if (isRightInput)
                {
                    Console.Write("Richtig: ");
                }
                else
                {
                    Console.Write("Falsch: ");
                    requests.Enqueue(vocab);
                }
                vocab.PrintReaction(input);
                Console.WriteLine();
                Console.ReadKey();
            }
        }

        private string InputAnswer()
        {
            string answer;
            while (true)
            {
                Console.ResetColor();
                Console.Write("Antwort: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                answer = Console.ReadLine();
                if (answer.Length == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("FEHLER: Ungültige Eingabe!");
                }
                else
                    break;
            }
            return answer;
        }
    }
}
