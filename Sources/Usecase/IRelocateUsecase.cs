using Entity.Organization;
using Entity.Persons;
using System.Collections.ObjectModel;

namespace Usecase
{
    /// <summary>
    /// 社員異動のユースケースを提供します。
    /// </summary>
    public interface IRelocateUsecase
    {
        /// <summary>
        /// 組織情報一覧を取得します。
        /// </summary>
        ReadOnlyCollection<OrganizationInfo> Organizations { get; }

        /// <summary>
        /// 社員の所属組織変更が発生したイベント
        /// </summary>
        event Action<OnArisedProblemsEventArgs> OnArisedProblems;

        /// <summary>
        /// 組織人員問題が発生したイベント
        /// </summary>
        event Action<Person> OnPersonUpdate;

        /// <summary>
        /// 所属組織を取得します。
        /// </summary>
        /// <param name="personn">調査対象</param>
        /// <returns>所属組織</returns>
        OrganizationBase GetAssignedOrganization(Person personn);

        /// <summary>
        /// 異動します。
        /// </summary>
        /// <param name="person">対象社員</param>
        /// <param name="newOrganization">異動先</param>
        /// <param name="isBoss">組織長として異動の場合　true</param>
        void Relocate(Person person, OrganizationBase newOrganization, bool isBoss);
    }
}