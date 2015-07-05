#region BSD License
/*
Copyright © 2015, KimikoMuffin.
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this
   list of conditions and the following disclaimer. 
2. Redistributions in binary form must reproduce the above copyright notice,
   this list of conditions and the following disclaimer in the documentation
   and/or other materials provided with the distribution.
3. The names of its contributors may not be used to endorse or promote 
   products derived from this software without specific prior written 
   permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
#endregion

using System.Media;
using System.Windows;

namespace UIconEdit.Maker
{
    /// <summary>
    /// Interaction logic for QuestionWindow.xaml
    /// </summary>
    partial class QuestionWindow
    {
        public QuestionWindow(MainWindow mainWindow, string message, string caption)
        {
            InitializeComponent();
            Owner = mainWindow;
            TextMessage = message;
            Title = caption;
            var langFile = mainWindow.SettingsFile.LanguageFile;
            ButtonOKMessage = langFile.ButtonOK;
            ButtonCancelMessage = langFile.ButtonCancel;
            ButtonYesMessage = langFile.ButtonYes;
            ButtonNoMessage = langFile.ButtonNo;
        }

        #region Result
        private static readonly DependencyPropertyKey ResultPropertyKey = DependencyProperty.RegisterReadOnly("Result", typeof(MessageBoxResult), typeof(QuestionWindow),
            new PropertyMetadata(MessageBoxResult.Cancel));
        public static readonly DependencyProperty ResultProperty = ResultPropertyKey.DependencyProperty;

        public MessageBoxResult Result
        {
            get { return (MessageBoxResult)GetValue(ResultProperty); }
            private set { SetValue(ResultPropertyKey, value); }
        }
        #endregion

        #region TextMessage
        public static readonly DependencyProperty TextMessageProperty = DependencyProperty.Register("TextMessage", typeof(string), typeof(QuestionWindow));

        public string TextMessage
        {
            get { return (string)GetValue(TextMessageProperty); }
            set { SetValue(TextMessageProperty, value); }
        }
        #endregion

        #region ButtonOKEnabled
        public static readonly DependencyProperty ButtonOKEnabledProperty = DependencyProperty.Register("ButtonOKEnabled", typeof(bool), typeof(QuestionWindow),
            new PropertyMetadata(true));

        public bool ButtonOKEnabled
        {
            get { return (bool)GetValue(ButtonOKEnabledProperty); }
            set { SetValue(ButtonOKEnabledProperty, value); }
        }
        #endregion

        #region ButtonOKMessage
        public static readonly DependencyProperty ButtonOKMessageProperty = DependencyProperty.Register("ButtonOKMessage", typeof(string), typeof(QuestionWindow),
            new PropertyMetadata("_OK"), ButtonOKMessageValidate);

        private static bool ButtonOKMessageValidate(object value)
        {
            return value != null;
        }

        public string ButtonOKMessage
        {
            get { return (string)GetValue(ButtonOKMessageProperty); }
            set { SetValue(ButtonOKMessageProperty, value); }
        }
        #endregion

        #region ButtonCancelEnabled
        public static readonly DependencyProperty ButtonCancelEnabledProperty = DependencyProperty.Register("ButtonCancelEnabled", typeof(bool), typeof(QuestionWindow),
            new PropertyMetadata(true));

        public bool ButtonCancelEnabled
        {
            get { return (bool)GetValue(ButtonCancelEnabledProperty); }
            set { SetValue(ButtonCancelEnabledProperty, value); }
        }
        #endregion

        #region ButtonCancelMessage
        public static readonly DependencyProperty ButtonCancelMessageProperty = DependencyProperty.Register("ButtonCancelMessage", typeof(string), typeof(QuestionWindow),
            new PropertyMetadata("_Cancel"), ButtonCancelMessageValidate);

        private static bool ButtonCancelMessageValidate(object value)
        {
            return value != null;
        }

        public string ButtonCancelMessage
        {
            get { return (string)GetValue(ButtonCancelMessageProperty); }
            set { SetValue(ButtonCancelMessageProperty, value); }
        }
        #endregion

        #region ButtonYesEnabled
        public static readonly DependencyProperty ButtonYesEnabledProperty = DependencyProperty.Register("ButtonYesEnabled", typeof(bool), typeof(QuestionWindow),
            new PropertyMetadata(false));

        public bool ButtonYesEnabled
        {
            get { return (bool)GetValue(ButtonYesEnabledProperty); }
            set { SetValue(ButtonYesEnabledProperty, value); }
        }
        #endregion

        #region ButtonYesMessage
        public static readonly DependencyProperty ButtonYesMessageProperty = DependencyProperty.Register("ButtonYesMessage", typeof(string), typeof(QuestionWindow),
            new PropertyMetadata("_Yes"), ButtonYesMessageValidate);

        private static bool ButtonYesMessageValidate(object value)
        {
            return value != null;
        }

        public string ButtonYesMessage
        {
            get { return (string)GetValue(ButtonYesMessageProperty); }
            set { SetValue(ButtonYesMessageProperty, value); }
        }
        #endregion

        #region ButtonNoEnabled
        public static readonly DependencyProperty ButtonNoEnabledProperty = DependencyProperty.Register("ButtonNoEnabled", typeof(bool), typeof(QuestionWindow),
            new PropertyMetadata(false));

        public bool ButtonNoEnabled
        {
            get { return (bool)GetValue(ButtonNoEnabledProperty); }
            set { SetValue(ButtonNoEnabledProperty, value); }
        }
        #endregion

        #region ButtonNoMessage
        public static readonly DependencyProperty ButtonNoMessageProperty = DependencyProperty.Register("ButtonNoMessage", typeof(string), typeof(QuestionWindow),
            new PropertyMetadata("_No"), ButtonNoMessageValidate);

        private static bool ButtonNoMessageValidate(object value)
        {
            return value != null;
        }

        public string ButtonNoMessage
        {
            get { return (string)GetValue(ButtonNoMessageProperty); }
            set { SetValue(ButtonNoMessageProperty, value); }
        }
        #endregion

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.Yes;
            DialogResult = true;
            Close();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.OK;
            DialogResult = true;
            Close();
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.No;
            DialogResult = false;
            Close();
        }

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            SystemSounds.Question.Play();
        }
    }
}
