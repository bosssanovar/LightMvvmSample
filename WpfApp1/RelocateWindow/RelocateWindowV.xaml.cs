using Entity.Organization;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1.RelocateWindow
{
    /// <summary>
    /// RelocateWindowV.xaml の相互作用ロジック
    /// </summary>
    public partial class RelocateWindowV : Window
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
        /// <param name="model">Modelインスタンス</param>
        public RelocateWindowV(RelocateWindowM model)
        {
            #region init View Members

            #endregion

            #region init ViewModel Members

            _model = model;

            OrganizationItems = GetItems();

            SelectedOrganization = _model.SelectedOrganization.ToReactivePropertySlimAsSynchronized(x => x.Value)
                .AddTo(_disposables);

            IsBoss = _model.IsBoss.ToReactivePropertySlimAsSynchronized(x => x.Value)
                .AddTo(_disposables);

            IsOkEnabled = _model.SelectedOrganization
                .Select(x => x is not null)
                .ToReadOnlyReactivePropertySlim()
                .AddTo(_disposables);

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
