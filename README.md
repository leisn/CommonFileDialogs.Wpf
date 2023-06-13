# CommonFileDialogs

CommonFileDialogs cropped from Windows API Code Pack 1.1, only for WPF.
Fork of https://github.com/emako/CommonFileDialogs.

## Usage

```c#
using WindowsAPICodePack.Dialogs;

using CommonOpenFileDialog dialog = new()
{
    IsFolderPicker = true,
};

if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
{
    _ = dialog.FileName;
}
```

## README

This is a fork of the Microsoft © Windows API Code Pack, based on a repository created by [Aybe](https://github.com/aybe/Windows-API-Code-Pack-1.1). 
Only support .Net6.0-Widnows.

## Licence

See [LICENSE](LICENSE) for the original licence (retrieved from [WebArchive](http://web.archive.org/web/20130717101016/http://archive.msdn.microsoft.com/WindowsAPICodePack/Project/License.aspx)). The library is not developed anymore by Microsoft and seems to have been left as 'free to use'. A clarification or update about the licence terms from Microsoft is welcome, however.

