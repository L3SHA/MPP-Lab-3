namespace AssemblyBrowser
{
    public class PropertyData
    {
        public string Name { get; private set; }

        public string PropertyType { get; private set; }

        public PropertyData(string name, string propertyType)
        {
            Name = name;
            PropertyType = propertyType;
        }

        public override string ToString()
        {
            return Name + " " + PropertyType;
        }
    }
}
