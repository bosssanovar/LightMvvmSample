using Entity.Organization;
using Entity.Persons;

namespace Usecase.Sub
{
    /// <summary>
    /// 組織辞任問題を検査するインタフェース
    /// </summary>
    public interface ICheckProblems
    {
        #region Properties ------------------------------------------------------------------------------------

        /// <summary>
        /// 長不在組織の一覧を取得します。
        /// </summary>
        List<OrganizationBase> NoBossOrganizaiotns { get; }

        /// <summary>
        /// 未所属社員の一覧を取得します。
        /// </summary>
        List<Person> UnAssignedPersons { get; }

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// 組織辞任問題を検査する
        /// </summary>
        /// <returns><see cref="Problems"/>問題一覧</returns>
        List<Problems> Check();

        #endregion --------------------------------------------------------------------------------------------
    }
}