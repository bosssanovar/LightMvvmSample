﻿using System.ComponentModel;
using System.Windows;
using Entity;

namespace WpfApp1.EditWindow
{
    /// <summary>
    /// EditWindowV.xaml の相互作用ロジック
    /// </summary>
    public partial class EditWindowV : Window
    {
        #region Fields ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="person">個人情報</param>
        public EditWindowV(Person person)
        {
            #region init View Members

            #endregion

            #region init ViewModel Members

            _person = person.Clone();

            FamilyName = _person.Name.Value.Family;
            FirstName = _person.Name.Value.First;
            Year = _person.Birthday.Value.Year;
            Month = _person.Birthday.Value.Month;
            Day = _person.Birthday.Value.Day;

            UpdatedPerson = _person;

            #endregion

            InitializeComponent();
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - protected ---------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - private -----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - override ----------------------------------------------------------------------------

        /// <summary>
        /// Close時処理
        /// </summary>
        /// <param name="e">キャンセルイベントデータ</param>
        protected override void OnClosing(CancelEventArgs e)
        {
            DisposeViewModelElement();

            base.OnClosing(e);
        }

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}
