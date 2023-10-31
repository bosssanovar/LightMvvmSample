using Entity.Organization;
using Entity.Persons;
using Repository;
using System;
using System.Collections.Generic;
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
            var checker = new CheckProblems(new PeopleRepositoryMock(), new OrganizationRepositoryMock());
            var result = checker.Check();
            Assert.Empty(result);
            Assert.DoesNotContain(Problems.Independent, result);
            Assert.DoesNotContain(Problems.NoBoss, result);

            // 無所属社員あり
            checker = new CheckProblems(new PeopleRepositoryMock(), new OrganizationRepositoryMock());
            result = checker.Check();
            Assert.Single(result);
            Assert.Contains(Problems.Independent, result);
            Assert.DoesNotContain(Problems.NoBoss, result);

            // 長不在組織あり
            checker = new CheckProblems(new PeopleRepositoryMock(), new OrganizationRepositoryMock());
            result = checker.Check();
            Assert.Single(result);
            Assert.DoesNotContain(Problems.Independent, result);
            Assert.Contains(Problems.NoBoss, result);

            // 無所属社員あり、かつ、長不在組織あり
            checker = new CheckProblems(new PeopleRepositoryMock(), new OrganizationRepositoryMock());
            result = checker.Check();
            Assert.Equal(2, result.Count);
            Assert.Contains(Problems.Independent, result);
            Assert.Contains(Problems.NoBoss, result);
        }
    }

    class PeopleRepositoryMock : IPeopleRepository
    {
        public People LoadPeople()
        {
            throw new NotImplementedException();
        }

        public void SavePeople(People people)
        {
            throw new NotImplementedException();
        }
    }

    class OrganizationRepositoryMock : IOrganizationRepository
    {
        public Organization LoadOrganization()
        {
            throw new NotImplementedException();
        }

        public void SaveOrganizaion(Organization organization)
        {
            throw new NotImplementedException();
        }
    }
}
