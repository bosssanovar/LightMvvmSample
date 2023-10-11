using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usecase
{
    /// <summary>
    /// Usecaseインスタンスを提供します。
    /// </summary>
    public static class UsecaseProvider
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

        #endregion

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        #region Repository

        private static PeopleRepository PeopleRepository
        {
            get
            {
                return _peopleRepository ??= new PeopleRepository();
            }
        }

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
