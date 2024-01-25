using Entity.Persons;

namespace Usecase
{
    /// <summary>
    /// 個人情報を削除するユースケースの機能を提供します。
    /// </summary>
    public interface IRemovePersonUsecase
    {
        /// <summary>
        /// 個人情報が削除されたことを通知します。
        /// </summary>
        event Action<Person> OnRemovePerson;

        /// <summary>
        /// 個人情報を削除します。
        /// </summary>
        /// <param name="person">個人情報</param>
        void RemovePerson(Person person);
    }
}