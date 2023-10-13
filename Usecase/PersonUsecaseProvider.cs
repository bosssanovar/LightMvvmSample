using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private static PeopleRepository _peopleRepository;

        #endregion

        #region Usecase

        private static PersonListViewUsecase _personListViewUsecase;
        private static InitializeUsecase _initializeUsecase;
        private static AddPersonUsecase _addPersonUsecase;
        private static RemovePersonUsecase _removePersonUsecase;
        private static UpdatePersonUsecase _updatePersonUsecase;

        #endregion

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        #region Repository

        private static PeopleRepository PeopleRepository => _peopleRepository ??= new PeopleRepository();

        #endregion

        #region Usecase

        /// <summary>
        /// 個人情報リストを表示するためのユースケースを取得します。
        /// </summary>
        public static PersonListViewUsecase PersonListViewUsecase => _personListViewUsecase ??= new PersonListViewUsecase(PeopleRepository);

        /// <summary>
        /// 設定値を初期化するユースケースを取得します。
        /// </summary>
        public static InitializeUsecase InitializeUsecase => _initializeUsecase ??= new InitializeUsecase(PeopleRepository);

        /// <summary>
        /// 個人情報を追加するためのユースケースを取得します。
        /// </summary>
        public static AddPersonUsecase AddPersonUsecase => _addPersonUsecase ??= new AddPersonUsecase(PeopleRepository);

        /// <summary>
        /// 個人情報を更新するためのユースケースを取得します。
        /// </summary>
        public static UpdatePersonUsecase UpdatePersonUsecase => _updatePersonUsecase ??= new UpdatePersonUsecase(PeopleRepository);

        /// <summary>
        /// 個人情報を削除するためのユースケースを取得します。
        /// </summary>
        public static RemovePersonUsecase RemovePersonUsecase => _removePersonUsecase ??= new RemovePersonUsecase(PeopleRepository);

        #endregion

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

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}
