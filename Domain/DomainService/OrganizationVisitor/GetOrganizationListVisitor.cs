using Entity.Organization;
using Entity.Service.OrganizationVisitor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DomainService.OrganizationVisitor
{
    /// <summary>
    /// 部署一覧を取得するVisitor
    /// </summary>
    internal class GetOrganizationListVisitor : IOrganizationVisitor
    {
        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Fields ----------------------------------------------------------------------------------------

        private readonly List<NameAndOrganization> _organizations = new();

        private ManagementOrganization? _upperDepartment;

        private ManagementOrganization? _upperSection;

        private string _name;

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        /// <summary>
        /// 組織一覧を取得します。
        /// </summary>
        public ReadOnlyCollection<NameAndOrganization> Oganizations
        {
            get
            {
                return new ReadOnlyCollection<NameAndOrganization>(_organizations);
            }
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// 実行する
        /// </summary>
        /// <param name="target">ターゲット</param>
        public void Visit(OrganizationBase target)
        {
            if (target is ManagementOrganization m)
            {
                CreateName(m);
            }
            else if (target is TerminalOrganization t)
            {
                CreateName(t);
            }

            _organizations.Add(new(_name, target));
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - internal ----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - protected ---------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - private -----------------------------------------------------------------------------

        private void CreateName(ManagementOrganization organization)
        {
            if (organization.Lank == Lanks.Campany)
            {
                _upperDepartment = null;
                _upperSection = null;
                return;
            }
            else if (organization.Lank == Lanks.Department)
            {
                _upperDepartment = organization;
                _upperSection = null;
            }
            else if (organization.Lank == Lanks.Section)
            {
                _upperSection = organization;
            }
            else
            {
                throw new NotImplementedException();
            }

            StringBuilder sb = GetNameUntilSection();
            _name = sb.ToString();
        }

        private void CreateName(TerminalOrganization organization)
        {
            StringBuilder sb = GetNameUntilSection();
            sb.Append(' ');
            sb.Append(organization.DisplayName);
            _name = sb.ToString();
        }

        private StringBuilder GetNameUntilSection()
        {
            var sb = new StringBuilder();

            sb.Append(_upperDepartment?.DisplayName);

            if (_upperSection is not null)
            {
                sb.Append(' ');
                sb.Append(_upperSection.DisplayName);
            }

            return sb;
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - override ----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        /// <summary>
        /// 垂直通貫の組織名と組織オブジェクトのペア
        /// </summary>
        public class NameAndOrganization
        {
            /// <summary>
            /// 垂直通貫の組織名を取得します。
            /// </summary>
            public string Name { get; }

            /// <summary>
            /// 組織オブジェクトを取得します。
            /// </summary>
            public OrganizationBase Organization { get; }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="name">垂直通貫の組織名</param>
            /// <param name="organization">組織オブジェクト</param>
            internal NameAndOrganization(string name, OrganizationBase organization)
            {
                Name = name;
                Organization = organization;
            }
        }
    }
}
