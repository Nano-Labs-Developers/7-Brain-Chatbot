namespace ChatBot.Client.Services.AIService
{
    public interface IAIService
    {
        void Initialize();
        String getOutput(String input);
    }
}
