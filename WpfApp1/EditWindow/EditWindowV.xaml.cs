using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Windows;
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
        public EditWindowV(PersonM model)
        {
            #region init View Members

            #endregion

            #region init ViewModel Members

            _model = model;

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

            SelectedPost = _model.Post.ToReactivePropertySlimAsSynchronized(x => x.Value)
                .AddTo(_disposables);

            PostItems = GetPostItems();

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

        private static ObservableCollection<ComboBoxItem<Posts>> GetPostItems()
        {
            var ret = new ObservableCollection<ComboBoxItem<Posts>>();

            var postAllItems = PostsExtend.GetAllDispValueList();

            foreach (var (value, disp) in postAllItems)
            {
                ret.Add(new ComboBoxItem<Posts>(disp, value));
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
