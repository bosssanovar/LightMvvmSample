using Entity.Organization;
using Entity.Persons;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Usecase;

namespace WpfApp1.MainWindow
{
    /// <summary>
    /// 個人情報のModel
    /// </summary>
    public class PersonM
    {
        #region Fields ----------------------------------------------------------------------------------------

        private readonly CompositeDisposable _disposables = new();

        private readonly PersonListViewUsecase _personListViewUsecase;

        #endregion --------------------------------------------------------------------------------------------

        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        /// <summary>
        /// 個人情報を取得します。
        /// </summary>
        public Person Person { get; }

        /// <summary>
        /// 名称を取得します。
        /// </summary>
        public ReactivePropertySlim<NameVO> Name { get; }

        /// <summary>
        /// 誕生日を取得します。
        /// </summary>
        public ReactivePropertySlim<BirthdayVO> Birthday { get; }

        /// <summary>
        /// 所属組織を取得します。
        /// </summary>
        public ReactivePropertySlim<OrganizationBase?> AssignedOrganization { get; }

        /// <summary>
        /// 役職を取得します。
        /// </summary>
        public ReadOnlyReactivePropertySlim<Posts> Post { get; }

        /// <summary>
        /// 所属組織の組織名を取得します。
        /// </summary>
        public ReadOnlyReactivePropertySlim<string?> AssignedOrgaizationName { get; }

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">氏名</param>
        /// <param name="birthDay">誕生日</param>
        /// <param name="assignedOrganization">所属組織</param>
        public PersonM(NameVO name, BirthdayVO birthDay, OrganizationBase? assignedOrganization)
            : this(new Person(name, birthDay), assignedOrganization)
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="person">個人情報</param>
        /// <param name="assignedOrganization">所属組織</param>
        public PersonM(Person person, OrganizationBase? assignedOrganization)
        {
            Person = person;

            _personListViewUsecase = PersonUsecaseProvider.PersonListViewUsecase;

            // Birthday
            Birthday = new ReactivePropertySlim<BirthdayVO>(person.Birthday)
                .AddTo(_disposables);

            // Name
            Name = new ReactivePropertySlim<NameVO>(person.Name)
                .AddTo(_disposables);

            AssignedOrganization = new ReactivePropertySlim<OrganizationBase?>(assignedOrganization)
                .AddTo(_disposables);

            Post = AssignedOrganization
                .Select(x => _personListViewUsecase.GetPost(Person))
                .ToReadOnlyReactivePropertySlim()
                .AddTo(_disposables);

            AssignedOrgaizationName = AssignedOrganization
                .Select(x => _personListViewUsecase.GetAssignedOrganizationName(Person))
                .ToReadOnlyReactivePropertySlim()
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
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - private -----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}
