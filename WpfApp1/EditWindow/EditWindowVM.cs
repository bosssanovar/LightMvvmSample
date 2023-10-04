using Domain;
using System;
using System.ComponentModel;
using System.Reactive.Disposables;
using System.Windows;

namespace WpfApp1.EditWindow
{
    /// <summary>
    /// EditWindowのViewModel
    /// </summary>
    public partial class EditWindowV : INotifyPropertyChanged
    {
        #region Fields ----------------------------------------------------------------------------------------

        private readonly CompositeDisposable _disposables = new();

        private readonly Person _person;

        #endregion --------------------------------------------------------------------------------------------

        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        /// <summary>
        /// 苗字を設定または取得します。
        /// </summary>
        public string FamilyName { get; set; }

        /// <summary>
        /// 名前を設定または取得します。
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// 誕生日　年を設定または取得します。
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// 誕生日　月を設定または取得します。
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// 誕生日　日を設定または取得します。
        /// </summary>
        public int Day { get; set; }

        /// <summary>
        /// OK終了したかを取得します。
        /// </summary>
        public bool IsOk { get; private set; } = false;

        /// <summary>
        /// 個人情報の変更結果を格納します。
        /// </summary>
        public Person Result { get; private set; }

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

                        Result = new Person(new NameVO(FamilyName, FirstName), new BirthdayVO(Year, Month, Day));
                        IsOk = true;

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
                        this.Close();
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

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - private -----------------------------------------------------------------------------

        private void DisposeViewModelElement()
        {
            _disposables.Dispose();
        }

        private bool IsValidParams()
        {
            if(!NameVO.IsValid(FamilyName, FirstName))
            {
                return false;
            }

            if(!BirthdayVO.IsValid(Year, Month, Day))
            {
                return false;
            }

            return true;
        }

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}
