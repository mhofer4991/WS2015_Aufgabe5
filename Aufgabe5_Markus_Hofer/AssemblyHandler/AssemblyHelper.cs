//-----------------------------------------------------------------------
// <copyright file="AssemblyHelper.cs" company="Markus Hofer">
//     Copyright (c) Markus Hofer
// </copyright>
// <summary>This static class provides methods which support the handling of assemblies.</summary>
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
    /// This static class provides methods which support the handling of assemblies.
    /// </summary>
    public static class AssemblyHelper
    {
        /// <summary>
        /// Checks if two types are equal.
        /// </summary>
        /// <param name="type1">The first type.</param>
        /// <param name="type2">The second type.</param>
        /// <returns>A boolean indicating whether the two types are equal or not.</returns>
        public static bool IsSameType(Type type1, Type type2)
        {
            return type1.FullName.Equals(type2.FullName);
        }

        /// <summary>
        /// Checks if it's possible to create a new instance of the given type.
        /// </summary>
        /// <param name="type">The given type.</param>
        /// <returns>A boolean indicating whether the type can be instantiated or not.</returns>
        public static bool CanBeInstantiated(Type type)
        {
            bool canBeInstantiated = true;

            if (type.IsAbstract)
            {
                canBeInstantiated = false;
            }
            else if (type.GetConstructors().Length < 1)
            {
                canBeInstantiated = false;
            }

            return canBeInstantiated;
        }

        /// <summary>
        /// Gets a list of all methods, which are provided by an instance of the given type.
        /// </summary>
        /// <param name="type">The given type.</param>
        /// <returns>A list of all methods, which are provided by an instance of the type.</returns>
        public static List<MethodInfo> GetInstanceBasedMethods(Type type)
        {
            List<MethodInfo> methods = new List<MethodInfo>();
            MethodInfo[] allMethods = type.GetMethods();

            for (int i = 0; i < allMethods.Length; i++)
            {
                if (!allMethods[i].IsStatic)
                {
                    methods.Add(allMethods[i]);
                }
            }

            return methods;
        }

        /// <summary>
        /// Gets a list of all static methods of the given type.
        /// </summary>
        /// <param name="type">The given type.</param>
        /// <returns>A list of all static methods of the type.</returns>
        public static List<MethodInfo> GetStaticMethods(Type type)
        {
            List<MethodInfo> methods = new List<MethodInfo>();
            MethodInfo[] allMethods = type.GetMethods();

            for (int i = 0; i < allMethods.Length; i++)
            {
                if (allMethods[i].IsStatic)
                {
                    methods.Add(allMethods[i]);
                }
            }

            return methods;
        }

        /// <summary>
        /// Tries to convert the given object to the given type.
        /// </summary>
        /// <param name="o">The given object, which will be converted.</param>
        /// <param name="type">The given type.</param>
        /// <param name="result">The result, which represents the converted object.</param>
        /// <returns>A boolean indicating whether the conversion was successful or not.</returns>
        public static bool TryConvertToType(object o, Type type, out object result)
        {
            try
            {                
                if (!type.IsEnum)
                {
                    result = Convert.ChangeType(o, type);
                }
                else
                {
                    result = Enum.Parse(type, o.ToString(), true);
                }

                return true;
            }
            catch (Exception)
            {
                /*if (exception is FormatException || exception is InvalidCastException || exception is ArgumentException)
                {
                }*/
            }

            result = null;

            return false;
        }

        /// <summary>
        /// Checks if the given method info returns some object.
        /// </summary>
        /// <param name="info">The given method info.</param>
        /// <returns>A boolean indicating whether the given method return some object or not.</returns>
        public static bool MethodReturnsSomeObject(MethodInfo info)
        {
            return !info.ReturnType.Equals(typeof(void));
        }
    }
}
