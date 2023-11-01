using Entity.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    /// <summary>
    /// 社員リスト取得のためのRepositoryを取得するインターフェース
    /// </summary>
    public interface IGetPersonsRepository
    {
        #region Properties ------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// 社員一覧を取得するためのEntityを取得します。
        /// </summary>
        /// <returns>社員一覧を取得するためのEntity</returns>
        public IGetPersons LoadPersonsGetter();

        #endregion --------------------------------------------------------------------------------------------
    }
}
