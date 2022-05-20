using System.Xml;
using AIMLbotRazor;
using AIMLbotRazor.Utils;

namespace AIMLbotRazor.AIMLTagHandlers
{
	public class think : AIMLTagHandler
	{
		public think(Bot bot, User user, SubQuery query, Request request, Result result, XmlNode templateNode)
			: base(bot, user, query, request, result, templateNode)
		{
		}

		protected override string ProcessChange()
		{
			return string.Empty;
		}
	}
}
