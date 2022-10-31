using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Vokabeltrainer.VocabCollections;

namespace Vokabeltrainer.Management
{
    internal static class SubjectManager
    {
        internal static Subject CurrentSubject { get; private set; }

        internal static void CreateSubject(string subjectName)
        {
            CurrentSubject = new Subject(subjectName);
            CurrentSubject.Save();
        }

        internal static void CreateSubject(string subjectName, List<Lection> lections)
        {
            CurrentSubject = new Subject(subjectName, lections);
            CurrentSubject.Save();
        }

        internal static void LoadSubject(string subjectName)
        {
            CurrentSubject = new Subject(subjectName, new List<Lection>());
            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                foreach (string path in Directory.GetFiles(AppContext.BaseDirectory + subjectName))
                {
                    using (FileStream stream = new FileStream(path, FileMode.Open))
                    {
                        CurrentSubject.Lections.Add((Lection)binaryFormatter.Deserialize(stream));
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Das Fach bzw. seine Lektionen konnten nicht korrekt geladen werden.");
            }
        }

        internal static string[] GetSubjectNames()
        {
            string[] fullNames = Directory.GetDirectories(AppContext.BaseDirectory);
            string[] names = new string[fullNames.Length];
            for(int i = 0; i < fullNames.Length; i++)
            {
                DirectoryInfo subject = new DirectoryInfo(fullNames[i]);
                names[i] = subject.Name;
            }
            return names;
        }

        internal static string[] GetLectionNames(string subjectName)
        {
            List<string> lections = new List<string>();
            foreach(string path in Directory.GetFiles(AppContext.BaseDirectory + subjectName))
            {
                lections.Add(new FileInfo(path).Name.Replace(".vocs", string.Empty));
            }
            return lections.ToArray();
        }

        internal static bool CheckIfSubjectExists(string subjectName)
        {
            return Directory.Exists(AppContext.BaseDirectory + subjectName);
        }
    }
}
