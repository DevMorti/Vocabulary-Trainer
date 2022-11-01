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
        internal AskingDirection AskingDirection { get; set; }
        internal List<Vocab> WrongVocabs { get; set; }
        internal Queue<VocabRequest> RequestQueue { get; set; }

        public Request(AskingDirection askingDirection)
        {
            AskingDirection = askingDirection;
            WrongVocabs = new List<Vocab>();
            RequestQueue = SubjectManager.CurrentSubject.GetRequest();
        }
    }
}
