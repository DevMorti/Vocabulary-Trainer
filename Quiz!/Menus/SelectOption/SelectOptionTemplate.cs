namespace Vokabeltrainer.Menus.SelectOption
{
    internal class SelectOptionTemplate
    {
        public Option[] Options { get; private set; }
        public string Message { get; private set; }

        public SelectOptionTemplate(Option[] options, string message)
        {
            Options = options;
            Message = message;
        }
    }
}
