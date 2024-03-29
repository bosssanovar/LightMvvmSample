﻿using System.Threading;
using System.Windows;
using DataStore;
using Entity;
using Usecase;
using WpfApp1.MainWindow;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Fields ----------------------------------------------------------------------------------------

        /// <summary>
        /// 多重起動を防止する為のミューテックス。
        /// </summary>
        private static Mutex? _mutex;

        #endregion --------------------------------------------------------------------------------------------

        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - protected ---------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - private -----------------------------------------------------------------------------

        private void ShutDownIfMultiActivate()
        {
            App._mutex = new Mutex(false, "Test-{C9386A33-46F3-072b-86C4-5BF04D0A0235}");
            if (!App._mutex.WaitOne(0, false))
            {
                App._mutex.Close();
                App._mutex = null;
                this.Shutdown();
                return;
            }

            return;
        }

        private static void InitObjects()
        {
            AppServiceProvider.BuildServiceCollection();

            AppServiceProvider.GetRequiredModel<IInitializeUsecase>().Initialize();
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - override ----------------------------------------------------------------------------

        /// <summary>
        /// アプリケーションが開始される時のイベント。
        /// </summary>
        /// <param name="e">イベント データ。</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            // 多重起動チェック
            ShutDownIfMultiActivate();

            // アプリケーション初期化
            InitObjects();

            // メイン ウィンドウ表示
            MainWindowM model = new (
                                    AppServiceProvider.GetRequiredModel<IPersonListViewUsecase>(),
                                    AppServiceProvider.GetRequiredModel<IUpdatePersonUsecase>(),
                                    AppServiceProvider.GetRequiredModel<IAddPersonUsecase>(),
                                    AppServiceProvider.GetRequiredModel<IRemovePersonUsecase>(),
                                    AppServiceProvider.GetRequiredModel<ICheckProblemsUsecase>(),
                                    AppServiceProvider.GetRequiredModel<IGetOrganizationStructureUsecase>(),
                                    AppServiceProvider.GetRequiredModel<ISaveLoadDataUsecase>()
                                    );
            MainWindowV window = new(model);
            window.Show();
        }

        /// <summary>
        /// アプリケーションが終了する時のイベント。
        /// </summary>
        /// <param name="e">イベント データ。</param>
        protected override void OnExit(ExitEventArgs e)
        {
            if (App._mutex == null)
            {
                return;
            }

            // アプリケーション設定の保存

            // ミューテックスの解放
            App._mutex.ReleaseMutex();
            App._mutex.Close();
            App._mutex = null;
        }

#endregion --------------------------------------------------------------------------------------------

#endregion --------------------------------------------------------------------------------------------
    }
}
