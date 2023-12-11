using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Windows;
using Entity.Organization;
using Entity.Persons;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using Usecase;
using WpfApp1.MainWindow;

namespace WpfApp1.EditWindow
{
    /// <summary>
    /// EditWindowV.xaml の相互作用ロジック
    /// </summary>
    public partial class EditWindowV : Window
    {
        #region Fields ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="model">個人情報</param>
        /// <param name="personListViewUsecase">社員リスト表示ユースケース</param>
        public EditWindowV(EditWindowM model, PersonListViewUsecase personListViewUsecase)
        {
            #region init View Members

            #endregion

            #region init ViewModel Members

            _model = model;
            _personListViewUsecase = personListViewUsecase;

            FamilyName = _model.FamilyName.ToReactivePropertySlimAsSynchronized(x => x.Value)
                .AddTo(_disposables);

            FirstName = _model.FirstName.ToReactivePropertySlimAsSynchronized(x => x.Value)
                .AddTo(_disposables);

            Year = _model.Year.ToReactivePropertySlimAsSynchronized(x => x.Value)
                .AddTo(_disposables);

            Month = _model.Month.ToReactivePropertySlimAsSynchronized(x => x.Value)
                .AddTo(_disposables);

            Day = _model.Day.ToReactivePropertySlimAsSynchronized(x => x.Value)
                .AddTo(_disposables);

            var organizationList = _personListViewUsecase.GetOrganizationInfos();

            SelectedOrganization = new ReactivePropertySlim<Entity.Organization.OrganizationBase>(organizationList[0].Organization)
                .AddTo(_disposables);

            OrganizationItems = GetPostItems(organizationList);

            #endregion

            InitializeComponent();
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - protected ---------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - private -----------------------------------------------------------------------------

        private static ObservableCollection<ComboBoxItem<OrganizationBase>> GetPostItems(
            ICollection<OrganizationInfo> infos)
        {
            var ret = new ObservableCollection<ComboBoxItem<OrganizationBase>>();

            foreach (var info in infos)
            {
                ret.Add(new ComboBoxItem<OrganizationBase>(info.FullName, info.Organization));
            }

            return ret;
        }

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
