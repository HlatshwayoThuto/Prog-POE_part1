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
            Console.WriteLine($"Nice to meet you, { name} !Ask me anything about cybersecurity.");
            Console.WriteLine("Type 'menu' if you need help with what to ask.");

            while (true)
            {
                Console.Write("You: ");
                string input = Console.ReadLine().ToLower();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Bot: I didn’t quite understand that. Could you rephrase?");
                    continue;
                }

                if (input == "menu")
                {
                    ShowMenu();
                    Console.Write("Choose a number or type your question: ");
                    string menuChoice = Console.ReadLine().ToLower();

                    if (int.TryParse(menuChoice, out int number) && number >= 1 && number <= menuQuestions.Count)
                    {
                        input = menuQuestions[number - 1].ToLower();
                        Console.WriteLine($"You selected: {menuQuestions[number - 1]}");
                    }
                    else
                    {
                        input = menuChoice;
                    }
                }

                bool found = false;
                foreach (var key in responses.Keys)
                {
                    if (input.Contains(key.Item1) && input.Contains(key.Item2))
                    {
                        Console.WriteLine($"Bot: { responses[key]}");
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    string generalResponse = GetGeneralResponse(input);
                    if (generalResponse != null)
                    {
                        Console.WriteLine($"Bot: { generalResponse}");
                    }
                    else
                    {
                        Console.WriteLine("Bot: I’m not sure I understand. Can you try asking in another way or type 'menu'?");
                    }
                }
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine("Here are some cybersecurity topics you can ask me about: ");
            for (int i = 0; i < menuQuestions.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {menuQuestions[i]}");
            }
            Console.WriteLine("You can also ask: " + string.Join(", ", generalQuestions));
        }

        private string GetGeneralResponse(string input)
        {
            if (input.Contains("how are you")) return "I'm just code, but I'm here and ready to help you stay cyber safe!";
            if (input.Contains("purpose")) return "I'm here to help raise cybersecurity awareness and help you understand online safety better.";
            if (input.Contains("what can i ask")) return "You can ask me about phishing, passwords, scams, privacy, and safe browsing.";
            if (input.Contains("who made you")) return "I was developed by a Programming 2A student as part of their project!";
            if (input.Contains("help people")) return "I help people learn how to stay safe online by answering cybersecurity questions.";
            return null;
        }

        private void InitializeResponses()
        {
            responses.Add(("phishing", "what"), "Phishing is a cyberattack where attackers pretend to be trustworthy sources to trick you into giving up sensitive information like passwords or banking details. They often use fake emails or websites.");
            responses.Add(("phishing", "protect"), "To protect yourself from phishing, never click suspicious links, verify email addresses carefully, and use multi-factor authentication wherever possible.");
            responses.Add(("password", "strong"), "A strong password includes uppercase and lowercase letters, numbers, and symbols. It should be at least 12 characters long and not include personal information.");
            responses.Add(("password", "reuse"), "Reusing passwords is dangerous because if one account is compromised, others are too. Use a password manager to store unique passwords for each account.");
            responses.Add(("browsing", "safe"), "To browse safely, use secure HTTPS websites, avoid clicking unknown links, and keep your browser up to date.");
            responses.Add(("website", "secure"), "A secure website usually starts with HTTPS and has a padlock symbol in the address bar. It encrypts your data to keep it safe.");
            responses.Add(("scam", "online"), "Online scams often come in the form of fake offers, impersonation, or investment frauds. Be skeptical of deals that seem too good to be true.");
            responses.Add(("scam", "avoid"), "Avoid scams by verifying sources, not sharing personal details with strangers, and reporting suspicious activity to your service provider.");
            responses.Add(("privacy", "protect"), "Protect your online privacy by using strong passwords, limiting data shared on social media, and enabling privacy settings on apps.");
            responses.Add(("privacy", "important"), "Online privacy is important because your personal data can be used for fraud, identity theft, or surveillance if not protected properly.");
        }
    }
}
