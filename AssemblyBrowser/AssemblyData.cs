using System.Collections.Generic;

namespace AssemblyBrowser
{
    public class AssemblyData
    {
        public Dictionary<string, NameSpaceData> NameSpaces { get; private set; }

        public AssemblyData()
        {
            NameSpaces = new Dictionary<string, NameSpaceData>();
        }
    }
}
