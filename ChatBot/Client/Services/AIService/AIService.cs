namespace ChatBot.Client.Services.AIService
{
    public class AIService : IAIService
    {
        const string UserId = "pasan";
        private Bot bot;
        private User user;

        public AIService()
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

        public string getOutput(string input)
        {
            Request r = new Request(input, user, bot);
            Result res = bot.Chat(r);
            string msg = (res.Output);

            if (msg.Length == 0)
            {
                msg = "I don't understand.";
            }
            
            return msg;
        }
    }
}
