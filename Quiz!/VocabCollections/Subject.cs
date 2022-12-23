using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Activation;
using System.Security.Cryptography;
using Vokabeltrainer.Vocabs;

namespace Vokabeltrainer.VocabCollections
{
    internal class Subject
    {
        public string Name { get; private set; }
        public List<Lection> Lections { get; private set; }
        
        public Subject(string name, List<Lection> lections)
        {
            Name = name;
            Lections = lections;
        }

        public Subject(string name)
        {
            Name = name;
            Lections = new List<Lection>();
        }

        public Queue<VocabRequest> GetRequest(AskingDirection askingDirection)
        {
            var Requests = from lection in Lections
                                   from vocab in lection
                                   orderby vocab.Level
                                   select vocab;
            int requestAmount = GetRequestAmount(Requests);
            int takeAmount = (int)Math.Floor((float)(requestAmount * 0.75F));
            List<Vocab> filtered = Requests.Take(takeAmount).ToList();
            List<Vocab> outFiltered = Requests.Skip(takeAmount).ToList();
            var levelGrouped = outFiltered.GroupBy((vocab) => vocab.Level);
            Random random = new Random();
            foreach(var levelIEnum in levelGrouped)
            {
                List<Vocab> levelGroup = levelIEnum.ToList();
                if (requestAmount - filtered.Count <= 0)
                    break;
                int add;
                if (requestAmount - filtered.Count > 1)
                    add = (requestAmount - filtered.Count) / 2;
                else
                    add = 1;

                for(int i = 0; i < add; i++)
                {
                    Vocab vocab = levelGroup[random.Next(0, levelGroup.Count())];
                    filtered.Add(vocab);
                    levelGroup.Remove(vocab);
                }
            }
            return filtered.ToRequestQueue(askingDirection);
        }

        public Queue<VocabRequest> GetRequest(string lectionName, AskingDirection askingDirection)
        {
            var filteredRequests = from lection in Lections
                                   where lection.Name == lectionName
                                   from vocab in lection
                                   //hier sollten Filter eingefügt werden
                                   select vocab;
            return filteredRequests.ToRequestQueue(askingDirection);
        }

        public void ChangeVocab(Vocab originVocab, Vocab changedVocab)
        {
            var vocabList = from lection in Lections
                            from actualVocab in lection
                            where actualVocab.Question == originVocab.Question && actualVocab.Answer == originVocab.Answer && actualVocab.Form == originVocab.Form
                            select actualVocab;

            if (vocabList.Count() > 0)
            {
                vocabList.ElementAt(0).ChangeToVocab(changedVocab);
            }
            else
                Console.WriteLine("Die Vokabel konnte leider nicht gefunden werden!");
        }

        public IEnumerable<Vocab> GetVocabs()
        {
            var vocabList = from lection in Lections
                            from actualVocab in lection
                            select actualVocab;
            return vocabList;
        }

        public Lection GetLectionOf(Vocab vocab)
        {
            var vocabList = from lection in Lections
                            from actualVocab in lection
                            where actualVocab.Question == vocab.Question && actualVocab.Answer == vocab.Answer && actualVocab.Form == vocab.Form
                            select lection;
            return vocabList.ElementAt(0);
        }

        public void ChangeVocab(Vocab changedVocab)
        {
            ChangeVocab(changedVocab, changedVocab);
        }

        public void Save()
        {
            DirectoryInfo directory = new DirectoryInfo(AppContext.BaseDirectory + Name);
            if(!directory.Exists)
                directory.Create();
            foreach (var lection in Lections)
                lection.Save();
        }

        private int GetRequestAmount(IEnumerable<Vocab> requests)
        {
            double askAmount = requests.Count() / requests.Average((vocab) => vocab.Level);
            if (requests.Count() > 100)
                askAmount = askAmount / (requests.Count() / 100);
            return (int)Math.Ceiling(askAmount);
        }
    }
}
