using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    /// <summary>
    /// DIコンテナのラッパー
    /// </summary>
    internal class ModelProvider
    {
        private static ServiceProvider _provider;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ModelProvider()
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
            _provider = serviceCollection.BuildServiceProvider();
        }

        /// <summary>
        /// DIにより、依存するインターフェースにインスタンスが設定されたModelクラスインスタンスを取得します。
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
