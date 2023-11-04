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
    public class BirthdayVO
    {
        #region Fields ----------------------------------------------------------------------------------------

        private readonly int _year;
        private readonly int _month;
        private readonly int _day;

        #endregion --------------------------------------------------------------------------------------------

        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        /// <summary>
        /// 生年月日の文字列を取得します。
        /// </summary>
        public string Text
        {
            get
            {
                return $"{_year}/{_month}/{_day}";
            }
        }

        /// <summary>
        /// 誕生年を取得します。
        /// </summary>
        public int Year => _year;

        /// <summary>
        /// 誕生月を取得します。
        /// </summary>
        public int Month => _month;

        /// <summary>
        /// 誕生の日を取得します。
        /// </summary>
        public int Day => _day;

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="year">誕生年</param>
        /// <param name="month">誕生月</param>
        /// <param name="day">誕生日</param>
        public BirthdayVO(int year, int month, int day)
        {
            ErrorCause cause = IsValid(year, month, day);
            if (cause != ErrorCause.None)
            {
                switch (cause)
                {
                    case ErrorCause.None:
                        break;
                    case ErrorCause.Year:
                        throw new ArgumentOutOfRangeException(nameof(year), "範囲外です");
                    case ErrorCause.Month:
                        throw new ArgumentOutOfRangeException(nameof(month), "範囲外です");
                    case ErrorCause.Day:
                        throw new ArgumentOutOfRangeException(nameof(day), "範囲外です");
                    default:
                        throw new InvalidProgramException();
                }
            }

            _year = year;
            _month = month;
            _day = day;
        }

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
            if (year < 1 || year > 9999)
            {
                return ErrorCause.Year;
            }

            // 月の判定
            if (month < 1 || month > 12)
            {
                return ErrorCause.Month;
            }

            // 日の判定
            if (day < 1)
            {
                return ErrorCause.Day;
            }

            // 月ごとの日の最大値を格納する配列
            int[] days = new int[12] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            // 閏年の場合、2月の最大値を29にする
            if (IsLeapYear(year))
            {
                days[1] = 29;
            }

            if (day > days[month - 1])
            {
                return ErrorCause.Day;
            }

            return ErrorCause.None;
        }

        /// <summary>
        /// 年齢を取得する。
        /// </summary>
        /// <param name="point">年齢算出の時間地点。今日時点を取りたければ、DateTime.Todayを指定する。</param>
        /// <returns>年齢</returns>
        public int GetAge(DateTime point)
        {
            var birth = new DateTime(_year, _month, _day);
            if (birth > point)
            {
                throw new ArgumentException("指定に日付に生まれていません。", nameof(point));
            }

            int age = point.Year - _year;

            // 誕生日がまだ来ていなければ、1引く
            if (point.Month < _month ||
                (point.Month == _month &&
                point.Day < _day))
            {
                age--;
            }

            return age;
        }

        /// <summary>
        /// 等価性を判定します。
        /// </summary>
        /// <param name="obj">比較対象</param>
        /// <returns>等価の場合はtrue</returns>
        public override bool Equals(object? obj)
        {
            //objがnullか、型が違うときは、等価でない
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            //NumberとMessageで比較する
            var c = (BirthdayVO)obj;
            return _year == c._year && _month == c._month && _day == c._day;
        }

        /// <summary>
        /// ハッシュコードを取得します。
        /// </summary>
        /// <returns>ハッシュ値</returns>
        public override int GetHashCode()
        {
            return _year ^ _month ^ _day;
        }

        /// <summary>
        /// == のオーバーライド
        /// </summary>
        /// <param name="c1">値１</param>
        /// <param name="c2">値2</param>
        /// <returns>等価の場合true</returns>
        public static bool operator ==(BirthdayVO c1, BirthdayVO c2)
        {
            //nullの確認（構造体のようにNULLにならない型では不要）
            //両方nullか（参照元が同じか）
            //(c1 == c2)とすると、無限ループ
            if (ReferenceEquals(c1, c2))
            {
                return true;
            }

            //どちらかがnullか
            //(c1 == null)とすると、無限ループ
            if (c1 is null || c2 is null)
            {
                return false;
            }

            return c1._year == c2._year && c1._month == c2._month && c1._day == c2._day;
        }

        /// <summary>
        /// != のオーバーライド
        /// </summary>
        /// <param name="c1">値1</param>
        /// <param name="c2">値2</param>
        /// <returns>等価でなければtrue</returns>
        public static bool operator !=(BirthdayVO c1, BirthdayVO c2)
        {
            return !(c1 == c2);
            //(c1 != c2)とすると、無限ループ
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
