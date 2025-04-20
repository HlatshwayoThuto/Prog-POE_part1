namespace CyberSecurityChatbot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AudioPlayer.PlayGreeting();
            CyberBot bot = new CyberBot();
            bot.StartConversation();
        }
    }
}
