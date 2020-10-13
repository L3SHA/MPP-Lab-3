using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.Windows.Controls;
using AssemblyBrowser;

namespace AssemblyTreeView
{
    class TreeViewModel : INotifyPropertyChanged
    {
        private TreeView _treeView;

        private CustomCommand _openDllCommand;

        private Node _selectedNode;

        public ObservableCollection<Node> Nodes { get; set; }

        public CustomCommand OpenDllCommand
        {
            get
            {
                return _openDllCommand ??
                  (_openDllCommand = new CustomCommand(obj =>
                  {
                      SetTreeData();
                  }));
            }
        }

        private void SetTreeData()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "dll files(*.dll) |*.dll| All files(*.*) |*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string path = openFileDialog.FileName;
                AssemblyBrowser.AssemblyBrowser assemblyBrowser = new AssemblyBrowser.AssemblyBrowser(path);
                AssemblyData data = assemblyBrowser.assemblyData;

                Nodes = new ObservableCollection<Node>();

                Node node = new Node();
                node.Data = "Namespaces";

                Nodes.Add(node);

                foreach (string name in data.NameSpaces.Keys)
                {

                    Node nameSpaceNode = new Node();
                    nameSpaceNode.Data = name;
                    Node nameSpaceNodeData = new Node();
                    nameSpaceNode.Nodes.Add(nameSpaceNodeData);
                    nameSpaceNodeData.Data = "Classes";
                    NameSpaceData nameSpaceData;
                    data.NameSpaces.TryGetValue(name, out nameSpaceData);

                    foreach (TypeData typeData in nameSpaceData.TypesList)
                    {
                        var typeNode = new Node();
                        var methodNode = new Node();
                        methodNode.Data = "Methods";
                        var fieldNode = new Node();
                        fieldNode.Data = "Fields";
                        var propertyNode = new Node();
                        propertyNode.Data = "Properties";
                        typeNode.Nodes.Add(methodNode);
                        typeNode.Nodes.Add(fieldNode);
                        typeNode.Nodes.Add(propertyNode);
                        typeNode.Data = typeData.Name;
                        var methods = typeData.Methods;
                        foreach (MethodData methodData in methods)
                        {
                            var methodNodeData = new Node();
                            methodNodeData.Data = methodData.ToString();
                            typeNode.Nodes[0].Nodes.Add(methodNodeData);
                        }
                        var fields = typeData.Fields;
                        foreach (FieldData fieldData in fields)
                        {
                            var fieldNodeData = new Node();
                            fieldNodeData.Data = fieldData.ToString();
                            typeNode.Nodes[1].Nodes.Add(fieldNodeData);
                        }
                        var properties = typeData.Properties;
                        foreach (PropertyData propertyData in properties)
                        {
                            var propertyNodeData = new Node();
                            propertyNodeData.Data = propertyData.ToString();
                            typeNode.Nodes[2].Nodes.Add(propertyNodeData);
                        }
                        nameSpaceNodeData.Nodes.Add(typeNode);
                    }
                    node.Nodes.Add(nameSpaceNode);
                }
            }
            _treeView.ItemsSource = Nodes;
        }

        public TreeViewModel(TreeView treeView)
        {
            _treeView = treeView;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public Node SelectedNode
        {
            get { return _selectedNode; }
            set
            {
                _selectedNode = value;
                OnPropertyChanged("SelectedNode");
            }
        }
    }
}
