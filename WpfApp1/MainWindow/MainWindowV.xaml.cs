using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Domain;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace WpfApp1.MainWindow
{
    /// <summary>
    /// MainWindowV.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindowV : Window
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="people">集団</param>
        public MainWindowV(People people)
        {
            #region init View Members

            #endregion

            #region init ViewModel Members

            _people = people;

            PersonsCount = _people.Persons
                .ObserveProperty(x => x.Count).ToReadOnlyReactivePropertySlim()
                .AddTo(_disposables);

            Persons = _people.Persons.ToReadOnlyReactiveCollection(x =>
            {
                var ret = new PersonVM(x);
                ret.OnDelete += (model) =>
                {
                    DeletePerson(model);
                };
                return ret;
            })
                .AddTo(_disposables);

            #endregion

            InitializeComponent();
        }

        #region Fields ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

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
            _disposables.Dispose();

            base.OnClosing(e);
        }

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}
