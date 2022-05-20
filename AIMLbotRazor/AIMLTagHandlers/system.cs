﻿using System.Xml;
using AIMLbotRazor;
using AIMLbotRazor.Utils;

namespace AIMLbotRazor.AIMLTagHandlers
{
	public class system : AIMLTagHandler
	{
		public system(Bot bot, User user, SubQuery query, Request request, Result result, XmlNode templateNode)
			: base(bot, user, query, request, result, templateNode)
		{
		}

		protected override string ProcessChange()
		{
			//bot.writeToLog("The system tag is not implemented in this bot");
			return string.Empty;
		}
	}
}
