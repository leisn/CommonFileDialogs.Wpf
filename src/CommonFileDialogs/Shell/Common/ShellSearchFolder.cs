//Copyright (c) Microsoft Corporation.  All rights reserved.

using MS.WindowsAPICodePack.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.WindowsAPICodePack.Shell
{
    /// <summary>Create and modify search folders.</summary>
    public class ShellSearchFolder : ShellSearchCollection
    {
        private SearchCondition searchCondition;

        private string[] searchScopePaths;

        /// <summary>
        /// Create a simple search folder. Once the appropriate parameters are set, the search folder can be enumerated to get the search results.
        /// </summary>
        /// <param name="searchCondition">Specific condition on which to perform the search (property and expected value)</param>
        /// <param name="searchScopePath">List of folders/paths to perform the search on. These locations need to be indexed by the system.</param>
        public ShellSearchFolder(SearchCondition searchCondition, params ShellContainer[] searchScopePath)
        {
            CoreHelpers.ThrowIfNotVista();

            NativeSearchFolderItemFactory = (ISearchFolderItemFactory)new SearchFolderItemFactoryCoClass();

            SearchCondition = searchCondition;

            if (searchScopePath != null && searchScopePath.Length > 0 && searchScopePath[0] != null)
            {
                SearchScopePaths = searchScopePath.Select(cont => cont.ParsingName);
            }
        }

        /// <summary>
        /// Create a simple search folder. Once the appropiate parameters are set, the search folder can be enumerated to get the search results.
        /// </summary>
        /// <param name="searchCondition">Specific condition on which to perform the search (property and expected value)</param>
        /// <param name="searchScopePath">List of folders/paths to perform the search on. These locations need to be indexed by the system.</param>
        public ShellSearchFolder(SearchCondition searchCondition, params string[] searchScopePath)
        {
            CoreHelpers.ThrowIfNotVista();

            NativeSearchFolderItemFactory = (ISearchFolderItemFactory)new SearchFolderItemFactoryCoClass();

            if (searchScopePath != null && searchScopePath.Length > 0 && searchScopePath[0] != null)
            {
                SearchScopePaths = searchScopePath;
            }

            SearchCondition = searchCondition;
        }

        /// <summary>
        /// Gets the <see cref="Microsoft.WindowsAPICodePack.Shell.SearchCondition"/> of the search. When this property is not set, the
        /// resulting search will have no filters applied.
        /// </summary>
        public SearchCondition SearchCondition
        {
            get => searchCondition;
            private set
            {
                searchCondition = value;

                NativeSearchFolderItemFactory.SetCondition(searchCondition.NativeSearchCondition);
            }
        }

        /// <summary>
        /// Gets the search scope, as specified using an array of locations to search. The search will include this location and all its
        /// subcontainers. The default is FOLDERID_Profile
        /// </summary>
        public IEnumerable<string> SearchScopePaths
        {
            get
            {
                foreach (var scopePath in searchScopePaths)
                {
                    yield return scopePath;
                }
            }
            private set
            {
                searchScopePaths = value.ToArray();
                var shellItems = new List<IShellItem>(searchScopePaths.Length);

                var shellItemGuid = new Guid(ShellIIDGuid.IShellItem);

                // Create IShellItem for all the scopes we were given
                foreach (var path in searchScopePaths)
                {
                    var hr = ShellNativeMethods.SHCreateItemFromParsingName(path, IntPtr.Zero, ref shellItemGuid, out IShellItem scopeShellItem);

                    if (CoreErrorHelper.Succeeded(hr)) { shellItems.Add(scopeShellItem); }
                }

                // Create a new IShellItemArray
                IShellItemArray scopeShellItemArray = new ShellItemArray(shellItems.ToArray());

                // Set the scope on the native ISearchFolderItemFactory
                var hResult = NativeSearchFolderItemFactory.SetScope(scopeShellItemArray);

                if (!CoreErrorHelper.Succeeded((int)hResult)) { throw new ShellException((int)hResult); }
            }
        }

        internal ISearchFolderItemFactory NativeSearchFolderItemFactory { get; set; }

        internal override IShellItem NativeShellItem
        {
            get
            {
                var guid = new Guid(ShellIIDGuid.IShellItem);

                if (NativeSearchFolderItemFactory == null) { return null; }

                var hr = NativeSearchFolderItemFactory.GetShellItem(ref guid, out var shellItem);

                if (!CoreErrorHelper.Succeeded(hr)) { throw new ShellException(hr); }

                return shellItem;
            }
        }
    }
}