using Entity.Organization;
using Entity.Persons;
using Reactive.Bindings;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive.Disposables;
using System.Windows;
using Usecase;
using WpfApp1.MainWindow;

namespace WpfApp1.EditWindow
{
    /// <summary>
    /// EditWindowのViewModel
    /// </summary>
    public partial class EditWindowV : INotifyPropertyChanged
    {
        #region Fields ----------------------------------------------------------------------------------------

        private readonly CompositeDisposable _disposables = new();

        private readonly EditWindowM _model;

        #endregion --------------------------------------------------------------------------------------------

        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        /// <summary>
        /// 苗字を取得します。
        /// </summary>
        public ReactivePropertySlim<string> FamilyName { get; }

        /// <summary>
        /// 名前を取得します。
        /// </summary>
        public ReactivePropertySlim<string> FirstName { get; }

        /// <summary>
        /// 誕生日　年を取得します。
        /// </summary>
        public ReactivePropertySlim<int> Year { get; }

        /// <summary>
        /// 誕生日　月を取得します。
        /// </summary>
        public ReactivePropertySlim<int> Month { get; }

        /// <summary>
        /// 誕生日　日を取得します。
        /// </summary>
        public ReactivePropertySlim<int> Day { get; }

        #region Ok Command

        private Command _okCommand;

        /// <summary>
        /// Ok コマンド
        /// </summary>
        public Command OkCommand
        {
            get
            {
                _okCommand ??= new Command(new Action(() =>
                    {
                        if (!IsValidParams())
                        {
                            MessageBox.Show(this, "データが不正", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        OnCompleted?.Invoke(_model.Person);
                        OnCompleted = null;

                        Close();
                    }));

                return _okCommand;
            }
        }

        #endregion

        #region Cancel Command

        private Command _cancelCommand;

        /// <summary>
        /// Cancel コマンド
        /// </summary>
        public Command CancelCommand
        {
            get
            {
                _cancelCommand ??= new Command(new Action(() =>
                    {
                        Close();
                    }));

                return _cancelCommand;
            }
        }

        #endregion

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        /// <summary>
        /// 不要だが、バインドした際のメモリリーク対策のため固定追加（必須） INotifyPropertyChanged
        /// </summary>
#pragma warning disable CS0067 // イベント 'EditWindowV.PropertyChanged' は使用されていません
        public event PropertyChangedEventHandler? PropertyChanged;
#pragma warning restore CS0067 // イベント 'EditWindowV.PropertyChanged' は使用されていません

        /// <summary>
        /// 画面操作完了時イベント
        /// </summary>
        public event Action<Person>? OnCompleted;

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - private -----------------------------------------------------------------------------

        private void DisposeViewModelElement()
        {
            _disposables.Dispose();

            _model.Dispose();
        }

        private bool IsValidParams()
        {
            if(!NameVO.IsValid(FamilyName.Value, FirstName.Value))
            {
                return false;
            }

            if(BirthdayVO.IsValid(Year.Value, Month.Value, Day.Value) != BirthdayVO.ErrorCause.None)
            {
                return false;
            }

            return true;
        }

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}
