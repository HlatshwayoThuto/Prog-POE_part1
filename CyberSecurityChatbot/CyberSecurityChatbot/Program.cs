namespace CyberSecurityChatbot
{
    internal class Program //This is the main class for launching the application
    {
        static void Main(string[] args)
        {
            // Play a WAV greeting file stored in the 'resources' folder
            // This gives the user an audio-based welcome when the chatbot starts before proceeding with any other function in the code.
            AudioPlayer.PlayGreeting();

            // Create a new instance of the chatbot logic class allowing u to load predefined questions, keywords, and response logic for user input
            CyberBot bot = new CyberBot();

            // Starts the chatbot conversation loop
            // Handles user name input, free-text input, menu selection, and keyword detection
            bot.StartConversation();
        }
    }
}
