using System.Xml;
using AIMLbotRazor;
using AIMLbotRazor.Utils;

namespace AIMLbotRazor.AIMLTagHandlers
{
	public class lowercase : AIMLTagHandler
	{
		public lowercase(Bot bot, User user, SubQuery query, Request request, Result result, XmlNode templateNode)
			: base(bot, user, query, request, result, templateNode)
		{
		}

		protected override string ProcessChange()
		{
			if (templateNode.Name.ToLower() == "lowercase")
			{
				return templateNode.InnerText.ToLower(bot.Locale);
			}
			return string.Empty;
		}
	}
}
