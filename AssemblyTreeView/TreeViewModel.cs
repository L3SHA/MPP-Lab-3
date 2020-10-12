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
            if (openFileDialog.ShowDialog() == true)
            {
                string path = openFileDialog.FileName;
                AssemblyBrowser.AssemblyBrowser assemblyBrowser = new AssemblyBrowser.AssemblyBrowser(path);
                AssemblyData data = assemblyBrowser.assemblyData;

                Nodes = new ObservableCollection<Node>();

                foreach (string name in data.NameSpaces.Keys)
                {
                    Node nameSpaceNode = new Node();
                    nameSpaceNode.Data = name;
                    NameSpaceData nameSpaceData;
                    data.NameSpaces.TryGetValue(name, out nameSpaceData);
                    foreach (TypeData typeData in nameSpaceData.TypesList)
                    {
                        var typeNode = new Node();
                        typeNode.Data = typeData.Name;
                        var methods = typeData.Methods;
                        foreach (MethodData methodData in methods)
                        {
                            var methodNode = new Node();
                            methodNode.Data = methodData.ToString();
                            typeNode.Nodes.Add(methodNode);
                        }
                        var fields = typeData.Fields;
                        foreach (FieldData fieldData in fields)
                        {
                            var fieldNode = new Node();
                            fieldNode.Data = fieldData.ToString();
                            typeNode.Nodes.Add(fieldNode);
                        }
                        var properties = typeData.Properties;
                        foreach (PropertyData propertyData in properties)
                        {
                            var propertyNode = new Node();
                            propertyNode.Data = propertyData.ToString();
                            typeNode.Nodes.Add(propertyNode);
                        }
                        nameSpaceNode.Nodes.Add(typeNode);
                    }
                    Nodes.Add(nameSpaceNode);
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
