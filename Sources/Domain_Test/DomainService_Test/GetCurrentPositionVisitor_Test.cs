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
    public class GetCurrentPositionVisitor_Test
    {
        [Fact]
        public void Visitor()
        {
            var target = new Person(new("3", "3"), new(1000, 1, 1));
            var target2 = new Person(new("2", "2"), new(1000, 1, 1));
            var targetBoss = new Person(new("1", "boss"), new(1000, 1, 1));

            // 組織構築
            var a = new TerminalOrganization(new("1"));
            a.SetBoss(targetBoss);
            a.AddMember(new Person(new("1", "1"), new(1000, 1, 1)));
            a.AddMember(new Person(new("1", "2"), new(1000, 1, 1)));
            a.AddMember(new Person(new("1", "3"), new(1000, 1, 1)));
            var b = new TerminalOrganization(new("2"));
            b.AddMember(new Person(new("2", "1"), new(1000, 1, 1)));
            b.AddMember(target2);
            var c = new ManagementOrganization(new("3"), Ranks.Section, new() { a, b });
            c.AddMember(new Person(new("3", "1"), new(1000, 1, 1)));
            c.AddMember(new Person(new("3", "2"), new(1000, 1, 1)));
            c.AddMember(target);
            var top = new ManagementOrganization(new("4"), Ranks.Department, new() { c });
            top.AddMember(new Person(new("4", "1"), new(1000, 1, 1)));
            top.AddMember(new Person(new("4", "2"), new(1000, 1, 1)));

            var visitor = new GetCurrentPositionVisitor(target);
            top.Accept(visitor);

            Assert.True(visitor.AssignedOrganization.SameIdentityAs(c));
            Assert.True(visitor.Post == Posts.Employee);

            visitor = new GetCurrentPositionVisitor(target2);
            top.Accept(visitor);

            Assert.True(visitor.AssignedOrganization.SameIdentityAs(b));
            Assert.True(visitor.Post == Posts.Employee);

            visitor = new GetCurrentPositionVisitor(targetBoss);
            top.Accept(visitor);

            Assert.True(visitor.AssignedOrganization.SameIdentityAs(a));
            Assert.True(visitor.Post == Posts.Chief);
        }
    }
}
