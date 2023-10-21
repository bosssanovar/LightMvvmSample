using Entity.Organization;
using Entity.Persons;
using Entity.Service.OrganizationVisitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DomainService.OrganizationVisitor
{
    /// <summary>
    /// 立場を取得します。
    /// </summary>
    internal class GetCurrentPositionVisitor : IOrganizationVisitor
    {
        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Fields ----------------------------------------------------------------------------------------

        private readonly Person _targetParson;

        private bool _isSearched = false;

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        /// <summary>
        /// 所属している組織を取得します。
        /// </summary>
        public OrganizationBase AssignedOrganization { get; private set; }

        /// <summary>
        /// 役職を取得します。
        /// </summary>
        public Posts Post { get; private set; }

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetParson">対象社員</param>
        public GetCurrentPositionVisitor(Person targetParson)
        {
            _targetParson = targetParson;
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// 実行します。
        /// </summary>
        /// <param name="target">ターゲット</param>
        public void Visit(OrganizationBase target)
        {
            if(_isSearched)
            {
                return;
            }

            if (target.IsContainDirectEmployee(_targetParson))
            {
                AssignedOrganization = target;
                Post = Posts.Employee;

                _isSearched = true;
            }
            else if (target.IsBoss(_targetParson))
            {
                AssignedOrganization = target;
                Post = target.Lank.GetBossPost();

                _isSearched = true;
            }
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - internal ----------------------------------------------------------------------------

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
