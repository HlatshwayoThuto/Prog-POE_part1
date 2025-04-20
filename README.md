# Prog-POE_part1
// Importing base system functions like Console for I/O and AppDomain
using System;

namespace CyberSecurityChatbot // Defines the namespace grouping for all chatbot-related classes
{
    internal class Program // Main class for launching the application
    {
        static void Main(string[] args) // Main method is the entry point of the application
        {
            // Play a WAV greeting file stored in the 'resources' folder
            // This gives the user an audio-based welcome when the chatbot starts
            AudioPlayer.PlayGreeting();

            // Create a new instance of the chatbot logic class
            // This loads predefined questions, keywords, and response logic
            CyberBot bot = new CyberBot();

            // Starts the chatbot conversation loop
            // Handles user name input, free-text input, menu selection, and keyword detection
            bot.StartConversation();
        }
    }
}

// CyberBot.cs - Main chatbot logic with keyword detection and conversation flow

using System;
using System.Collections.Generic;

namespace CyberSecurityChatbot
{
    internal class CyberBot
    {
        // Stores questions and answers based on keyword combinations
        private Dictionary<(string, string), string> responses = new Dictionary<(string, string), string>();

        // Stores general casual questions the user can ask
        private List<string> generalQuestions = new List<string>
        {
            "How are you?",
            "What is your purpose?",
            "What can I ask you about?",
            "Who made you?",
            "How do you help people?"
        };

        // Stores cybersecurity menu question options
        private List<string> menuQuestions = new List<string>
        {
            "What is phishing?",
            "How do I protect myself from phishing?",
            "How can I create a strong password?",
            "Is reusing passwords bad?",
            "What makes a website secure?",
            "How can I browse safely?",
            "What are online scams?",
            "How do I avoid scams online?",
            "How do I protect my privacy online?",
            "Why is online privacy important?"
        };

        public CyberBot()
        {
            InitializeResponses(); // Populates the dictionary with keyword-based responses
        }

        public void StartConversation()
        {
            // Greeting displayed when the chatbot starts
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║   🛡️  Welcome to CyberSecurity Bot! 🛡️   ║");
            Console.WriteLine("╚════════════════════════════════════════╝");

            // Prompting for user's name
            Console.Write("What's your name? ");
            string name = Console.ReadLine();

            // Greeting with user's name and instructions
            Console.WriteLine($"\nNice to meet you, {name}!");
            Console.WriteLine("Ask me anything about cybersecurity.");
            Console.WriteLine("Type 'menu' to see topics or 'exit' to quit.\n");

            // Main conversation loop that keeps accepting user input
            while (true)
            {
                // Prompt for user input
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("You: ");
                Console.ResetColor();

                string input = Console.ReadLine().ToLower(); // Read user input and convert to lowercase

                // Handles empty input
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Bot: I didn’t quite understand that. Could you rephrase?\n");
                    continue;
                }

                // Allows user to exit the chatbot
                if (input == "exit")
                {
                    Console.WriteLine("Bot: Thanks for chatting! Stay safe online. 👋");
                    break; // Ends the loop
                }

                // Handles menu logic when 'menu' is typed
                if (input == "menu")
                {
                    ShowMenu(); // Show list of topics
                    Console.Write("\nChoose a number, type your question, or type 'exit': ");
                    input = Console.ReadLine().ToLower();

                    if (input == "exit")
                    {
                        Console.WriteLine("Bot: Exiting menu. You can continue chatting.");
                        continue;
                    }

                    // Handles number-based menu selection
                    if (int.TryParse(input, out int number) && number >= 1 && number <= menuQuestions.Count)
                    {
                        input = menuQuestions[number - 1].ToLower();
                        Console.WriteLine($"\nYou selected: {menuQuestions[number - 1]}");
                    }
                }

                // Flag to track whether a matching response was found
                bool found = false;

                // Loop through all keyword pairs to match with user input
                foreach (var key in responses.Keys)
                {
                    if (input.Contains(key.Item1) && input.Contains(key.Item2))
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"\nBot: {responses[key]}\n");
                        Console.ResetColor();
                        found = true;
                        break;
                    }
                }

