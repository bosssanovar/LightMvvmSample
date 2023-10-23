using Entity.Organization;
using Entity.Persons;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usecase
{
    /// <summary>
    /// 個人情報を更新するユースケースの機能を提供します。
    /// </summary>
    public class UpdatePersonUsecase
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

        /// <summary>
        /// 個人情報が更新されたことを通知します。
        /// </summary>
        public event Action<Person> OnUpdatePerson;

        /// <summary>
        /// 組織長が交代して、前の組織長の所属が未定となった場合に発行されるイベント
        /// </summary>
        public event Action<OnKickedOutOldBossEnventArgs> OnKickedOutOldBoss;

        /// <summary>
        /// 組織長ポストが空欄となった場合に発行されるイベント
        /// </summary>
        public event Action<OnBecameVacantBossPositionEventArgs> OnBecameVacantBossPosition;

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="peopleRepository"><see cref="People"/>エンティティのリポジトリ</param>
        /// <param name="organizationRepository"><see cref="Organization"/>エンティティのリポジトリ</param>
        public UpdatePersonUsecase(PeopleRepository peopleRepository, OrganizationRepository organizationRepository)
        {
            _peopleRepository = peopleRepository;
            _organizationRepository = organizationRepository;
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// 個人情報を更新します。
        /// </summary>
        /// <param name="person">個人情報</param>
        /// <param name="organization">所属組織</param>
        /// <param name="isBoss">組織長か</param>
        public void Update(Person person, OrganizationBase organization, bool isBoss)
        {
            UpdataPerson(person);

            UpdateOrganization(person, organization, isBoss);
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - protected ---------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - private -----------------------------------------------------------------------------

        private void UpdataPerson(Person person)
        {
            var entity = _peopleRepository.LoadPeople();

            if (entity.IsContain(person))
            {
                entity.UpdatePersons(person);

                _peopleRepository.SavePeople(entity);

                OnUpdatePerson?.Invoke(person);
            }
            else
            {
                throw new ArgumentException("個人情報が存在しません。", nameof(person));
            }
        }

        private void UpdateOrganization(Person person, OrganizationBase organization, bool isBoss)
        {
            var entity = _organizationRepository.LoadOrganization();

            if (isBoss)
            {
                entity.OnBecameVacantBossPosition += Entity_OnBecameVacantBossPosition;
                entity.OnKickedOutOldBoss += Entity_OnKickedOutOldBoss;
                entity.SetBoss(person, organization);
                entity.OnBecameVacantBossPosition -= Entity_OnBecameVacantBossPosition;
                entity.OnKickedOutOldBoss -= Entity_OnKickedOutOldBoss;
            }
            else
            {
                entity.RelocateEmployee(person, organization);
            }
        }

        private void Entity_OnKickedOutOldBoss(OnKickedOutOldBossEnventArgs args)
        {
            OnKickedOutOldBoss?.Invoke(args);
        }

        private void Entity_OnBecameVacantBossPosition(OnBecameVacantBossPositionEventArgs args)
        {
            OnBecameVacantBossPosition?.Invoke(args);
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - override ----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}
