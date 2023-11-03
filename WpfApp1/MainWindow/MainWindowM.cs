using Entity.Persons;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
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

        /// <summary>
        /// 組織情報を取得します。
        /// </summary>
        public ReactivePropertySlim<string?> OrganizationInfo { get; private set; }

        /// <summary>
        /// 組織辞任問題情報を取得します。
        /// </summary>
        public ReactivePropertySlim<string?> ProblemsInfo { get; private set; }

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
            _addPersonUsecase.OnAddedPerson += PersonListViewUsecase_OnAddPerson;
            _addPersonUsecase.OnChangedOrganization += AddPersonUsecase_OnChangedOrganization;
            _addPersonUsecase.OnArisedProblems += AddPersonUsecase_OnArisedProblems;
            _removePersonUsecase.OnRemovePerson += PersonListViewUsecase_OnRemovePerson;

            OrganizationInfo = new ReactivePropertySlim<string?>(string.Empty)
                .AddTo(_disposables);
            ProblemsInfo = new ReactivePropertySlim<string?>(string.Empty)
                .AddTo(_disposables);
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
            _addPersonUsecase.OnAddedPerson -= PersonListViewUsecase_OnAddPerson;
            _addPersonUsecase.OnChangedOrganization -= AddPersonUsecase_OnChangedOrganization;
            _addPersonUsecase.OnArisedProblems -= AddPersonUsecase_OnArisedProblems;
            _removePersonUsecase.OnRemovePerson -= PersonListViewUsecase_OnRemovePerson;
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - protected ---------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - internal

        /* TODO K.I : アクセス修飾子見直し */
        /* TODO K.I : 値オブジェクトクラスをrecord型に置き換える */

        /// <summary>
        /// 社員を追加します。
        /// </summary>
        /// <param name="person">新社員</param>
        internal void AddPerson(Person person)
        {
            _addPersonUsecase.AddPerson(person);
        }

        #endregion

        #region Methods - private -----------------------------------------------------------------------------

        private void UpdatePersonUsecase_OnUpdatePerson(Person person)
        {
            UpdatePersons();
        }

        private void UpdatePersons()
        {
            Persons.Clear();
            foreach (var (person, organiation) in _personListViewUsecase.GetPersons())
            {
                Persons.Add(new PersonM(person, organiation));
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

        private void AddPersonUsecase_OnArisedProblems(OnArisedProblemsEventArgs obj)
        {
            var sb = new StringBuilder();
            sb.AppendLine("問題一覧　：　" + obj.Problems.Select(x => x.ToString()).Aggregate((a, b) => a + ", " + b));
            sb.AppendLine("無所属社員一覧　：　" + obj.UnAssignedPersons.Select(x => x.Name.FullName).Aggregate((a, b) => a + ", " + b));
            sb.AppendLine("長不在組織一覧　：　" + obj.NoBossOrganizations.Select(x => x.DisplayName).Aggregate((a, b) => a + ", " + b));
            ProblemsInfo.Value = sb.ToString();
        }

        private void AddPersonUsecase_OnChangedOrganization()
        {
            // TODO K.I : 実装
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - override ----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}
