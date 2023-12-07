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

        private static PersonListViewUsecase _personListViewUsecase;
        private static InitializeUsecase _initializeUsecase;
        private static AddPersonUsecase _addPersonUsecase;
        private static RemovePersonUsecase _removePersonUsecase;
        private static UpdatePersonUsecase _updatePersonUsecase;
        private static CheckProblemsUsecase _checkProblemsUsecase;
        private static RelocateUsecase _relocateUsecase;
        private static GetOrganizationStructureUsecase _getOrganizationStructureUsecase;
        private static SaveLoadDataUsecase _saveLoadDataUsecase;

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
        public static PersonListViewUsecase PersonListViewUsecase =>
            _personListViewUsecase ??= new PersonListViewUsecase(
                PeopleRepository,
                OrganizationRepository);

        /// <summary>
        /// 設定値を初期化するユースケースを取得します。
        /// </summary>
        public static InitializeUsecase InitializeUsecase =>
            _initializeUsecase ??= new InitializeUsecase(
                PeopleRepository,
                OrganizationRepository);

        /// <summary>
        /// 個人情報を追加するためのユースケースを取得します。
        /// </summary>
        public static AddPersonUsecase AddPersonUsecase =>
            _addPersonUsecase ??= new AddPersonUsecase(
                PeopleRepository,
                OrganizationRepository);

        /// <summary>
        /// 個人情報を更新するためのユースケースを取得します。
        /// </summary>
        public static UpdatePersonUsecase UpdatePersonUsecase =>
            _updatePersonUsecase ??= new UpdatePersonUsecase(
                PeopleRepository);

        /// <summary>
        /// 個人情報を削除するためのユースケースを取得します。
        /// </summary>
        public static RemovePersonUsecase RemovePersonUsecase =>
            _removePersonUsecase ??= new RemovePersonUsecase(
                PeopleRepository);

        /// <summary>
        /// 組織人員問題を確認するためのユースケースを取得します。
        /// </summary>
        public static CheckProblemsUsecase CheckProblemsUsecase =>
            _checkProblemsUsecase ??= new CheckProblemsUsecase(
                OrganizationRepository);

        /// <summary>
        /// 社員異動のためのユースケースを取得します。
        /// </summary>
        public static RelocateUsecase RelocateUsecase =>
            _relocateUsecase ??= new RelocateUsecase(
                OrganizationRepository);

        /// <summary>
        /// 組織構造情報を取得するためのユースケースを取得します。
        /// </summary>
        public static GetOrganizationStructureUsecase GetOrganizationStructureUsecase =>
            _getOrganizationStructureUsecase ??= new GetOrganizationStructureUsecase(
                OrganizationRepository);

        /// <summary>
        /// データファイルの保存・読み込みを行うユースケースを取得します。
        /// </summary>
        public static SaveLoadDataUsecase SaveLoadDataUsecase =>
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
