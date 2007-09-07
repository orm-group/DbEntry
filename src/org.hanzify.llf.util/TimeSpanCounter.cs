﻿
using System;

namespace org.hanzify.llf.util
{
	public class TimeSpanCounter
	{
		private DateTime _TimeStart;

		public TimeSpanCounter()
		{
			_TimeStart = DateTime.Now;
		}

		public TimeSpan Diff
		{
			get
			{
				return DateTime.Now.Subtract(_TimeStart);
			}
		}

		public double GetMilliseconds()
		{
			return Diff.TotalMilliseconds;
		}

		public double GetSeconds()
		{
			return Diff.TotalSeconds;
		}

		public string GetMillisecondsString()
		{
			return GetMillisecondsString("Time span is {0} ms.");
		}

		public string GetMillisecondsString(string format)
		{
			return string.Format(format, GetMilliseconds());
		}

		public string GetSecondsString()
		{
			return GetSecondsString("Time span {0} s .");
		}

		public string GetSecondsString(string format)
		{
			return string.Format(format, GetSeconds());
		}

		public override string ToString()
		{
			return GetMillisecondsString();
		}

	}
}