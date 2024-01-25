using Entity.Persons;

namespace Usecase
{
    /// <summary>
    /// 個人情報を更新するユースケースの機能を提供します。
    /// </summary>
    public interface IUpdatePersonUsecase
    {
        /// <summary>
        /// 個人情報が更新されたことを通知します。
        /// </summary>
        event Action<Person> OnUpdatePerson;

        /// <summary>
        /// 個人情報を更新します。
        /// </summary>
        /// <param name="person">個人情報</param>
        void Update(Person person);
    }
}