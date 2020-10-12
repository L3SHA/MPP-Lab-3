using System.Collections.ObjectModel;

namespace AssemblyTreeView
{
    public class Node
    {
        public Node()
        {
            Nodes = new ObservableCollection<Node>();
        }

        public string Data { get; set; }
        public ObservableCollection<Node> Nodes { get; set; }
    }
}
