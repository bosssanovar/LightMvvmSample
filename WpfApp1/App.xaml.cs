using System.Threading;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// アプリケーションが開始される時のイベント。
        /// </summary>
        /// <param name="e">イベント データ。</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            // 多重起動チェック
            App._mutex = new Mutex(false, "Test-{C9386A33-46F3-072b-86C4-5BF04D0A0235}");
            if (!App._mutex.WaitOne(0, false))
            {
                App._mutex.Close();
                App._mutex = null;
                this.Shutdown();
                return;
            }

            // ここでアプリケーション初期化

            // メイン ウィンドウ表示
            Sample window = new();
            window.Show();
        }

        /// <summary>
        /// アプリケーションが終了する時のイベント。
        /// </summary>
        /// <param name="e">イベント データ。</param>
        protected override void OnExit(ExitEventArgs e)
        {
            if (App._mutex == null) { return; }

            // アプリケーション設定の保存

            // ミューテックスの解放
            App._mutex.ReleaseMutex();
            App._mutex.Close();
            App._mutex = null;
        }

        /// <summary>
        /// 多重起動を防止する為のミューテックス。
        /// </summary>
        private static Mutex? _mutex;
    }
}
