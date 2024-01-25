using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Entity;
using Entity.Organization;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using Usecase;
using WpfApp1.DI;
using WpfApp1.EditWindow;
using WpfApp1.RelocateWindow;

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
        /// <param name="model">モデルインスタンス</param>
        public MainWindowV(MainWindowM model)
        {
            #region init View Members

            #endregion

            #region init ViewModel Members

            _model = model;

            PersonsCount = _model.Persons
                .ObserveProperty(x => x.Count).ToReadOnlyReactivePropertySlim()
                .AddTo(_disposables);

            Persons = _model.Persons.ToReadOnlyReactiveCollection(
                x =>
                {
                    var ret = new PersonVM(x);
                    ret.OnEdit += (person) =>
                    {
                        var editWindow = new EditWindowV(new EditWindowM(person))
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
                    ret.OnRelocate += (person) =>
                    {
                        var usecase = ModelProvider.GetRequiredModel<IRelocateUsecase>();
                        usecase.OnPersonUpdate += Usecase_OnPersonUpdate;
                        usecase.OnArisedProblems += Usecase_OnArisedProblems;
                        var window = new RelocateWindowV(new RelocateWindowM(person, usecase))
                        {
                            Owner = this,
                        };
                        window.ShowDialog();
                        usecase.OnPersonUpdate -= Usecase_OnPersonUpdate;
                        usecase.OnArisedProblems -= Usecase_OnArisedProblems;
                    };
                    return ret;
                })
                .AddTo(_disposables);

            ProblemsInfo = _model.ProblemsInfo.ToReadOnlyReactivePropertySlim()
                .AddTo(_disposables);

            OrganizationInfo = _model.OrganizationInfo.ToReadOnlyReactivePropertySlim()
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
