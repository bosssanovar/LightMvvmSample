using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Persons
{
    /// <summary>
    /// 誕生日クラス
    /// </summary>
    /// <param name="Year">念</param>
    /// <param name="Month">月</param>
    /// <param name="Day">日</param>
    public record BirthdayVO(int Year, int Month, int Day)
    {
        #region Fields ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        /// <summary>
        /// 誕生日　念
        /// </summary>
        public int Year { get; }
            = IsValidYear(Year)
                ? Year
                : throw new ArgumentOutOfRangeException(nameof(Year), "範囲外です。");

        /// <summary>
        /// 誕生日　月
        /// </summary>
        public int Month { get; }
            = IsValidMonth(Month)
                ? Month
                : throw new ArgumentOutOfRangeException(nameof(Month), "範囲外です。");

        /// <summary>
        /// 誕生日　日
        /// </summary>
        public int Day { get; }
            = IsValidDay(Year, Month, Day)
                ? Day
                : throw new ArgumentOutOfRangeException(nameof(Day), "範囲外です。");

        /// <summary>
        /// 生年月日の文字列を取得します。
        /// </summary>
        public string Text
        {
            get
            {
                return $"{Year}/{Month}/{Day}";
            }
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// パラメータが有効かを判定します。
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="day">日</param>
        /// <returns>エラー種別</returns>
        public static ErrorCause IsValid(int year, int month, int day)
        {
            // 年の判定
            if (!IsValidYear(year))
            {
                return ErrorCause.Year;
            }

            // 月の判定
            if (!IsValidMonth(month))
            {
                return ErrorCause.Month;
            }

            if (!IsValidDay(year, month, day))
            {
                return ErrorCause.Day;
            }

            return ErrorCause.None;
        }

        private static bool IsValidYear(int year)
        {
            return year >= 1 && year <= 9999;
        }

        private static bool IsValidMonth(int month)
        {
            return month >= 1 && month <= 12;
        }

        private static bool IsValidDay(int year, int month, int day)
        {
            // 日の判定
            // 月ごとの日の最大値を格納する配列
            int[] days = new int[12] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            // 閏年の場合、2月の最大値を29にする
            if (IsLeapYear(year))
            {
                days[1] = 29;
            }

            var isDay = true;
            if (day < 1 || day > days[month - 1])
            {
                isDay = false;
            }

            return isDay;
        }

        /// <summary>
        /// 年齢を取得する。
        /// </summary>
        /// <param name="point">年齢算出の時間地点。今日時点を取りたければ、DateTime.Todayを指定する。</param>
        /// <returns>年齢</returns>
        public int GetAge(DateTime point)
        {
            var birth = new DateTime(Year, Month, Day);
            if (birth > point)
            {
                throw new ArgumentException("指定に日付に生まれていません。", nameof(point));
            }

            int age = point.Year - Year;

            // 誕生日がまだ来ていなければ、1引く
            if (point.Month < Month ||
                (point.Month == Month &&
                point.Day < Day))
            {
                age--;
            }

            return age;
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - private -----------------------------------------------------------------------------

        private static bool IsLeapYear(int year)
        {
            if (year % 400 == 0)
            {
                return true;
            }

            if (year % 100 == 0)
            {
                return false;
            }

            if (year % 4 == 0)
            {
                return true;
            }

            return false;
        }

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        /// <summary>
        /// エラー内容
        /// </summary>
        public enum ErrorCause
        {
            /// <summary>
            /// エラーなし
            /// </summary>
            None,

            /// <summary>
            /// 年が不正
            /// </summary>
            Year,

            /// <summary>
            /// 月が不正
            /// </summary>
            Month,

            /// <summary>
            /// 日が不正
            /// </summary>
            Day,
        }
    }
}
