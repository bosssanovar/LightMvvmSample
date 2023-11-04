using Entity.DomainService;
using Entity.Organization;
using Entity.Persons;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usecase;
using Usecase.Sub;
using Xunit;

namespace Usecase_Test
{
    public class FixProblemsUseCase_Test
    {
        private readonly Person _person = new(new("aaaa", "aaaaa"), new(1000, 1, 1));
        private readonly OrganizationBase _organization = new TerminalOrganization(new("aaa"));

        [Fact]
        public void Assign_問題が消える()
        {
            void Uc_OnArisedProblems(OnArisedProblemsEventArgs obj)
            {
                Assert.Fail("到達してはいけない");
            }
            void Uc_OnUpdatePerson(Person obj)
            {
                Assert.True(obj.SameIdentityAs(_person));
            }
            void Uc_OnUpdateOrganizaiton(OrganizationBase obj)
            {
                Assert.True(obj.SameIdentityAs(_organization));
            }

            var uc = new FixProblemsUseCase(new CheckProblemsMock_問題解消(), new AssignRepositoryMock_アサイン確認(_person, _organization));
            uc.OnArisedProblems += Uc_OnArisedProblems;
            uc.OnUpdatePerson += Uc_OnUpdatePerson;
            uc.OnUpdateOrganizaiton += Uc_OnUpdateOrganizaiton;

            uc.Assign(_person, _organization, false);
        }

        [Fact]
        public void Assign_問題が残る()
        {
            void Uc_OnArisedProblems(OnArisedProblemsEventArgs obj)
            {
                Assert.Equal(2, obj.Problems.Count);
                Assert.Contains(obj.UnAssignedPersons, x => x.SameIdentityAs(_person));
                Assert.Contains(obj.NoBossOrganizations, x => x.SameIdentityAs(_organization));
            }
            void Uc_OnUpdatePerson(Person obj)
            {
                Assert.True(obj.SameIdentityAs(_person));
            }
            void Uc_OnUpdateOrganizaiton(OrganizationBase obj)
            {
                Assert.True(obj.SameIdentityAs(_organization));
            }

            var uc = new FixProblemsUseCase(new CheckProblemsMock_問題残存(_person, _organization), new AssignRepositoryMock_アサイン確認(_person, _organization));
            uc.OnArisedProblems += Uc_OnArisedProblems;
            uc.OnUpdatePerson += Uc_OnUpdatePerson;
            uc.OnUpdateOrganizaiton += Uc_OnUpdateOrganizaiton;

            uc.Assign(_person, _organization, false);
        }

        private class CheckProblemsMock_問題解消 : ICheckProblems
        {
            public List<OrganizationBase> NoBossOrganizaiotns => new();

            public List<Person> UnAssignedPersons => new();

            public List<Problems> Check()
            {
                return new();
            }
        }

        private class CheckProblemsMock_問題残存 : ICheckProblems
        {
            public List<OrganizationBase> NoBossOrganizaiotns { get; } = new();

            public List<Person> UnAssignedPersons { get; } = new();

            public CheckProblemsMock_問題残存(Person person, OrganizationBase organization)
            {
                NoBossOrganizaiotns.Add(organization);
                UnAssignedPersons.Add(person);
            }

            public List<Problems> Check()
            {
                return new() { Problems.UnAssigned, Problems.NoBoss };
            }
        }

        private class AssignRepositoryMock_アサイン確認 : IAssignRepository
        {
            private readonly Person _person;
            private readonly OrganizationBase _organization;

            public AssignRepositoryMock_アサイン確認(Person person, OrganizationBase organization)
            {
                _person = person;
                _organization = organization;
            }

            public IAssign LoadAssigner()
            {
                return new AssignMock(_person, _organization);
            }

            public void SaveAssigner(IAssign assigner)
            {
            }
        }

        private class BuilderMock : IOrganizationBuilder
        {
            public OrganizationBase Build()
            {
                return new TerminalOrganization(new("aaaa"));
            }
        }

        private class AssignMock : IAssign
        {
            private readonly Person _person;
            private readonly OrganizationBase _organization;

            public AssignMock(Person person, OrganizationBase organization)
            {
                _person = person;
                _organization = organization;
            }

            void IAssign.Assign(Person person, OrganizationBase organization, bool isBoss)
            {
                Assert.True(person.SameIdentityAs(_person));
                Assert.True(organization.SameIdentityAs(_organization));
            }
        }
    }
}
