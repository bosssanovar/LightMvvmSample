using Entity.Organization;
using Entity.Persons;
using Repository;

namespace Usecase
{
    /// <summary>
    /// データを初期化するユースケースを提供する
    /// </summary>
    public class InitializeUsecaseDummy : IInitializeUsecase
    {
        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Fields ----------------------------------------------------------------------------------------

        private readonly IPeopleRepository _peopleRepository;

        private readonly IOrganizationRepository _organizationRepository;

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="peopleRepository"><see cref="IPeople"/>エンティティのリポジトリ</param>
        /// <param name="organizationRepository"><see cref="IOrganization"/>エンティティのリポジトリ</param>
        public InitializeUsecaseDummy(IPeopleRepository peopleRepository, IOrganizationRepository organizationRepository)
        {
            _peopleRepository = peopleRepository;
            _organizationRepository = organizationRepository;
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <inheritdoc/>
        public void Initialize()
        {
            var organization = _organizationRepository.LoadOrganization();
            var organizations = organization.GetOrganizationInfos().Select(x => x.Organization).ToList();

            var people = new People();
            AddPerson(new(new("山本", "康之"), new(1960, 11, 20)), 0);
            AddPerson(new(new("小笠原", "敦美"), new(2004, 4, 26)), 0, true);
            AddPerson(new(new("松川", "晋平"), new(1959, 7, 17)), 0);
            AddPerson(new(new("古川", "博子"), new(2013, 8, 17)), 15, true);
            AddPerson(new(new("江崎", "延行"), new(1967, 4, 21)), 14, true);
            AddPerson(new(new("高瀬", "美樹"), new(2005, 7, 23)), 2);
            AddPerson(new(new("若山", "泰司"), new(1984, 6, 21)), 10, true);
            AddPerson(new(new("加藤", "慶二"), new(2005, 11, 17)), 9);
            AddPerson(new(new("斎藤", "香織"), new(1999, 12, 9)), 12, true);
            AddPerson(new(new("西尾", "晋平"), new(2012, 3, 30)), 18, true);
            AddPerson(new(new("二宮", "貴弘"), new(1991, 11, 11)), 6, true);
            AddPerson(new(new("佐藤", "祐樹"), new(1966, 2, 11)), 4, true);
            AddPerson(new(new("野村", "ゆうこ"), new(2007, 11, 30)), 7, true);
            AddPerson(new(new("野畑", "潤"), new(2021, 8, 25)), 1);
            AddPerson(new(new("渡邊", "康一"), new(1937, 12, 12)), 13, true);
            AddPerson(new(new("高木", "弘也"), new(1964, 11, 7)), 18);
            AddPerson(new(new("森山", "真由美"), new(1981, 5, 7)), 16, true);
            AddPerson(new(new("島田", "淳"), new(1938, 2, 8)), 10);
            AddPerson(new(new("大塚", "一起"), new(2003, 1, 24)), 7);
            AddPerson(new(new("川島", "修"), new(1990, 4, 16)), 7);
            AddPerson(new(new("吉村", "英輝"), new(1955, 7, 25)), 13);
            AddPerson(new(new("白井", "祐子"), new(1952, 8, 10)), 12);
            AddPerson(new(new("桑原", "眞"), new(2015, 7, 22)), 14);
            AddPerson(new(new("長洲", "里香"), new(1946, 1, 21)), 16);
            AddPerson(new(new("鈴江", "直美"), new(2007, 1, 6)), 1);
            AddPerson(new(new("重光", "大輔"), new(1969, 2, 1)), 10);
            AddPerson(new(new("佐藤", "昇"), new(1945, 2, 19)), 3, true);
            AddPerson(new(new("石川", "孝之"), new(2006, 4, 10)), 9, true);
            AddPerson(new(new("廣瀬", "明香"), new(1986, 12, 26)), 14);
            AddPerson(new(new("真島", "将義"), new(2009, 5, 3)), 9);
            AddPerson(new(new("松井", "明子"), new(2009, 10, 17)), 18);
            AddPerson(new(new("鈴木", "正也"), new(1950, 1, 7)), 5, true);
            AddPerson(new(new("北見", "亜希子"), new(1923, 1, 12)), 9);
            AddPerson(new(new("古城", "郁夫"), new(1960, 10, 5)), 16);
            AddPerson(new(new("西元", "和也"), new(1951, 7, 29)), 4);
            AddPerson(new(new("石川", "敏郎"), new(1937, 3, 13)), 9);
            AddPerson(new(new("藤井", "圭介"), new(2016, 1, 17)), 16);
            AddPerson(new(new("杉村", "富士夫"), new(2000, 3, 6)), 16);
            AddPerson(new(new("稲垣", "綾香"), new(1938, 5, 20)), 4);
            AddPerson(new(new("末松", "雅之"), new(1960, 1, 29)), 2);
            AddPerson(new(new("春田", "毅"), new(1955, 1, 17)), 2);
            AddPerson(new(new("松倉", "綾子"), new(2007, 2, 19)), 5);
            AddPerson(new(new("坂本", "弘司"), new(1955, 4, 30)), 12);
            AddPerson(new(new("福田", "啓三"), new(1991, 9, 29)), 8, true);
            AddPerson(new(new("川久保", "裕之"), new(2001, 8, 9)), 1);
            AddPerson(new(new("堀部", "誠"), new(2008, 2, 18)), 3);
            AddPerson(new(new("遠藤", "豊子"), new(1977, 1, 12)), 7);
            AddPerson(new(new("嘉村", "純子"), new(1926, 8, 13)), 3);
            AddPerson(new(new("磯貝", "裕介"), new(1942, 9, 21)), 1, true);
            AddPerson(new(new("加藤", "小百合"), new(1952, 1, 14)), 2, true);
            AddPerson(new(new("菊地", "聡"), new(1958, 10, 15)), 7);
            AddPerson(new(new("渡辺", "浩二"), new(1946, 11, 22)), 2);
            AddPerson(new(new("川上", "仁"), new(1995, 12, 16)), 8);
            AddPerson(new(new("加納", "雅"), new(1969, 11, 19)), 17);
            AddPerson(new(new("秋元", "周治"), new(1938, 7, 8)), 5);
            AddPerson(new(new("白井", "信二"), new(1939, 8, 12)), 14);
            AddPerson(new(new("林", "高広"), new(2006, 9, 3)), 9);
            AddPerson(new(new("百瀬", "かおり"), new(2005, 2, 14)), 15);
            AddPerson(new(new("加藤", "一人"), new(1978, 11, 16)), 15);
            AddPerson(new(new("高川", "朋代"), new(1951, 3, 17)), 15);
            AddPerson(new(new("宮田", "昇太"), new(1980, 4, 20)), 8);
            AddPerson(new(new("平田", "大"), new(1935, 7, 28)), 3);
            AddPerson(new(new("大嶋", "信一"), new(1988, 1, 17)), 14);
            AddPerson(new(new("神田", "智恵子"), new(1983, 1, 9)), 16);
            AddPerson(new(new("鈴木", "将之"), new(1966, 9, 17)), 13);
            AddPerson(new(new("寺内", "浩之"), new(1943, 9, 14)), 11, true);
            AddPerson(new(new("松下", "優"), new(1982, 4, 1)), 7);
            AddPerson(new(new("鈴木", "敏洋"), new(1958, 2, 28)), 2);
            AddPerson(new(new("石川", "有加"), new(1972, 7, 26)), 1);
            AddPerson(new(new("小池", "昌紀"), new(2023, 11, 1)), 1);
            AddPerson(new(new("倉光", "正史"), new(2019, 9, 2)), 10);
            AddPerson(new(new("窪田", "喬"), new(1951, 2, 14)), 18);
            AddPerson(new(new("川中", "宏美"), new(1957, 12, 20)), 1);
            AddPerson(new(new("渡部", "英一郎"), new(1996, 12, 20)), 16);
            AddPerson(new(new("小堀", "知子"), new(1965, 2, 17)), 16);
            AddPerson(new(new("福田", "直登"), new(1987, 12, 14)), 17);
            AddPerson(new(new("北本", "正道"), new(1965, 1, 3)), 9);
            AddPerson(new(new("大原", "貴志"), new(1996, 1, 11)), 2);
            AddPerson(new(new("柴田", "恭介"), new(1972, 4, 8)), 13);
            AddPerson(new(new("滝沢", "義徳"), new(1962, 3, 24)), 18);
            AddPerson(new(new("本城", "真子"), new(1984, 10, 4)), 5);
            AddPerson(new(new("岡本", "雅俊"), new(2018, 8, 20)), 17, true);
            AddPerson(new(new("榎本", "真美"), new(1970, 2, 8)), 16);
            AddPerson(new(new("有馬", "卓也"), new(1979, 3, 15)), 13);
            AddPerson(new(new("上田", "正光"), new(1957, 5, 2)), 4);
            AddPerson(new(new("平", "奈那"), new(1929, 9, 20)), 18);
            AddPerson(new(new("山田", "裕之"), new(2002, 7, 2)), 18);
            AddPerson(new(new("吉本", "京子"), new(1930, 1, 9)), 7);
            AddPerson(new(new("田中", "恵子"), new(1942, 3, 16)), 11);
            AddPerson(new(new("阿部", "泰"), new(1948, 1, 6)), 10);
            AddPerson(new(new("野田", "泉"), new(1954, 8, 22)), 5);
            AddPerson(new(new("山本", "奈央"), new(1936, 9, 29)), 3);
            AddPerson(new(new("長谷川", "岳人"), new(1957, 4, 3)), 1);
            AddPerson(new(new("薄井", "知穂"), new(2005, 11, 30)), 14);
            AddPerson(new(new("杉本", "将光"), new(1993, 10, 25)), 14);
            AddPerson(new(new("吉田", "賢二"), new(2017, 9, 3)), 17);
            AddPerson(new(new("飯島", "欣也"), new(1988, 12, 11)), 3);
            AddPerson(new(new("東山", "拓也"), new(1931, 11, 2)), 17);
            AddPerson(new(new("牧原", "理恵"), new(1927, 5, 14)), 9);
            AddPerson(new(new("竹内", "知佳"), new(2002, 10, 9)), 4);
            AddPerson(new(new("小林", "友"), new(1974, 11, 18)), 5);
            AddPerson(new(new("和久田", "美奈子"), new(1941, 6, 21)), 18);
            AddPerson(new(new("中山", "功"), new(1937, 12, 25)), 10);
            AddPerson(new(new("津島", "修"), new(1941, 2, 28)), 11);
            AddPerson(new(new("林", "隆"), new(1943, 6, 20)), 17);
            AddPerson(new(new("梅澤", "有香"), new(1924, 9, 30)), 11);
            AddPerson(new(new("赤嶺", "隆志"), new(2006, 8, 16)), 13);
            AddPerson(new(new("佐藤", "智子"), new(1983, 8, 21)), 18);
            AddPerson(new(new("佐藤", "真実"), new(1986, 7, 5)), 14);
            AddPerson(new(new("遠藤", "聡"), new(2009, 6, 30)), 17);
            AddPerson(new(new("細井", "潤"), new(1972, 7, 16)), 12);
            AddPerson(new(new("岩瀬", "政弘"), new(1963, 12, 16)), 12);
            AddPerson(new(new("西内", "裕"), new(1930, 1, 13)), 18);
            AddPerson(new(new("藤田", "晴香"), new(2001, 1, 18)), 12);
            AddPerson(new(new("有井", "友香"), new(2017, 10, 23)), 12);
            AddPerson(new(new("竹内", "千幸"), new(1945, 11, 22)), 6);
            AddPerson(new(new("立川", "智美"), new(1989, 4, 28)), 4);
            AddPerson(new(new("川崎", "由美子"), new(1969, 8, 13)), 14);
            AddPerson(new(new("森", "麻里子"), new(1937, 10, 22)), 1);
            AddPerson(new(new("中山", "正幸"), new(1979, 8, 7)), 7);
            AddPerson(new(new("松島", "聖"), new(1927, 8, 26)), 4);
            AddPerson(new(new("富井", "孝之"), new(1931, 5, 16)), 5);
            AddPerson(new(new("永渕", "栄一"), new(1998, 1, 4)), 18);
            AddPerson(new(new("木下", "康之"), new(1954, 3, 1)), 15);
            AddPerson(new(new("井上", "考史"), new(2012, 6, 15)), 2);
            AddPerson(new(new("渡辺", "慎平"), new(2011, 11, 13)), 15);
            AddPerson(new(new("三浦", "興平"), new(1924, 6, 28)), 14);
            AddPerson(new(new("磯貝", "一哲"), new(1997, 7, 22)), 10);
            AddPerson(new(new("大隅", "宏幸"), new(1947, 3, 23)), 8);
            AddPerson(new(new("佐野", "隼人"), new(1996, 11, 25)), 5);
            AddPerson(new(new("近藤", "佳祐"), new(1952, 2, 23)), 7);
            AddPerson(new(new("前田", "大典"), new(1964, 1, 10)), 18);
            AddPerson(new(new("植田", "哲哉"), new(1986, 5, 13)), 16);
            AddPerson(new(new("大竹", "哲治"), new(1964, 8, 15)), 16);
            AddPerson(new(new("長谷川", "伸洋"), new(1943, 3, 5)), 1);
            AddPerson(new(new("川田", "大作"), new(1925, 1, 20)), 15);
            AddPerson(new(new("竹内", "沙織"), new(1970, 3, 23)), 1);
            AddPerson(new(new("南", "充"), new(1937, 11, 24)), 8);
            AddPerson(new(new("堀内", "和良"), new(1933, 6, 21)), 11);
            AddPerson(new(new("川口", "弘一郎"), new(1947, 11, 2)), 9);

            _peopleRepository.SavePeople(people);
            _organizationRepository.SaveOrganizaion(organization);

            void AddPerson(Person person, int organizationIndex, bool isBoss = false)
            {
                people.AddPerson(person);
                organization.AddNewMember(person);
                if (isBoss)
                {
                    organization.SetBoss(person, organizations[organizationIndex]);
                }
                else
                {
                    organization.RelocateEmployee(person, organizations[organizationIndex]);
                }
            }
        }

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
