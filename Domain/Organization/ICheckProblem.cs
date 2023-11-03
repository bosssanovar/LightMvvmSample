using Entity.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Organization
{
    /// <summary>
    /// 組織人員問題をチェックするインターフェース
    /// </summary>
    public interface ICheckProblem
    {
        #region Properties ------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// 未所属社員の一覧を取得します。
        /// </summary>
        /// <returns>未所属社員の一覧</returns>
        public List<Person> GetUnAssignedPersons();

        /// <summary>
        /// 組織長不在組織の一覧を取得します。
        /// </summary>
        /// <returns>組織長不在組織の一覧</returns>
        public List<OrganizationBase> GetNoBossOrganizaiotns();

        #endregion --------------------------------------------------------------------------------------------
    }
}
