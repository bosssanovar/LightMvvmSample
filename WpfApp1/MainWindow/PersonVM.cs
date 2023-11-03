using System;
using System.ComponentModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Entity.Persons;
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

        /// <summary>
        /// 所属組織の組織名を取得します。
        /// </summary>
        public ReadOnlyReactivePropertySlim<string?> AssignedOrgaizationName { get; }

        /// <summary>
        /// 役職を取得します。
        /// </summary>
        public ReadOnlyReactivePropertySlim<string?> Post { get; }

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
                        OnEdit?.Invoke(_model.Person);
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
                        OnDelete?.Invoke(_model.Person);
                    }));

                return _deleteCommand;
            }
        }

        #endregion

        #region Relocate Command

        private Command _relocateCommand;

        /// <summary>
        /// Relocate コマンド
        /// </summary>
        public Command RelocateCommand
        {
            get
            {
                _relocateCommand ??= new Command(new Action(() =>
                    {
                        OnRelocate?.Invoke(_model.Person);
                    }));

                return _relocateCommand;
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
        public event Action<Person>? OnEdit;

        /// <summary>
        /// 削除要求を発行する
        /// </summary>
        public event Action<Person>? OnDelete;

        /// <summary>
        /// 人事異動要求を発行する
        /// </summary>
        public event Action<Person>? OnRelocate;

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

            AssignedOrgaizationName = _model.AssignedOrgaizationName
                .ToReadOnlyReactivePropertySlim()
                .AddTo(_disposables);

            Post = _model.Post.Select(x => x.GetDisplayText())
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

            _model.Dispose();

            OnEdit = null;
            OnDelete = null;
            OnRelocate = null;
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
