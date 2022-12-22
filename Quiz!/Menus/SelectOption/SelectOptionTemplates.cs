using System;
using System.Collections.Generic;
using Vokabeltrainer.Management;
using Vokabeltrainer.Menus;
using Vokabeltrainer.Vocabs;
using Vokabeltrainer.VocabCollections;
using Quiz_.Menus.SelectOption;
using System.Globalization;

namespace Vokabeltrainer.Menus.SelectOption
{
    internal static class SelectOptionTemplates
    {
        public static readonly SelectOptionTemplate StartMenu = new SelectOptionTemplate(new IOption[]
        {
            new Option("Abfrage", () => new AskVocabMenu()),
            new Option("Eingabe", () => new FeedVocabMenu()),
            new Option("Editieren", () => new EditVocabMenu()),
            new Option("Beenden", () => Environment.Exit(2)),
        }, "### Vokabeltrainer ###");

        public static readonly DynamicSelectOptionTemplate SubjectMenu = new DynamicSelectOptionTemplate(GetSubjectOptions, "Fach auswählen");

        public static readonly DynamicSelectOptionTemplate LectionMenu = new DynamicSelectOptionTemplate(GetLectionOptions, "Lektion auswählen");

        public static readonly DynamicSelectOptionTemplate SubjectMenuCreate = new DynamicSelectOptionTemplate(() =>
        {
            List<IOption> options = GetSubjectOptions();
            options.Add(new Option("Fach erstellen", () => new CreateSubjectMenu()));
            return options;
        }, "Fach auswählen");

        public static readonly DynamicSelectOptionTemplate LectionMenuCreate = new DynamicSelectOptionTemplate(() =>
        {
            List<IOption> options = GetLectionOptions();
            options.Add(new Option("Lektion erstellen", () => new CreateLectionMenu()));
            return options;
        }, "Lektion auswählen");

        public static readonly DynamicSelectOptionTemplate AskingDirectionMenu = new DynamicSelectOptionTemplate(() =>
        {
            List<IOption> options = new List<IOption>
            {
                new Option($"Deutsch -> {SubjectManager.CurrentSubject.Name} + Form", () =>
            {
                RequestManager.CurrentRequest = new Request(AskingDirection.QuestionToAnswer);
            }),
                new Option($"{SubjectManager.CurrentSubject.Name} -> Form + Deutsch", () =>
                {
                    RequestManager.CurrentRequest = new Request(AskingDirection.AnswerToQuestion);
                })
            };
            return options;
        }, "Abfragerichtung auswählen");

        public static readonly DynamicSelectOptionTemplate VocabMenu = new DynamicSelectOptionTemplate(() =>
        {
            List<IOption> options = new List<IOption>();
            foreach (Vocab vocab in LectionManager.CurrentLection)
            {
                options.Add(new ChooseVocabOption(vocab.ToString(), vocab));
            }
            options.Add(new Option("Abbrechen", () => { new SelectOptionMenu(StartMenu); }));
            return options;
        }, "Vokabel auswählen");

        private static List<IOption> GetLectionOptions()
        {
            List<IOption> options = new List<IOption>();
            foreach (string lectionName in SubjectManager.GetLectionNames(SubjectManager.CurrentSubject.Name))
            {
                options.Add(new Option(lectionName, () => LectionManager.LoadLection(lectionName, SubjectManager.CurrentSubject.Name)));
            }
            return options;
        }

        private static List<IOption> GetSubjectOptions()
        {
            List<IOption> options = new List<IOption>();
            foreach (string subjectName in SubjectManager.GetSubjectNames())
            {
                options.Add(new Option(subjectName, () => SubjectManager.LoadSubject(subjectName)));
            }
            return options;
        }
    }
}
