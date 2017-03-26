//-----------------------------------------------------------------------
// <copyright file="AssemblyType.cs" company="Markus Hofer">
//     Copyright (c) Markus Hofer
// </copyright>
// <summary>This class represents a type from an assembly.</summary>
//-----------------------------------------------------------------------
namespace Aufgabe5_Markus_Hofer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// This class represents a type from an assembly.
    /// </summary>
    public class AssemblyType
    {
        /// <summary> A list of static methods from the represented type. </summary>
        private List<MethodInfo> staticMethods;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyType"/> class.
        /// </summary>
        /// <param name="type">The represented type.</param>
        public AssemblyType(Type type)
        {
            this.Type = type;
            this.Children = new List<AssemblyType>();

            this.CanInstantiate = AssemblyHelper.CanBeInstantiated(type);
            this.staticMethods = AssemblyHelper.GetStaticMethods(type);
        }

        /// <summary>
        /// Gets the represented type.
        /// </summary>
        /// <value>The represented type.</value>
        public Type Type { get; private set; }

        /// <summary>
        /// Gets a list of all children types, which inherit from this type.
        /// </summary>
        /// <value>A list of all children types, which inherit from this type.</value>
        public List<AssemblyType> Children { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the type can be instantiated or not.
        /// </summary>
        /// <value>A value indicating whether the type can be instantiated or not.</value>
        public bool CanInstantiate { get; private set; }

        /// <summary>
        /// Gets a list of all public constructors.
        /// </summary>
        /// <value>A list of all public constructors.</value>
        public ConstructorInfo[] Constructors
        {
            get
            {
                return this.Type.GetConstructors();
            }
        }

        /// <summary>
        /// Gets a list of all static methods by this type.
        /// </summary>
        /// <value>A list of all static methods by this type.</value>
        public List<MethodInfo> StaticMethods
        {
            get
            {
                return this.staticMethods;
            }
        }
    }
}
