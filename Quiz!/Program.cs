using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Vokabeltrainer.Menus.SelectOption;
namespace Vokabeltrainer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            new SelectOptionMenu(SelectOptionTemplates.StartMenu);
        }
    }
}
