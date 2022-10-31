using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public Queue<VocabRequest> GetRequest()
        {
            var filteredRequests = from lection in Lections
                                   from vocab in lection
                                   //hier sollten Filter eingefügt werden
                                   select vocab;

            Queue<VocabRequest> request = new Queue<VocabRequest>();
            foreach (Vocab vocab in filteredRequests)
                request.Enqueue(new VocabRequest(vocab));

            return request;
        }

        public Queue<VocabRequest> GetRequest(string lectionName)
        {
            var filteredRequests = from lection in Lections
                                   where lection.Name == lectionName
                                   from vocab in lection
                                   //hier sollten Filter eingefügt werden
                                   select vocab;

            Queue<VocabRequest> request = new Queue<VocabRequest>();
            foreach (Vocab vocab in filteredRequests)
                request.Enqueue(new VocabRequest(vocab));

            return request;
        }

        public void ChangeVocab(Vocab changedVocab)
        {
            var vocabList = from lection in Lections
                            from actualVocab in lection
                            where actualVocab.Question == changedVocab.Question && actualVocab.Answer == changedVocab.Answer && actualVocab.Form == changedVocab.Form
                            select actualVocab;

            if (vocabList.Count() > 0)
            {
                vocabList.ElementAt(0).ChangeToVocab(changedVocab);
            }
            else
                Console.WriteLine("Die Vokabel konnte leider nicht gefunden werden!");
        }

        public void Save()
        {
            DirectoryInfo directory = new DirectoryInfo(AppContext.BaseDirectory + Name);
            if(!directory.Exists)
                directory.Create();
            foreach (var lection in Lections)
                lection.Save();
        }
    }
}
