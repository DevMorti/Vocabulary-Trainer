using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vokabeltrainer.Management;
using Vokabeltrainer.Vocabs;

namespace Quiz_.Menus.SelectOption
{
    internal class ChooseVocabOption : IOption
    {
        public string Text { get; }
        public Action Action { get; }
        private Vocab Vocab { get;}

        public ChooseVocabOption(string text, Vocab vocab)
        {
            Text = text;
            Vocab = vocab;
            Action = () =>
            {
                VocabManager.CurrentVocab = Vocab;
            };
        }
    }
}
