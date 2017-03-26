//-----------------------------------------------------------------------
// <copyright file="ParameterizedVM.cs" company="Markus Hofer">
//     Copyright (c) Markus Hofer
// </copyright>
// <summary>View models, which inherit from this class, are able to display a list of parameters.</summary>
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
    /// View models, which inherit from this class, are able to display a list of parameters.
    /// </summary>
    public abstract class ParameterizedVM
    {
        /// <summary> The list of parameters. </summary>
        private ObservableCollection<ParameterVM> parameters;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterizedVM"/> class.
        /// </summary>
        /// <param name="info">The list of parameters, which is represented by this view model.</param>
        public ParameterizedVM(ParameterInfo[] info)
        {
            this.parameters = new ObservableCollection<ParameterVM>();

            foreach (ParameterInfo paramsInfo in info)
            {
                this.parameters.Add(new ParameterVM(paramsInfo));
            }
        }

        /// <summary>
        /// Gets the list of parameters.
        /// </summary>
        /// <value>The list of parameters.</value>
        public ObservableCollection<ParameterVM> Parameters
        {
            get
            {
                return this.parameters;
            }
        }
    }
}
