using System.Reflection;

namespace System
{
    /// <summary>Contains methods to create types of objects locally or remotely, or obtain references to existing remote objects. This class cannot be inherited. </summary>
    /// <filterpriority>2</filterpriority>
    public static class Activator2
    {
        /// <summary>Creates an instance of the specified type using that type's default constructor.</summary>
        /// <returns>A reference to the newly created object.</returns>
        /// <param name="type">The type of object to create. </param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="type" /> is null. </exception>
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="type" /> is not a RuntimeType. -or-<paramref name="type" /> is an open generic type (that is, the <see cref="P:System.Type.ContainsGenericParameters" /> property returns true).</exception>
        /// <exception cref="T:System.NotSupportedException">
        /// <paramref name="type" /> cannot be a <see cref="T:System.Reflection.Emit.TypeBuilder" />.-or- Creation of <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, <see cref="T:System.Void" />, and <see cref="T:System.RuntimeArgumentHandle" /> types, or arrays of those types, is not supported.-or-The assembly that contains <paramref name="type" /> is a dynamic assembly that was created with <see cref="F:System.Reflection.Emit.AssemblyBuilderAccess.Save" />. </exception>
        /// <exception cref="T:System.Reflection.TargetInvocationException">The constructor being called throws an exception. </exception>
        /// <exception cref="T:System.MethodAccessException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MemberAccessException" />, instead.The caller does not have permission to call this constructor. </exception>
        /// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism. </exception>
        /// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">The COM type was not obtained through <see cref="Overload:System.Type.GetTypeFromProgID" /> or <see cref="Overload:System.Type.GetTypeFromCLSID" />. </exception>
        /// <exception cref="T:System.MissingMethodException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MissingMemberException" />, instead.No matching public constructor was found. </exception>
        /// <exception cref="T:System.Runtime.InteropServices.COMException">
        /// <paramref name="type" /> is a COM object but the class identifier used to obtain the type is invalid, or the identified class is not registered. </exception>
        /// <exception cref="T:System.TypeLoadException">
        /// <paramref name="type" /> is not a valid type. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
        /// </PermissionSet>
        public static object CreateInstance(Type type)
            => Activator.CreateInstance(type);

        /// <summary>Creates an instance of the specified type using the constructor that best matches the specified parameters.</summary>
        /// <returns>A reference to the newly created object.</returns>
        /// <param name="type">The type of object to create. </param>
        /// <param name="args">An array of arguments that match in number, order, and type the parameters of the constructor to invoke. If <paramref name="args" /> is an empty array or null, the constructor that takes no parameters (the default constructor) is invoked. </param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="type" /> is null. </exception>
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="type" /> is not a RuntimeType. -or-<paramref name="type" /> is an open generic type (that is, the <see cref="P:System.Type.ContainsGenericParameters" /> property returns true).</exception>
        /// <exception cref="T:System.NotSupportedException">
        /// <paramref name="type" /> cannot be a <see cref="T:System.Reflection.Emit.TypeBuilder" />.-or- Creation of <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, <see cref="T:System.Void" />, and <see cref="T:System.RuntimeArgumentHandle" /> types, or arrays of those types, is not supported. -or-The assembly that contains <paramref name="type" /> is a dynamic assembly that was created with <see cref="F:System.Reflection.Emit.AssemblyBuilderAccess.Save" />.-or-The constructor that best matches <paramref name="args" /> has varargs arguments. </exception>
        /// <exception cref="T:System.Reflection.TargetInvocationException">The constructor being called throws an exception. </exception>
        /// <exception cref="T:System.MethodAccessException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MemberAccessException" />, instead.The caller does not have permission to call this constructor. </exception>
        /// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism. </exception>
        /// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">The COM type was not obtained through <see cref="Overload:System.Type.GetTypeFromProgID" /> or <see cref="Overload:System.Type.GetTypeFromCLSID" />. </exception>
        /// <exception cref="T:System.MissingMethodException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MissingMemberException" />, instead.No matching public constructor was found. </exception>
        /// <exception cref="T:System.Runtime.InteropServices.COMException">
        /// <paramref name="type" /> is a COM object but the class identifier used to obtain the type is invalid, or the identified class is not registered. </exception>
        /// <exception cref="T:System.TypeLoadException">
        /// <paramref name="type" /> is not a valid type. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
        ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, RemotingConfiguration" />
        /// </PermissionSet>
        public static object CreateInstance(Type type, params object[] args)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            Type[] argsType;
            if (args == null || args.Length == 0)
                argsType = null;
            else
            {
                argsType = new Type[args.Length];
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i] == null)
                        throw new ArgumentException("None of args elements should be null", nameof(args));

                    argsType[i] = args[i].GetType();
                }
            }
            BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public;
            var ctor = type.GetConstructor(bindingAttr, null, argsType, null);
            return ctor.Invoke(args);
        }

        /// <summary>Creates an instance of the type designated by the specified generic type parameter, using the parameterless constructor.</summary>
        /// <returns>A reference to the newly created object.</returns>
        /// <typeparam name="T">The type to create.</typeparam>
        /// <exception cref="T:System.MissingMethodException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MissingMemberException" />, instead.The type that is specified for <paramref name="T" /> does not have a parameterless constructor. </exception>
        public static T CreateInstance<T>()
            => Activator.CreateInstance<T>();
    }
}
