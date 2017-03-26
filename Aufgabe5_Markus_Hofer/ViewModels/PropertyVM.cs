//-----------------------------------------------------------------------
// <copyright file="PropertyVM.cs" company="Markus Hofer">
//     Copyright (c) Markus Hofer
// </copyright>
// <summary>This class represents a view model, which provides all needed information about a property.</summary>
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
    /// This class represents a view model, which provides all needed information about a property.
    /// </summary>
    public class PropertyVM : INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyVM"/> class.
        /// </summary>
        /// <param name="property">The property, which is represented by this view model.</param>
        public PropertyVM(InstanceProperty property)
        {
            this.Property = property;
        }

        /// <summary>
        /// Gets called when a property has been changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the represented property.
        /// </summary>
        /// <value>The represented property.</value>
        public InstanceProperty Property { get; private set; }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        /// <value>The name of the property.</value>
        public string Name
        {
            get
            {
                return this.Property.Info.Name;
            }
        }

        /// <summary>
        /// Gets the type of the property.
        /// </summary>
        /// <value>The type of the property.</value>
        public Type Type
        {
            get
            {
                return this.Property.Info.PropertyType;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the value of the property can be changed or not.
        /// </summary>
        /// <value>A value indicating whether the value of the property can be changed or not.</value>
        public bool CanWrite
        {
            get
            {
                return this.Property.Info.CanWrite;
            }
        }

        /// <summary>
        /// Gets or sets the value of the property.
        /// </summary>
        /// <value>The value of the property.</value>
        public object Value
        {
            get
            {
                return this.Property.Value;
            }

            set
            {
                this.Property.Value = value;
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
