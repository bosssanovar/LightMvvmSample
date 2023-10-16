using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Organization
{
    /// <summary>
    /// 部の列挙子
    /// </summary>
    public enum Departments
    {
        /// <summary>
        /// AAA部
        /// </summary>
        AAA,

        /// <summary>
        /// BBBBBB部
        /// </summary>
        BBBBBB,

        /// <summary>
        /// CCCCCCCCCCCCCCC部
        /// </summary>
        CCCCCCCCCCCCCCC,
    }

    /// <summary>
    /// <see cref="Departments"/>列挙子の拡張メソッド
    /// </summary>
    public static partial class DepartmentsExtend
    {
        /// <summary>
        /// 表示用文字列を取得します。
        /// </summary>
        /// <param name="post"><see cref="Departments"/>列挙子</param>
        /// <returns>表示用文字列</returns>
        public static string GetDisplayText(this Departments post)
        {
            return post switch
            {
                Departments.AAA => "AAA部",
                Departments.BBBBBB => "BBBBBB部",
                Departments.CCCCCCCCCCCCCCC => "CCCCCCCCCCCCCCC部",
                _ => throw new ArgumentOutOfRangeException(nameof(post), "未定義です"),
            };
        }

        /// <summary>
        /// 部署ランク一覧の表示と値ペアのリストを取得します。
        /// </summary>
        /// <returns>部署ランク一覧の表示と値ペアのリスト</returns>
        public static List<(Departments value, string disp)> GetAllDispValueList()
        {
            var ret = new List<(Departments, string)>();

            var list = Enum.GetValues(typeof(Departments))
            .Cast<Departments>()
            .ToList();

            foreach (var p in list)
            {
                ret.Add((p, p.GetDisplayText()));
            }

            return ret;
        }
    }
}
