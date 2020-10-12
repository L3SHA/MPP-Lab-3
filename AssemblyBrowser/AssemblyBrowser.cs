using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Reflection;

namespace AssemblyBrowser
{
    public class AssemblyBrowser
    {
        private BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public;

        public AssemblyData assemblyData { get; private set; }

        public AssemblyBrowser(string path)
        {
            Assembly assembly = Assembly.LoadFrom(path);

            assemblyData = new AssemblyData();

            var nameSpaces = assemblyData.NameSpaces;

            SetNameSpaces(nameSpaces, assembly);
        }


        private void SetNameSpaces(Dictionary<string, NameSpaceData> nameSpaces, Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.Namespace != null)
                {
                    NameSpaceData nameSpaceData;

                    if (!nameSpaces.TryGetValue(type.Namespace, out nameSpaceData))
                    {
                        nameSpaceData = new NameSpaceData();
                        nameSpaces.Add(type.Namespace, nameSpaceData);
                    }

                    var types = nameSpaceData.TypesList;

                    SetTypes(types, type);

                }
            }
        }

        private void SetTypes(List<TypeData> types, Type type)
        {
            TypeData typeData = new TypeData(type.FullName);

            var methods = typeData.Methods;

            SetMethods(methods, type);

            var fields = typeData.Fields;

            SetFields(fields, type);

            var properties = typeData.Properties;

            SetProperties(properties, type);

            types.Add(typeData);
        }

        private void SetMethods(List<MethodData> methods, Type type)
        {
            foreach (MethodInfo methodInfo in type.GetMethods(bindingFlags))
            {
                var parameters = GetMethodParams(methodInfo);

                string accessModifier;

                if (methodInfo.IsPublic)
                {
                    accessModifier = "Public";
                }
                else
                {
                    accessModifier = "Non-public";
                }

                methods.Add(new MethodData(accessModifier, methodInfo.Name, parameters, methodInfo.ReturnType.ToString()));
            }
        }

        private Dictionary<string, string> GetMethodParams(MethodInfo methodInfo)
        {
            var parameters = new Dictionary<string, string>();

            foreach (ParameterInfo parameterInfo in methodInfo.GetParameters())
            {
                parameters.Add(parameterInfo.Name, parameterInfo.ParameterType.ToString());
            }

            return parameters;
        }

        private void SetFields(List<FieldData> fields, Type type)
        {
            foreach (FieldInfo fieldInfo in type.GetFields(bindingFlags))
            {
                if (fieldInfo.GetCustomAttribute(typeof(CompilerGeneratedAttribute)) == null)
                {
                    string accessModifier;

                    if (fieldInfo.IsPublic)
                    {
                        accessModifier = "Public";
                    }
                    else
                    {
                        accessModifier = "Non-public";
                    }

                    fields.Add(new FieldData(accessModifier, fieldInfo.Name, fieldInfo.FieldType.ToString()));
                }
            }
        }

        private void SetProperties(List<PropertyData> properties, Type type)
        {
            foreach (PropertyInfo propertyInfo in type.GetProperties(bindingFlags))
            {
                properties.Add(new PropertyData(propertyInfo.Name, propertyInfo.PropertyType.ToString()));
            }
        }

    }
}
