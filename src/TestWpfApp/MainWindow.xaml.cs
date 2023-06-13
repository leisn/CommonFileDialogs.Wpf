using System.Windows;
using System.Windows.Controls;
using WindowsAPICodePack.Dialogs;
namespace TestWpfApp;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void OpenFolder_Click(object sender, RoutedEventArgs e)
    {
        using CommonOpenFileDialog dialog = new()
        {
            IsFolderPicker = true,
        };

        if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
        {
            string selectedFolder = dialog.FileName;
            (sender as Button)!.Content = selectedFolder;
        }
    }
    private void OpenFile_Click(object sender, RoutedEventArgs e)
    {
        using CommonOpenFileDialog dialog = new()
        {
            IsFolderPicker = false,
        };

        if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
        {
            string selectedFolder = dialog.FileName;
            (sender as Button)!.Content = selectedFolder;
        }
    }
    private void SaveFile_Click(object sender, RoutedEventArgs e)
    {
        using CommonSaveFileDialog dialog = new()
        {
           
        };

        if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
        {
            string selectedFolder = dialog.FileName;
            (sender as Button)!.Content = selectedFolder;
        }
    }
}
