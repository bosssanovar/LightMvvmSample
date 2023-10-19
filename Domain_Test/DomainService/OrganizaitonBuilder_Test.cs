using Entity.Organization;
using Entity.Service;
using Entity.Service.OrganizationVisitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Entity_Test.DomainService
{
    public class OrganizaitonBuilder_Test
    {
        [Fact]
        public void Build()
        {
            var top = OrganizaitonBuilder.Build();

            var visitor = new NameListVisitor();
            top.Accept(visitor);
        }

        private class NameListVisitor : IOrganizationVisitor
        {
            #region Constants -------------------------------------------------------------------------------------

            #endregion --------------------------------------------------------------------------------------------

            #region Fields ----------------------------------------------------------------------------------------

            private ManagementOrganization? _upperDepartment;

            private ManagementOrganization? _upperSection;

            private readonly StringBuilder _sb = new();

            #endregion --------------------------------------------------------------------------------------------

            #region Properties ------------------------------------------------------------------------------------
            public string NameList
            {
                get
                {
                    return _sb.ToString();
                }
            }

            #endregion --------------------------------------------------------------------------------------------

            #region Events ----------------------------------------------------------------------------------------

            #endregion --------------------------------------------------------------------------------------------

            #region Constructor -----------------------------------------------------------------------------------

            #endregion --------------------------------------------------------------------------------------------

            #region Methods ---------------------------------------------------------------------------------------

            #region Methods - public ------------------------------------------------------------------------------
            public void Visit(OrganizationBase target)
            {
                if (target is ManagementOrganization m)
                {
                    Visit(m);
                }
                else if(target is TerminalOrganization t)
                {
                    Visit(t);
                }
            }

            public void Visit(ManagementOrganization organization)
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
                    throw new ArgumentException();
                }

                StringBuilder sb = GetNameUntilSection();
                sb.Append(Environment.NewLine);
                _sb.Append(sb);
            }

            public void Visit(TerminalOrganization organization)
            {
                StringBuilder sb = GetNameUntilSection();
                sb.Append(' ');
                sb.Append(organization.DisplayName);
                sb.Append(Environment.NewLine);
                _sb.Append(sb);
            }

            #endregion --------------------------------------------------------------------------------------------

            #region Methods - internal ----------------------------------------------------------------------------

            #endregion --------------------------------------------------------------------------------------------

            #region Methods - protected ---------------------------------------------------------------------------

            #endregion --------------------------------------------------------------------------------------------

            #region Methods - private -----------------------------------------------------------------------------

            private StringBuilder GetNameUntilSection()
            {
                var sb = new StringBuilder();
                sb.Append(_upperDepartment?.DisplayName);
                sb.Append(' ');
                sb.Append(_upperSection?.DisplayName);
                return sb;
            }

            #endregion --------------------------------------------------------------------------------------------

            #region Methods - override ----------------------------------------------------------------------------

            #endregion --------------------------------------------------------------------------------------------

            #endregion --------------------------------------------------------------------------------------------
        }
    }
}
