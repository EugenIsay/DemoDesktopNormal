using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using System;
using System.IO;
using System.Xml.Linq;

namespace DemoDecktopNormal
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            var folderPath = $"{Environment.CurrentDirectory}\\Assets";
            System.IO.DirectoryInfo folderInfo = new DirectoryInfo(folderPath);
            foreach (FileInfo file in folderInfo.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in folderInfo.GetDirectories())
            {
                dir.Delete(true);
            }
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();
                desktop.ShutdownRequested += Desktop_ShutdownRequested;
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void Desktop_ShutdownRequested(object? sender, ShutdownRequestedEventArgs e)
        {
            var folderPath = $"{Environment.CurrentDirectory}\\Assets";
            System.IO.DirectoryInfo folderInfo = new DirectoryInfo(folderPath);
            foreach (FileInfo file in folderInfo.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in folderInfo.GetDirectories())
            {
                dir.Delete(true);
            }

        }
    }
}