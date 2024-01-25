using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usecase.Sub;

namespace Usecase
{
    /// <summary>
    /// 個人情報関連の唯一のUsecaseインスタンスを提供します。
    /// </summary>
    public static class PersonUsecaseProvider
    {
        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Fields ----------------------------------------------------------------------------------------

        #region Repository

        private static IPeopleRepository _peopleRepository;

        private static IOrganizationRepository _organizationRepository;

        private static IDataStore _dataStore;

        #endregion

        #region Usecase

        private static IPersonListViewUsecase _personListViewUsecase;
        private static IInitializeUsecase _initializeUsecase;
        private static IAddPersonUsecase _addPersonUsecase;
        private static IRemovePersonUsecase _removePersonUsecase;
        private static IUpdatePersonUsecase _updatePersonUsecase;
        private static ICheckProblemsUsecase _checkProblemsUsecase;
        private static IRelocateUsecase _relocateUsecase;
        private static IGetOrganizationStructureUsecase _getOrganizationStructureUsecase;
        private static ISaveLoadDataUsecase _saveLoadDataUsecase;

        #endregion

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        #region Repository

        private static IPeopleRepository PeopleRepository =>
            _peopleRepository ??= new PeopleRepository();

        private static IOrganizationRepository OrganizationRepository =>
            _organizationRepository ??= new OrganizationRepository();

        #endregion

        #region Usecase

        /// <summary>
        /// 個人情報リストを表示するためのユースケースを取得します。
        /// </summary>
        public static IPersonListViewUsecase PersonListViewUsecase =>
            _personListViewUsecase ??= new PersonListViewUsecase(
                PeopleRepository,
                OrganizationRepository);

        /// <summary>
        /// 設定値を初期化するユースケースを取得します。
        /// </summary>
        public static IInitializeUsecase InitializeUsecase =>
#if DUMMY
            _initializeUsecase ??= new InitializeUsecaseDummy(
                PeopleRepository,
                OrganizationRepository);
#else
            _initializeUsecase ??= new InitializeUsecase(
                PeopleRepository,
                OrganizationRepository);
#endif

        /// <summary>
        /// 個人情報を追加するためのユースケースを取得します。
        /// </summary>
        public static IAddPersonUsecase AddPersonUsecase =>
            _addPersonUsecase ??= new AddPersonUsecase(
                PeopleRepository,
                OrganizationRepository);

        /// <summary>
        /// 個人情報を更新するためのユースケースを取得します。
        /// </summary>
        public static IUpdatePersonUsecase UpdatePersonUsecase =>
            _updatePersonUsecase ??= new UpdatePersonUsecase(
                PeopleRepository);

        /// <summary>
        /// 個人情報を削除するためのユースケースを取得します。
        /// </summary>
        public static IRemovePersonUsecase RemovePersonUsecase =>
            _removePersonUsecase ??= new RemovePersonUsecase(
                PeopleRepository);

        /// <summary>
        /// 組織人員問題を確認するためのユースケースを取得します。
        /// </summary>
        public static ICheckProblemsUsecase CheckProblemsUsecase =>
            _checkProblemsUsecase ??= new CheckProblemsUsecase(
                OrganizationRepository);

        /// <summary>
        /// 社員異動のためのユースケースを取得します。
        /// </summary>
        public static IRelocateUsecase RelocateUsecase =>
            _relocateUsecase ??= new RelocateUsecase(
                OrganizationRepository);

        /// <summary>
        /// 組織構造情報を取得するためのユースケースを取得します。
        /// </summary>
        public static IGetOrganizationStructureUsecase GetOrganizationStructureUsecase =>
            _getOrganizationStructureUsecase ??= new GetOrganizationStructureUsecase(
                OrganizationRepository);

        /// <summary>
        /// データファイルの保存・読み込みを行うユースケースを取得します。
        /// </summary>
        public static ISaveLoadDataUsecase SaveLoadDataUsecase =>
            _saveLoadDataUsecase ??= new SaveLoadDataUsecase(
                PeopleRepository,
                OrganizationRepository,
                _dataStore);

#endregion

#endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// データを保存・読み込みを行うクラスオブジェクトを設定します。
        /// </summary>
        /// <param name="dataStore"><see cref="IDataStore"/>を実装したクラスインスタンス</param>
        public static void SetDataStore(IDataStore dataStore)
        {
            _dataStore = dataStore;
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
