using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
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
            if(year < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(year));
            }

            if (month < 1 || month > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(month));
            }

            if (day < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(month));
            }
            else
            {
                // TODO : 月ごと、閏年などもみて
                if(day > 31)
                {
                    throw new ArgumentOutOfRangeException(nameof(month));
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
        /// <returns>有効ならtrue</returns>
        public static bool IsValid(int year, int month, int day)
        {
            if (year < 1)
            {
                return false;
            }

            if (month < 1 || month > 12)
            {
                return false;
            }

            if (day < 1)
            {
                return false;
            }
            else
            {
                // TODO : 月ごと、閏年などもみて
                if (day > 31)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 年齢を取得する。
        /// </summary>
        /// <param name="point">年齢算出の時間地点。今日時点を取りたければ、DateTime.Todayを指定する。</param>
        /// <returns>年齢</returns>
        public int GetAge(DateTime point)
        {
            var birth = new DateTime(_year, _month, _day);
            if(birth > point)
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

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - private -----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}
