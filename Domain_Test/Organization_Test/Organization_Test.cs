using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Entity.Organization;
using Entity.Service;
using Entity.DomainService.OrganizationVisitor;
using Entity.DomainService;
using Entity.Persons;

namespace Entity_Test.Organization_Test
{
    public class Organization_Test
    {
        [Fact]
        public void Clone()
        {
            var a = new Organization(new BuilderMock());
            var b = a.Clone();

            Assert.NotEqual(a, b);
            Assert.False(a == b);

            var aList = a.GetOrganizationInfos();
            var bList = b.GetOrganizationInfos();

            Assert.True(aList.Count == bList.Count);
            Assert.True(aList[2].FullName == bList[2].FullName);
        }

        [Fact]
        public void 組織一覧取得()
        {
            var a = new Organization(new BuilderMock());
            var aList = a.GetOrganizationInfos();

            var b = new BuilderMock().Build();
            var visitor = new GetOrganizationListVisitor();
            b.Accept(visitor);

            Assert.True(aList.Count == visitor.Oganizations.Count);
            Assert.True(aList[2].FullName == visitor.Oganizations[2].FullName);
        }

        [Fact]
        public void 所属組織に社員追加()
        {
            var builder = new BuilderMock();
            var targetOrganization = builder.TestTargetOrganization1;
            var targetPerson = new Person(new("aaa", "aaa"), new(1000, 1, 1));

            var organization = new Organization(builder);

            organization.RelocateEmployee(targetPerson, targetOrganization);

            Assert.True(targetOrganization.SameIdentityAs(organization.GetAssignedOrganization(targetPerson)));
        }

        [Fact]
        public void 所属組織に社員追加_例外()
        {
            var builder = new BuilderMock();
            //var targetOrganization = builder.TestTargetOrganization;
            var notTargetOrganization = new TerminalOrganization(new("error"));
            var targetPerson = new Person(new("aaa", "aaa"), new(1000, 1, 1));

            var organization = new Organization(builder);

            try
            {
                organization.RelocateEmployee(targetPerson, notTargetOrganization);
            }
            catch (ArgumentException)
            {
                return;
            }
            catch (NotImplementedException)
            {
                Assert.Fail();
                return;
            }

            Assert.Fail();
        }

        [Fact]
        public void 社員が異動()
        {
            var builder = new BuilderMock();
            var targetOrganization = builder.TestTargetOrganization1;
            var targetOrganization2 = builder.TestTargetOrganization2;
            var targetPerson = new Person(new("aaa", "aaa"), new(1000, 1, 1));

            var organization = new Organization(builder);

            organization.RelocateEmployee(targetPerson, targetOrganization);
            organization.RelocateEmployee(targetPerson, targetOrganization2);

            Assert.False(targetOrganization.SameIdentityAs(organization.GetAssignedOrganization(targetPerson)));
            Assert.True(targetOrganization2.SameIdentityAs(organization.GetAssignedOrganization(targetPerson)));

            organization.RelocateEmployee(targetPerson, targetOrganization);

            Assert.True(targetOrganization.SameIdentityAs(organization.GetAssignedOrganization(targetPerson)));
            Assert.False(targetOrganization2.SameIdentityAs(organization.GetAssignedOrganization(targetPerson)));
        }

        [Fact]
        public void 社員の所属組織取得()
        {
            var builder = new BuilderMock();
            var targetOrganization = builder.TestTargetOrganization1;
            var targetPerson = new Person(new("aaa", "aaa"), new(1000, 1, 1));
            var organization = new Organization(builder);

            organization.RelocateEmployee(targetPerson, targetOrganization);

            Assert.True(targetOrganization.SameIdentityAs(organization.GetAssignedOrganization(targetPerson)));
        }

        [Fact]
        public void 社員の所属組織取得_例外()
        {
            var builder = new BuilderMock();
            var targetPerson = new Person(new("aaa", "aaa"), new(1000, 1, 1));
            var organization = new Organization(builder);

            try
            {
                // 追加しないで取得
                organization.GetAssignedOrganization(targetPerson);
            }
            catch (ArgumentException)
            {
                return;
            }
            catch (NotImplementedException)
            {
                Assert.Fail();
                return;
            }

            Assert.Fail();
        }

