//-----------------------------------------------------------------------
// <copyright file="TypeInputVM.cs" company="Markus Hofer">
//     Copyright (c) Markus Hofer
// </copyright>
// <summary>This view model holds all information for the input of a type.</summary>
//-----------------------------------------------------------------------
namespace Aufgabe5_Markus_Hofer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// This view model holds all information for the input of a type.
    /// </summary>
    public class TypeInputVM : INotifyPropertyChanged
    {
        /// <summary> The current input of the user. </summary>
        private object input;

        /// <summary> Indicates whether the current input is valid or not. </summary>
        private bool invalid;

        /// <summary> Tells if there is a local variable with the same name as the input. </summary>
        private bool hasLocalVariable;

        /// <summary> Tells if the user wants to use the local variable. </summary>
        private bool usesLocalVariable;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeInputVM"/> class.
        /// </summary>
        /// <param name="name">The name, which describes the input.</param>
        /// <param name="type">The type of the input.</param>
        public TypeInputVM(string name, Type type)
        {
            this.Name = name;
            this.Type = type;

            this.UsesLocalVariable = true;

            /*if (this.IsEnum)
            {
                if (this.EnumValues.Length > 0)
                {
                    this.Input = this.EnumValues.GetValue(0);
                }
            }*/
        }

        /// <summary>
        /// Gets called when a property has been changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the type of the input.
        /// </summary>
        /// <value>The type of the input.</value>
        public Type Type { get; private set; }

        /// <summary>
        /// Gets the name which describes the input.
        /// </summary>
        /// <value>The name which describes the input.</value>
        public string Name { get; private set; }
        
        /// <summary>
        /// Gets or sets the content of the input.
        /// </summary>
        /// <value>The content of the input.</value>
        public object Input
        {
            get
            {
                return this.input;
            }

            set
            {
                this.input = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the current input is valid or not.
        /// </summary>
        /// <value>A value indicating whether the current input is valid or not.</value>
        public bool Invalid
        {
            get
            {
                return this.invalid;
            }

            set
            {
                this.invalid = value;
                this.Notify();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether there is a local variable with the same name as the input or not.
        /// </summary>
        /// <value>A value indicating whether there is a local variable with the same name as the input or not.</value>
        public bool HasLocalVariable
        {
            get
            {
                return this.hasLocalVariable;
            }

            set
            {
                this.hasLocalVariable = value;
                this.Notify();

                // Also needs to be updated.
                this.UsesLocalVariable = this.usesLocalVariable;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the user wants to use the local variable or not.
        /// </summary>
        /// <value>A value indicating whether the user wants to use the local variable or not.</value>
        public bool UsesLocalVariable
        {
            get
            {
                return this.hasLocalVariable && this.usesLocalVariable;
            }

            set
            {
                this.usesLocalVariable = value;
                this.Notify();
            }
        }

        /// <summary>
        /// Notifies about changed properties.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        private void Notify([CallerMemberName]string propertyName = null)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
