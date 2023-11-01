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
        /// <param name="persons">社員一覧</param>
        /// <returns>未所属社員の一覧</returns>
        public List<Person> GetUnAssignedPersons(List<Person> persons);

        /// <summary>
        /// 組織長不在組織の一覧を取得します。
        /// </summary>
        /// <returns>組織長不在組織の一覧</returns>
        public List<Organization> GetNoBossOrganizaiotns();

        #endregion --------------------------------------------------------------------------------------------
    }
}
