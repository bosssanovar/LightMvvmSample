﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive.Disposables;
using Domain;
using Reactive.Bindings;

namespace WpfApp1.MainWindow
{
    /// <summary>
    /// MainWindowのViewModel
    /// </summary>
    public partial class MainWindowV : INotifyPropertyChanged
    {
        #region Fields ----------------------------------------------------------------------------------------

        private readonly CompositeDisposable _disposables = new();

        private readonly People _people;

        #endregion --------------------------------------------------------------------------------------------

        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        /// <summary>
        /// 個人情報リストの件数を取得します。
        /// </summary>
        public ReadOnlyReactivePropertySlim<int> PersonsCount { get; }

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        /// <summary>
        /// 不要だが、バインドした際のメモリリーク対策のため固定追加（必須） INotifyPropertyChanged
        /// </summary>
#pragma warning disable CS0067 // イベント 'Sample.PropertyChanged' は使用されていません
        public event PropertyChangedEventHandler? PropertyChanged;
#pragma warning restore CS0067 // イベント 'Sample.PropertyChanged' は使用されていません

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - private -----------------------------------------------------------------------------

        private void AddPerson()
        {
            _people.Persons.Add(new Person(new NameVO("a", "a"), new BirthdayVO(2012, 1, 1)));
        }

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}