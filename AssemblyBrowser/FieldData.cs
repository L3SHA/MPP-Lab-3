namespace AssemblyBrowser
{
    public class FieldData
    {
        public string AccessModifier { get; private set; }

        public string Name { get; private set; }

        public string FieldType { get; private set; }

        public FieldData(string accessModifier, string name, string fieldType)
        {
            AccessModifier = accessModifier;
            Name = name;
            FieldType = fieldType;
        }

        public override string ToString()
        {
            return AccessModifier + " " + Name + " " + FieldType;
        }
    }
}
