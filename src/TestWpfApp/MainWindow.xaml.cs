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

    private void Button_Click(object sender, RoutedEventArgs e)
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
}
