using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using Vokabeltrainer.Vocabs;
using System.IO;

namespace Vokabeltrainer.VocabCollections
{
    [Serializable]
    internal class Lection : List<Vocab>
    {
        public string Name { get; private set; }
        public string Subject { get; private set; }

        public Lection(string name, string subject)
        {
            Name = name;
            Subject = subject;
        }

        public void Save()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream = new FileStream(AppContext.BaseDirectory + $@"{Subject}\{Name}.vocs", FileMode.Create))
            {
                binaryFormatter.Serialize(fileStream, this);
            }
        }
    }
}
