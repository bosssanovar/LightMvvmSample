﻿using Entity.Organization;
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
    public class PersonListViewUsecase : IPersonListViewUsecase
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
        /// <param name="peopleRepository"><see cref="People"/>エンティティのリポジトリ</param>
        /// <param name="organizationRepository"><see cref="Organization"/>エンティティのリポジトリ</param>
        public PersonListViewUsecase(IPeopleRepository peopleRepository, IOrganizationRepository organizationRepository)
        {
            _peopleRepository = peopleRepository;
            _organizationRepository = organizationRepository;
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <inheritdoc/>
        public ReadOnlyCollection<(Person Person, OrganizationBase? Organiation)> GetPersons()
        {
            var ret = new List<(Person Person, OrganizationBase? Organiation)>();

            var people = _peopleRepository.LoadPeople();
            var organization = _organizationRepository.LoadOrganization();

            foreach (var person in people.Persons)
            {
                ret.Add(new(person, organization.GetAssignedOrganization(person)));
            }

            return new ReadOnlyCollection<(Person Person, OrganizationBase? Organiation)>(ret);
        }

        /// <inheritdoc/>
        public Posts GetPost(Person person)
        {
            var organization = _organizationRepository.LoadOrganization();

            return organization.GetPost(person);
        }

        /// <inheritdoc/>
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
