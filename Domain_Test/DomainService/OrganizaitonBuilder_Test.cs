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
            public string NameList { get; private set; } = string.Empty;
            public void Visit(OrganizationBase target)
            {
                NameList += target.DisplayName + Environment.NewLine;
            }
        }
    }
}
