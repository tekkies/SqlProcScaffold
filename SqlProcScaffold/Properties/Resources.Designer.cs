﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SqlProcScaffold.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SqlProcScaffold.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to MIT License
        ///
        ///Copyright (c) 2019 Andy Joiner
        ///
        ///Permission is hereby granted, free of charge, to any person obtaining a copy
        ///of this software and associated documentation files (the &quot;Software&quot;), to deal
        ///in the Software without restriction, including without limitation the rights
        ///to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
        ///copies of the Software, and to permit persons to whom the Software is
        ///furnished to do so, subject to the following conditions:
        ///
        ///The above copyright no [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string LICENSE {
            get {
                return ResourceManager.GetString("LICENSE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to using System.Data;
        ///using System.Data.Common;
        ///using System.Data.SqlClient;
        ///
        ///namespace SprocWrapper.Procs
        ///{
        ///    public class Proc
        ///    {
        ///        protected SqlCommand _command;
        ///
        ///        protected void AddParameterIfNotNull(string nameWithoutAt, object value)
        ///        {
        ///            if (value != null)
        ///            {
        ///                AddParameter(nameWithoutAt, value);
        ///            }
        ///        }
        ///
        ///        protected void AddParameter(string name, object value)
        ///        {
        ///            _command.Parameters. [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string Proc {
            get {
                return ResourceManager.GetString("Proc", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to # SqlProcScaffold
        ///Generate strongly typed C# SQL scaffold 
        ///.
        /// </summary>
        internal static string README {
            get {
                return ResourceManager.GetString("README", resourceCulture);
            }
        }
    }
}