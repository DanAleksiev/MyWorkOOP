using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Stealer
    {
    public class Spy
        {
        public string StealFieldInfo(string target,params string[] targetFields)
            {
            Type classType = Type.GetType(target);
            FieldInfo[] classFields = classType.GetFields
                (BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            StringBuilder sb = new StringBuilder();

            object classInstance = Activator.CreateInstance(classType, new object[] { });

            sb.AppendLine($"Class under investigation: {target}");

            foreach (FieldInfo field in classFields.Where(x => targetFields.Contains(x.Name)))
                {
                sb.AppendLine($"{field.Name} = {field.GetValue(classInstance)}");
                }
            return sb.ToString().Trim();
            }

        public string AnalyzeAccessModifiers(string target)
            {
            Type classType = Type.GetType(target);
            StringBuilder stringBuilder= new StringBuilder();

            FieldInfo[] fields = classType.GetFields(BindingFlags.Instance |BindingFlags.Static | BindingFlags.Public);
            PropertyInfo[] properties = classType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            MethodInfo[] methods = classType.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);

            foreach (var field in fields)
                {
                stringBuilder.AppendLine($"{field.Name} must be private!");
                }
            foreach (var method in properties.Where(x=>x.Name.StartsWith("get")))
                {
                stringBuilder.AppendLine($"{method.Name} have to be public!");
                }         
            
            foreach (var method in methods.Where(x=>x.Name.StartsWith("set")))
                {
                stringBuilder.AppendLine($"{method.Name} have to be private!");
                }

            return stringBuilder.ToString().Trim();
            }

        public string RevealPrivateMethods(string className)
            {
            StringBuilder sb = new StringBuilder();
            Type classType = Type.GetType(className);
            MethodInfo[] methods = classType.GetMethods(BindingFlags.Instance|BindingFlags.NonPublic);

            sb.AppendLine($"All Private Methods of Class: {className}");
            sb.AppendLine($"Base Class: {classType.BaseType.Name}");

            foreach (var method in methods)
                {
                sb.AppendLine(method.Name);
                }
            return sb.ToString().Trim();
            }

        public string CollectGettersAndSetters(string className)
            {
            StringBuilder sb = new StringBuilder();
            Type classType = Type.GetType(className);
            MethodInfo[] methods = classType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);


            foreach (var method in methods.Where(x => x.Name.StartsWith("get")))
                {
                sb.AppendLine($"{method.Name} will return {method.ReturnType}");
                }
            foreach(var method in methods.Where(y => y.Name.StartsWith("set")))
                {
                sb.AppendLine($"{method.Name} will set field of {method.ReturnType}");
                }
            return sb.ToString().Trim();
            }
        }
    }
