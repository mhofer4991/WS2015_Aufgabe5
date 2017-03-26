//-----------------------------------------------------------------------
// <copyright file="TypeVM.cs" company="Markus Hofer">
//     Copyright (c) Markus Hofer
// </copyright>
// <summary>This class represents a view model, which provides all needed information about a type.</summary>
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

    /// <summary>
    /// This class represents a view model, which provides all needed information about a type.
    /// </summary>
    public class TypeVM
    {
        /// <summary> The type, which is represented by this view model. </summary>
        private AssemblyType assemblyType;

        /// <summary> The child types, which inherit from this type. </summary>
        private ObservableCollection<TypeVM> children;

        /// <summary> Static methods which are provided by this type. </summary>
        private ObservableCollection<MethodVM> staticMethods;

        /// <summary> Constructors, which can be used to create an instance of this type. </summary>
        private ObservableCollection<ConstructorVM> constructors;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeVM"/> class.
        /// </summary>
        /// <param name="type">The type, which is represented by this view model.</param>
        public TypeVM(AssemblyType type)
        {
            this.children = new ObservableCollection<TypeVM>();
            this.staticMethods = new ObservableCollection<MethodVM>();
            this.constructors = new ObservableCollection<ConstructorVM>();

            this.Update(type);
        }

        /// <summary>
        /// Gets the represented type.
        /// </summary>
        /// <value>The represented type.</value>
        public Type Type
        {
            get
            {
                return this.assemblyType.Type;
            }
        }

        /// <summary>
        /// Gets the full name of the type.
        /// </summary>
        /// <value>The full name of the type.</value>
        public string FullName
        {
            get
            {
                return this.assemblyType.Type.FullName;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the type can be instantiated or not.
        /// </summary>
        /// <value>A value indicating whether the type can be instantiated or not.</value>
        public bool CanInstantiate
        {
            get
            {
                return this.assemblyType.CanInstantiate;
            }
        }

        /// <summary>
        /// Gets a list of types, which inherit from this type.
        /// </summary>
        /// <value>A list of types, which inherit from this type.</value>
        public ObservableCollection<TypeVM> Children
        {
            get
            {
                return this.children;
            }
        }

        /// <summary>
        /// Gets a list of static methods  provided by this type.
        /// </summary>
        /// <value>A list of static methods provided by this type.</value>
        public ObservableCollection<MethodVM> StaticMethods
        {
            get
            {
                return this.staticMethods;
            }
        }

        /// <summary>
        /// Gets a list of constructors from this type.
        /// </summary>
        /// <value>A list of constructors from this type.</value>
        public ObservableCollection<ConstructorVM> Constructors
        {
            get
            {
                return this.constructors;
            }
        }

        /// <summary>
        /// Tells the view model that the type and it's childs have been changed.
        /// </summary>
        /// <param name="type">The type, which has been changed.</param>
        public void Update(AssemblyType type)
        {
            this.assemblyType = type;

            // Apply values.
            this.staticMethods.Clear();
            this.constructors.Clear();
            this.children.Clear();

            foreach (ConstructorInfo info in this.assemblyType.Constructors)
            {
                this.constructors.Add(new ConstructorVM(info));
            }

            foreach (AssemblyType child in this.assemblyType.Children)
            {
                this.children.Add(new TypeVM(child));
            }

            foreach (MethodInfo method in this.assemblyType.StaticMethods)
            {
                this.staticMethods.Add(new MethodVM(method));
            }
        }
    }
}
