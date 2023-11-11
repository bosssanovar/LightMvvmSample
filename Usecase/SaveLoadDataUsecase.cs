using DataStore;
using Entity.Organization;
using Entity.Persons;
using Entity.Persons.DataPackets;
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
    /// データを保存するユースケース
    /// </summary>
    public class SaveLoadDataUsecase
    {
        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Fields ----------------------------------------------------------------------------------------

        private PeopleRepository _peopleRepository;

        private OrganizationRepository _organizationRepository;

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        /// <summary>
        /// 社員リストが更新されたことを通知します。
        /// </summary>
        public event Action OnPeopleUpdated;

        /// <summary>
        /// 組織構成が変更されたことを通知します。
        /// </summary>
        public event Action OnOrganizationUpdated;

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="peopleRepository"><see cref="People"/>エンティティのリポジトリ</param>
        /// <param name="organizationRepository"><see cref="Organization"/>エンティティのリポジトリ</param>
        public SaveLoadDataUsecase(PeopleRepository peopleRepository, OrganizationRepository organizationRepository)
        {
            _peopleRepository = peopleRepository;
            _organizationRepository = organizationRepository;
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// データをファイルに保存します。
        /// </summary>
        /// <param name="path">保存パス</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task Save(string path)
        {
            var people = _peopleRepository.LoadPeople();
            var peoplePacket = people.ExportPacket();
            var packet = new Entity.EntityPacket() { People = peoplePacket };

            await DataFile.SaveData(path, packet);
        }

        /// <summary>
        /// データをファイルから読み込みます。
        /// </summary>
        /// <param name="path">ファイルパス</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task Load(string path)
        {
            PeoplePacket peoplePacket;

            try
            {
                // Get data packets
                var packet = await DataFile.LoadData(path);
                peoplePacket = packet.People;
            }
            catch
            {
                throw;
            }

            // Load entities
            var people = _peopleRepository.LoadPeople();
            var organization = _organizationRepository.LoadOrganization();

            // Clear entities
            organization.ClearAll();
            people.ClearAll();

            // Import
            people.ImportPacket(peoplePacket);
            people.Persons.ToList().ForEach(x => organization.AddNewMember(x));
            // TODO : organization.Import

            // Save entities
            _peopleRepository.SavePeople(people);
            _organizationRepository.SaveOrganizaion(organization);

            // Notify
            OnPeopleUpdated?.Invoke();
            OnOrganizationUpdated?.Invoke();
            // TODO K.I : 組織人員問題を通知
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
