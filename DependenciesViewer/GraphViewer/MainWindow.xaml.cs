using System.Windows;

using Microsoft.Win32;

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
            var model = new MainWindowModel();
            DataContext = model;
            model.SetGraphAction = (graph) =>
                {
                    GraphViewer.Graph = graph;
                };
        }

        private void SelecteAssemblyButton_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog { CheckFileExists = true, CheckPathExists = true, DefaultExt = ".dll", Filter = "Assembly files|*.dll" };

            // Set filter for file extension and default file extension

            // Display OpenFileDialog by calling ShowDialog method
            var result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                AssemblyNameTextBox.Text = dlg.FileName;
            }
        }
    }
}
