﻿using Entity.DomainService;
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
    public class OrganizationRepository
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

        /// <summary>
        /// <see cref="Organization"/>エンティティの複製を取得します。
        /// </summary>
        /// <returns><see cref="Organization"/>エンティティの複製インスタンス</returns>
        public Organization LoadOrganization() => _organization.Clone();

        /// <summary>
        /// <see cref="Organization"/>エンティティを保存します。
        /// </summary>
        /// <param name="organization"><see cref="Organization"/>エンティティ</param>
        public void SaveOrganizaion(Organization organization)
        {
            _organization = organization.Clone();
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