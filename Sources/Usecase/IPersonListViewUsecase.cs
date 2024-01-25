using Entity.Organization;
using Entity.Persons;
using System.Collections.ObjectModel;

namespace Usecase
{
    /// <summary>
    /// 個人情報リストを表示するユースケース機能を提供します。
    /// </summary>
    public interface IPersonListViewUsecase
    {
        /// <summary>
        /// 所属組織の組織名を取得します。
        /// </summary>
        /// <param name="person">社員</param>
        /// <returns>所属組織名称</returns>
        string GetAssignedOrganizationName(Person person);

        /// <summary>
        /// 社員リストを取得します。
        /// </summary>
        /// <returns>Peopleエンティティ</returns>
        ReadOnlyCollection<(Person Person, OrganizationBase? Organiation)> GetPersons();

        /// <summary>
        /// 役職を取得します。
        /// </summary>
        /// <param name="person">社員</param>
        /// <returns>指定社員の役職</returns>
        Posts GetPost(Person person);
    }
}