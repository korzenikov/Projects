namespace GraphViewer
{
    partial class DependenciesViewerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gViewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            this.label1 = new System.Windows.Forms.Label();
            this.AssemblyNameTextBox = new System.Windows.Forms.TextBox();
            this.SelectAssemblyButton = new System.Windows.Forms.Button();
            this.LoadAssemblyButton = new System.Windows.Forms.Button();
            this.RootTypeComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // gViewer
            // 
            this.gViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gViewer.AsyncLayout = false;
            this.gViewer.AutoScroll = true;
            this.gViewer.BackwardEnabled = false;
            this.gViewer.BuildHitTree = true;
            this.gViewer.CurrentLayoutMethod = Microsoft.Msagl.GraphViewerGdi.LayoutMethod.SugiyamaScheme;
            this.gViewer.ForwardEnabled = false;
            this.gViewer.Graph = null;
            this.gViewer.LayoutAlgorithmSettingsButtonVisible = true;
            this.gViewer.LayoutEditingEnabled = true;
            this.gViewer.Location = new System.Drawing.Point(0, 90);
            this.gViewer.MouseHitDistance = 0.05D;
            this.gViewer.Name = "gViewer";
            this.gViewer.NavigationVisible = true;
            this.gViewer.NeedToCalculateLayout = true;
            this.gViewer.PanButtonPressed = false;
            this.gViewer.SaveAsImageEnabled = true;
            this.gViewer.SaveAsMsaglEnabled = true;
            this.gViewer.SaveButtonVisible = true;
            this.gViewer.SaveGraphButtonVisible = true;
            this.gViewer.SaveInVectorFormatEnabled = true;
            this.gViewer.Size = new System.Drawing.Size(1122, 527);
            this.gViewer.TabIndex = 0;
            this.gViewer.ToolBarIsVisible = true;
            this.gViewer.ZoomF = 1D;
            this.gViewer.ZoomFraction = 0.5D;
            this.gViewer.ZoomWindowThreshold = 0.05D;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Assembly:";
            // 
            // AssemblyNameTextBox
            // 
            this.AssemblyNameTextBox.Location = new System.Drawing.Point(15, 26);
            this.AssemblyNameTextBox.Name = "AssemblyNameTextBox";
            this.AssemblyNameTextBox.Size = new System.Drawing.Size(395, 20);
            this.AssemblyNameTextBox.TabIndex = 2;
            // 
            // SelectAssemblyButton
            // 
            this.SelectAssemblyButton.Location = new System.Drawing.Point(416, 23);
            this.SelectAssemblyButton.Name = "SelectAssemblyButton";
            this.SelectAssemblyButton.Size = new System.Drawing.Size(75, 23);
            this.SelectAssemblyButton.TabIndex = 3;
            this.SelectAssemblyButton.Text = "Select";
            this.SelectAssemblyButton.UseVisualStyleBackColor = true;
            this.SelectAssemblyButton.Click += new System.EventHandler(this.SelectAssemblyButton_Click);
            // 
            // LoadAssemblyButton
            // 
            this.LoadAssemblyButton.Location = new System.Drawing.Point(15, 53);
            this.LoadAssemblyButton.Name = "LoadAssemblyButton";
            this.LoadAssemblyButton.Size = new System.Drawing.Size(112, 23);
            this.LoadAssemblyButton.TabIndex = 4;
            this.LoadAssemblyButton.Text = "Load assembly";
            this.LoadAssemblyButton.UseVisualStyleBackColor = true;
            this.LoadAssemblyButton.Click += new System.EventHandler(this.LoadAssemblyButton_Click);
            // 
            // RootTypeComboBox
            // 
            this.RootTypeComboBox.FormattingEnabled = true;
            this.RootTypeComboBox.Location = new System.Drawing.Point(534, 24);
            this.RootTypeComboBox.Name = "RootTypeComboBox";
            this.RootTypeComboBox.Size = new System.Drawing.Size(250, 21);
            this.RootTypeComboBox.TabIndex = 5;
            this.RootTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.RootTypeComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(531, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Root type:";
            // 
            // DependenciesViewerForm
            // 
            this.ClientSize = new System.Drawing.Size(1122, 617);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.RootTypeComboBox);
            this.Controls.Add(this.LoadAssemblyButton);
            this.Controls.Add(this.SelectAssemblyButton);
            this.Controls.Add(this.AssemblyNameTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gViewer);
            this.Name = "DependenciesViewerForm";
            this.Text = "Dependencies Viewer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Msagl.GraphViewerGdi.GViewer gViewer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox AssemblyNameTextBox;
        private System.Windows.Forms.Button SelectAssemblyButton;
        private System.Windows.Forms.Button LoadAssemblyButton;
        private System.Windows.Forms.ComboBox RootTypeComboBox;
        private System.Windows.Forms.Label label2;
    }
}

