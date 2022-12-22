using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vokabeltrainer.Management;
using Vokabeltrainer.Vocabs;

namespace Vokabeltrainer.VocabCollections
{
    internal class Request
    {
        internal AskingDirection AskingDirection { get; private set; }
        internal List<Vocab> WrongVocabs { get; set; }
        internal Queue<VocabRequest> Requests { get; private set; }
        internal int CountedRequests { get; private set; }

        public Request(AskingDirection askingDirection)
        {
            AskingDirection = askingDirection;
            WrongVocabs = new List<Vocab>();
            Requests = SubjectManager.CurrentSubject.GetRequest();
            CountedRequests = Requests.Count;
        }

        public Request(AskingDirection askingDirection, string lectionName)
        {
            AskingDirection= askingDirection;
            WrongVocabs = new List<Vocab>();
            Requests = SubjectManager.CurrentSubject.GetRequest(lectionName);
            CountedRequests = Requests.Count;
        }

        public float PercentWrong()
        {
            float result = WrongVocabs.Count();
            result = (result / CountedRequests) * 100;
            return result;
        }

        public float PercentRight()
        {
            float result = 100 - PercentWrong();
            return result;
        }
    }
}
