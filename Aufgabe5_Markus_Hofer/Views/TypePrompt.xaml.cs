//-----------------------------------------------------------------------
// <copyright file="TypePrompt.xaml.cs" company="Markus Hofer">
//     Copyright (c) Markus Hofer
// </copyright>
// <summary>This class can be used to display a prompt where the user has to enter valid values for specific types.</summary>
//-----------------------------------------------------------------------
namespace Aufgabe5_Markus_Hofer.Views
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
    using System.Windows.Shapes;

    /// <summary>
    /// This class can be used to display a prompt where the user has to enter valid values for specific types.
    /// </summary>
    public partial class TypePrompt : Window
    {
        /// <summary> A list of input view models, which represent the prompt. </summary>
        private ObservableCollection<TypeInputVM> inputFields;

        /// <summary> A list of local variables, which can be entered instead of a static value. </summary>
        private ObservableCollection<CreatedInstanceVM> localVariables;

        /// <summary> A list of input objects, which represents the result of the closed window. </summary>
        private object[] input;

        /// <summary> Indicates whether the user should be prompted to enter a name. </summary>
        private bool requiresName;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypePrompt"/> class.
        /// </summary>
        /// <param name="listOfNames">A list of names, which describe each input field.</param>
        /// <param name="listOfTypes">A list of types, which are expected for each input field.</param>
        public TypePrompt(string[] listOfNames, Type[] listOfTypes)
        {
            this.InitializeComponent();

            this.inputFields = new ObservableCollection<TypeInputVM>();
            
            if (listOfNames.Length != listOfTypes.Length)
            {
                throw new ArgumentException("The amount of names and types have to be the same!");
            }

            for (int i = 0; i < listOfNames.Length; i++)
            {
                this.inputFields.Add(new TypeInputVM(listOfNames[i], listOfTypes[i]));
            }

            this.ListOfParameterEntries.ItemsSource = this.inputFields;
            this.NameTextBox.DataContext = this;
        }

        /// <summary>
        /// Gets the name that has been entered by the user.
        /// </summary>
        /// <value>The name that has been entered by the user.</value>
        public string NewName { get; private set; }

        /// <summary>
        /// Gets a list of objects that have been entered by the user.
        /// </summary>
        /// <value>A list of objects that have been entered by the user.</value>
        public object[] Input
        {
            get
            {
                return this.input;
            }
        }

        /// <summary>
        /// Gets or sets a list of local variables, which can be used instead of static values.
        /// </summary>
        /// <value>A list of local variables, which can be used instead of static values.</value>
        public ObservableCollection<CreatedInstanceVM> LocalVariables
        {
            get
            {
                return this.localVariables;
            }

            set
            {
                this.localVariables = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the user should be prompted to enter a name.
        /// </summary>
        /// <value>A value indicating whether the user should be prompted to enter a name.</value>
        public bool RequiresName
        {
            get
            {
                return this.requiresName;
            }

            set
            {
                this.requiresName = value;
                this.NameTextBox.IsEnabled = this.requiresName;
            }
        }

        /// <summary>
        /// Gets called when the user clicks the OK button of the window.
        /// If all input fields can be converted to the expected type, the window will be closed.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Event args passed by the sender.</param>
        private void InstantiateButton_Click(object sender, RoutedEventArgs e)
        {
            bool invalid = false;

            this.input = new object[this.inputFields.Count];

            // Did the user must enter a name?
            if (this.requiresName)
            {
                this.NewName = this.NameTextBox.Text;

                if (string.IsNullOrWhiteSpace(this.NewName))
                {
                    MessageBox.Show("The name cannot be empty!");
                    return;
                }
            }
            
            // Try to convert the value of each input field to the expected type.
            for (int i = 0; i < this.inputFields.Count; i++)
            {
                object inputValue = this.inputFields[i].Input;

                // Look for the local variable, if the user wants to use it instead of a constant value.
                if (this.inputFields[i].UsesLocalVariable)
                {
                    List<CreatedInstanceVM> found = this.localVariables.Where(x => x.Name.Equals(inputValue.ToString())).ToList();

                    if (found.Count > 0)
                    {
                        inputValue = found[0].Instance;
                    }
                }
                
                this.inputFields[i].Invalid = !AssemblyHelper.TryConvertToType(inputValue, this.inputFields[i].Type, out this.input[i]);
                
                if (this.inputFields[i].Invalid)
                {
                    invalid = true;
                }
            }

            if (!invalid)
            {
                this.DialogResult = true;
            }
        }

        /// <summary>
        /// Gets called when the value of an input field has been changed.
        /// It checks if the current content of the input field is also the name of a local variable.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Event args passed by the sender.</param>
        private void ParameterInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox source = (TextBox)e.Source;
            TypeInputVM vm = (TypeInputVM)source.Tag;

            if (this.localVariables.Any(x => x.Name.Equals(source.Text)))
            {
                if (!vm.HasLocalVariable)
                {
                    vm.HasLocalVariable = true;
                }
            }
            else
            {
                vm.HasLocalVariable = false;
            }
        }
    }
}
