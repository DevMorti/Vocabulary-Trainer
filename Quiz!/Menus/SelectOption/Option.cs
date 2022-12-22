using Quiz_.Menus.SelectOption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vokabeltrainer.Menus.SelectOption
{
    internal class Option : IOption
    {
        public string Text { get; }
        public Action Action { get; }

        public Option(string text, Action action)
        {
            Text = text;
            Action = action;
        }
    }
}
