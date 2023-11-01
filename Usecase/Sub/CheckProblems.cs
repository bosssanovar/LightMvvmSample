using Entity.Organization;
using Entity.Persons;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usecase.Sub
{
    /// <summary>
    /// 組織辞任問題を検査するクラス
    /// </summary>
    internal class CheckProblems : ICheckProblems
    {
        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Fields ----------------------------------------------------------------------------------------

        private readonly IGetPersonsRepository _peopleRepository;

        private readonly ICheckProblemRepository _organizationRepository;

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        /// <summary>
        /// 未所属社員の一覧を取得します。
        /// </summary>
        public List<Person> UnAssignedPersons { get; private set; } = new List<Person>();

        /// <summary>
        /// 長不在組織の一覧を取得します。
        /// </summary>
        public List<OrganizationBase> NoBossOrganizaiotns { get; private set; } = new List<OrganizationBase>();

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="peopleRepository"><see cref="IGetPersonsRepository"/></param>
        /// <param name="organizationRepository"><see cref="ICheckProblemRepository"/></param>
        public CheckProblems(IGetPersonsRepository peopleRepository, ICheckProblemRepository organizationRepository)
        {
            _peopleRepository = peopleRepository;
            _organizationRepository = organizationRepository;
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// 組織辞任問題を検査する
        /// </summary>
        /// <returns><see cref="Problems"/>問題一覧</returns>
        public List<Problems> Check()
        {
            var ret = new List<Problems>();

            var people = _peopleRepository.LoadPersonsGetter();
            var persons = people.Persons;

            var organization = _organizationRepository.LoadProblemChecker();

            NoBossOrganizaiotns = organization.GetNoBossOrganizaiotns();
            if (NoBossOrganizaiotns.Count > 0)
            {
                ret.Add(Problems.NoBoss);
            }

            UnAssignedPersons = organization.GetUnAssignedPersons(persons.ToList());
            if (UnAssignedPersons.Count > 0)
            {
                ret.Add(Problems.UnAssigned);
            }

            return ret;
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - internal ----------------------------------------------------------------------------

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
