namespace ChatBot.Server.Services.AIService
{
    public interface IAIMessageService
    {
        void Initialize();
        String getOutput(String input);
    }
}
