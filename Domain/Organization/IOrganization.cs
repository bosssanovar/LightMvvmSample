using Entity.Organization.DataPackets;
using Entity.Persons;
using System.Collections.ObjectModel;

namespace Entity.Organization
{
    /// <summary>
    /// 組織データのインターフェース
    /// </summary>
    public interface IOrganization
    {
        #region Properties ------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// 新入社員を登録する。
        /// </summary>
        /// <param name="person">侵入社員</param>
        void AddNewMember(Person person);

        /// <summary>
        /// 組織にアサインする。
        /// </summary>
        /// <param name="person">社員</param>
        /// <param name="newOrganization">社員を追加する組織</param>
        /// <param name="isBoss">組織長としてアサインする場合 true</param>
        /// <exception cref="ArgumentException">追加対象の組織がない場合</exception>
        void Assign(Person person, OrganizationBase newOrganization, bool isBoss);

        /// <summary>
        /// 設定を全て初期化
        /// </summary>
        void ClearAll();

        /// <summary>
        /// データパケットを出力します。
        /// </summary>
        /// <returns>データパケット</returns>
        OrganizationPacket ExportPacket();

        /// <summary>
        /// 社員の所属組織を取得します。
        /// </summary>
        /// <param name="person">ターゲット社員</param>
        /// <returns>所属している組織</returns>
        OrganizationBase GetAssignedOrganization(Person person);

        /// <summary>
        /// 組織長を取得します。
        /// </summary>
        /// <param name="organization">対象組織</param>
        /// <returns>組織長</returns>
        Person GetBoss(OrganizationBase organization);

        /// <summary>
        /// 組織長不在組織の一覧を取得します。
        /// </summary>
        /// <returns>組織長不在組織の一覧</returns>
        List<OrganizationBase> GetNoBossOrganizaiotns();

        /// <summary>
        /// 組織情報を取得します。
        /// </summary>
        /// <returns>組織情報一覧</returns>
        ReadOnlyCollection<OrganizationInfo> GetOrganizationInfos();

        /// <summary>
        /// 所属する組織の組織名を取得します。
        /// </summary>
        /// <param name="person">対象社員</param>
        /// <returns>組織名称</returns>
        string GetOrganizationName(Person person);

        /// <summary>
        /// 組織構造を取得します。
        /// </summary>
        /// <returns>組織構造</returns>
        string GetOrganizationStructure();

        /// <summary>
        /// 社員の現在の役職を取得します。
        /// </summary>
        /// <param name="person">対象社員</param>
        /// <returns>役職</returns>
        /// <exception cref="ArgumentException">指定社員は組織内に存在しない場合</exception>
        Posts GetPost(Person person);

        /// <summary>
        /// 未所属社員の一覧を取得します。
        /// </summary>
        /// <returns>未所属社員の一覧</returns>
        List<Person> GetUnAssignedPersons();

        /// <summary>
        /// データパケットを読み込みます。
        /// </summary>
        /// <param name="packet">データパケット</param>
        /// <param name="persons">社員リスト</param>
        void ImportPacket(OrganizationPacket packet, List<Person> persons);

        /// <summary>
        /// 退職します。
        /// </summary>
        /// <param name="targetPerson">退職する社員</param>
        void Leave(Person targetPerson);

        /// <summary>
        /// 直属社員を指定組織に異動します。
        /// </summary>
        /// <param name="person">異動する社員</param>
        /// <param name="newOrganization">社員を追加する組織</param>
        void RelocateEmployee(Person person, OrganizationBase newOrganization);

        /// <summary>
        /// 組織長を設定します。
        /// 新しい組織長に指定された社員は、元居た部署から削除されます。
        /// </summary>
        /// <param name="newBoss">対象社員</param>
        /// <param name="organization">対象組織</param>
        void SetBoss(Person newBoss, OrganizationBase organization);

        #endregion --------------------------------------------------------------------------------------------
    }
}