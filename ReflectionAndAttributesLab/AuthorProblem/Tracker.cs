using System;
using System.Reflection;


namespace AuthorProblem
    {
    public class Tracker
        {
        public void PrintMethodsByAuthor()
            {
            var type = typeof(StartUp);
            var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);

            foreach (var method in methods)
                {
                if (method.CustomAttributes.Any(x => x.AttributeType == typeof(AuthorAttribute)))
                    {
                    var atributes = method.GetCustomAttributes(false);
                    foreach (AuthorAttribute attr in atributes)
                        {

                        Console.WriteLine("{0} is writen by {1}", method.Name, attr.Name);
                        }
                    }
                }
            }
        }
    }
