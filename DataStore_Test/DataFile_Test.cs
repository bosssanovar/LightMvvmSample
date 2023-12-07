using DataStore;
using Entity.Persons;
using Entity.Persons.DataPackets;
using System.Net.Sockets;

namespace DataStore_Test
{
    public class DataFile_Test
    {
        [Fact]
        public async Task SaveOpen()
        {
            var dataFile = new DataFile("testData.txt");
            await dataFile.SaveData(Package);
            var loaded = await dataFile.LoadData();

            for (int i = 0; i < Package.People.Persons.Count; i++)
            {
                Assert.Equal(Package.People.Persons[i].Name.First, loaded.People.Persons[i].Name.First);
                Assert.Equal(Package.People.Persons[i].Name.Family, loaded.People.Persons[i].Name.Family);
                Assert.Equal(Package.People.Persons[i].Birthday.Year, loaded.People.Persons[i].Birthday.Year);
                Assert.Equal(Package.People.Persons[i].Birthday.Month, loaded.People.Persons[i].Birthday.Month);
                Assert.Equal(Package.People.Persons[i].Birthday.Day, loaded.People.Persons[i].Birthday.Day);
            }
        }

        public static EntityPacket Package
        {
            get
            {
                return new()
                {
                    People = new PeoplePacket()
                    {
                        Persons = new()
                        {
                            new(){
                                Name = new(){
                                    Family = "aaaa",
                                    First = "aaaaaa"},
                                Birthday = new(){
                                    Year = 1000,
                                    Month=1,
                                    Day=1 } },
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
                    }
                };
            }
        }
    }
}