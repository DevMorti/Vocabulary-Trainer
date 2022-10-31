using System;

namespace Vokabeltrainer.Vocabs
{
    internal class VocabRequest : Vocab
    {
        public bool FirstTimeRight { get; private set; }
        public bool LastTimeRight { get; private set; }

        public VocabRequest(string question, string answer, string form, byte level) : base(question, answer, form, level)
        {
            FirstTimeRight = true;
            LastTimeRight = false;
        }
        public VocabRequest(Vocab vocab) : base(vocab.Question, vocab.Answer, vocab.Form, vocab.Level)
        {
            FirstTimeRight = true;
            LastTimeRight = false;
        }

        public bool IsRightInput(string input, AskingDirection direction)
        {
            string answer = GetAnswer(direction).FormatVocabAnswer();
            input = input.FormatVocabAnswer();

            if (answer == input)
                return true;
            return false;
        }

        public void PrintReaction(string input, AskingDirection direction)
        {
            string answer = GetAnswer(direction);
            string[] lettersBetweenBrackets = answer.LettersBetween('(', ')');
            int positionInBrackets = 0;


            for (int i = 0; i < answer.Length; i++)
            {
                char fullChar = answer[i];
                if (input.Length -1 >= i)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(fullChar);
                    continue;
                }

                char character = answer.ToLower()[i];
                char comparedChar = input.ToLower()[i];

                if (character == comparedChar && fullChar == '(')
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    positionInBrackets++;
                }

                else if (character == comparedChar)
                    Console.ForegroundColor = ConsoleColor.White;

                else if (fullChar == '(')
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write('(' + lettersBetweenBrackets[positionInBrackets] + ')');

                    answer = answer.Replace('(' + lettersBetweenBrackets[positionInBrackets] + ')', string.Empty)
                    .Replace("  ", string.Empty).Trim(new char[] { ' ' });
                    positionInBrackets++;
                    i--;
                    continue;
                }

                else
                    Console.ForegroundColor = ConsoleColor.Red;

                Console.Write(answer[i]);
                Console.ResetColor();        
            }    
        }

        public void LogInput(bool rightInput)
        {
            if (rightInput)
            {
                if (FirstTimeRight && !LastTimeRight && Level < 7)
                    Level++;
                LastTimeRight = true;
            }
            else
            {
                if(!FirstTimeRight && Level > 0)
                    Level--;
                FirstTimeRight = false;
                LastTimeRight = false;
            }
        }
    }
}
