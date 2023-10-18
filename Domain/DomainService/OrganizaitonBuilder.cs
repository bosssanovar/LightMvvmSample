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
    internal class OrganizaitonBuilder
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
        public static ManagementOrganization Build()
        {
            var departmentList = new List<OrganizationBase>();

            // 部　を生成
            for(int departmentIndex = 0; departmentIndex < 3; departmentIndex++)
            {
                var sectionList = new List<OrganizationBase>();

                // 課　を生成
                for(int sectionIndex = 0; sectionIndex < 4; sectionIndex++)
                {
                    var teamList = new List<OrganizationBase>();

                    // チーム　を生成
                    for (int teamIndex = 0; teamIndex < 3; teamIndex++)
                    {
                        teamList.Add(new TerminalOrganization(new($"第{teamIndex + 1}")));
                    }

                    sectionList.Add(new ManagementOrganization(new($"第{sectionIndex + 1}"), Lanks.Section, teamList));
                }

                departmentList.Add(new ManagementOrganization(new($"第{departmentIndex + 1}技術"), Lanks.Department, sectionList));
            }

            var campany = new ManagementOrganization(new("●●エンジニアリング"), Lanks.Campany, departmentList);

            return campany;
        }

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
