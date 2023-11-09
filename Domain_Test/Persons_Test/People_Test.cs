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

        [Fact]
        public void ExportImport()
        {
            const string name1 = "aaaaa";
            const string name2 = "aaaaaaaaa";
            const int year = 1000;
            const int month = 1;
            const int day = 1;

            var packet = new PeoplePacket()
            {
                Persons = new()
                {
                    new(){
                        Name = new(){
                            Family = name1,
                            First = name2},
                        Birthday = new(){
                            Year = year,
                            Month=month,
                            Day=day } },
                    new(){
                        Name = new(){
                            Family = "bbbb",
                            First = "bbbb"},
                        Birthday = new(){
                            Year = 2000,
                            Month=2,
                            Day=2 } },
                    new(){
                        Name = new(){
                            Family = "cc",
                            First = "ccc"},
                        Birthday = new(){
                            Year = 3000,
                            Month=3,
                            Day=3 } },
                    new(){
                        Name = new(){
                            Family = "d",
                            First = "dddd"},
                        Birthday = new(){
                            Year = 4000,
                            Month=4,
                            Day=4 } },
                }
            };

            var people = new People();
            people.ImportPacket(packet);

            var testPerson = people.Persons[0];
            Assert.Equal(name1, testPerson.Name.Family);
            Assert.Equal(name2, testPerson.Name.First);
            Assert.Equal(year, testPerson.Birthday.Year);
            Assert.Equal(month, testPerson.Birthday.Month);
            Assert.Equal(day, testPerson.Birthday.Day);

            var exported = people.ExportPacket();

            for(int i = 0; i < packet.Persons.Count; i++)
            {
                Assert.Equal(packet.Persons[i].Name.First, exported.Persons[i].Name.First);
                Assert.Equal(packet.Persons[i].Name.Family, exported.Persons[i].Name.Family);
                Assert.Equal(packet.Persons[i].Birthday.Year, exported.Persons[i].Birthday.Year);
                Assert.Equal(packet.Persons[i].Birthday.Month, exported.Persons[i].Birthday.Month);
                Assert.Equal(packet.Persons[i].Birthday.Day, exported.Persons[i].Birthday.Day);
            }
        }
    }
}
