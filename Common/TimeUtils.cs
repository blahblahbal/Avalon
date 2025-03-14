namespace Avalon.Common
{
	public static class TimeUtils
	{
		public static int TimeToTicks(int days = 0, int hours = 0, int minutes = 0, int seconds = 0) => DaysToTicks(days) + HoursToTicks(hours) + MinutesToTicks(minutes) + SecondsToTicks(seconds);

		/// <summary>
		/// 414 is the highest acceptable value, anything higher will result in a value greater than <see cref="int.MaxValue"/>
		/// </summary>
		/// <param name="days"></param>
		/// <returns></returns>
		public static int DaysToTicks(int days) => HoursToTicks(days * 24);

		/// <summary>
		/// 9,942 is the highest acceptable value, anything higher will result in a value greater than <see cref="int.MaxValue"/>
		/// </summary>
		/// <param name="hours"></param>
		/// <returns></returns>
		public static int HoursToTicks(int hours) => MinutesToTicks(hours * 60);

		/// <summary>
		/// 596,523 is the highest acceptable value, anything higher will result in a value greater than <see cref="int.MaxValue"/>
		/// </summary>
		/// <param name="minutes"></param>
		/// <returns></returns>
		public static int MinutesToTicks(int minutes) => SecondsToTicks(minutes * 60);

		/// <summary>
		/// 35,791,394 is the highest acceptable value, anything higher will result in a value greater than <see cref="int.MaxValue"/>
		/// </summary>
		/// <param name="seconds"></param>
		/// <returns></returns>
		public static int SecondsToTicks(int seconds) => seconds * 60;
	}
}
