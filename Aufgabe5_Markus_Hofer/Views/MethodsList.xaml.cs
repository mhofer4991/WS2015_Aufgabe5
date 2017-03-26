//-----------------------------------------------------------------------
// <copyright file="MethodsList.xaml.cs" company="Markus Hofer">
//     Copyright (c) Markus Hofer
// </copyright>
// <summary>This class can be used to display a list of method with it's parameters.</summary>
//-----------------------------------------------------------------------
namespace Aufgabe5_Markus_Hofer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;

    /// <summary>
    /// This class can be used to display a list of method with it's parameters.
    /// </summary>
    public partial class MethodsList : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MethodsList"/> class.
        /// </summary>
        public MethodsList()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets called when the user wants to invoke the selected method.
        /// </summary>
        public event RoutedEventHandler MethodCalled;

        /// <summary>
        /// Gets called when the user wants to invoke the selected method.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Event args passed by the sender.</param>
        private void CallMethodMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (this.MethodCalled != null)
            {
                this.MethodCalled(this, e);
            }
        }
    }
}
