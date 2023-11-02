using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Entity;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using Usecase;
using WpfApp1.EditWindow;

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
        public MainWindowV()
        {
            #region init View Members

            #endregion

            #region init ViewModel Members

            _updatePersonUsecase = PersonUsecaseProvider.UpdatePersonUsecase;
            _addPersonUsecase = PersonUsecaseProvider.AddPersonUsecase;
            _removePersonUsecase = PersonUsecaseProvider.RemovePersonUsecase;
            _personListViewUsecase = PersonUsecaseProvider.PersonListViewUsecase;

            _model = new MainWindowM();

            PersonsCount = _model.Persons
                .ObserveProperty(x => x.Count).ToReadOnlyReactivePropertySlim()
                .AddTo(_disposables);

            Persons = _model.Persons.ToReadOnlyReactiveCollection(
                x =>
                {
                    var ret = new PersonVM(x);
                    ret.OnEdit += (person) =>
                    {
                        var editWindow = new EditWindowV(new EditWindowM(person), _personListViewUsecase)
                        {
                            Owner = this,
                        };
                        editWindow.OnCompleted += EditWindow_OnCompletedEdit;
                        editWindow.ShowDialog();
                        editWindow.OnCompleted -= EditWindow_OnCompletedEdit;
                    };
                    ret.OnDelete += (model) =>
                    {
                        RemovePerson(model);
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
            DisposeViewModelElement();

            base.OnClosing(e);
        }

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}
