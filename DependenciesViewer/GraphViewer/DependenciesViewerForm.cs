using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

using DependenciesResolver;

using Microsoft.Msagl.Drawing;

namespace GraphViewer
{
    public partial class DependenciesViewerForm : Form
    {
        private const string RootIsUndefined = "None";

        private Assembly _assembly;

        public DependenciesViewerForm()
        {
            InitializeComponent();
        }

        private string GetNodeId(Type type)
        {
            return type.FullName;
        }

        private void SelectAssemblyButton_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog { CheckFileExists = true, CheckPathExists = true, FileName = "*.dll" };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                AssemblyNameTextBox.Text = dlg.FileName;
            }
        }

        private void LoadAssemblyButton_Click(object sender, EventArgs e)
        {
            var assemblyFile = AssemblyNameTextBox.Text;
            if (string.IsNullOrWhiteSpace(assemblyFile))
            {
                MessageBox.Show("Specify assembly name first!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
           
            _assembly = Assembly.ReflectionOnlyLoadFrom(assemblyFile);
            RootTypeComboBox.Items.Add(RootIsUndefined);
            RootTypeComboBox.Items.AddRange(_assembly.GetTypes().Select(x => GetNodeId(x)).ToArray());
            RootTypeComboBox.SelectedIndex = 0;
        }

        private void ViewGraph(Assembly assembly, Type rooType)
        {
            var resolver = new DependenciesResolver.DependenciesResolver(assembly, true);
            ClassInfo[] classes = rooType == null ? resolver.GetAllClasses().ToArray() : resolver.GetClassesFromRootType(rooType).ToArray();

            var graph = new Graph("graph");

            foreach (var classInfo in classes)
            {
                Type type = classInfo.Type;
                var rootId = GetNodeId(type);
                var node = graph.AddNode(rootId);
                node.LabelText = type.Name;
                if (type == rooType)
                {
                    node.Attr.FillColor = Color.PaleGreen;
                }
                foreach (var referencedType in classInfo.ReferencedTypes)
                {
                    graph.AddEdge(rootId, GetNodeId(referencedType));
                }
            }

            gViewer.Graph = graph;
        }

        private void RootTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type rooType = null;
            string typeName = RootTypeComboBox.Text;
            if (typeName != RootIsUndefined)
            {
                rooType = _assembly.GetType(typeName);
            }

            ViewGraph(_assembly, rooType);
        }
    }
}
