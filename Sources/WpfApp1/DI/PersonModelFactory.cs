using Entity.Organization;
using Entity.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usecase;
using WpfApp1.MainWindow;

namespace WpfApp1.DI
{
    /// <summary>
    /// <see cref="PersonM"/>のファクトリ
    /// </summary>
    internal class PersonModelFactory
    {
        private readonly IPersonListViewUsecase _personListViewUsecase;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="personListViewUsecase">人員ユースケース</param>
        public PersonModelFactory(IPersonListViewUsecase personListViewUsecase)
        {
            _personListViewUsecase = personListViewUsecase;
        }

        /// <summary>
        /// 生成します。
        /// </summary>
        /// <param name="person">社員情報</param>
        /// <param name="organization">所属組織</param>
        /// <returns>インスタンス</returns>
        public PersonM Create(Person person, OrganizationBase? organization)
        {
            return new PersonM(person, organization, _personListViewUsecase);
        }
    }
}
