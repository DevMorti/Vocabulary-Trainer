using System;
using System.Collections.Generic;
using Vokabeltrainer.Management;
using Vokabeltrainer.Menus;
using Vokabeltrainer.Vocabs;

namespace Vokabeltrainer.Menus.SelectOption
{
    internal static class SelectOptionTemplates
    {
        public static readonly SelectOptionTemplate StartMenu = new SelectOptionTemplate(new Option[]
        {
            new Option("Abfrage", () => new AskVocabMenu()),
            new Option("Eingabe", () => new FeedVocabMenu()),
            new Option("Beenden", () => Environment.Exit(2)),
        }, "### Vokabeltrainer ###");

        public static readonly DynamicSelectOptionTemplate SubjectMenu = new DynamicSelectOptionTemplate(GetSubjectOptions, "Fach auswählen");

        public static readonly DynamicSelectOptionTemplate LectionMenu = new DynamicSelectOptionTemplate(GetLectionOptions, "Lektion auswählen");

        public static readonly DynamicSelectOptionTemplate SubjectMenuCreate = new DynamicSelectOptionTemplate(() =>
        {
            List<Option> options = GetSubjectOptions();
            options.Add(new Option("Fach erstellen", () => new CreateSubjectMenu()));
            return options;
        }, "Fach auswählen");

        public static readonly DynamicSelectOptionTemplate LectionMenuCreate = new DynamicSelectOptionTemplate(() =>
        {
            List<Option> options = GetLectionOptions();
            options.Add(new Option("Lektion erstellen", () => new CreateLectionMenu()));
            return options;
        }, "Lektion auswählen");

        public static readonly DynamicSelectOptionTemplate AskingDirectionMenu = new DynamicSelectOptionTemplate(() =>
        {
            List<Option> options = new List<Option>();
            options.Add(new Option($"Deutsch -> {SubjectManager.CurrentSubject.Name} + Form", () => RequestManager.CurrentRequest.AskingDirection = AskingDirection.QuestionToAnswer));
            options.Add(new Option($"{SubjectManager.CurrentSubject.Name} -> Form + Deutsch", () => RequestManager.CurrentRequest.AskingDirection = AskingDirection.AnswerToQuestion));
            return options;
        }, "Abfragerichtung auswählen");

        private static List<Option> GetLectionOptions()
        {
            List<Option> options = new List<Option>();
            foreach (string lectionName in SubjectManager.GetLectionNames(SubjectManager.CurrentSubject.Name))
            {
                options.Add(new Option(lectionName, () => LectionManager.LoadLection(lectionName, SubjectManager.CurrentSubject.Name)));
            }
            return options;
        }

        private static List<Option> GetSubjectOptions()
        {
            List<Option> options = new List<Option>();
            foreach (string subjectName in SubjectManager.GetSubjectNames())
            {
                options.Add(new Option(subjectName, () => SubjectManager.LoadSubject(subjectName)));
            }
            return options;
        }
    }
}
