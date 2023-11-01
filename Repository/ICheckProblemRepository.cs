using Entity.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    /// <summary>
    /// 組織人員問題の有無をチェックするインターフェース
    /// </summary>
    public interface ICheckProblemRepository
    {
        #region Properties ------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// 組織人員問題チェック用のEntityを取得します。
        /// </summary>
        /// <returns>組織人員問題チェック用のEntity</returns>
        public ICheckProblem LoadProblemChecker();

        #endregion --------------------------------------------------------------------------------------------
    }
}
