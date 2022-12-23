using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vokabeltrainer.Vocabs;

namespace Vokabeltrainer.VocabCollections
{
    internal static class VocabCollectionsExtensions
    {
        public static Queue<VocabRequest> ToRequestQueue(this IEnumerable<Vocab> vocabIEnumerable, AskingDirection askingDirection)
        {
            List<Vocab> vocabs = vocabIEnumerable.ToList();
            Queue<VocabRequest> RequestQueue = new Queue<VocabRequest>();
            Random random = new Random();
            for(int i = vocabs.Count(); i > 0; i--)
            {
                Vocab vocab = vocabs.ElementAt(random.Next(0, vocabs.Count()));
                RequestQueue.Enqueue(new VocabRequest(vocab, askingDirection));
                vocabs.Remove(vocab);
            }
            return RequestQueue;
        }
    }
}
