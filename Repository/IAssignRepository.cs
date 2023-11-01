using Entity.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    /// <summary>
    /// 社員を組織にアサインするEntityを取得するインターフェース
    /// </summary>
    public interface IAssignRepository
    {
        #region Properties ------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// 組織に社員をアサインするEntityを取得します。
        /// </summary>
        /// <returns>組織に社員をアサインするEntity</returns>
        public IAssign LoadAssigner();

        #endregion --------------------------------------------------------------------------------------------
    }
}
