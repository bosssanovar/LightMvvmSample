using Entity.Persons;

namespace Usecase
{
    /// <summary>
    /// 個人情報を追加するユースケースの機能を提供します。
    /// </summary>
    public interface IAddPersonUsecase
    {
        /// <summary>
        /// 個人情報が追加されたことを通知します。
        /// </summary>
        event Action<Person> OnAddedPerson;

        /// <summary>
        /// 組織人員問題が発生したことを通知します。
        /// </summary>
        event Action<OnArisedProblemsEventArgs> OnArisedProblems;

        /// <summary>
        /// 組織構成が変更されたことを通知します。
        /// </summary>
        event Action OnChangedOrganization;

        /// <summary>
        /// 個人情報を保存します。
        /// </summary>
        /// <param name="person">個人情報</param>
        void AddPerson(Person person);
    }
}