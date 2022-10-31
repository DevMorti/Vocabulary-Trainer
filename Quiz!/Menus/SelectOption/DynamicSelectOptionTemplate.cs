using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vokabeltrainer.Menus.SelectOption
{
    internal delegate List<Option> OptionsDelegate();
    internal class DynamicSelectOptionTemplate
    {
        public OptionsDelegate GetOptions { get; private set; }
        public string Message { get; private set; }

        public DynamicSelectOptionTemplate(OptionsDelegate getOptions, string message)
        {
            GetOptions = getOptions;
            Message = message;
        }
    }
}
