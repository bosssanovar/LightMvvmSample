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
    public class AddDirectEmployeeVisitor_Test
    {
        [Fact]
        public void Visit()
        {
            var target = new Person(new("target", "target"), new(1000, 1, 1));
            var target2 = new Person(new("target", "target"), new(1000, 1, 1));

            // 組織構築
            var a = new TerminalOrganization(new("1"));
            a.SetBoss(new Person(new("1", "boss"), new(1000, 1, 1)));
            a.AddMember(new Person(new("1", "1"), new(1000, 1, 1)));
            a.AddMember(new Person(new("1", "2"), new(1000, 1, 1)));
            a.AddMember(new Person(new("1", "3"), new(1000, 1, 1)));
            var b = new TerminalOrganization(new("2"));
            b.SetBoss(new Person(new("2", "boss"), new(1000, 1, 1)));
            b.AddMember(new Person(new("2", "1"), new(1000, 1, 1)));
            //b.AddMember(target2);
            var c = new ManagementOrganization(new("3"), Ranks.Section, new() { a, b });
            c.SetBoss(new Person(new("3", "boss"), new(1000, 1, 1)));
            c.AddMember(new Person(new("3", "1"), new(1000, 1, 1)));
            c.AddMember(new Person(new("3", "2"), new(1000, 1, 1)));
            //c.AddMember(target); // ※ target here!
            var top = new ManagementOrganization(new("4"), Ranks.Department, new() { c });
            top.SetBoss(new Person(new("4", "boss"), new(1000, 1, 1)));
            top.AddMember(new Person(new("4", "1"), new(1000, 1, 1)));
            top.AddMember(new Person(new("4", "2"), new(1000, 1, 1)));

            Assert.False(c.IsContainDirectEmployee(target));
            Assert.False(b.IsContainDirectEmployee(target2));

            var visitor = new AddDirectEmployeeVisitor(target, c);
            top.Accept(visitor);

            Assert.True(c.IsContainDirectEmployee(target));
            Assert.False(b.IsContainDirectEmployee(target2));

            visitor = new AddDirectEmployeeVisitor(target2, b);
            top.Accept(visitor);

            Assert.True(c.IsContainDirectEmployee(target));
            Assert.True(b.IsContainDirectEmployee(target2));

        }
    }
}
