using Entity.Organization;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.RelocateWindow
{
    /// <summary>
    /// Relocate WindowのViewModel
    /// </summary>
    public partial class RelocateWindowV : INotifyPropertyChanged
    {
        #region Fields ----------------------------------------------------------------------------------------

        private readonly CompositeDisposable _disposables = new();

        private readonly RelocateWindowM _model;

        #endregion --------------------------------------------------------------------------------------------

        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        /// <summary>
        /// 社員名称を取得します。
        /// </summary>
        public string PersonName
        {
            get
            {
                return _model.Person.Name.FullName;
            }
        }

        /// <summary>
        /// 組織一覧のコンボボックスアイテムを取得します。
        /// </summary>
        public ReadOnlyCollection<ComboBoxItem<OrganizationBase>> OrganizationItems { get; init; }

        /// <summary>
        /// 異動先の設定値
        /// </summary>
        public ReactivePropertySlim<OrganizationBase> SelectedOrganization { get; }

        /// <summary>
        /// 組織長として異動するかの設定値
        /// </summary>
        public ReactivePropertySlim<bool> IsBoss { get; }

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
                        _model.Relocate();

                        Close();
                    }));

                return _relocateCommand;
            }
        }

        #endregion

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        /// <summary>
        /// 不要だが、バインドした際のメモリリーク対策のため固定追加（必須） INotifyPropertyChanged
        /// </summary>
#pragma warning disable CS0067 // イベント 'RelocateWindowV.PropertyChanged' は使用されていません
        public event PropertyChangedEventHandler? PropertyChanged;
#pragma warning restore CS0067 // イベント 'RelocateWindowV.PropertyChanged' は使用されていません

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - private -----------------------------------------------------------------------------

        private ReadOnlyCollection<ComboBoxItem<OrganizationBase>> GetItems()
        {
            var items = new List<ComboBoxItem<OrganizationBase>>();

            foreach (var info in _model.GetOrganizationInfos())
            {
                items.Add(new(info.FullName, info.Organization));
            }

            var ret = new ReadOnlyCollection<ComboBoxItem<OrganizationBase>>(items);

            return ret;
        }

        private void DisposeViewModelElement()
        {
            _disposables.Dispose();

            _model.Dispose();
        }

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}
