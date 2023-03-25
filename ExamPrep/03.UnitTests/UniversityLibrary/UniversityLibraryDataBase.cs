using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace UniversityLibrary.Test
    {
    public class UniversityLibraryDataBase
        {
        private string dbPath = "../../../catalog.txt";

        public List<TextBook> GetCatalogue()
            {
            using (StreamReader reader = new StreamReader(dbPath)) 
            return JsonConvert.DeserializeObject<List<TextBook>>(reader.ReadToEnd());
            }

        public void SaveProducts (List<TextBook> books)
            {
            using (StreamWriter writer = new StreamWriter(dbPath))
                {
                writer.WriteLine(JsonConvert.SerializeObject(books));
                }
            }
        }
    }
