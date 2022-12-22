using Quiz_.Menus.SelectOption;

namespace Vokabeltrainer.Menus.SelectOption
{
    internal class SelectOptionTemplate
    {
        public Quiz_.Menus.SelectOption.IOption[] Options { get; private set; }
        public string Message { get; private set; }

        public SelectOptionTemplate(Quiz_.Menus.SelectOption.IOption[] options, string message)
        {
            Options = options;
            Message = message;
        }
    }
}
