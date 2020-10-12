using System.Collections.Generic;

namespace AssemblyBrowser
{
    public class NameSpaceData
    {
        public List<TypeData> TypesList { get; private set; }

        public NameSpaceData()
        {
            TypesList = new List<TypeData>();
        }
    }
}
