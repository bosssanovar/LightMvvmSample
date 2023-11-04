using Entity.DomainService;
using Entity.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Service
{
    /// <summary>
    /// 組織構成を構築するクラス
    /// </summary>
    public class OrganizaitonBuilder : IOrganizationBuilder
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

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// 組織構成を構築します。
        /// </summary>
        /// <returns>構築した組織構成の最上位組織</returns>
        public OrganizationBase Build()
        {
            List<OrganizationBase> departmentList = GetDepartmentList();

            var campany = new ManagementOrganization(new("●●エンジニアリング"), Ranks.Campany, departmentList);

            return campany;
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - protected ---------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - private -----------------------------------------------------------------------------

        private static List<OrganizationBase> GetDepartmentList()
        {
            var ret = new List<OrganizationBase>();

            // 部　を生成
            for (int index = 0; index < 2; index++)
            {
                List<OrganizationBase> sectionList = GetSectionList();

                ret.Add(new ManagementOrganization(new($"第{index + 1}技術"), Ranks.Department, sectionList));
            }

            return ret;
        }

        private static List<OrganizationBase> GetSectionList()
        {
            var ret = new List<OrganizationBase>();

            // 課　を生成
            for (int index = 0; index < 2; index++)
            {
                List<OrganizationBase> teamList = GetTeamList();

                ret.Add(new ManagementOrganization(new($"第{index + 1}"), Ranks.Section, teamList));
            }

            return ret;
        }

        private static List<OrganizationBase> GetTeamList()
        {
            var ret = new List<OrganizationBase>();

            // チーム　を生成
            for (int index = 0; index < 3; index++)
            {
                ret.Add(new TerminalOrganization(new($"第{index + 1}")));
            }

            return ret;
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - override ----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}
