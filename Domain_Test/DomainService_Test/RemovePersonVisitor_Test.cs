using Entity.Organization;
using Entity.Persons;
using Entity.Service.OrganizationVisitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Entity_Test.DomainService_Test
{
    public class RemovePersonVisitor_Test
    {
        [Fact]
        public void Visit()
        {
            var target = new Person(new("target", "target"), new(1000, 1, 1));
            var target2 = new Person(new("target", "target"), new(1000, 1, 1));
            var targetBoss = new Person(new("target", "target"), new(1000, 1, 1));

            // 組織構築
            var a = new TerminalOrganization(new("1"));
            a.AddMember(new Person(new("1", "1"), new(1000, 1, 1)));
            a.AddMember(new Person(new("1", "2"), new(1000, 1, 1)));
            a.AddMember(new Person(new("1", "3"), new(1000, 1, 1)));
            var b = new TerminalOrganization(new("2"));
            b.AddMember(new Person(new("2", "1"), new(1000, 1, 1)));
            b.AddMember(target2);
            var c = new ManagementOrganization(new("3"), Lanks.Section, new(){ a, b });
            c.AddMember(new Person(new("3", "1"), new(1000, 1, 1)));
            c.AddMember(new Person(new("3", "2"), new(1000, 1, 1)));
            c.AddMember(target); // ※ target here!
            c.SetBoss(targetBoss);
            var top = new ManagementOrganization(new("4"), Lanks.Department, new() { c });
            top.AddMember(new Person(new("4", "1"), new(1000, 1, 1)));
            top.AddMember(new Person(new("4", "2"), new(1000, 1, 1)));

            Assert.True(c.IsContainDirectEmployee(target));
            Assert.True(b.IsContainDirectEmployee(target2));
            Assert.True(c.IsBoss(targetBoss));

            var visitor = new RemovePersonVisitor(target);
            top.Accept(visitor);

            Assert.False(c.IsContainDirectEmployee(target));
            Assert.True(b.IsContainDirectEmployee(target2));
            Assert.True(c.IsBoss(targetBoss));

            visitor = new RemovePersonVisitor(target2);
            top.Accept(visitor);

            Assert.False(c.IsContainDirectEmployee(target));
            Assert.False(b.IsContainDirectEmployee(target2));
            Assert.True(c.IsBoss(targetBoss));

            visitor = new RemovePersonVisitor(targetBoss);
            top.Accept(visitor);

            Assert.False(c.IsContainDirectEmployee(target));
            Assert.False(b.IsContainDirectEmployee(target2));
            Assert.False(c.IsBoss(targetBoss));
        }
    }
}
