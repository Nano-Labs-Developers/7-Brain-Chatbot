namespace ChatBot.Server.Services.AIService
{
    public class AIMessageService : IAIMessageService
    {
        const string UserId = "blackcat";
        private Bot bot;
        private User user;

        public AIMessageService()
        {
            bot = new Bot();
            user = new User(UserId, bot);
            Initialize();
        }

        public void Initialize()
        {
            bot.loadSettings();
            bot.isAcceptingUserInput = false;
            bot.loadAIMLFromFiles();
            bot.isAcceptingUserInput = true;
        }

        public String getOutput(String input)
        {
            Request r = new Request(input, user, bot);
            Result res = bot.Chat(r);
            string msg = (res.Output);

            if (msg.Length == 0)
            {
                msg = "I don't understand.";
            }
            Thread.Sleep(1000);

            return msg;
        }
    }
}
