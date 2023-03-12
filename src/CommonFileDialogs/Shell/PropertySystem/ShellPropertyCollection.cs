//Copyright (c) Microsoft Corporation.  All rights reserved.


using MS.WindowsAPICodePack.Internal;
using System;
using System.Collections.Generic;

using System.Collections.ObjectModel;
using System.Runtime.InteropServices;

namespace WindowsAPICodePack.Shell.PropertySystem
{
    /// <summary>Creates a readonly collection of IProperty objects.</summary>
    public class ShellPropertyCollection : ReadOnlyCollection<IShellProperty>, IDisposable
    {
        /// <summary>Creates a new Property collection given an IShellItem2 native interface</summary>
        /// <param name="parent">Parent ShellObject</param>
        public ShellPropertyCollection(ShellObject parent)
            : base(new List<IShellProperty>())
        {
            ParentShellObject = parent;
            IPropertyStore nativePropertyStore = null;
            try
            {
                nativePropertyStore = CreateDefaultPropertyStore(ParentShellObject);
            }
            catch
            {
                if (parent != null)
                {
                    parent.Dispose();
                }
                throw;
            }
            finally
            {
                if (nativePropertyStore != null)
                {
                    Marshal.ReleaseComObject(nativePropertyStore);
                    nativePropertyStore = null;
                }
            }
        }

        /// <summary>Creates a new <c>ShellPropertyCollection</c> object with the specified file or folder path.</summary>
        /// <param name="path">The path to the file or folder.</param>
        public ShellPropertyCollection(string path) : this(ShellObjectFactory.Create(path)) { }

        /// <summary>Creates a new Property collection given an IPropertyStore object</summary>
        /// <param name="nativePropertyStore">IPropertyStore</param>
        internal ShellPropertyCollection(IPropertyStore nativePropertyStore)
            : base(new List<IShellProperty>())
        {
            NativePropertyStore = nativePropertyStore;
        }

        /// <summary>Implement the finalizer.</summary>
        ~ShellPropertyCollection()
        {
            Dispose(false);
        }

        private IPropertyStore NativePropertyStore { get; set; }
        private ShellObject ParentShellObject { get; set; }

        /// <summary>Release the native objects.</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal static IPropertyStore CreateDefaultPropertyStore(ShellObject shellObj)
        {
            var guid = new Guid(ShellIIDGuid.IPropertyStore);
            var hr = shellObj.NativeShellItem2.GetPropertyStore(
                   ShellNativeMethods.GetPropertyStoreOptions.BestEffort,
                   ref guid,
                   out var nativePropertyStore);

            // throw on failure
            if (nativePropertyStore == null || !CoreErrorHelper.Succeeded(hr))
            {
                throw new ShellException(hr);
            }

            return nativePropertyStore;
        }

        /// <summary>Release the native and managed objects</summary>
        /// <param name="disposing">Indicates that this is being called from Dispose(), rather than the finalizer.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (NativePropertyStore != null)
            {
                Marshal.ReleaseComObject(NativePropertyStore);
                NativePropertyStore = null;
            }
        }
    }
}