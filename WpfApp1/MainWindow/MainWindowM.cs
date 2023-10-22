using Entity.Persons;
using Reactive.Bindings;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using Usecase;

namespace WpfApp1.MainWindow
{
    /// <summary>
    /// MainWindowのModel
    /// </summary>
    public class MainWindowM
    {
        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Fields ----------------------------------------------------------------------------------------

        private readonly CompositeDisposable _disposables = new();

        private readonly PersonListViewUsecase _personListViewUsecase;

        private readonly UpdatePersonUsecase _updatePersonUsecase;

        private readonly AddPersonUsecase _addPersonUsecase;

        private readonly RemovePersonUsecase _removePersonUsecase;

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        /// <summary>
        /// 個人情報リストを取得します。
        /// </summary>
        public ReactiveCollection<PersonM> Persons { get; private set; }

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindowM()
        {
            _personListViewUsecase = PersonUsecaseProvider.PersonListViewUsecase;
            _updatePersonUsecase = PersonUsecaseProvider.UpdatePersonUsecase;
            _addPersonUsecase = PersonUsecaseProvider.AddPersonUsecase;
            _removePersonUsecase = PersonUsecaseProvider.RemovePersonUsecase;

            Persons = new ReactiveCollection<PersonM>();
            UpdatePersons();

            _updatePersonUsecase.OnUpdatePerson += UpdatePersonUsecase_OnUpdatePerson;
            _addPersonUsecase.OnAddPerson += PersonListViewUsecase_OnAddPerson;
            _removePersonUsecase.OnRemovePerson += PersonListViewUsecase_OnRemovePerson;
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// 各種破棄処理
        /// </summary>
        public void Dispose()
        {
            _disposables.Dispose();

            _updatePersonUsecase.OnUpdatePerson -= UpdatePersonUsecase_OnUpdatePerson;
            _addPersonUsecase.OnAddPerson -= PersonListViewUsecase_OnAddPerson;
            _removePersonUsecase.OnRemovePerson -= PersonListViewUsecase_OnRemovePerson;
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - protected ---------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - private -----------------------------------------------------------------------------

        private void UpdatePersonUsecase_OnUpdatePerson(Person person)
        {
            UpdatePersons();
        }

        private void UpdatePersons()
        {
            Persons.Clear();
            foreach (var p in _personListViewUsecase.GetPersons())
            {
                Persons.Add(new PersonM(
                    p,
                    _personListViewUsecase.GetPost(p),
                    _personListViewUsecase.GetAssignedOrganizationName(p)));
            }
        }

        private void PersonListViewUsecase_OnRemovePerson(Person obj)
        {
            UpdatePersons();
        }

        private void PersonListViewUsecase_OnAddPerson(Person obj)
        {
            UpdatePersons();
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - override ----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}
