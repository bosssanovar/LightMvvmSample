using System;
using System.ComponentModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Domain;
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

        private readonly Person _model;

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        /// <summary>
        /// 変更通知を発行する
        /// </summary>
#pragma warning disable CS0067 // イベント 'PersonVM.PropertyChanged' は使用されていません
        public event PropertyChangedEventHandler? PropertyChanged;
#pragma warning restore CS0067 // イベント 'PersonVM.PropertyChanged' は使用されていません

        /// <summary>
        /// 名前を取得します。
        /// </summary>
        public ReadOnlyReactivePropertySlim<string?> FullName { get; }

        /// <summary>
        /// 年齢を取得します。
        /// </summary>
        public ReadOnlyReactivePropertySlim<int> Age { get; }

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="model">モデル</param>
        public PersonVM(Person model)
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
