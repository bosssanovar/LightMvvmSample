using Entity.DomainService;
using Entity.Organization;
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
        public void Check()
        {
            // 問題なし
            var checker = new CheckProblems(new PeopleRepositoryMock(), new OrganizationRepositoryMock(false, false));
            var result = checker.Check();
            Assert.Empty(result);
            Assert.DoesNotContain(Problems.UnAssigned, result);
            Assert.DoesNotContain(Problems.NoBoss, result);
            Assert.Empty(checker.UnAssignedPersons);
            Assert.Empty(checker.NoBossOrganizaiotns);

            // 無所属社員あり
            checker = new CheckProblems(new PeopleRepositoryMock(), new OrganizationRepositoryMock(true, false));
            result = checker.Check();
            Assert.Single(result);
            Assert.Contains(Problems.UnAssigned, result);
            Assert.DoesNotContain(Problems.NoBoss, result);
            Assert.Equal(3, checker.UnAssignedPersons.Count);
            Assert.Empty(checker.NoBossOrganizaiotns);

            // 長不在組織あり
            checker = new CheckProblems(new PeopleRepositoryMock(), new OrganizationRepositoryMock(false, true));
            result = checker.Check();
            Assert.Single(result);
            Assert.DoesNotContain(Problems.UnAssigned, result);
            Assert.Contains(Problems.NoBoss, result);
            Assert.Empty(checker.UnAssignedPersons);
            Assert.Equal(2, checker.NoBossOrganizaiotns.Count);

            // 無所属社員あり、かつ、長不在組織あり
            checker = new CheckProblems(new PeopleRepositoryMock(), new OrganizationRepositoryMock(true, true));
            result = checker.Check();
            Assert.Equal(2, result.Count);
            Assert.Contains(Problems.UnAssigned, result);
            Assert.Contains(Problems.NoBoss, result);
            Assert.Equal(3, checker.UnAssignedPersons.Count);
            Assert.Equal(2, checker.NoBossOrganizaiotns.Count);
        

        private class PeopleRepositoryMock : IGetPersonsRepository
        {
            public IGetPersons LoadPersonsGetter()
            {
                return new PeopleMock();
            }
        }

        private class OrganizationRepositoryMock : ICheckProblemRepository
        {
            readonly bool _isUnAssigned = false;
            readonly bool _isNoBoss = false;

            public OrganizationRepositoryMock(bool isUnAssigned, bool isNoBoss)
            {
                _isUnAssigned = isUnAssigned;
                _isNoBoss = isNoBoss;
            }

            public ICheckProblem LoadProblemChecker()
            {
                return new OrganizationMock(_isUnAssigned, _isNoBoss);
            }
        }

        private class PeopleMock : IGetPersons
        {
            // テストで不要なため、雑データ
            public ReadOnlyCollection<Person> Persons => new ReadOnlyCollection<Person>(new List<Person>() { new Person(new("", ""), new(1000, 1, 1)) });
        }

        private class OrganizationMock : ICheckProblem
        {
            readonly bool _isUnAssigned = false;
            readonly bool _isNoBoss = false;

            public OrganizationMock(bool isUnAssigned, bool isNoBoss)
            {
                _isUnAssigned = isUnAssigned;
                _isNoBoss = isNoBoss;
            }

            public List<Organization> GetNoBossOrganizaiotns()
            {
                var ret = new List<Organization>();

                if (_isNoBoss)
                {
                    ret.Add(new(new Builder()));
                    ret.Add(new(new Builder()));
                }

                return ret;
            }

            public List<Person> GetUnAssignedPersons(List<Person> persons)
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
