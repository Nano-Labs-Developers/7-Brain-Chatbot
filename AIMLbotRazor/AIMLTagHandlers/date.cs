using System;
using System.Xml;
using AIMLbotRazor;
using AIMLbotRazor.Utils;

namespace AIMLbotRazor.AIMLTagHandlers
{
    internal class date : AIMLTagHandler
	{
		public date(Bot bot, User user, SubQuery query, Request request, Result result, XmlNode templateNode)
		: base(bot, user, query, request, result, templateNode)
		{
		}

		protected override string ProcessChange()
		{
			if (templateNode.Name.ToLower() == "date")
			{
				return DateTime.Now.ToString(bot.Locale);
			}
			return string.Empty;
		}
	}
}
