using Entity.Persons;
using Entity.Service.OrganizationVisitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Organization
{
    /// <summary>
    /// 無所属社員のグループ
    /// </summary>
    internal class UnAssignedMembersGroup : OrganizationBase
    {
        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Fields ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UnAssignedMembersGroup()
            : this(new("無所属"), Ranks.Outside)
        {
        }

        private UnAssignedMembersGroup(UnAssignedMembersGroup original)
            : base(original)
        {
        }

        private UnAssignedMembersGroup(OrganizationNameVO name, Ranks rank)
            : base(name, rank)
        {
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// 無所属社員を取得します。
        /// </summary>
        /// <returns>無所属社員一覧</returns>
        public List<Person> GetMembers()
        {
            return Members.Select(x => x.Clone()).ToList();
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - internal ----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - protected ---------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - private -----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - override ----------------------------------------------------------------------------

        /// <inheritdoc/>
        public override UnAssignedMembersGroup Clone()
        {
            return new UnAssignedMembersGroup(this);
        }

        /// <inheritdoc/>
        internal override void Accept(IOrganizationVisitor visitor)
        {
            visitor.Visit(this);
        }

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}
