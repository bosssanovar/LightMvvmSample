using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataStore
{
    /// <summary>
    /// データファイルクラス
    /// </summary>
    public class DataFile
    {
        private readonly string _filePath;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        public DataFile(string filePath)
        {
            _filePath = filePath;
        }

        /// <summary>
        /// データファイルからEntityのデータパケットを取得します。
        /// </summary>
        /// <returns>Entityデータパケット</returns>
        public async Task<EntityPacket> LoadData()
        {
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                PropertyNameCaseInsensitive = true,
            };

            using var stream = new FileStream(_filePath, FileMode.Open, FileAccess.Read);

            return await JsonSerializer.DeserializeAsync<EntityPacket>(stream, options)
                ?? throw new ArgumentException("読み込みに失敗しました。");
        }

        /// <summary>
        /// データファイルに保存します。
        /// </summary>
        /// <param name="packet">データパケット</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task SaveData(EntityPacket packet)
        {
            JsonSerializerOptions options = new()
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            using FileStream fileStream = File.Create(_filePath);
            using StreamWriter writer = new(fileStream, System.Text.Encoding.UTF8);
            string str = JsonSerializer.Serialize<EntityPacket>(packet, options);
            await writer.WriteLineAsync(str);
        }
    }
}