        [Fact]
        public void 新組織に組織長設定()
        {
            var builder = new BuilderMock();
            var targetOrganization = builder.TestTargetOrganization1;
            var boss = new Person(new("aaa", "aaa"), new(1000, 1, 1));
            var organization = new Organization(builder);

            organization.OnKickedOutOldBoss += (args) => Assert.Fail();
            organization.SetBoss(boss, targetOrganization);

            Assert.True(boss.SameIdentityAs(organization.GetBoss(targetOrganization)));
        }

        [Fact]
        public void 新組織に組織長設定_例外()
        {
            var builder = new BuilderMock();
            var targetOrganization = new ManagementOrganization(new("aa"), Lanks.Department, new());
            var boss = new Person(new("aaa", "aaa"), new(1000, 1, 1));
            var organization = new Organization(builder);

            organization.OnKickedOutOldBoss += (args) => Assert.Fail();
            try
            {
                organization.SetBoss(boss, targetOrganization);
            }
            catch (ArgumentException)
            {
                return;
            }
            catch
            {
                Assert.Fail();
            }

            Assert.Fail();
        }

        [Fact]
        public void 組織長を取得()
        {
            var builder = new BuilderMock();
            var targetOrganization = builder.TestTargetOrganization1;
            var boss = new Person(new("aaa", "aaa"), new(1000, 1, 1));
            var organization = new Organization(builder);

            organization.OnKickedOutOldBoss += (args) => Assert.Fail();
            organization.SetBoss(boss, targetOrganization);

            Assert.True(boss.SameIdentityAs(organization.GetBoss(targetOrganization)));
        }

        [Fact]
        public void 組織長を取得_例外()
        {
            var builder = new BuilderMock();
            var targetOrganization = builder.TestTargetOrganization1;
            var boss = new Person(new("aaa", "aaa"), new(1000, 1, 1));
            var organization = new Organization(builder);

            try
            {
                Assert.True(boss.SameIdentityAs(organization.GetBoss(targetOrganization)));
            }
            catch (ArgumentException)
            {
                return;
            }
            catch
            {
                Assert.Fail();
            }

            Assert.Fail();
        }

        [Fact]
        public void 社員の役職取得_直属社員()
        {
            var builder = new BuilderMock();
            var targetOrganization = builder.TestTargetOrganization1;
            var targetPerson = new Person(new("aaa", "aaa"), new(1000, 1, 1));
            var organization = new Organization(builder);

            organization.RelocateEmployee(targetPerson, targetOrganization);

            Assert.Equal(Posts.Employee, organization.GetPost(targetPerson));
        }

        [Fact]
        public void 社員の役職取得_Boss()
        {
            var builder = new BuilderMock();
            var targetOrganization = builder.TestTargetOrganization1;
            var targetPerson = new Person(new("aaa", "aaa"), new(1000, 1, 1));
            var organization = new Organization(builder);

            organization.SetBoss(targetPerson, targetOrganization);

            Assert.Equal(Posts.Chief, organization.GetPost(targetPerson));
        }

        [Fact]
        public void 社員の役職取得_例外()
        {
            var builder = new BuilderMock();
            var targetOrganization = builder.TestTargetOrganization1;
            var targetPerson = new Person(new("aaa", "aaa"), new(1000, 1, 1));
            var targetPerson2 = new Person(new("aaa", "aaa"), new(1000, 1, 1));
            var organization = new Organization(builder);

            organization.RelocateEmployee(targetPerson, targetOrganization);

            try
            {
                Assert.Equal(Posts.Employee, organization.GetPost(targetPerson2));
            }
            catch (ArgumentException)
            {
                return;
            }
            catch
            {
                Assert.Fail();
            }
            Assert.Fail();

        }

        [Fact]
        public void 社員の退社()
        {
            var builder = new BuilderMock();
            var targetOrganization = builder.TestTargetOrganization1;
            var targetPerson = new Person(new("aaa", "aaa"), new(1000, 1, 1));
            var organization = new Organization(builder);

            organization.RelocateEmployee(targetPerson, targetOrganization);

            organization.Leave(targetPerson);

            try
            {
                organization.GetPost(targetPerson);
            }
            catch (ArgumentException)
            {
                return;
            }
            catch
            {
                Assert.Fail();
            }
            Assert.Fail();
        }

