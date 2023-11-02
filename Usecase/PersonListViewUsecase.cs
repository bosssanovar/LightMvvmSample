using Entity.Organization;
using Entity.Persons;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usecase
{
    /// <summary>
    /// 個人情報リストを表示するユースケース機能を提供します。
    /// </summary>
    public class PersonListViewUsecase
    {
        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Fields ----------------------------------------------------------------------------------------

        private readonly PeopleRepository _peopleRepository;

        private readonly OrganizationRepository _organizationRepository;

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="peopleRepository"><see cref="People"/>エンティティのリポジトリ</param>
        /// <param name="organizationRepository"><see cref="Organization"/>エンティティのリポジトリ</param>
        public PersonListViewUsecase(PeopleRepository peopleRepository, OrganizationRepository organizationRepository)
        {
            _peopleRepository = peopleRepository;
            _organizationRepository = organizationRepository;
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// 社員リストを取得します。
        /// </summary>
        /// <returns>Peopleエンティティ</returns>
        public ReadOnlyCollection<(Person person, OrganizationBase? organiation)> GetPersons()
        {
            var ret = new List<(Person person, OrganizationBase? Organiation)>();

            var people = _peopleRepository.LoadPeople();
            var organization = _organizationRepository.LoadOrganization();

            foreach(var person in people.Persons)
            {
                ret.Add(new(person, organization.GetAssignedOrganization(person)));
            }

            return new ReadOnlyCollection<(Person person, OrganizationBase? Organiation)>(ret);
        }

        /// <summary>
        /// 組織情報一覧を取得します。
        /// </summary>
        /// <returns>組織情報一覧</returns>
        public ReadOnlyCollection<OrganizationInfo> GetOrganizationInfos()
        {
            var organization = _organizationRepository.LoadOrganization();

            return organization.GetOrganizationInfos();
        }

        /// <summary>
        /// 役職を取得します。
        /// </summary>
        /// <param name="person">社員</param>
        /// <returns>指定社員の役職</returns>
        public Posts GetPost(Person person)
        {
            var organization = _organizationRepository.LoadOrganization();

            return organization.GetPost(person);
        }

        /// <summary>
        /// 所属組織の組織名を取得します。
        /// </summary>
        /// <param name="person">社員</param>
        /// <returns>所属組織名称</returns>
        public string GetAssignedOrganizationName(Person person)
        {
            var organization = _organizationRepository.LoadOrganization();

            return organization.GetOrganizationName(person);
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
