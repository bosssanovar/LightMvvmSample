using Microsoft.Extensions.DependencyInjection;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usecase;
using WpfApp1.EditWindow;
using WpfApp1.MainWindow;
using WpfApp1.RelocateWindow;

namespace WpfApp1
{
    /// <summary>
    /// DIコンテナのラッパー
    /// </summary>
    internal class ProductServiceProvider
    {
        private static ServiceProvider _provider;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ProductServiceProvider()
        {
            var serviceCollection = new ServiceCollection();
            _provider = serviceCollection.BuildServiceProvider();
        }

        /// <summary>
        /// インターフェースの実装関係を定義します。
        /// </summary>
        public static void BuildServiceCollection()
        {
            var serviceCollection = new ServiceCollection();

            AddInfrastructujres(serviceCollection);
            AddRepositories(serviceCollection);
            AddUsecases(serviceCollection);

            _provider = serviceCollection.BuildServiceProvider();
        }

        private static void AddInfrastructujres(ServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IDataStore, DataStore.DataFile>();
        }

        private static void AddRepositories(ServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IPeopleRepository, PeopleRepository>();
            serviceCollection.AddSingleton<IOrganizationRepository, OrganizationRepository>();
        }

        private static void AddUsecases(ServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IPersonListViewUsecase, PersonListViewUsecase>();
#if DUMMY
            serviceCollection.AddTransient<IInitializeUsecase, InitializeUsecaseDummy>();
#else
            serviceCollection.AddTransient<IInitializeUsecase, InitializeUsecase>();
#endif
            serviceCollection.AddTransient<IAddPersonUsecase, AddPersonUsecase>();
            serviceCollection.AddTransient<IRemovePersonUsecase, RemovePersonUsecase>();
            serviceCollection.AddTransient<IUpdatePersonUsecase, UpdatePersonUsecase>();
            serviceCollection.AddTransient<ICheckProblemsUsecase, CheckProblemsUsecase>();
            serviceCollection.AddTransient<IRelocateUsecase, RelocateUsecase>();
            serviceCollection.AddTransient<IGetOrganizationStructureUsecase, GetOrganizationStructureUsecase>();
            serviceCollection.AddTransient<ISaveLoadDataUsecase, SaveLoadDataUsecase>();
        }

        /// <summary>
        /// DIにより、依存するインターフェースにインスタンスが設定されたクラスインスタンスを取得します。
        /// </summary>
        /// <typeparam name="T">Modelクラス</typeparam>
        /// <returns>Modelインスタンス</returns>
        public static T GetRequiredModel<T>()
            where T : notnull
        {
            return _provider.GetRequiredService<T>();
        }
    }
}
