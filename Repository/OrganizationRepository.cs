using Entity.DomainService;
using Entity.Organization;
using Entity.Persons;
using Entity.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    /// <summary>
    /// <see cref="Organization"/>エンティティのリポジトリ
    /// </summary>
    public class OrganizationRepository : IOrganizationRepository
    {
        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Fields ----------------------------------------------------------------------------------------

        private Organization _organization;

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public OrganizationRepository()
        {
            _organization = new Organization(new OrganizaitonBuilder());
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <inheritdoc/>
        public Organization LoadOrganization() => _organization;

        /// <inheritdoc/>
        public void SaveOrganizaion(Organization organization)
        {
            _organization = organization;
        }

        /// <inheritdoc/>
        public IOrganization LoadAssigner() => _organization;

        /// <inheritdoc/>
        public void SaveAssigner(IOrganization assigner)
        {
            _organization = (Organization)assigner;
        }

        /// <inheritdoc/>
        public IOrganization LoadProblemChecker() => _organization;

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
