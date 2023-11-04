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
        public void 組織一覧取得()
        {
            var a = new Organization(new BuilderMock_組織構造のみ());
            var aList = a.GetOrganizationInfos();

            var b = new BuilderMock_組織構造のみ().Build();
            var visitor = new GetOrganizationListVisitor();
            b.Accept(visitor);

            Assert.True(aList.Count == visitor.Oganizations.Count);
            Assert.True(aList[2].FullName == visitor.Oganizations[2].FullName);
        }

        [Fact]
        public void 新社員追加()
        {
            var builder = new BuilderMock_組織構造のみ();
            var organization = new Organization(builder);
            var targetPerson = new Person(new("aaa", "aaa"), new(1000, 1, 1));

            organization.AddNewMember(targetPerson);

            Assert.Equal("無所属", organization.GetOrganizationName(targetPerson));
        }

        [Fact]
        public void 所属組織に社員追加()
        {
            var builder = new BuilderMock_組織構造のみ();
            var organization = new Organization(builder);
            var targetOrganization = builder.TestTargetOrganization1;
            var targetPerson = new Person(new("aaa", "aaa"), new(1000, 1, 1));

            organization.AddNewMember(targetPerson);

            organization.RelocateEmployee(targetPerson, targetOrganization);

            Assert.True(targetOrganization.SameIdentityAs(organization.GetAssignedOrganization(targetPerson)));
        }

        [Fact]
        public void 所属組織に社員追加_例外()
        {
            var builder = new BuilderMock_組織構造のみ();
            var organization = new Organization(builder);
            //var targetOrganization = builder.TestTargetOrganization;
            var notTargetOrganization = new TerminalOrganization(new("error"));
            var targetPerson = new Person(new("aaa", "aaa"), new(1000, 1, 1));

            organization.AddNewMember(targetPerson);

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
            var builder = new BuilderMock_組織構造のみ();
            var organization = new Organization(builder);
            var targetOrganization = builder.TestTargetOrganization1;
            var targetOrganization2 = builder.TestTargetOrganization2;
            var targetPerson = new Person(new("aaa", "aaa"), new(1000, 1, 1));

            organization.AddNewMember(targetPerson);

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
            var builder = new BuilderMock_組織構造のみ();
            var organization = new Organization(builder);
            var targetOrganization = builder.TestTargetOrganization1;
            var targetPerson = new Person(new("aaa", "aaa"), new(1000, 1, 1));

            organization.AddNewMember(targetPerson);

            organization.RelocateEmployee(targetPerson, targetOrganization);

            Assert.True(targetOrganization.SameIdentityAs(organization.GetAssignedOrganization(targetPerson)));
        }

        [Fact]
        public void 未所属社員の所属組織取得()
        {
            var builder = new BuilderMock_組織構造のみ();
            var organization = new Organization(builder);
            var targetPerson = new Person(new("aaa", "aaa"), new(1000, 1, 1));

            organization.AddNewMember(targetPerson);

            var o = organization.GetAssignedOrganization(targetPerson);

            Assert.IsType<UnAssignedMembersGroup>(o);
        }

        [Fact]
        public void 新組織に組織長設定()
        {
            var builder = new BuilderMock_組織構造のみ();
            var organization = new Organization(builder);
            var targetOrganization = builder.TestTargetOrganization1;
            var boss = new Person(new("aaa", "aaa"), new(1000, 1, 1));

            organization.AddNewMember(boss);

            organization.SetBoss(boss, targetOrganization);

            Assert.True(boss.SameIdentityAs(organization.GetBoss(targetOrganization)));
        }

        [Fact]
        public void 新組織に組織長設定_例外()
        {
            var builder = new BuilderMock_組織構造のみ();
            var organization = new Organization(builder);
            var targetOrganization = new ManagementOrganization(new("aa"), Ranks.Department, new());
            var boss = new Person(new("aaa", "aaa"), new(1000, 1, 1));

            organization.AddNewMember(boss);

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
            var builder = new BuilderMock_組織構造のみ();
            var organization = new Organization(builder);
            var targetOrganization = builder.TestTargetOrganization1;
            var boss = new Person(new("aaa", "aaa"), new(1000, 1, 1));

            organization.AddNewMember(boss);

            organization.SetBoss(boss, targetOrganization);

            Assert.True(boss.SameIdentityAs(organization.GetBoss(targetOrganization)));
        }

        [Fact]
        public void 組織長を取得_例外()
        {
            var builder = new BuilderMock_組織構造のみ();
            var organization = new Organization(builder);
            var targetOrganization = builder.TestTargetOrganization1;
            var boss = new Person(new("aaa", "aaa"), new(1000, 1, 1));

            organization.AddNewMember(boss);

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
            var builder = new BuilderMock_組織構造のみ();
            var organization = new Organization(builder);
            var targetOrganization = builder.TestTargetOrganization1;
            var targetPerson = new Person(new("aaa", "aaa"), new(1000, 1, 1));

            organization.AddNewMember(targetPerson);

            organization.RelocateEmployee(targetPerson, targetOrganization);

            Assert.Equal(Posts.Employee, organization.GetPost(targetPerson));
        }

        [Fact]
        public void 社員の役職取得_無所属()
        {
            var builder = new BuilderMock_組織構造のみ();
            var organization = new Organization(builder);
            var targetPerson = new Person(new("aaa", "aaa"), new(1000, 1, 1));

            organization.AddNewMember(targetPerson);

            Assert.Equal(Posts.Employee, organization.GetPost(targetPerson));
        }

        [Fact]
        public void 社員の役職取得_Boss()
        {
            var builder = new BuilderMock_組織構造のみ();
            var organization = new Organization(builder);
            var targetOrganization = builder.TestTargetOrganization1;
            var targetPerson = new Person(new("aaa", "aaa"), new(1000, 1, 1));

            organization.AddNewMember(targetPerson);

            organization.SetBoss(targetPerson, targetOrganization);

            Assert.Equal(Posts.Chief, organization.GetPost(targetPerson));
        }

        [Fact]
        public void 社員の役職取得_例外()
        {
            var builder = new BuilderMock_組織構造のみ();
            var organization = new Organization(builder);
            var targetOrganization = builder.TestTargetOrganization1;
            var targetPerson = new Person(new("aaa", "aaa"), new(1000, 1, 1));
            var targetPerson2 = new Person(new("aaa", "aaa"), new(1000, 1, 1));

            organization.AddNewMember(targetPerson);

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
        public void 社員の所属組織名を取得()
        {
            var builder = new BuilderMock_組織構造のみ();
            var organization = new Organization(builder);
            var targetOrganization = builder.TestTargetOrganization1;
            var targetPerson = new Person(new("aaa", "aaa"), new(1000, 1, 1));

            organization.AddNewMember(targetPerson);

            organization.RelocateEmployee(targetPerson, targetOrganization);

            Assert.Equal(builder.TestTargetOrganization1Name, organization.GetOrganizationName(targetPerson));
        }

        [Fact]
        public void 社員の所属組織名を取得_無所属()
        {
            var builder = new BuilderMock_組織構造のみ();
            var organization = new Organization(builder);
            var targetPerson = new Person(new("aaa", "aaa"), new(1000, 1, 1));

            organization.AddNewMember(targetPerson);

            Assert.Equal("無所属", organization.GetOrganizationName(targetPerson));
        }

        [Fact]
        public void 社員の所属組織名を取得_例外()
        {
            var builder = new BuilderMock_組織構造のみ();
            var organization = new Organization(builder);
            var targetPerson = new Person(new("aaa", "aaa"), new(1000, 1, 1));

            try
            {
                var _ = organization.GetOrganizationName(targetPerson);
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
            var builder = new BuilderMock_組織構造のみ();
            var organization = new Organization(builder);
            var targetOrganization = builder.TestTargetOrganization1;
            var targetPerson = new Person(new("aaa", "aaa"), new(1000, 1, 1));

            organization.AddNewMember(targetPerson);

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
        public void 社員の退社_配属前()
        {
            var builder = new BuilderMock_組織構造のみ();
            var organization = new Organization(builder);
            var targetPerson = new Person(new("aaa", "aaa"), new(1000, 1, 1));

            organization.AddNewMember(targetPerson);

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
            var builder = new BuilderMock_組織構造のみ();
            var organization = new Organization(builder);
            var targetPerson = new Person(new("aaa", "aaa"), new(1000, 1, 1));

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
            var builder = new BuilderMock_組織構造のみ();
            var organization = new Organization(builder);
            var targetOrganization = builder.TestTargetOrganization1;
            var targetBoss = new Person(new("aaa", "aaa"), new(1000, 1, 1));

            organization.AddNewMember(targetBoss);

            organization.SetBoss(targetBoss, targetOrganization);

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
        public void 組織長交代()
        {
            var builder = new BuilderMock_組織構造のみ();
            var organization = new Organization(builder);
            var targetOrganization = builder.TestTargetOrganization1;
            var boss = new Person(new("aaa", "aaa"), new(1000, 1, 1));
            var boss2 = new Person(new("aaa", "aaa"), new(1000, 1, 1));

            organization.AddNewMember(boss);
            organization.AddNewMember(boss2);

            organization.SetBoss(boss, targetOrganization);
            organization.SetBoss(boss2, targetOrganization);

            Assert.True(boss2.SameIdentityAs(organization.GetBoss(targetOrganization)));
        }

        [Fact]
        public void 昇進_平から主任_同じチーム()
        {
            var builder = new BuilderMock_組織構造のみ();
            var organization = new Organization(builder);
            var targetOrganization = builder.TestTargetOrganization1;
            var person = new Person(new("aaa", "aaa"), new(1000, 1, 1));

            organization.AddNewMember(person);

            organization.RelocateEmployee(person, targetOrganization);

            Assert.True(targetOrganization.SameIdentityAs(organization.GetAssignedOrganization(person)));
            Assert.Equal(Posts.Employee, organization.GetPost(person));

            organization.SetBoss(person, targetOrganization);

            Assert.True(targetOrganization.SameIdentityAs(organization.GetAssignedOrganization(person)));
            Assert.Equal(Posts.Chief, organization.GetPost(person));
        }

        [Fact]
        public void 昇進_平から主任_別チーム()
        {
            var builder = new BuilderMock_組織構造のみ();
            var organization = new Organization(builder);
            var targetOrganization = builder.TestTargetOrganization1;
            var targetOrganization2 = builder.TestTargetOrganization2;
            var person = new Person(new("aaa", "aaa"), new(1000, 1, 1));

            organization.AddNewMember(person);

            organization.RelocateEmployee(person, targetOrganization);

            Assert.True(targetOrganization.SameIdentityAs(organization.GetAssignedOrganization(person)));
            Assert.False(targetOrganization2.SameIdentityAs(organization.GetAssignedOrganization(person)));
            Assert.Equal(Posts.Employee, organization.GetPost(person));

            organization.SetBoss(person, targetOrganization2);

            Assert.False(targetOrganization.SameIdentityAs(organization.GetAssignedOrganization(person)));
            Assert.True(targetOrganization2.SameIdentityAs(organization.GetAssignedOrganization(person)));
            Assert.Equal(Posts.Chief, organization.GetPost(person));
        }

        [Fact]
        public void 昇進_主任から課長_同じ課()
        {
            var builder = new BuilderMock_組織構造のみ();
            var organization = new Organization(builder);
            var targetOrganization = builder.TestTargetOrganization2;
            var targetOrganizationNext = builder.TestTargetOrganization3;
            var person = new Person(new("aaa", "aaa"), new(1000, 1, 1));

            organization.AddNewMember(person);

            organization.SetBoss(person, targetOrganization);

            Assert.True(targetOrganization.SameIdentityAs(organization.GetAssignedOrganization(person)));
            Assert.Equal(Posts.Chief, organization.GetPost(person));

            organization.SetBoss(person, targetOrganizationNext);

            Assert.False(targetOrganization.SameIdentityAs(organization.GetAssignedOrganization(person)));
            Assert.True(targetOrganizationNext.SameIdentityAs(organization.GetAssignedOrganization(person)));
            Assert.Equal(Posts.SectionChief, organization.GetPost(person));

            organization.Leave(person);

            try
            {
                var o = organization.GetAssignedOrganization(person);
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
        public void 組織人員問題検出_長不在組織あり()
        {
            var builder = new BuilderMock_一部組織人員問題あり();
            var organization = new Organization(builder);
            ICheckProblem problemChecker = organization;

            var unAssignedPersons = problemChecker.GetUnAssignedPersons();
            Assert.Empty(unAssignedPersons);
            var noBossOrganizations = problemChecker.GetNoBossOrganizaiotns();
            Assert.Single(noBossOrganizations);
            Assert.Contains(builder.NoBossOrganization, noBossOrganizations);
        }

        [Fact]
        public void 組織人員問題検出_長不在組織_And_無所属社員あり()
        {
            var builder = new BuilderMock_一部組織人員問題あり();
            var organization = new Organization(builder);
            ICheckProblem problemChecker = organization;

            // 準備
            var addPerson = new Person(new("aaaa", "bbbbbb"), new(1000, 1, 1));
            organization.AddNewMember(addPerson);

            // 評価
            var unAssignedPersons = problemChecker.GetUnAssignedPersons();
            Assert.Single(unAssignedPersons);
            Assert.Contains(unAssignedPersons, x => x.SameIdentityAs(addPerson));
            var noBossOrganizations = problemChecker.GetNoBossOrganizaiotns();
            Assert.Single(noBossOrganizations);
            Assert.Contains(noBossOrganizations, x => x.SameIdentityAs(builder.NoBossOrganization));
        }

        [Fact]
        public void 組織人員問題検出_無所属社員あり()
        {
            var builder = new BuilderMock_一部組織人員問題あり();
            var organization = new Organization(builder);
            ICheckProblem problemChecker = organization;
            IAssign assigner = organization;

            // 準備
            var addPerson = new Person(new("aaaa", "bbbbbb"), new(1000, 1, 1));
            var boss = new Person(new("aaaa", "bbbbbb"), new(1000, 1, 1));
            organization.AddNewMember(addPerson);
            organization.AddNewMember(boss);
            assigner.Assign(boss, builder.NoBossOrganization, true);

            // 評価
            var unAssignedPersons = problemChecker.GetUnAssignedPersons();
            Assert.Single(unAssignedPersons);
            Assert.Contains(unAssignedPersons, x => x.SameIdentityAs(addPerson));
            var noBossOrganizations = problemChecker.GetNoBossOrganizaiotns();
            Assert.Empty(noBossOrganizations);
            Assert.DoesNotContain(noBossOrganizations, x => x.SameIdentityAs(builder.NoBossOrganization));
        }

        [Fact]
        public void 組織構造情報()
        {
            var builder = new BuilderMock_組織情報収集用();
            var organization = new Organization(builder);

            var structure = organization.GetOrganizationStructure();

            Assert.True(structure != null);
        }

        private class BuilderMock_組織構造のみ : IOrganizationBuilder
        {
            public OrganizationBase TestTargetOrganization1 { get; private set; }

            public OrganizationBase TestTargetOrganization2 { get; private set; }

            public OrganizationBase TestTargetOrganization3 { get; private set; }

            public string TestTargetOrganization1Name { get; private set; }

            public OrganizationBase Build()
            {
                // 組織構築
                var a = new TerminalOrganization(new("1"));
                var b = new TerminalOrganization(new("2"));
                var c = new ManagementOrganization(new("3"), Ranks.Section, new() { a, b });
                var d = new TerminalOrganization(new("4"));
                var e = new TerminalOrganization(new("5"));
                var f = new ManagementOrganization(new("6"), Ranks.Section, new() { d, e });
                var top = new ManagementOrganization(new("7"), Ranks.Department, new() { c, f });

                TestTargetOrganization1 = b;
                TestTargetOrganization2 = e;
                TestTargetOrganization3 = f;

                TestTargetOrganization1Name = top.DisplayName + " " + c.DisplayName + " " + b.DisplayName;

                return top;
            }
        }

        private class BuilderMock_一部組織人員問題あり : IOrganizationBuilder
        {
            public OrganizationBase NoBossOrganization { get; private set; }

            public Person AssignedPerson { get; private set; }

            public OrganizationBase Build()
            {
                AssignedPerson = new(new("bbb", "bbbbb"), new(1000, 1, 1));

                // 組織構築
                // 配属社員：AssignedPersonのみ
                // 長不在組織：NoBossOrganization
                var a = new TerminalOrganization(new("1"));
                a.SetBoss(new(new("aaa", "aaa"), new(1000, 1, 1)));
                a.AddMember(AssignedPerson);
                var b = new TerminalOrganization(new("2"));
                b.SetBoss(new(new("aaa", "aaa"), new(1000, 1, 1)));
                var c = new ManagementOrganization(new("3"), Ranks.Section, new() { a, b });
                c.SetBoss(new(new("aaa", "aaa"), new(1000, 1, 1)));
                var d = new TerminalOrganization(new("4"));
                d.SetBoss(new(new("aaa", "aaa"), new(1000, 1, 1)));
                var e = new TerminalOrganization(new("5"));
                e.SetBoss(new(new("aaa", "aaa"), new(1000, 1, 1)));
                var f = new ManagementOrganization(new("6"), Ranks.Section, new() { d, e });
                var top = new ManagementOrganization(new("7"), Ranks.Department, new() { c, f });
                top.SetBoss(new(new("aaa", "aaa"), new(1000, 1, 1)));

                NoBossOrganization = f;

                return top;
            }
        }

        private class BuilderMock_組織情報収集用 : IOrganizationBuilder
        {
            public OrganizationBase Build()
            {
                // 組織構築
                // 配属社員：AssignedPersonのみ
                // 長不在組織：NoBossOrganization
                var a = new TerminalOrganization(new("1"));
                a.SetBoss(new(new("第１チーム", "ボス"), new(1000, 1, 1)));
                a.AddMember(new(new("だい１ちーむ", "ひら１"), new(1000, 1, 1)));
                a.AddMember(new(new("だい１ちーむ", "ひら２"), new(1000, 1, 1)));
                a.AddMember(new(new("だい１ちーむ", "ひら３"), new(1000, 1, 1)));
                var b = new TerminalOrganization(new("2"));
                b.SetBoss(new(new("第２チーム", "ボス"), new(1000, 1, 1)));
                b.AddMember(new(new("だい2ちーむ", "ひら１"), new(1000, 1, 1)));
                b.AddMember(new(new("だい2ちーむ", "ひら２"), new(1000, 1, 1)));
                b.AddMember(new(new("だい2ちーむ", "ひら３"), new(1000, 1, 1)));
                var c = new ManagementOrganization(new("3"), Ranks.Section, new() { a, b });
                c.SetBoss(new(new("第３課", "ボス"), new(1000, 1, 1)));
                c.AddMember(new(new("だい３か", "ひら１"), new(1000, 1, 1)));
                c.AddMember(new(new("だい３か", "ひら２"), new(1000, 1, 1)));
                c.AddMember(new(new("だい３か", "ひら３"), new(1000, 1, 1)));
                var d = new TerminalOrganization(new("4"));
                d.AddMember(new(new("だい４ちーむ", "ひら１"), new(1000, 1, 1)));
                d.AddMember(new(new("だい４ちーむ", "ひら２"), new(1000, 1, 1)));
                d.AddMember(new(new("だい４ちーむ", "ひら３"), new(1000, 1, 1)));
                var e = new TerminalOrganization(new("5"));
                e.SetBoss(new(new("第5チーム", "ボス"), new(1000, 1, 1)));
                e.AddMember(new(new("だい５ちーむ", "ひら１"), new(1000, 1, 1)));
                e.AddMember(new(new("だい５ちーむ", "ひら２"), new(1000, 1, 1)));
                e.AddMember(new(new("だい５ちーむ", "ひら３"), new(1000, 1, 1)));
                var f = new ManagementOrganization(new("6"), Ranks.Section, new() { d, e });
                f.SetBoss(new(new("第6課", "ボス"), new(1000, 1, 1)));
                var top = new ManagementOrganization(new("7"), Ranks.Department, new() { c, f });
                top.SetBoss(new(new("第7部", "ボス"), new(1000, 1, 1)));

                return top;
            }
        }
    }
}
