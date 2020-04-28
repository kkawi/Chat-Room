using System;

namespace ClientTCP
{
    class Bot
    {
        public string nameBot = "SC9";
        public string botHelp = "there is a bot in the chat SC9, its commands @bot / cmd";


        public string NameBot
        {
            get
            {
                return nameBot;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(nameBot))
                {
                    throw new ArgumentNullException("Bot name cannot be empty");
                }
            }
        }
    }
}
