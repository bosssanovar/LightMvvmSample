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
    internal static class DataFile
    {
        /// <summary>
        /// データファイルからEntityのデータパケットを取得します。
        /// </summary>
        /// <param name="path">ファイルパス</param>
        /// <returns>Entityデータパケット</returns>
        public static async Task<EntityPacket> LoadData(string path)
        {
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                PropertyNameCaseInsensitive = true,
            };

            using var stream = new FileStream(path, FileMode.Open, FileAccess.Read);

            return await JsonSerializer.DeserializeAsync<EntityPacket>(stream, options)
                ?? throw new ArgumentException("読み込みに失敗しました。", nameof(path));
        }

        /// <summary>
        /// データファイルに保存します。
        /// </summary>
        /// <param name="path">ファイルパス</param>
        /// <param name="packet">データパケット</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task SaveData(string path, EntityPacket packet)
        {
            JsonSerializerOptions options = new()
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            using FileStream fileStream = File.Create(path);
            using StreamWriter writer = new(fileStream, System.Text.Encoding.UTF8);
            string str = JsonSerializer.Serialize<EntityPacket>(packet, options);
            await writer.WriteLineAsync(str);
        }
    }
}
