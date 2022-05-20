using AIMLbotRazor;
using AIMLbotRazor.Utils;

namespace AIMLbotRazor.Normalize
{
	public class StripIllegalCharacters : TextTransformer
	{
		public StripIllegalCharacters(Bot bot, string inputString) : base(bot, inputString)
		{
		}

		public StripIllegalCharacters(Bot bot) : base(bot)
		{
		}

		protected override string ProcessChange()
		{
			return bot.Strippers.Replace(inputString, " ");
		}
	}
}
