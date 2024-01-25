using Entity.Persons;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

        private readonly IPersonListViewUsecase _personListViewUsecase;
        private readonly IUpdatePersonUsecase _updatePersonUsecase;
        private readonly IAddPersonUsecase _addPersonUsecase;
        private readonly IRemovePersonUsecase _removePersonUsecase;
        private readonly ICheckProblemsUsecase _checkProblemsUsecase;
        private readonly IGetOrganizationStructureUsecase _getOrganizationStructureUsecase;
        private readonly ISaveLoadDataUsecase _saveLoadDataUsecase;

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
            _checkProblemsUsecase = PersonUsecaseProvider.CheckProblemsUsecase;
            _getOrganizationStructureUsecase = PersonUsecaseProvider.GetOrganizationStructureUsecase;
            _saveLoadDataUsecase = PersonUsecaseProvider.SaveLoadDataUsecase;

            Persons = new ReactiveCollection<PersonM>();

            _updatePersonUsecase.OnUpdatePerson += UpdatePersonUsecase_OnUpdatePerson;
            _addPersonUsecase.OnAddedPerson += PersonListViewUsecase_OnAddPerson;
            _addPersonUsecase.OnChangedOrganization += AddPersonUsecase_OnChangedOrganization;
            _addPersonUsecase.OnArisedProblems += AddPersonUsecase_OnArisedProblems;
            _removePersonUsecase.OnRemovePerson += PersonListViewUsecase_OnRemovePerson;
            _checkProblemsUsecase.OnArisedProblems += AddPersonUsecase_OnArisedProblems;
            _saveLoadDataUsecase.OnPeopleUpdated += SaveLoadDataUsecase_OnPeopleUpdated;
            _saveLoadDataUsecase.OnOrganizationUpdated += SaveLoadDataUsecase_OnOrganizationUpdated;
            _saveLoadDataUsecase.OnArisedProblems += SaveLoadDataUsecase_OnArisedProblems;

            OrganizationInfo = new ReactivePropertySlim<string?>(string.Empty)
                .AddTo(_disposables);
            ProblemsInfo = new ReactivePropertySlim<string?>(string.Empty)
                .AddTo(_disposables);

            UpdatePersons();
            _checkProblemsUsecase.Check();
            UpdateOrganizationStructure();
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
            _checkProblemsUsecase.OnArisedProblems -= AddPersonUsecase_OnArisedProblems;
            _saveLoadDataUsecase.OnPeopleUpdated -= SaveLoadDataUsecase_OnPeopleUpdated;
            _saveLoadDataUsecase.OnOrganizationUpdated -= SaveLoadDataUsecase_OnOrganizationUpdated;
            _saveLoadDataUsecase.OnArisedProblems -= SaveLoadDataUsecase_OnArisedProblems;
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - protected ---------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - internal

        /// <summary>
        /// 社員を追加します。
        /// </summary>
        /// <param name="person">新社員</param>
        internal void AddPerson(Person person)
        {
            _addPersonUsecase.AddPerson(person);
        }

        /// <summary>
        /// 社員を削除します。
        /// </summary>
        /// <param name="person">社員</param>
        internal void RemovePerson(Person person)
        {
            _removePersonUsecase.RemovePerson(person);
        }

        /// <summary>
        /// 社員情報を更新します。
        /// </summary>
        /// <param name="args">社員</param>
        internal void Update(Person args)
        {
            _updatePersonUsecase.Update(args);
        }

        /// <summary>
        /// 社員リストを更新します。
        /// </summary>
        internal void UpdatePersons()
        {
            Persons.Clear();
            foreach (var (person, organiation) in _personListViewUsecase.GetPersons())
            {
                Persons.Add(new PersonM(person, organiation));
            }
        }

        /// <summary>
        /// 組織人員問題表示を更新します。
        /// </summary>
        /// <param name="obj">イベントデータ</param>
        internal void UpdateProglemInfo(OnArisedProblemsEventArgs obj)
        {
            var sb = new StringBuilder();
            sb.AppendLine("問題一覧　：　" + obj.Problems.Select(x => x.ToString()).DefaultIfEmpty("なし").Aggregate((a, b) => a + ", " + b));
            sb.AppendLine("無所属社員一覧　：　");
            sb.AppendLine(obj.UnAssignedPersons.Select(x => x.Name.FullName).DefaultIfEmpty("なし").Aggregate((a, b) => a + ", " + b));
            sb.AppendLine("長不在組織一覧　：　");
            sb.AppendLine(obj.NoBossOrganizations.Select(x => x.DisplayName).DefaultIfEmpty("なし").Aggregate((a, b) => a + ", " + b));
            ProblemsInfo.Value = sb.ToString();
        }

        /// <summary>
        /// 組織構造を更新します。
        /// </summary>
        public void UpdateOrganizationStructure()
        {
            OrganizationInfo.Value = _getOrganizationStructureUsecase.GetOrganizationSructureInfo();
        }

        /// <summary>
        /// データファイルから読み込みます。
        /// </summary>
        /// <param name="path">ファイルパス</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        internal async Task Load(string path)
        {
            await _saveLoadDataUsecase.Load(path);
        }

        /// <summary>
        /// データファイルに保存します。
        /// </summary>
        /// <param name="path">ファイルパス</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        internal async Task Save(string path)
        {
            await _saveLoadDataUsecase.Save(path);
        }

        #endregion

        #region Methods - private -----------------------------------------------------------------------------

        private void UpdatePersonUsecase_OnUpdatePerson(Person person)
        {
            UpdatePersons();
            UpdateOrganizationStructure();
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
            UpdateProglemInfo(obj);
        }

        private void AddPersonUsecase_OnChangedOrganization()
        {
            UpdateOrganizationStructure();
        }

        private void SaveLoadDataUsecase_OnPeopleUpdated()
        {
            UpdatePersons();
        }

        private void SaveLoadDataUsecase_OnOrganizationUpdated()
        {
            UpdateOrganizationStructure();
        }

        private void SaveLoadDataUsecase_OnArisedProblems(OnArisedProblemsEventArgs obj)
        {
            UpdateProglemInfo(obj);
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - override ----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}
