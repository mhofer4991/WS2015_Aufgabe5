//-----------------------------------------------------------------------
// <copyright file="MethodVM.cs" company="Markus Hofer">
//     Copyright (c) Markus Hofer
// </copyright>
// <summary>This class represents a view model, which provides all needed information about a method.</summary>
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
    /// This class represents a view model, which provides all needed information about a method.
    /// </summary>
    public class MethodVM : ParameterizedVM
    {
        /// <summary> The method, which is represented by this view model. </summary>
        private MethodInfo info;

        /// <summary> The name of the method. </summary>
        private string name;

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodVM"/> class.
        /// </summary>
        /// <param name="info">The method, which is represented by this view model.</param>
        public MethodVM(MethodInfo info) : base(info.GetParameters())
        {
            this.info = info;

            this.name = info.Name + "()";
        }

        /// <summary>
        /// Gets the represented method.
        /// </summary>
        /// <value>The represented method.</value>
        public MethodInfo Info
        {
            get
            {
                return this.info;
            }
        }

        /// <summary>
        /// Gets the name of the method.
        /// </summary>
        /// <value>The name of the method.</value>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Gets the return type of the method.
        /// </summary>
        /// <value>The return type of the method.</value>
        public Type ReturnType
        {
            get
            {
                return this.Info.ReturnType;
            }
        }
    }
}
