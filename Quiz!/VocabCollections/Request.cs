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
        internal List<Vocab> WrongVocabs { get; private set; }
        internal Queue<VocabRequest> Requests { get; private set; }
        internal int CountedRequests { get; private set; }

        public Request(AskingDirection askingDirection)
        {
            AskingDirection = askingDirection;
            WrongVocabs = new List<Vocab>();
            Requests = SubjectManager.CurrentSubject.GetRequest();
            CountedRequests = Requests.Count;
        }

        public int PercentWrong()
        {
            return WrongVocabs.Count / CountedRequests * 100;
        }

        public int PercentRight()
        {
            return 100 - PercentWrong();
        }
    }
}
