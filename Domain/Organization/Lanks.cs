namespace Entity.Organization
{
    /// <summary>
    /// 組織ランクの列挙子
    /// </summary>
    public enum Lanks
    {
        /// <summary>
        /// 社
        /// </summary>
        Campany,

        /// <summary>
        /// 部
        /// </summary>
        Department,

        /// <summary>
        /// 課
        /// </summary>
        Section,

        /// <summary>
        /// チーム
        /// </summary>
        Team,
    }

    /// <summary>
    /// <see cref="Lanks"/>列挙子の拡張メソッド
    /// </summary>
    public static partial class LanksExtend
    {
        /// <summary>
        /// 表示用文字列を取得します。
        /// </summary>
        /// <param name="value"><see cref="Lanks"/>列挙子</param>
        /// <returns>表示用文字列</returns>
        public static string GetDisplayText(this Lanks value)
        {
            return value switch
            {
                Lanks.Campany => "社",
                Lanks.Department => "部",
                Lanks.Section => "課",
                Lanks.Team => "チーム",
                _ => throw new ArgumentOutOfRangeException(nameof(value), "未定義です"),
            };
        }

        /// <summary>
        /// 部署ランク一覧の表示と値ペアのリストを取得します。
        /// </summary>
        /// <returns>部署ランク一覧の表示と値ペアのリスト</returns>
        public static List<(Lanks value, string disp)> GetAllDispValueList()
        {
            var ret = new List<(Lanks, string)>();

            var list = Enum.GetValues(typeof(Lanks))
            .Cast<Lanks>()
            .ToList();

            foreach (var p in list)
            {
                ret.Add((p, p.GetDisplayText()));
            }

            return ret;
        }
    }
}
