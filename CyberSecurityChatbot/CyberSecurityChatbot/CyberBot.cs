using System;
using System.Collections.Generic;// Needed for using List<T> and Dictionary<T>
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurityChatbot
{
    internal class CyberBot
    {
        // Dictionary using keyword pairs as keys, and full explanations as values
        // Example key: ("phishing", "what") → value: explanation string
        private Dictionary<(string, string), string> responses = new Dictionary<(string, string), string>();

        // List of general, casual questions the user may ask the bot
        private List<string> generalQuestions = new List<string>
        {
            "How are you?",
            "What is your purpose?",
            "What can I ask you about?",
            "Who made you?",
            "How do you help people?"
        };

        // List of predefined cybersecurity-related questions and these are shown in the menu for convenience
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

        // Constructor: called when a CyberBot object is created, this loads the dictionary of keyword responses
        // Constructor method for the CyberBot class
        public CyberBot()
        {
            // Calls the method to set up predefined responses to keywords
            InitializeResponses();
        }

        // Method that begins the user interaction with the chatbot
        public void StartConversation()
        {
            // Prints a decorative welcome banner in the console
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║   🛡️  Welcome to CyberSecurity Bot! 🛡️   ║");
            Console.WriteLine("╚════════════════════════════════════════╝");

            // Asks the user to enter their name
            Console.Write("What's your name? ");
            string name = Console.ReadLine(); // Stores the user's input as their name

            // Greets the user using their provided name
            Console.WriteLine($"\nNice to meet you, {name}!");

            // Provides some initial instructions on how to interact with the bot
            Console.WriteLine("Ask me anything about cybersecurity.");
            Console.WriteLine("Type 'menu' to see topics or 'exit' to quit.\n");

            // Loop that keeps the conversation running until the user types "exit"
            while (true)
            {
                // Changes the text color to yellow for user's input prompt
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("You: ");
                Console.ResetColor(); // Resets the console color to default

                // Reads and lowers the user's input to make matching easier
                string input = Console.ReadLine().ToLower();

                // If the user doesn't type anything or just presses enter
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Bot: I didn’t quite understand that. Could you rephrase?\n");
                    continue; // Skips the rest and goes back to the top of the loop
                }

                // If user types "exit", the bot says goodbye and ends the loop
                if (input == "exit")
                {
                    Console.WriteLine("Bot: Thanks for chatting! Stay safe online. 👋");
                    break;
                }

                // If user types "menu", it shows predefined question options
                if (input == "menu")
                {
                    ShowMenu(); // Calls method to display the list of topics

                    Console.Write("\nChoose a number, type your question, or type 'exit': ");
                    input = Console.ReadLine().ToLower(); // Reads the user's choice after showing the menu

                    // If user types exit at this stage, don't close app — just return to main chat
                    if (input == "exit")
                    {
                        Console.WriteLine("Bot: Exiting menu. You can continue chatting.");
                        continue;
                    }

                    // Checks if input is a number that matches one of the listed menu options
                    if (int.TryParse(input, out int number) && number >= 1 && number <= menuQuestions.Count)
                    {
                        // Sets input to the actual question text from the menu
                        input = menuQuestions[number - 1].ToLower();
                        Console.WriteLine($"\nYou selected: {menuQuestions[number - 1]}");
                    }
                }

                // Used to check if the bot has found a response based on keyword matching
                bool found = false;

                // Loops through all keyword pairs in the dictionary
                foreach (var key in responses.Keys)
                {
                    // If user input contains both keywords (Item1 and Item2), respond accordingly
                    if (input.Contains(key.Item1) && input.Contains(key.Item2))
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan; // Set text color to cyan for bot response
                        Console.WriteLine($"\nBot: {responses[key]}\n"); // Shows the response
                        Console.ResetColor(); // Resets color back to default
                        found = true; // Mark that we found a matching response
                        break; // Stop searching after first match is found
                    }
                }

                // If no keyword-based response was found
                if (!found)
                {
                    // Try to match general input (like "how are you", etc.)
                    string generalResponse = GetGeneralResponse(input);

                    // If a general response was found
                    if (generalResponse != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"\nBot: {generalResponse}\n");
                        Console.ResetColor();
                    }
                    else
                    {
                        // If nothing matched, tell the user the bot didn’t understand
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nBot: I’m not sure I understand. Try rephrasing or type 'menu' for help.\n");
                        Console.ResetColor();
                    }
                }
            }
        }

        // Displays a list of menu questions and general suggestions
        private void ShowMenu()
        {
            Console.ForegroundColor = ConsoleColor.Magenta; // Change text to magenta for style

            Console.WriteLine("\n📘 You can ask me about:");

            // Display each question in the menu with its number
            for (int i = 0; i < menuQuestions.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {menuQuestions[i]}");
            }

            // Shows additional suggestions of what users can ask
            Console.WriteLine("\n💬 You can also ask: " + string.Join(", ", generalQuestions));

            Console.ResetColor(); // Revert text color
        }

        // Returns a general bot response if the user input contains one of the phrases below
        private string GetGeneralResponse(string input)
        {
            if (input.Contains("how are you")) return "I'm just code, but I'm here and ready to help you stay cyber safe!";
            if (input.Contains("purpose")) return "I'm here to raise awareness and answer questions about cybersecurity.";
            if (input.Contains("what can i ask")) return "You can ask about phishing, passwords, scams, privacy, or safe browsing.";
            if (input.Contains("who made you")) return "I was created by a Programming 2A student for their POE project.";
            if (input.Contains("help people")) return "I help people understand online safety through simple explanations.";

            // If none of the general phrases are found, return nothing
            return null;
        }

        // Adds keyword-based responses to a dictionary for later use during the conversation
        private void InitializeResponses()
        {
            // The dictionary uses two keywords as the key to map to a specific response
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