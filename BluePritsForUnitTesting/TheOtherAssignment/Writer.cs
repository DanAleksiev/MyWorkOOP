using PlanetWars.IO.Contracts;
using System;

namespace PlanetWars.IO
{
    public class FileWriter : IWriter
        {
        string path = "../../../output.txt";
        public void Write(string message)
            {
            using (StreamWriter writer = new StreamWriter(path, true))
                {
                writer.Write(message);

                }
            }

        public void WriteLine(string message)
            {
            using (StreamWriter writer = new StreamWriter(path, true))
                {
                writer.WriteLine(message);

                }
            }
        }
    }
