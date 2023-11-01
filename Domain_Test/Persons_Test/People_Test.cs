using Entity.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Entity_Test.Persons_Test
{
    public class People_Test
    {
        [Fact]
        public void IGetPerson_社員リスト取得()
        {
            var people = new People();
            IGetPersons personsGetter = people;
            Person p = new(new("aaa", "aaaaa"), new(1000, 1, 1));
            Person p2 = new(new("aaa", "aaaaa"), new(1000, 1, 1));

            // 下準備
            people.AddPerson(new(new("aaa", "aaaa"), new(1000, 1, 1)));
            people.AddPerson(new(new("aaa", "aaaa"), new(1000, 1, 1)));
            people.AddPerson(new(new("aaa", "aaaa"), new(1000, 1, 1)));
            people.AddPerson(new(new("aaa", "aaaa"), new(1000, 1, 1)));
            people.AddPerson(p);
            people.AddPerson(new(new("aaa", "aaaa"), new(1000, 1, 1)));
            people.AddPerson(new(new("aaa", "aaaa"), new(1000, 1, 1)));

            // 実行
            var list = personsGetter.Persons;

            // 評価
            Assert.Equal(7, list.Count);
            Assert.Contains(list, x => x.SameIdentityAs(p));
            Assert.DoesNotContain(list, x => x.SameIdentityAs(p2));
        }
    }
}
