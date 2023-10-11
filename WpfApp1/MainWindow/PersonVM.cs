using System;
using System.ComponentModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Entity;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace WpfApp1.MainWindow
{
    /// <summary>
    /// Person ViewModel
    /// </summary>
    public class PersonVM : INotifyPropertyChanged, IDisposable
    {
        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Fields ----------------------------------------------------------------------------------------

        private readonly CompositeDisposable _disposables = new();

        private readonly PersonM _model;

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        /// <summary>
        /// 名前を取得します。
        /// </summary>
        public ReadOnlyReactivePropertySlim<string?> FullName { get; }

        /// <summary>
        /// 年齢を取得します。
        /// </summary>
        public ReadOnlyReactivePropertySlim<int> Age { get; }

        #region Edit Command

        private Command _editCommand;

        /// <summary>
        /// Edit コマンド
        /// </summary>
        public Command EditCommand
        {
            get
            {
                _editCommand ??= new Command(new Action(() =>
                    {
                        OnEdit?.Invoke(_model);
                    }));

                return _editCommand;
            }
        }

        #endregion

        #region Delete Command

        private Command _deleteCommand;

        /// <summary>
        /// Delete コマンド
        /// </summary>
        public Command DeleteCommand
        {
            get
            {
                _deleteCommand ??= new Command(new Action(() =>
                    {
                        OnDelete?.Invoke(_model);
                    }));

                return _deleteCommand;
            }
        }

        #endregion

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        /// <summary>
        /// 変更通知を発行する
        /// </summary>
#pragma warning disable CS0067 // イベント 'PersonVM.PropertyChanged' は使用されていません
        public event PropertyChangedEventHandler? PropertyChanged;
#pragma warning restore CS0067 // イベント 'PersonVM.PropertyChanged' は使用されていません

        /// <summary>
        /// 編集要求を発行する。
        /// </summary>
        public event Action<PersonM>? OnEdit;

        /// <summary>
        /// 削除要求を発行する
        /// </summary>
        public event Action<PersonM>? OnDelete;

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="model">モデル</param>
        public PersonVM(PersonM model)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));

            FullName = _model.Name.Select(x => x.FullName)
                .ToReadOnlyReactivePropertySlim()
                .AddTo(_disposables);

            Age = _model.Birthday.Select(x => x.GetAge(DateTime.Today))
                .ToReadOnlyReactivePropertySlim()
                .AddTo(_disposables);
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// 破棄します。
        /// </summary>
        public void Dispose()
        {
            _disposables.Dispose();

            OnEdit = null;
            OnDelete = null;
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - protected ---------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - private -----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - override ----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}
