using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vokabeltrainer.Menus
{
    abstract internal class Menu
    {
        public Menu()
        {
            Console.Clear();
            DisplayMenu();
        }

        public abstract void DisplayMenu();
    }
}
