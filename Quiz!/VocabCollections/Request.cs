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

        public Request(AskingDirection askingDirection)
        {
            AskingDirection = askingDirection;
            WrongVocabs = new List<Vocab>();
            Requests = SubjectManager.CurrentSubject.GetRequest();
        }
    }
}
