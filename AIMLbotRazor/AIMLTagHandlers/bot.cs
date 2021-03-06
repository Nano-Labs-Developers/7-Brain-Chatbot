using System.Xml;
using AIMLbotRazor;
using AIMLbotRazor.Utils;

namespace AIMLbotRazor.AIMLTagHandlers
{
	public class bot : AIMLTagHandler
    {
		public bot(Bot bot, User user, SubQuery query, Request request, Result result, XmlNode templateNode)
		: base(bot, user, query, request, result, templateNode)
		{
		}

		protected override string ProcessChange()
		{
			if (templateNode.Name.ToLower() == "bot" && templateNode.Attributes.Count == 1 && templateNode.Attributes[0].Name.ToLower() == "name")
			{
				string value = templateNode.Attributes["name"].Value;
				return bot.GlobalSettings.grabSetting(value);
			}
			return string.Empty;
		}
	}
}
