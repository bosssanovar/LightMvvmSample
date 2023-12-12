using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usecase
{
    /// <summary>
    /// 設定データを保存と読み込みを行うインターフェース
    /// </summary>
    public interface IDataStore
    {
        /// <summary>
        /// データファイルからEntityのデータパケットを取得します。
        /// </summary>
        /// <param name="path">ファイルパス</param>
        /// <returns>Entityデータパケット</returns>
        Task<EntityPacket> LoadData(string path);

        /// <summary>
        /// データファイルに保存します。
        /// </summary>
        /// <param name="path">ファイルパス</param>
        /// <param name="packet">データパケット</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task SaveData(string path, EntityPacket packet);
    }
}
