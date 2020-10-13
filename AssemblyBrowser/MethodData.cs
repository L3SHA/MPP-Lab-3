using System.Collections.Generic;

namespace AssemblyBrowser
{
    public class MethodData
    {
        public string AccessModifier { get; private set; }

        public string Name { get; private set; }

        public Dictionary<string, string> Parameters { get; private set; }

        public string ReturnValue { get; private set; }

        public MethodData(string accessModifier, string name, Dictionary<string, string> parameters, string returnValue)
        {
            AccessModifier = accessModifier;
            Name = name;
            Parameters = parameters;
            ReturnValue = returnValue;
        }

        public override string ToString()
        {
            string paramsString = "";

            foreach (string name in Parameters.Keys)
            {
                string type;
                Parameters.TryGetValue(name, out type);
                paramsString += type + " " + name + ", "; 
            }

            if (paramsString.Length != 0) 
            {
                paramsString = paramsString.Remove(paramsString.Length - 2, 2); 
            }
            else
            {
                paramsString = " ";
            }
            return ReturnValue + " " + Name + "(" + paramsString + ")";
        }
    }
}
