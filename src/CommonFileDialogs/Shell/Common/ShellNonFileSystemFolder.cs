// Copyright (c) Microsoft Corporation. All rights reserved.

namespace WindowsAPICodePack.Shell
{
    /// <summary>Represents a Non FileSystem folder (e.g. My Computer, Control Panel)</summary>
    public class ShellNonFileSystemFolder : ShellFolder
    {
        internal ShellNonFileSystemFolder()
        {
            // Empty
        }

        internal ShellNonFileSystemFolder(IShellItem2 shellItem) => nativeShellItem = shellItem;
    }
}