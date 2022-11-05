using System;
using Vokabeltrainer.Management;

namespace Vokabeltrainer.Vocabs
{
    [Serializable]
    internal class Vocab
    {
        public string Question { get; private set; }
        public string Answer { get; private set; }
        public string Form { get; private set; }
        public byte Level { get; protected set; }

        public Vocab(string question, string answer, string form, byte level)
        {
            Question = question;
            Answer = answer;
            Form = form;
            Level = level;
        }

        public Vocab(Vocab vocab)
        {
            Question = vocab.Question;
            Answer = vocab.Answer;
            Form = vocab.Form;
            Level = vocab.Level;
        }

        public Vocab(string vocabString)
        {
            if (!vocabString.IsVocabString())
                throw new InvalidOperationException();
            string[] splittetString = vocabString.Split('&');
            Answer = splittetString[0];
            Question = splittetString[splittetString.Length - 1];
            Level = 0;
            if (splittetString.Length == 3)
                Form = splittetString[1];
            else
                Form = null;
        }

        public void ChangeToVocab(Vocab changedVocab)
        {
            Question = changedVocab.Question;
            Answer = changedVocab.Answer;
            Form = changedVocab.Form;
            Level = changedVocab.Level;
        }

        public string GetAnswer(AskingDirection direction)
        {
            if (direction == AskingDirection.QuestionToAnswer && Form != null)
                return Answer + " " + Form;
            else if (direction == AskingDirection.QuestionToAnswer)
                return Answer;
            else if (Form != null)
                return Question + " " + Form;
            else
                return Question;
        }

        public string GetAnswer()
        {
            return GetAnswer(RequestManager.CurrentRequest.AskingDirection);
        }

        public string GetQuestion(AskingDirection direction)
        {
            if (direction == AskingDirection.QuestionToAnswer)
                return Question;
            else
                return Answer;
        }

        public string GetQuestion()
        {
            return GetQuestion(RequestManager.CurrentRequest.AskingDirection);
        }

        public override string ToString()
        {
            string form = null;
            if (Form != null)
                form = $" - {Form}";
            string asString = $"{Answer}{form} - {Question}";
            return asString;
        }
    }
}
