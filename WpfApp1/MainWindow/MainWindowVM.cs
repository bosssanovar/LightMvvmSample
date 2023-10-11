using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive.Disposables;
using Entity;
using Reactive.Bindings;
using Usecase;
using WpfApp1.EditWindow;

namespace WpfApp1.MainWindow
{
    /// <summary>
    /// MainWindowのViewModel
    /// </summary>
    public partial class MainWindowV : INotifyPropertyChanged
    {
        #region Fields ----------------------------------------------------------------------------------------

        private readonly CompositeDisposable _disposables = new();

        private readonly MainWindowM _model;

        private readonly PersonListViewUsecase _personListViewUsecase;

        #endregion --------------------------------------------------------------------------------------------

        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        /// <summary>
        /// 個人情報リストの件数を取得します。
        /// </summary>
        public ReadOnlyReactivePropertySlim<int> PersonsCount { get; }

        /// <summary>
        /// 個人情報リストを取得します。
        /// </summary>
        public ReadOnlyReactiveCollection<PersonVM> Persons { get; }

        #region Add Command

        private Command _addCommand;

        /// <summary>
        /// Add コマンド
        /// </summary>
        public Command AddCommand
        {
            get
            {
                _addCommand ??= new Command(new Action(() =>
                {
                    var edit = new EditWindowV(new PersonM(new NameVO(string.Empty, string.Empty), new BirthdayVO(1900, 1, 1)), _personListViewUsecase)
                    {
                        Owner = this,
                    };
                    edit.ShowDialog();
                }));

                return _addCommand;
            }
        }

        #endregion

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

        private void RemovePerson(PersonM person)
        {
            _personListViewUsecase.RemovePerson(person.Identifire);
        }

        private void DisposeViewModelElement()
        {
            _disposables.Dispose();
        }

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}
