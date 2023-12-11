using Entity.Organization;
using Entity.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usecase.Sub;

namespace Usecase
{
    /// <summary>
    /// 組織人員問題が発生した際のイベントデータ
    /// </summary>
    public class OnArisedProblemsEventArgs
    {
        /// <summary>
        /// 問題一覧を取得します。
        /// </summary>
        public List<Problems> Problems { get; } = new();

        /// <summary>
        /// 無所属社員の一覧を取得します。
        /// </summary>
        public List<Person> UnAssignedPersons { get; } = new();

        /// <summary>
        /// 長不在組織の一覧を取得します。
        /// </summary>
        public List<OrganizationBase> NoBossOrganizations { get; } = new();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="problems">問題一覧</param>
        /// <param name="unAssignedPersons">無所属社員の一覧</param>
        /// <param name="noBossOrganizations">長不在組織の一覧</param>
        public OnArisedProblemsEventArgs(List<Problems> problems, List<Person> unAssignedPersons, List<OrganizationBase> noBossOrganizations)
        {
            Problems = problems;
            UnAssignedPersons = unAssignedPersons;
            NoBossOrganizations = noBossOrganizations;
        }
    }
}
