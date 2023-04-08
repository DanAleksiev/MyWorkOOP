namespace RobotService.IO
{
    using RobotService.IO.Contracts;
    using System;
    using System.IO;

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
