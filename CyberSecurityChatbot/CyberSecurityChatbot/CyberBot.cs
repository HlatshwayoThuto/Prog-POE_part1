using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurityChatbot
{
    internal class CyberBot
    {
        private Dictionary<(string, string), string> responses = new Dictionary<(string, string), string>();
        private List<string> generalQuestions = new List<string>
        {
            "How are you?",
            "What is your purpose?",
            "What can I ask you about?",
            "Who made you?",
            "How do you help people?"
        };

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
            InitializeResponses();
        }

        public void StartConversation()
        {
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║   🛡️  Welcome to CyberSecurity Bot! 🛡️   ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.Write("What's your name? ");
            string name = Console.ReadLine();
            Console.WriteLine($"\nNice to meet you, {name}!");
            Console.WriteLine("Ask me anything about cybersecurity.");
            Console.WriteLine("Type 'menu' to see topics or 'exit' to quit.\n");

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("You: ");
                Console.ResetColor();

                string input = Console.ReadLine().ToLower();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Bot: I didn’t quite understand that. Could you rephrase?\n");
                    continue;
                }

                if (input == "exit")
                {
                    Console.WriteLine("Bot: Thanks for chatting! Stay safe online. 👋");
                    break;
                }

                if (input == "menu")
                {
                    ShowMenu();
                    Console.Write("\nChoose a number, type your question, or type 'exit': ");
                    input = Console.ReadLine().ToLower();

                    if (input == "exit")
                    {
                        Console.WriteLine("Bot: Exiting menu. You can continue chatting.");
                        continue;
                    }

                    if (int.TryParse(input, out int number) && number >= 1 && number <= menuQuestions.Count)
                    {
                        input = menuQuestions[number - 1].ToLower();
                        Console.WriteLine($"\nYou selected: {menuQuestions[number - 1]}");
                    }
                }

                bool found = false;
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
            if (input.Contains("how are you")) return "I'm just code, but I'm here and ready to help you stay cyber safe!";
            if (input.Contains("purpose")) return "I'm here to raise awareness and answer questions about cybersecurity.";
            if (input.Contains("what can i ask")) return "You can ask about phishing, passwords, scams, privacy, or safe browsing.";
            if (input.Contains("who made you")) return "I was created by a Programming 2A student for their POE project.";
            if (input.Contains("help people")) return "I help people understand online safety through simple explanations.";
            return null;
        }

        private void InitializeResponses()
        {
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