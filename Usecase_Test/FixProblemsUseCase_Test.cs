using Entity.DomainService;
using Entity.Organization;
using Entity.Organization.DataPackets;
using Entity.Persons;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private class AssignRepositoryMock_アサイン確認 : IOrganizationRepository
        {
            private readonly Person _person;
            private readonly OrganizationBase _organization;

            public AssignRepositoryMock_アサイン確認(Person person, OrganizationBase organization)
            {
                _person = person;
                _organization = organization;
            }

            public IOrganization LoadAssigner()
            {
                return new AssignMock(_person, _organization);
            }

            public void SaveAssigner(IOrganization assigner)
            {
            }

            #region 不要インターフェースメソッド
            public Organization LoadOrganization()
            {
                throw new NotImplementedException();
            }

            public IOrganization LoadProblemChecker()
            {
                throw new NotImplementedException();
            }

            public void SaveOrganizaion(Organization organization)
            {
                throw new NotImplementedException();
            }
            #endregion
        }

        private class BuilderMock : IOrganizationBuilder
        {
            public OrganizationBase Build()
            {
                return new TerminalOrganization(new("aaaa"));
            }
        }

        private class AssignMock : IOrganization
        {
            private readonly Person _person;
            private readonly OrganizationBase _organization;

            public AssignMock(Person person, OrganizationBase organization)
            {
                _person = person;
                _organization = organization;
            }

            #region 不要インターフェースメソッド

            public void AddNewMember(Person person)
            {
                throw new NotImplementedException();
            }

            public void ClearAll()
            {
                throw new NotImplementedException();
            }

            public OrganizationPacket ExportPacket()
            {
                throw new NotImplementedException();
            }

            public OrganizationBase GetAssignedOrganization(Person person)
            {
                throw new NotImplementedException();
            }

            public Person GetBoss(OrganizationBase organization)
            {
                throw new NotImplementedException();
            }

            public List<OrganizationBase> GetNoBossOrganizaiotns()
            {
                throw new NotImplementedException();
            }

            public ReadOnlyCollection<OrganizationInfo> GetOrganizationInfos()
            {
                throw new NotImplementedException();
            }

            public string GetOrganizationName(Person person)
            {
                throw new NotImplementedException();
            }

            public string GetOrganizationStructure()
            {
                throw new NotImplementedException();
            }

            public Posts GetPost(Person person)
            {
                throw new NotImplementedException();
            }

            public List<Person> GetUnAssignedPersons()
            {
                throw new NotImplementedException();
            }

            public void ImportPacket(OrganizationPacket packet, List<Person> persons)
            {
                throw new NotImplementedException();
            }

            public void Leave(Person targetPerson)
            {
                throw new NotImplementedException();
            }

            public void RelocateEmployee(Person person, OrganizationBase newOrganization)
            {
                throw new NotImplementedException();
            }

            public void SetBoss(Person newBoss, OrganizationBase organization)
            {
                throw new NotImplementedException();
            }

            #endregion

            void IOrganization.Assign(Person person, OrganizationBase organization, bool isBoss)
            {
                Assert.True(person.SameIdentityAs(_person));
                Assert.True(organization.SameIdentityAs(_organization));
            }
        }
    }
}
