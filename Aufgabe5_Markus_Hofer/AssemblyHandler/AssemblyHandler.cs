//-----------------------------------------------------------------------
// <copyright file="AssemblyHandler.cs" company="Markus Hofer">
//     Copyright (c) Markus Hofer
// </copyright>
// <summary>This class provides methods to handle an assembly and it's types.</summary>
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
    /// This class provides methods to handle an assembly and it's types.
    /// </summary>
    public class AssemblyHandler
    {
        /// <summary>
        /// Delegate for event OnAssemblyLoadFailed.
        /// </summary>
        /// <param name="e">Exception, which caused the load fail.</param>
        public delegate void AssemblyLoadFailed(Exception e);

        /// <summary>
        /// Delegate for event OnInstanceCreationFailed.
        /// </summary>
        /// <param name="e">Exception, which caused the creation fail. </param>
        public delegate void InstanceCreationFailed(Exception e);

        /// <summary>
        /// Delegate for event OnMethodInvokingFailed.
        /// </summary>
        /// <param name="e">Exception, which caused the method invoking fail. </param>
        public delegate void MethodInvokingFailed(Exception e);

        /// <summary>
        /// Gets called when an exception occurred during the loading an assembly.
        /// </summary>
        public event AssemblyLoadFailed OnAssemblyLoadFailed;

        /// <summary>
        /// Gets called when an exception occurred during the creation of a new instance.
        /// </summary>
        public event InstanceCreationFailed OnInstanceCreationFailed;

        /// <summary>
        /// Gets called when an exception occurred during the invoking of a method.
        /// </summary>
        public event MethodInvokingFailed OnMethodInvokingFailed;

        /// <summary>
        /// Adds all available types of the assembly with the given filename to the given tree.
        /// </summary>
        /// <param name="filename">The given filename, where the assembly is located.</param>
        /// <param name="tree">The given tree, which is represented as a list of types.</param>
        /// <returns>The extended tree.</returns>
        public List<AssemblyType> AddAssemblyToTree(string filename, List<AssemblyType> tree)
        {
            Assembly assembly = null;

            try
            {
                assembly = Assembly.LoadFrom(filename);
            }
            catch (Exception e)
            {
                if (this.OnAssemblyLoadFailed != null)
                {
                    this.OnAssemblyLoadFailed(e);
                }
            }

            if (assembly != null)
            {
                Type[] types = assembly.GetTypes();

                for (int i = 0; i < types.Length; i++)
                {
                    this.AddType(types[i], tree);
                }
            }

            return tree;
        }

        /// <summary>
        /// Adds the given type to the given tree.
        /// </summary>
        /// <param name="type">The given type.</param>
        /// <param name="tree">The given tree.</param>
        /// <returns>A new instance of an assembly type, which contains the added type and the children.</returns>
        public AssemblyType AddType(Type type, List<AssemblyType> tree)
        {
            List<AssemblyType> used;
            
            // If there is no base type, we add it to the current tree.
            if (type.BaseType == null)
            {
                used = tree;
            }
            else
            {
                // Otherwise, we add it to the child tree of the base type.
                used = this.AddType(type.BaseType, tree).Children;
            }

            List<AssemblyType> found = used.Where(x => x.Type.FullName.Equals(type.FullName)).ToList();

            if (found.Count < 1)
            {
                AssemblyType vm = new AssemblyType(type);

                used.Add(vm);

                return vm;
            }
            else
            {
                return found[0];
            }
        }

        /// <summary>
        /// Creates a new instance with the given constructor info and the required parameters.
        /// </summary>
        /// <param name="info">The given constructor info.</param>
        /// <param name="parameters">The given list of parameters, which are required to create the new instance.</param>
        /// <returns>The created new instance.</returns>
        public object CreateInstance(ConstructorInfo info, object[] parameters)
        {
            try
            {
                return info.Invoke(parameters);
            }
            catch (Exception e)
            {
                if (this.OnInstanceCreationFailed != null)
                {
                    this.OnInstanceCreationFailed(e);
                }
            }

            return null;
        }
        
        /// <summary>
        /// Invokes a method by using the given method info, the invoking object and a list of parameters.
        /// </summary>
        /// <param name="info">The given method info.</param>
        /// <param name="invokingObject">The object, which invokes the given method.</param>
        /// <param name="parameters">A list of parameters passed to the method.</param>
        /// <returns>An object, which represents the result of the method.</returns>
        public object InvokeMethod(MethodInfo info, object invokingObject, object[] parameters)
        {
            try
            {
                return info.Invoke(invokingObject, parameters);
            }
            catch (Exception e)
            {
                if (this.OnMethodInvokingFailed != null)
                {
                    this.OnMethodInvokingFailed(e);
                }
            }

            return null;
        }
    }
}
