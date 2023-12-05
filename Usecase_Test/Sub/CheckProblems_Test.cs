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
using Usecase.Sub;
using Xunit;

namespace Usecase_Test.Sub
{
    public class CheckProblems_Test
    {
        [Fact]
        public void Check_問題なし()
        {
            // 問題なし
            var checker = new CheckProblems(new OrganizationRepositoryMock(false, false));
            var result = checker.Check();
            Assert.Empty(result);
            Assert.DoesNotContain(Problems.UnAssigned, result);
            Assert.DoesNotContain(Problems.NoBoss, result);
            Assert.Empty(checker.UnAssignedPersons);
            Assert.Empty(checker.NoBossOrganizaiotns);
        }

        [Fact]
        public void Check_無所属社員あり()
        {

            // 無所属社員あり
            var checker = new CheckProblems(new OrganizationRepositoryMock(true, false));
            var result = checker.Check();
            Assert.Single(result);
            Assert.Contains(Problems.UnAssigned, result);
            Assert.DoesNotContain(Problems.NoBoss, result);
            Assert.Equal(3, checker.UnAssignedPersons.Count);
            Assert.Empty(checker.NoBossOrganizaiotns);
        }

        [Fact]
        public void Check_長不在組織あり()
        {

            // 長不在組織あり
            var checker = new CheckProblems(new OrganizationRepositoryMock(false, true));
            var result = checker.Check();
            Assert.Single(result);
            Assert.DoesNotContain(Problems.UnAssigned, result);
            Assert.Contains(Problems.NoBoss, result);
            Assert.Empty(checker.UnAssignedPersons);
            Assert.Equal(2, checker.NoBossOrganizaiotns.Count);
        }

        [Fact]
        public void Check_無所属社員あり_かつ_長不在組織あり()
        {

            // 無所属社員あり、かつ、長不在組織あり
            var checker = new CheckProblems(new OrganizationRepositoryMock(true, true));
            var result = checker.Check();
            Assert.Equal(2, result.Count);
            Assert.Contains(Problems.UnAssigned, result);
            Assert.Contains(Problems.NoBoss, result);
            Assert.Equal(3, checker.UnAssignedPersons.Count);
            Assert.Equal(2, checker.NoBossOrganizaiotns.Count);
        }

        private class OrganizationRepositoryMock : IOrganizationRepository
        {
            readonly bool _isUnAssigned = false;
            readonly bool _isNoBoss = false;

            public OrganizationRepositoryMock(bool isUnAssigned, bool isNoBoss)
            {
                _isUnAssigned = isUnAssigned;
                _isNoBoss = isNoBoss;
            }

            public IOrganization LoadOrganization()
            {
                return new OrganizationMock(_isUnAssigned, _isNoBoss);
            }

            #region 不要インターフェースメソッド

            public void SaveOrganizaion(IOrganization organization)
            {
                throw new NotImplementedException();
            }
            #endregion
        }

        private class OrganizationMock : IOrganization
        {
            readonly bool _isUnAssigned = false;
            readonly bool _isNoBoss = false;

            public OrganizationMock(bool isUnAssigned, bool isNoBoss)
            {
                _isUnAssigned = isUnAssigned;
                _isNoBoss = isNoBoss;
            }

            public List<Person> GetUnAssignedPersons()
            {
                var ret = new List<Person>();

                if (_isUnAssigned)
                {
                    ret.Add(new(new("aaaa", "AAAA"), new(1000, 1, 1)));
                    ret.Add(new(new("aaaa", "AAAA"), new(1000, 1, 1)));
                    ret.Add(new(new("aaaa", "AAAA"), new(1000, 1, 1)));
                }

                return ret;
            }

            public List<OrganizationBase> GetNoBossOrganizaiotns()
            {
                var ret = new List<OrganizationBase>();

                if (_isNoBoss)
                {
                    ret.Add(new TerminalOrganization(new("aaaa")));
                    ret.Add(new TerminalOrganization(new("aaaa")));
                }

                return ret;
            }

            #region 不要インターフェースメソッド
            public void AddNewMember(Person person)
            {
                throw new NotImplementedException();
            }

            public void Assign(Person person, OrganizationBase newOrganization, bool isBoss)
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
        }

        private class Builder : IOrganizationBuilder
        {
            public OrganizationBase Build()
            {
                return new TerminalOrganization(new(""));
            }
        }
    }
}
