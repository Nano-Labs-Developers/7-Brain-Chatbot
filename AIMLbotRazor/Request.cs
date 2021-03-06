using System;
using AIMLbotRazor;

namespace AIMLbotRazor
{
	public class Request
	{
		public string rawInput;

		public DateTime StartedOn;

		public User user;

		public Bot bot;

		public Result result;

		public bool hasTimedOut;

		public Request(string rawInput, User user, Bot bot)
		{
			this.rawInput = rawInput;
			this.user = user;
			this.bot = bot;
			StartedOn = DateTime.Now;
		}
	}
}
