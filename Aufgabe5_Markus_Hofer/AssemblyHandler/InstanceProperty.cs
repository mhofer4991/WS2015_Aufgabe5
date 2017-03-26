//-----------------------------------------------------------------------
// <copyright file="InstanceProperty.cs" company="Markus Hofer">
//     Copyright (c) Markus Hofer
// </copyright>
// <summary>This class represents a property of a created instance.</summary>
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
    /// This class represents a property of a created instance.
    /// </summary>
    public class InstanceProperty
    {
        /// <summary> The instance, which holds the represented property. </summary>
        private object instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="InstanceProperty"/> class.
        /// </summary>
        /// <param name="info">The represented property.</param>
        /// <param name="instance">The instance, which holds the property.</param>
        public InstanceProperty(PropertyInfo info, object instance)
        {
            this.instance = instance;
            this.Info = info;
        }

        /// <summary>
        /// Gets the represented property.
        /// </summary>
        /// <value>The represented property.</value>
        public PropertyInfo Info { get; private set; }

        /// <summary>
        /// Gets or sets the value of the property.
        /// </summary>
        /// <value>The value of the property.</value>
        public object Value
        {
            get
            {
                try
                {
                    return this.Info.GetValue(this.instance);
                }
                catch (Exception)
                {
                    return null;
                }
            }

            set
            {
                if (this.Info.CanWrite)
                {
                    this.Info.SetValue(this.instance, value);
                }
            }
        }
    }
}
