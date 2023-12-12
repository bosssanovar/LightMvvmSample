using System.Collections.ObjectModel;
using Entity.Persons.DataPackets;

namespace Entity.Persons
{
    /// <summary>
    /// 集団クラス
    /// </summary>
    public class People : IPeople
    {
        #region Fields ----------------------------------------------------------------------------------------

        private readonly List<Person> _persons;

        #endregion --------------------------------------------------------------------------------------------

        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        /// <inheritdoc/>
        public ReadOnlyCollection<Person> Persons
        {
            get
            {
                return _persons.ToList()
                    .AsReadOnly();
            }
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public People()
        {
            _persons = new List<Person>();
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <inheritdoc/>
        public void RemovePerson(Person person)
        {
            _persons.RemoveAll(x => x.SameIdentityAs(person));
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">指定した社員が存在しない場合</exception>
        public void UpdatePersons(Person person)
        {
            if (!_persons.Any(x => x.SameIdentityAs(person)))
            {
                throw new ArgumentException("指定した社員は存在しません。", nameof(person));
            }

            Person p = _persons.Single(x => x.SameIdentityAs(person));
            person.CopyTo(p);
        }

        /// <inheritdoc/>
        public void AddPerson(Person person)
        {
            _persons.Add(person);
        }

        /// <inheritdoc/>
        public Person GetPerson(Person person)
        {
            return _persons.First(x => x.SameIdentityAs(person));
        }

        /// <inheritdoc/>
        public bool IsContain(Person person)
        {
            return _persons.Any(x => x.SameIdentityAs(person));
        }

        /// <inheritdoc/>
        public void ImportPacket(PeoplePacket packet)
        {
            foreach (var person in packet.Persons)
            {
                AddPerson(person.Get());
            }
        }

        /// <inheritdoc/>
        public PeoplePacket ExportPacket() => new() { Persons = _persons.Select(x => x.ExportPacket()).ToList() };

        /// <inheritdoc/>
        public void ClearAll()
        {
            _persons.Clear();
        }

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
