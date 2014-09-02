using System.Windows;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;

namespace GraphViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var graph = new Graph();
            graph.AddEdge("1", "2");
            graphViewer.Graph = graph;

            var form = new DependenciesViewerForm();
            form.Show();
        }
    }
}
