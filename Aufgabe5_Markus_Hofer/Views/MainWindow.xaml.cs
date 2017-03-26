//-----------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="Markus Hofer">
//     Copyright (c) Markus Hofer
// </copyright>
// <summary>The user can use this program to load assemblies, display their structure and create instances of instantiable classes.</summary>
//-----------------------------------------------------------------------
namespace Aufgabe5_Markus_Hofer
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using Microsoft.Win32;

    /// <summary>
    /// The user can use this program to load assemblies, display their structure and create instances of classes.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary> The tree of objects, which is represented as a list. </summary>
        private List<AssemblyType> objectsTree;

        /// <summary> The object view model, which represents the objects tree. </summary>
        private ObservableCollection<TypeVM> objectsList;

        /// <summary> A list of instances, which have been created by the user. </summary>
        private ObservableCollection<CreatedInstanceVM> createdInstancesList;

        /// <summary> The assembly handler, which handles the assemblies. </summary>
        private AssemblyHandler assemblyHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();

            this.objectsTree = new List<AssemblyType>();
            this.objectsList = new ObservableCollection<TypeVM>();
            this.createdInstancesList = new ObservableCollection<CreatedInstanceVM>();

            this.assemblyHandler = new AssemblyHandler();

            this.assemblyHandler.OnAssemblyLoadFailed += this.AssemblyHandler_OnAssemblyLoadFailed;
            this.assemblyHandler.OnInstanceCreationFailed += this.AssemblyHandler_OnInstanceCreationFailed;
            this.assemblyHandler.OnMethodInvokingFailed += this.AssemblyHandler_OnMethodInvokingFailed;
            
            this.ObjectsTreeView.ItemsSource = this.objectsList;
            this.ListOfCreatedInstances.ItemsSource = this.createdInstancesList;
        }

        /// <summary>
        /// Gets called when the invoking of a method failed.
        /// </summary>
        /// <param name="e">The exception, which caused the method to fail.</param>
        private void AssemblyHandler_OnMethodInvokingFailed(Exception e)
        {
            MessageBox.Show("The method could not be invoked!");
        }

        /// <summary>
        /// Gets called when the instantiation of an object failed.
        /// </summary>
        /// <param name="e">The exception, which caused the instantiation of an object to fail.</param>
        private void AssemblyHandler_OnInstanceCreationFailed(Exception e)
        {
            MessageBox.Show("The object could not be instantiated!");
        }

        /// <summary>
        /// Gets called when the loading of an assembly failed.
        /// </summary>
        /// <param name="e">The exception, which caused the loading of the assembly to fail.</param>
        private void AssemblyHandler_OnAssemblyLoadFailed(Exception e)
        {
            if (e is BadImageFormatException)
            {
                MessageBox.Show("The file is not a valid assembly!");
            }
            else
            {
                MessageBox.Show("The assembly could not be loaded from the file!");
            }
        }

        /// <summary>
        /// Gets called when the user wants to add an assembly by selecting the specific menu item.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Event args passed by the sender.</param>
        private void OpenAssemblyMenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == true)
            {
                string st = dialog.FileName;

                this.assemblyHandler.AddAssemblyToTree(st, this.objectsTree);

                this.UpdateTreeViewModel();
            }
        }

        /// <summary>
        /// Gets called when the user wants to add an assembly by dropping it in the area of the objects tree.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Event args passed by the sender.</param>
        private void ObjectsTreeView_Drop(object sender, DragEventArgs e)
        {
            string[] filenames = (string[])e.Data.GetData(DataFormats.FileDrop, true);

            for (int i = 0; i < filenames.Length; i++)
            {
                this.assemblyHandler.AddAssemblyToTree(filenames[i], this.objectsTree);

                this.UpdateTreeViewModel();
            }
        }

        /// <summary>
        /// Updates the displayed tree by updating the list of view models.
        /// </summary>
        private void UpdateTreeViewModel()
        {
            foreach (AssemblyType type in this.objectsTree)
            {
                List<TypeVM> found = this.objectsList.Where(x => AssemblyHelper.IsSameType(type.Type, x.Type)).ToList();

                if (found.Count < 1)
                {
                    this.objectsList.Add(new TypeVM(type));
                }
                else
                {
                    found[0].Update(type);
                }
            }
        }

        /// <summary>
        /// Gets called when the user is going to click on an object node.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Event args passed by the sender.</param>
        private void ObjectsTreeView_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.HandleClickOnTreeViewOfObjects();
        }

        /// <summary>
        /// Gets called when the selected item of the tree view changed.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Event args passed by the sender.</param>
        private void ObjectsTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            this.HandleClickOnTreeViewOfObjects();
        }

        /// <summary>
        /// Handles the click on an object node from the tree view.
        /// </summary>
        private void HandleClickOnTreeViewOfObjects()
        {
            this.TypePropertiesWindow.DataContext = this.ObjectsTreeView.SelectedItem;

            this.CreatedInstancePropertiesWindow.Visibility = Visibility.Collapsed;
            this.TypePropertiesWindow.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Gets called when the user is going to click on a created instance.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Event args passed by the sender.</param>
        private void ListOfCreatedInstances_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.HandleClickOnListOfCreatedInstances();
        }

        /// <summary>
        /// Gets called when the selection of the list of created instances changed.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Event args passed by the sender.</param>
        private void ListOfCreatedInstances_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.HandleClickOnListOfCreatedInstances();
        }

        /// <summary>
        /// Handles the click on a created instance from the list.
        /// </summary>
        private void HandleClickOnListOfCreatedInstances()
        {
            if (this.ListOfCreatedInstances.SelectedItem != null)
            {
                CreatedInstanceVM selected = (CreatedInstanceVM)this.ListOfCreatedInstances.SelectedItem;

                this.CreatedInstancePropertiesWindow.DataContext = selected;

                this.TypePropertiesWindow.Visibility = Visibility.Collapsed;
                this.CreatedInstancePropertiesWindow.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Gets called when the user wants to create a new instance of the currently selected object of the tree view.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Event args passed by the sender.</param>
        private void InstantiateObjectMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ConstructorVM constructor = (ConstructorVM)((MenuItem)e.Source).Tag;

            if (constructor != null)
            {
                string[] names = constructor.Info.GetParameters().Select(x => x.Name).ToArray();
                Type[] types = constructor.Info.GetParameters().Select(x => x.ParameterType).ToArray();

                Views.TypePrompt prompt = new Views.TypePrompt(names, types) { Title = "Properties of new instance" };
                prompt.RequiresName = true;
                prompt.LocalVariables = this.createdInstancesList;

                if (prompt.ShowDialog() == true)
                {
                    object created = this.assemblyHandler.CreateInstance(constructor.Info, prompt.Input);

                    if (created != null)
                    {
                        // If an instance with the same name already exists, it has to be deleted before.
                        List<CreatedInstanceVM> found = this.createdInstancesList.Where(x => x.Name.Equals(prompt.NewName)).ToList();

                        found.All(x => this.createdInstancesList.Remove(x));

                        this.createdInstancesList.Add(new CreatedInstanceVM(new CreatedInstance(prompt.NewName, created)));
                    }
                }
            }
        }

        /// <summary>
        /// Gets called when the user wants to invoke a method of the currently selected instance.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Event args passed by the sender.</param>
        private void InstanceMethodsList_MethodCalled(object sender, RoutedEventArgs e)
        {
            MethodVM selectedMethod = (MethodVM)((MenuItem)e.Source).Tag;

            if (selectedMethod != null)
            {
                this.CallMethod(selectedMethod.Info, ((CreatedInstanceVM)this.ListOfCreatedInstances.SelectedItem).Instance);
            }
        }

        /// <summary>
        /// Gets called when the user wants to invoke a static method of the currently selected object of the tree view.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Event args passed by the sender.</param>
        private void StaticMethodsList_MethodCalled(object sender, RoutedEventArgs e)
        {
            MethodVM selectedMethod = (MethodVM)((MenuItem)e.Source).Tag;

            if (selectedMethod != null)
            {
                this.CallMethod(selectedMethod.Info, null);
            }
        }

        /// <summary>
        /// Calls the given method info by the given invoking object.
        /// </summary>
        /// <param name="info">The given method info.</param>
        /// <param name="invokingObject">The given invoking object.</param>
        private void CallMethod(MethodInfo info, object invokingObject)
        {
            // If the method returns some object or the method requires some parameters, we have to prompt the user.
            if (AssemblyHelper.MethodReturnsSomeObject(info) || info.GetParameters().Length > 0)
            {
                object[] parameters = new object[] { };
                string resultName = string.Empty;

                string[] names = info.GetParameters().Select(x => x.Name).ToArray();
                Type[] types = info.GetParameters().Select(x => x.ParameterType).ToArray();

                Views.TypePrompt prompt = new Views.TypePrompt(names, types) { Title = "Properties of called method" };
                prompt.LocalVariables = this.createdInstancesList;
                prompt.RequiresName = AssemblyHelper.MethodReturnsSomeObject(info);

                if (prompt.ShowDialog() == true)
                {
                    parameters = prompt.Input;
                    resultName = prompt.NewName;

                    object result = this.assemblyHandler.InvokeMethod(info, invokingObject, parameters);

                    if (result != null)
                    {
                        if (AssemblyHelper.MethodReturnsSomeObject(info))
                        {
                            // If an instance with the same name already exists, it has to be deleted before.
                            List<CreatedInstanceVM> found = this.createdInstancesList.Where(x => x.Name.Equals(resultName)).ToList();

                            found.All(x => this.createdInstancesList.Remove(x));

                            CreatedInstance created = new CreatedInstance(resultName, result);

                            this.createdInstancesList.Add(new CreatedInstanceVM(created));
                        }
                    }
                }
            }
            else
            {
                this.assemblyHandler.InvokeMethod(info, invokingObject, null);
            }
        }

        /// <summary>
        /// Gets called when the user wants to modify the property of the currently selected instance.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Event args passed by the sender.</param>
        private void ModifyPropertyMenuItem_Click(object sender, RoutedEventArgs e)
        {
            PropertyVM property = (PropertyVM)((MenuItem)e.Source).Tag;

            if (property != null)
            {
                string[] names = new string[] { property.Name };
                Type[] types = new Type[] { property.Type };

                Views.TypePrompt prompt = new Views.TypePrompt(names, types) { Title = "Properties of " + property.Name };
                prompt.RequiresName = false;
                prompt.LocalVariables = this.createdInstancesList;

                if (prompt.ShowDialog() == true)
                {
                    property.Value = prompt.Input[0];
                }
            }
        }

        /// <summary>
        /// Gets called when the user wants to change the name of the currently selected instance.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Event args passed by the sender.</param>
        private void ChangeInstanceNameMenuItem_Click(object sender, RoutedEventArgs e)
        {
            CreatedInstanceVM createdInstance = (CreatedInstanceVM)((MenuItem)e.Source).Tag;

            if (createdInstance != null)
            {
                Views.TypePrompt prompt = new Views.TypePrompt(new string[0], new Type[0]) { Title = "Properties of " + createdInstance.Name };
                prompt.RequiresName = true;

                if (prompt.ShowDialog() == true)
                {
                    // If an instance with the same name already exists, it has to be deleted before.
                    List<CreatedInstanceVM> found = this.createdInstancesList.Where(x => x.Name.Equals(prompt.NewName)).ToList();

                    found.All(x => this.createdInstancesList.Remove(x));

                    createdInstance.Name = prompt.NewName;
                }
            }
        }
    }
}