//-----------------------------------------------------------------------
// <copyright file="ConstructorVM.cs" company="Markus Hofer">
//     Copyright (c) Markus Hofer
// </copyright>
// <summary>This class represents a view model, which provides all needed information about a constructor.</summary>
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
    /// This class represents a view model, which provides all needed information about a constructor.
    /// </summary>
    public class ConstructorVM : ParameterizedVM
    {
        /// <summary> The constructor, which is represented by this view model. </summary>
        private ConstructorInfo info;

        /// <summary> Description of the constructor. </summary>
        private string description;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstructorVM"/> class.
        /// </summary>
        /// <param name="info">The constructor, which is represented by this view model.</param>
        public ConstructorVM(ConstructorInfo info) : base(info.GetParameters())
        {
            this.info = info;

            // Apply values.
            this.description = "Constructor with " + this.Parameters.Count + " parameters";
        }

        /// <summary>
        /// Gets the represented constructor.
        /// </summary>
        /// <value>The represented constructor.</value>
        public ConstructorInfo Info
        {
            get
            {
                return this.info;
            }
        }

        /// <summary>
        /// Gets the description of the constructor.
        /// </summary>
        /// <value>The description of the constructor.</value>
        public string Description
        {
            get
            {
                return this.description;
            }
        }
    }
}
