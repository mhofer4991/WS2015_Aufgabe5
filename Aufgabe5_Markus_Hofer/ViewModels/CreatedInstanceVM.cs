//-----------------------------------------------------------------------
// <copyright file="CreatedInstanceVM.cs" company="Markus Hofer">
//     Copyright (c) Markus Hofer
// </copyright>
// <summary>This class represents a view model, which provides all needed information about a created instance.</summary>
//-----------------------------------------------------------------------
namespace Aufgabe5_Markus_Hofer
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// This class represents a view model, which provides all needed information about a created instance.
    /// </summary>
    public class CreatedInstanceVM : INotifyPropertyChanged
    {
        /// <summary> The instance, which is represented by this view model. </summary>
        private CreatedInstance createdInstance;

        /// <summary> The list of methods available for the represented instance. </summary>
        private ObservableCollection<MethodVM> methods;

        /// <summary> The list of public properties for the represented instance. </summary>
        private ObservableCollection<PropertyVM> properties;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatedInstanceVM"/> class.
        /// </summary>
        /// <param name="createdInstance">The instance, which is represented by this view model.</param>
        public CreatedInstanceVM(CreatedInstance createdInstance)
        {
            this.createdInstance = createdInstance;

            this.methods = new ObservableCollection<MethodVM>();
            this.properties = new ObservableCollection<PropertyVM>();

            foreach (MethodInfo info in this.createdInstance.Methods)
            {
                this.methods.Add(new MethodVM(info));
            }
            
            foreach (InstanceProperty info in this.createdInstance.Properties)
            {
                this.properties.Add(new PropertyVM(info));
            }
        }

        /// <summary>
        /// Gets called when a property has been changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the instance represented by this view model.
        /// </summary>
        /// <value>The instance represented by this view model.</value>
        public object Instance
        {
            get
            {
                return this.createdInstance.Instance;
            }
        }

        /// <summary>
        /// Gets the type of the represented instance.
        /// </summary>
        /// <value>The type of the represented instance.</value>
        public Type Type
        {
            get
            {
                return this.createdInstance.Instance.GetType();
            }
        }

        /// <summary>
        /// Gets or sets the name of the represented instance.
        /// </summary>
        /// <value>The name of the represented instance.</value>
        public string Name
        {
            get
            {
                return this.createdInstance.Name;
            }

            set
            {
                this.createdInstance.Name = value;
                this.Notify();
            }
        }

        /// <summary>
        /// Gets a list of all methods available for this instance.
        /// </summary>
        /// <value>A list of all methods available for this instance.</value>
        public ObservableCollection<MethodVM> Methods
        {
            get
            {
                return this.methods;
            }
        }

        /// <summary>
        /// Gets a list of all public properties of this instance.
        /// </summary>
        /// <value>A list of all public properties of this instance.</value>
        public ObservableCollection<PropertyVM> Properties
        {
            get
            {
                return this.properties;
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
