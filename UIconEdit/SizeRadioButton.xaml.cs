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

using System.Windows;

namespace UIconEdit.Maker
{
    /// <summary>
    /// Interaction logic for SizeRadioButton.xaml
    /// </summary>
    partial class SizeRadioButton
    {
        public SizeRadioButton()
        {
            InitializeComponent();
        }

        #region LanguageFile
        public static readonly DependencyProperty LanguageFileProperty = DependencyProperty.Register(nameof(LanguageFile), typeof(LanguageFile), typeof(SizeRadioButton));

        public LanguageFile LanguageFile
        {
            get { return (LanguageFile)GetValue(LanguageFileProperty); }
            set { SetValue(LanguageFileProperty, value); }
        }
        #endregion

        #region EntryWidth
        public static readonly DependencyProperty EntryWidthProperty = DependencyProperty.Register(nameof(EntryWidth), typeof(short), typeof(SizeRadioButton),
            new PropertyMetadata((short)32));

        public short EntryWidth
        {
            get { return (short)GetValue(EntryWidthProperty); }
            set { SetValue(EntryWidthProperty, value); }
        }
        #endregion

        #region EntryHeight
        public static readonly DependencyProperty EntryHeightProperty = DependencyProperty.Register(nameof(EntryHeight), typeof(short), typeof(SizeRadioButton),
            new PropertyMetadata((short)32));

        public short EntryHeight
        {
            get { return (short)GetValue(EntryHeightProperty); }
            set { SetValue(EntryHeightProperty, value); }
        }
        #endregion
    }
}
