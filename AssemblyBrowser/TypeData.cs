using System.Collections.Generic;

namespace AssemblyBrowser
{
    public class TypeData
    {
        public string Name { get; private set; }

        public List<MethodData> Methods { get; private set; }

        public List<FieldData> Fields { get; private set; }

        public List<PropertyData> Properties { get; private set; }

        public TypeData(string name)
        {
            Name = name;
            Methods = new List<MethodData>();
            Fields = new List<FieldData>();
            Properties = new List<PropertyData>();
        }
    }
}
