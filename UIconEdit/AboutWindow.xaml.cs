using System;
using System.Windows;

namespace UIconEdit.Maker
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    partial class AboutWindow : Window
    {
        private MainWindow _owner;

        public AboutWindow(MainWindow mainWindow)
        {
            SetValue(VersionPropertyKey, typeof(AboutWindow).Assembly.GetName().Version);

            Owner = _owner = mainWindow;
            InitializeComponent();
        }

        public SettingsFile Settings { get { return _owner.SettingsFile; } }

        #region Version
        private static readonly DependencyPropertyKey VersionPropertyKey = DependencyProperty.RegisterReadOnly(nameof(Version), typeof(Version), typeof(AboutWindow),
            new PropertyMetadata(new Version(0, 0, 0, 0)));
        public static readonly DependencyProperty VersionProperty = VersionPropertyKey.DependencyProperty;

        public Version Version { get { return (Version)GetValue(VersionProperty); } }
        #endregion
    }
}
