using Entity;
using System.Text.Encodings.Web;
using System.Text.Json;
using Usecase;

namespace DataStore
{
    /// <summary>
    /// データファイルクラス
    /// </summary>
    public class DataFile : IDataStore
    {
        /// <inheritdoc/>
        public async Task<EntityPacket> LoadData(string path)
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

        /// <inheritdoc/>
        public async Task SaveData(string path, EntityPacket packet)
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
