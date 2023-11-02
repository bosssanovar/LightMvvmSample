using Entity.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Organization
{
    /// <summary>
    /// 社員を組織にアサインするインターフェース
    /// </summary>
    public interface IAssign
    {
        #region Properties ------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// 組織にアサインする。
        /// </summary>
        /// <param name="person">社員</param>
        /// <param name="newOrganization">社員を追加する組織</param>
        /// <param name="isBoss">組織長としてアサインする場合 true</param>
        /// <exception cref="ArgumentException">追加対象の組織がない場合</exception>
        public void Assign(Person person, OrganizationBase newOrganization, bool isBoss);

        /// <summary>
        /// 複製します。
        /// </summary>
        /// <returns>複製</returns>
        public IAssign Clone();

        #endregion --------------------------------------------------------------------------------------------
    }
}