        [Fact]
        public void 社員の退社_例外()
        {
            var builder = new BuilderMock();
            var targetPerson = new Person(new("aaa", "aaa"), new(1000, 1, 1));
            var organization = new Organization(builder);

            try
            {
                organization.Leave(targetPerson);
            }
            catch (ArgumentException)
            {
                return;
            }
            catch
            {
                Assert.Fail();
            }
            Assert.Fail();
        }

        [Fact]
        public void 組織長の退社()
        {
            var builder = new BuilderMock();
            var targetOrganization = builder.TestTargetOrganization1;
            var targetBoss = new Person(new("aaa", "aaa"), new(1000, 1, 1));
            var organization = new Organization(builder);

            organization.SetBoss(targetBoss, targetOrganization);

            organization.OnBecameVacantBossPosition += (args) =>
            {
                Assert.True(args.Organization.SameIdentityAs(targetOrganization));
            };
            organization.Leave(targetBoss);

            // 例外が出ることが正解
            try
            {
                organization.GetPost(targetBoss);
            }
            catch (ArgumentException)
            {
                return;
            }
            catch
            {
                Assert.Fail();
            }
            Assert.Fail();
        }

        [Fact]
        public void 組織長の退社_例外()
        {
            var builder = new BuilderMock();
            var targetOrganization = builder.TestTargetOrganization1;
            var targetBoss = new Person(new("aaa", "aaa"), new(1000, 1, 1));
            var organization = new Organization(builder);

            try
            {
                organization.OnBecameVacantBossPosition += (args) =>
                {
                    Assert.Fail();
                };
                organization.Leave(targetBoss);
            }
            catch (ArgumentException)
            {
                return;
            }
            catch
            {
                Assert.Fail();
            }
            Assert.Fail();
        }

        [Fact]
        public void 組織長交代()
        {
            var builder = new BuilderMock();
            var targetOrganization = builder.TestTargetOrganization1;
            var boss = new Person(new("aaa", "aaa"), new(1000, 1, 1));
            var boss2 = new Person(new("aaa", "aaa"), new(1000, 1, 1));
            var organization = new Organization(builder);

            organization.OnKickedOutOldBoss += (args) =>
            {
                Assert.True(args.OldBoss.SameIdentityAs(boss));
            };
            organization.SetBoss(boss, targetOrganization);
            organization.SetBoss(boss2, targetOrganization);

            Assert.True(boss2.SameIdentityAs(organization.GetBoss(targetOrganization)));
        }

        private class BuilderMock : IOrganizationBuilder
        {
            #region Constants -------------------------------------------------------------------------------------

            #endregion --------------------------------------------------------------------------------------------

            #region Fields ----------------------------------------------------------------------------------------

            #endregion --------------------------------------------------------------------------------------------

            #region Properties ------------------------------------------------------------------------------------

            public OrganizationBase TestTargetOrganization1 { get; } = new TerminalOrganization(new("2"));

            public OrganizationBase TestTargetOrganization2 { get; } = new TerminalOrganization(new("5"));

            #endregion --------------------------------------------------------------------------------------------

            #region Events ----------------------------------------------------------------------------------------

            #endregion --------------------------------------------------------------------------------------------

            #region Constructor -----------------------------------------------------------------------------------

            #endregion --------------------------------------------------------------------------------------------

            #region Methods ---------------------------------------------------------------------------------------

            #region Methods - public ------------------------------------------------------------------------------

            public OrganizationBase Build()
            {
                // 組織構築
                var a = new TerminalOrganization(new("1"));
                var b = TestTargetOrganization1;
                var c = new ManagementOrganization(new("3"), Lanks.Section, new() { a, b });
                var d = new TerminalOrganization(new("4"));
                var e = TestTargetOrganization2;
                var f = new ManagementOrganization(new("6"), Lanks.Section, new() { d, e });
                var top = new ManagementOrganization(new("7"), Lanks.Department, new() { c, f });

                return top;
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
}
