namespace Avalon.Common
{
    public static class TimeUtils
    {
		public static int TimeToTicks(int days, int hours, int minutes, int seconds) => DaysToTicks(days) + HoursToTicks(hours) + MinutesToTicks(minutes) + SecondsToTicks(seconds);
		public static int DaysToTicks(int days) => HoursToTicks(days * 24);
		public static int HoursToTicks(int hours) => MinutesToTicks(hours * 60);
		public static int MinutesToTicks(int minutes) => SecondsToTicks(minutes * 60);
		public static int SecondsToTicks(int seconds) => seconds * 60;
	}
}
