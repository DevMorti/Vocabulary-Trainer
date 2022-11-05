using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vokabeltrainer.Menus.SelectOption;
using Vokabeltrainer.Vocabs;
using Vokabeltrainer.Management;
using System.Diagnostics;

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
            SummarizeRequest();
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
                Debug.WriteLine($"1. {vocab.ToString()}");
                bool isRightInput = vocab.IsRightInput(input);
                Debug.WriteLine($"2. {vocab.ToString()}");
                vocab.LogInput(vocab.IsRightInput(input));
                Debug.WriteLine($"3. {vocab.ToString()}");
                SubjectManager.CurrentSubject.ChangeVocab(vocab);
                Debug.WriteLine($"4. {vocab.ToString()}");
                if (isRightInput)
                {
                    Console.Write("Richtig: ");
                }
                else
                {
                    Console.Write("Falsch: ");
                    requests.Enqueue(vocab);
                }
                Debug.WriteLine($"5. {vocab.ToString()}");
                vocab.PrintReaction(input);
                Debug.WriteLine($"6. {vocab.ToString()}");
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

        private void SummarizeRequest()
        {
            Console.WriteLine($"Super gemacht: {RequestManager.CurrentRequest.PercentRight()}% richtig");
            Console.WriteLine("------------------------------");
            Console.WriteLine();
            Console.WriteLine("Merke");
            Console.WriteLine("-----");
            foreach(Vocab vocab in RequestManager.CurrentRequest.WrongVocabs)
            {
                Console.WriteLine(vocab.ToString());
            }
            Console.ReadKey();
        }
    }
}
