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
        private readonly GViewer _gViewer = new GViewer();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            WindowsFormsHost.Child = _gViewer;
            var graph = new Graph();
            graph.AddEdge("1", "2");
            _gViewer.Graph = graph;
        }
    }
}
