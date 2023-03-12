//Copyright (c) Microsoft Corporation.  All rights reserved.

namespace WindowsAPICodePack.Shell
{
    /// <summary>Represents a link to existing FileSystem or Virtual item.</summary>
    public class ShellLink : ShellObject
    {
        /// <summary>Path for this file e.g. c:\Windows\file.txt,</summary>
        private string _internalPath;

        internal ShellLink(IShellItem2 shellItem) => nativeShellItem = shellItem;

        /// <summary>The path for this link</summary>
        public virtual string Path
        {
            get
            {
                if (_internalPath == null && NativeShellItem != null)
                {
                    _internalPath = base.ParsingName;
                }
                return _internalPath;
            }
            protected set => _internalPath = value;
        }

    }
}