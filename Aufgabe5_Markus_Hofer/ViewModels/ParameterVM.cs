//-----------------------------------------------------------------------
// <copyright file="ParameterVM.cs" company="Markus Hofer">
//     Copyright (c) Markus Hofer
// </copyright>
// <summary>This class represents a view model, which provides all needed information about a parameter.</summary>
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
    /// This class represents a view model, which provides all needed information about a parameter.
    /// </summary>
    public class ParameterVM
    {
        /// <summary> The parameter, which is represented by this view model. </summary>
        private ParameterInfo info;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterVM"/> class.
        /// </summary>
        /// <param name="info">The parameter, which is represented by this view model.</param>
        public ParameterVM(ParameterInfo info)
        {
            this.info = info;
        }

        /*public ParameterInfo Info
        {
            get
            {
                return this.info;
            }
        }*/

        /// <summary>
        /// Gets the name of the parameter.
        /// </summary>
        /// <value>The name of the parameter.</value>
        public string Name
        {
            get
            {
                return this.info.Name;
            }
        }

        /// <summary>
        /// Gets the type of the parameter.
        /// </summary>
        /// <value>The type of the parameter.</value>
        public Type Type
        {
            get
            {
                return this.info.ParameterType;
            }
        }
    }
}
