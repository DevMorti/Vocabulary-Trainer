using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_.Menus.SelectOption
{
    internal interface IOption
    {
        string Text { get; }
        Action Action { get; }
    }
}
