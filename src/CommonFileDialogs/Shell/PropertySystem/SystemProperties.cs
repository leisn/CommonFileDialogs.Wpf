//Copyright (c) Microsoft Corporation.  All rights reserved.

using WindowsAPICodePack.Shell.Resources;
using MS.WindowsAPICodePack.Internal;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace WindowsAPICodePack.Shell.PropertySystem
{


    /// <summary>
    /// Provides easy access to all the system properties (property keys and their descriptions)
    /// </summary>
    public static class SystemProperties
    {

        /// <summary>
        /// Returns the property description for a given property key.
        /// </summary>
        /// <param name="propertyKey">Property key of the property whose description is required.</param>
        /// <returns>Property Description for a given property key</returns>
        public static ShellPropertyDescription GetPropertyDescription(PropertyKey propertyKey) => ShellPropertyDescriptionsCache.Cache.GetPropertyDescription(propertyKey);


        /// <summary>
        /// Gets the property description for a given property's canonical name.
        /// </summary>
        /// <param name="canonicalName">Canonical name of the property whose description is required.</param>
        /// <returns>Property Description for a given property key</returns>
        public static ShellPropertyDescription GetPropertyDescription(string canonicalName)
        {

            var result = PropertySystemNativeMethods.PSGetPropertyKeyFromName(canonicalName, out var propKey);

            if (!CoreErrorHelper.Succeeded(result))
            {
                throw new ArgumentException(LocalizedMessages.ShellInvalidCanonicalName, Marshal.GetExceptionForHR(result));
            }
            return ShellPropertyDescriptionsCache.Cache.GetPropertyDescription(propKey);
        }
    }
}
