using Entity.DomainService.OrganizationVisitor;
using Entity.Organization;
using Entity.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Entity_Test.DomainService_Test
{
    public class SetBossVisitor_Test
    {
        [Fact]
        public void Visit()
        {
            var boss = new Person(new("target", "target"), new(1000, 1, 1));
            var newBoss = new Person(new("target", "target"), new(1000, 1, 1));

            // 組織構築
            var a = new TerminalOrganization(new("1"));
            a.AddMember(new Person(new("1", "1"), new(1000, 1, 1)));
            a.AddMember(new Person(new("1", "2"), new(1000, 1, 1)));
            a.AddMember(new Person(new("1", "3"), new(1000, 1, 1)));
            var b = new TerminalOrganization(new("2"));
            b.SetBoss(boss);
            b.AddMember(new Person(new("2", "1"), new(1000, 1, 1)));
            var c = new ManagementOrganization(new("3"), Ranks.Section, new() { a, b });
            c.AddMember(new Person(new("3", "1"), new(1000, 1, 1)));
            c.AddMember(new Person(new("3", "2"), new(1000, 1, 1)));
            var top = new ManagementOrganization(new("4"), Ranks.Department, new() { c });
            top.AddMember(new Person(new("4", "1"), new(1000, 1, 1)));
            top.AddMember(new Person(new("4", "2"), new(1000, 1, 1)));

            Assert.True(b.IsBoss(boss));
            Assert.False(b.IsBoss(newBoss));

            var visitor = new SetBossVisitor(newBoss, b);
            top.Accept(visitor);

            Assert.False(b.IsBoss(boss));
            Assert.True(b.IsBoss(newBoss));
        }
    }
}