                // If no specific topic found, check for general casual question
                if (!found)
                {
                    string generalResponse = GetGeneralResponse(input);
                    if (generalResponse != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"\nBot: {generalResponse}\n");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nBot: I’m not sure I understand. Try rephrasing or type 'menu' for help.\n");
                        Console.ResetColor();
                    }
                }
            }
        }

        private void ShowMenu()
        {
            // Display the list of cybersecurity topics
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n📘 You can ask me about:");
            for (int i = 0; i < menuQuestions.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {menuQuestions[i]}");
            }

            Console.WriteLine("\n💬 You can also ask: " + string.Join(", ", generalQuestions));
            Console.ResetColor();
        }

        private string GetGeneralResponse(string input)
        {
            // Returns responses for known general/casual phrases
            if (input.Contains("how are you")) return "I'm just code, but I'm here and ready to help you stay cyber safe!";
            if (input.Contains("purpose")) return "I'm here to raise awareness and answer questions about cybersecurity.";
            if (input.Contains("what can i ask")) return "You can ask about phishing, passwords, scams, privacy, or safe browsing.";
            if (input.Contains("who made you")) return "I was created by a Programming 2A student for their POE project.";
            if (input.Contains("help people")) return "I help people understand online safety through simple explanations.";
            return null; // If nothing matches
        }

        private void InitializeResponses()
        {
            // Keyword pairs and their matching cybersecurity-related answers
            responses.Add(("phishing", "what"), "Phishing is a cyberattack where attackers impersonate trusted sources to steal information.");
            responses.Add(("phishing", "protect"), "Never click on suspicious links, and verify emails before responding to avoid phishing.");
            responses.Add(("password", "strong"), "Use a strong password with uppercase, lowercase, numbers, and symbols (12+ characters).");
            responses.Add(("password", "reuse"), "Avoid reusing passwords across websites. Use a password manager for safety.");
            responses.Add(("browsing", "safe"), "Use HTTPS sites, avoid popups and downloads, and enable browser security settings.");
            responses.Add(("website", "secure"), "A secure site uses HTTPS and shows a padlock icon in the browser address bar.");
            responses.Add(("scam", "online"), "Online scams often involve fake emails, investment frauds, or impersonation attempts.");
            responses.Add(("scam", "avoid"), "Avoid scams by being skeptical of deals that sound too good to be true.");
            responses.Add(("privacy", "protect"), "Protect your privacy by limiting data shared online and using privacy settings.");
            responses.Add(("privacy", "important"), "Online privacy is important to avoid identity theft and unauthorized tracking.");
        }
    }
}

// AudioPlayer.cs - Handles playing an audio greeting at the start of the chatbot

using System;              // Provides core .NET functionality including exceptions and the Console
using System.IO;           // Used for file path combination and file existence checks
using System.Media;        // Provides the SoundPlayer class to play WAV audio files

namespace CyberSecurityChatbot
{
    internal class AudioPlayer
    {
        // Public static method to play the greeting audio
        public static void PlayGreeting()
        {
            // Build the absolute path to the greeting.wav file in the 'resources' folder
            // AppDomain.CurrentDomain.BaseDirectory points to the bin/Debug/netX.Y folder at runtime
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "resources", "greeting.wav");

            // Check if the audio file actually exists at the specified path
            if (File.Exists(filePath))
            {
                try
                {
                    // Create a SoundPlayer instance and load the WAV file
                    using (SoundPlayer player = new SoundPlayer(filePath))
                    {
                        // Play the audio synchronously, i.e., wait until it finishes
                        player.PlaySync();
                    }
                }
                catch (Exception ex) // If there's an error (e.g., file corrupt or audio device issue)
                {
                    Console.WriteLine("Audio error: " + ex.Message); // Display the error message
                }
            }
            else // File does not exist at the constructed path
            {
                Console.WriteLine("Greeting audio not found at: " + filePath); // Debug message
                Console.WriteLine("⚠️ Make sure the 'greeting.wav' file exists in the 'resources' folder.");
                Console.WriteLine("💡 If using .NET Core, also ensure the System.Windows.Extensions NuGet package is installed.");
            }
        }
    }
}