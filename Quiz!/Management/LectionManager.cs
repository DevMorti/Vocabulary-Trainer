using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Vokabeltrainer.VocabCollections;

namespace Vokabeltrainer.Management
{
    internal static class LectionManager
    {
        internal static Lection CurrentLection { get; private set; }

        internal static void CreateLection(string name, string subject)
        {
            CurrentLection = new Lection(name, subject);
            CurrentLection.Save();
        }

        internal static Lection GetLection(string name, string subject)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            Lection lection = null;
            try
            {
                using (FileStream fileStream = new FileStream(AppContext.BaseDirectory + $@"{subject}\{name}.vocs", FileMode.Open))
                {
                    lection = (Lection)binaryFormatter.Deserialize(fileStream);
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            return lection;
        }

        internal static void LoadLection(string name, string subject)
        {
            CurrentLection = GetLection(name, subject);
        }

        internal static bool CheckIfLectionExists(string name, string subject)
        {
            return File.Exists(AppContext.BaseDirectory + $@"{subject}\{name}.vocs");
        }
    }
}
