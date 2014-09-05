using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;

using DependenciesResolver;

using Microsoft.Msagl.Drawing;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;

namespace GraphViewer
{
    public class MainWindowModel : BindableBase
    {
        private const string RootIsUndefined = "None";

        private string _assemblyName;

        private Assembly _assembly;

        private IReadOnlyCollection<string> _availableTypes;

        private string _selectedType;

        public MainWindowModel()
        {
            LoadAssemblyCommand = new DelegateCommand(LoadAssembly, () => !string.IsNullOrWhiteSpace(AssemblyName));
        }

        #region Commands

        public DelegateCommand LoadAssemblyCommand { get; private set; }

        #endregion


        #region Properties

        public string AssemblyName
        {
            get
            {
                return _assemblyName;
            }

            set
            {
                if (SetProperty(ref _assemblyName, value))
                {
                    LoadAssemblyCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public IReadOnlyCollection<string> AvailableTypes
        {
            get
            {
                return _availableTypes;
            }

            set
            {
                SetProperty(ref _availableTypes, value);
            }
        }

        public string SelectedType
        {
            get
            {
                return _selectedType;
            }

            set
            {
                if (SetProperty(ref _selectedType, value))
                {
                    OnSelectedTypeChanged();
                }
            }
        }

        public Action<Graph> SetGraphAction { get; set; }

        #endregion

        private void LoadAssembly()
        {
            var assemblyFile = AssemblyName;
            if (string.IsNullOrWhiteSpace(assemblyFile))
            {
                MessageBox.Show("Specify assembly name first!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _assembly = Assembly.ReflectionOnlyLoadFrom(assemblyFile);


            AvailableTypes = new[] { RootIsUndefined }.Concat(_assembly.GetTypes().Select(GetNodeId)).ToArray();
            SelectedType = AvailableTypes.First();
        }

        private void OnSelectedTypeChanged()
        {
            Type rooType = null;
            string typeName = SelectedType;
            if (typeName != RootIsUndefined)
            {
                rooType = _assembly.GetType(typeName);
            }

            ViewGraph(_assembly, rooType);
        }

        private string GetNodeId(Type type)
        {
            return type.FullName;
        }

        private void ViewGraph(Assembly assembly, Type rooType)
        {
            var resolver = new DependenciesResolver.DependenciesResolver(assembly);
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

            if (SetGraphAction != null)
            {
                SetGraphAction(graph);
            }
        }
    }
}
