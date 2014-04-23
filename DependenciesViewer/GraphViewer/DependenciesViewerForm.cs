using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Entities;
using Microsoft.Msagl.Drawing;

namespace GraphViewer
{
    public partial class DependenciesViewerForm : Form
    {
        public DependenciesViewerForm()
        {
            InitializeComponent();
            var graph = new Graph("graph");

            Type rootType = typeof(ClassA);
            Assembly entitiesAssembly = rootType.Assembly;
            var resolver = new DependenciesResolver.DependenciesResolver(entitiesAssembly);
            var classes = resolver.GetClassesFromRootType(rootType).ToArray();
            foreach (var classInfo in classes)
            {
                Type type = classInfo.Type;
                var rootId = GetNodeId(type);
                graph.AddNode(rootId);
                foreach (var referencedType in classInfo.ReferencedTypes)
                {
                    graph.AddEdge(rootId, GetNodeId(referencedType));
                }
            }

            gViewer.Graph = graph;
        }

        private string GetNodeId(Type type)
        {
            return type.FullName.Replace("Entities.", string.Empty);
        }
    }
}
