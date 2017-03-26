//-----------------------------------------------------------------------
// <copyright file="CreatedInstance.cs" company="Markus Hofer">
//     Copyright (c) Markus Hofer
// </copyright>
// <summary>This class represents an instance of a type.</summary>
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
    /// This class represents an instance of a type.
    /// </summary>
    public class CreatedInstance
    {
        /// <summary> The name of the instance. </summary>
        private string name;

        /// <summary> A list of instance based methods. </summary>
        private List<MethodInfo> methods;

        /// <summary> A list of all public properties. </summary>
        private List<InstanceProperty> properties;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatedInstance"/> class.
        /// </summary>
        /// <param name="name">The name of the instance.</param>
        /// <param name="instance">The represented instance.</param>
        public CreatedInstance(string name, object instance)
        {
            this.name = name;
            this.Instance = instance;

            this.methods = AssemblyHelper.GetInstanceBasedMethods(instance.GetType());

            this.properties = new List<InstanceProperty>();

            foreach (PropertyInfo info in this.Instance.GetType().GetProperties())
            {
                this.properties.Add(new InstanceProperty(info, this.Instance));
            }
        }

        /// <summary>
        /// Gets the represented instance.
        /// </summary>
        /// <value>The represented instance.</value>
        public object Instance { get; private set; }

        /// <summary>
        /// Gets or sets the name of the instance.
        /// </summary>
        /// <value>The name of the instance.</value>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;
            }
        }

        /// <summary>
        /// Gets a list of all instance based methods.
        /// </summary>
        /// <value>A list of all instance based methods.</value>
        public List<MethodInfo> Methods
        {
            get
            {
                return this.methods;
            }
        }

        /// <summary>
        /// Gets a list of all public properties.
        /// </summary>
        /// <value>A list of all public properties.</value>
        public List<InstanceProperty> Properties
        {
            get
            {
                return this.properties;
            }
        }
    }
}
