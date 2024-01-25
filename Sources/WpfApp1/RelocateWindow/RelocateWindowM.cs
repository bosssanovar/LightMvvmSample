using Entity.Organization;
using Entity.Persons;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using Usecase;

namespace WpfApp1.RelocateWindow
{
    /// <summary>
    /// Relocate Windowのモデル
    /// </summary>
    public class RelocateWindowM
    {
        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Fields ----------------------------------------------------------------------------------------

        private readonly CompositeDisposable _disposables = new();

        private readonly IRelocateUsecase _relocateUsecase;

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        /// <summary>
        /// 異動対象社員を取得します。
        /// </summary>
        public Person Person { get; init; }

        /// <summary>
        /// 異動先の設定値
        /// </summary>
        public ReactivePropertySlim<OrganizationBase> SelectedOrganization { get; }

        /// <summary>
        /// 組織長として異動するかの設定値
        /// </summary>
        public ReactivePropertySlim<bool> IsBoss { get; }

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="person">異動対象社員</param>
        /// <param name="relocateUsecase">異動ユースケース</param>
        public RelocateWindowM(Person person, IRelocateUsecase relocateUsecase)
        {
            _relocateUsecase = relocateUsecase;
            Person = person;
            SelectedOrganization = new ReactivePropertySlim<OrganizationBase>(relocateUsecase.GetAssignedOrganization(person));
            IsBoss = new ReactivePropertySlim<bool>(false);
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// 組織情報一覧を取得します。
        /// </summary>
        /// <returns>組織情報一覧</returns>
        public ReadOnlyCollection<OrganizationInfo> GetOrganizationInfos()
        {
            return _relocateUsecase.Organizations;
        }

        /// <summary>
        /// 各種破棄処理
        /// </summary>
        public void Dispose()
        {
            _disposables.Dispose();
        }

        /// <summary>
        /// 社員を異動します。
        /// </summary>
        internal void Relocate()
        {
            _relocateUsecase.Relocate(Person, SelectedOrganization.Value, IsBoss.Value);
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
