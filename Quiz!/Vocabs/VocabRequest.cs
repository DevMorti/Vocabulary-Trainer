using System;
using Vokabeltrainer.Management;

namespace Vokabeltrainer.Vocabs
{
    internal class VocabRequest : Vocab
    {
        public static Random Random = new Random();
        public bool FirstTimeRight { get; private set; }
        public bool LastTimeRight { get; private set; }
        private AskingDirection askingDirection;
        private AskingDirection AskingDirection 
        { 
            get 
            {
                return askingDirection;
            } 
            set 
            {
                if (value != AskingDirection.Mixed)
                    askingDirection = value;
                else
                {
                    int direction = Random.Next(0, 2);
                    askingDirection = (AskingDirection)direction;
                }
            }
        }

        public VocabRequest(string question, string answer, string form, byte level, AskingDirection askingDirection) : base(question, answer, form, level)
        {
            InitializeRequestProperties(askingDirection);
        }
        public VocabRequest(Vocab vocab, AskingDirection askingDirection) : base(vocab.Question, vocab.Answer, vocab.Form, vocab.Level)
        {
            InitializeRequestProperties(askingDirection);
        }

        public bool IsRightInput(string input, AskingDirection direction)
        {
            string answer = GetAnswer(direction).FormatVocabAnswer();
            input = input.FormatVocabAnswer();

            if (answer == input)
                return true;
            return false;
        }

        public bool IsRightInput(string input)
        {
            return IsRightInput(input, AskingDirection);
        }

        public void PrintReaction(string input, AskingDirection direction)
        {
            string answer = GetAnswer(direction);
            string[] lettersBetweenBrackets = answer.LettersBetween('(', ')');
            int positionInBrackets = 0;


            for (int i = 0; i < answer.Length; i++)
            {
                char fullChar = answer[i];
                if (input.Length <= i)
                {
                    if (fullChar == '(')
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write('(' + lettersBetweenBrackets[positionInBrackets] + ')');

                        answer = answer.Replace('(' + lettersBetweenBrackets[positionInBrackets] + ')', string.Empty)
                        .Replace("  ", string.Empty).Trim(new char[] { ' ' });
                        positionInBrackets++;
                        i--;
                        continue;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(fullChar);
                        continue;
                    }
                }

                char character = answer.ToLower()[i];
                char comparedChar = input.ToLower()[i];

                if (character == comparedChar && fullChar == '(')
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    positionInBrackets++;
                }

                else if (character == comparedChar)
                    Console.ForegroundColor = ConsoleColor.Green;

                else
                    Console.ForegroundColor = ConsoleColor.Red;

                Console.Write(answer[i]);
                Console.ResetColor();        
            }
            Console.ResetColor();
        }

        public void PrintReaction(string input)
        {
            PrintReaction(input, AskingDirection);
        }

        public void LogInput(bool rightInput)
        {
            if (rightInput)
            {
                //FirstTimeRight Standardwert: true
                if (FirstTimeRight && Level < 7)//wenn Vokabel noch nicht abgefragt wurde und richtig war
                    Level++;
                LastTimeRight = true;
            }
            else
            {
                if (FirstTimeRight)//wenn Vokabel noch nicht abgefragt wurde und falsch war
                {
                    if(Level > 0)
                        Level--;
                    RequestManager.CurrentRequest.WrongVocabs.Add(this);
                }
                FirstTimeRight = false;
                LastTimeRight = false;
            }
        }

        public string GetAnswer() 
        {
            return GetAnswer(AskingDirection);
        }

        public string GetQuestion()
        {
            return GetQuestion(AskingDirection);
        }

        private void InitializeRequestProperties(AskingDirection askingDirection)
        {
            FirstTimeRight = true;
            LastTimeRight = false;
            AskingDirection = askingDirection;
        }
    }
}
