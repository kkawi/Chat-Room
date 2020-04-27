using System;

namespace ClientTCP
{
    class Bot
    {
        public string nameBot;

        public string NameBot
        {
            get
            {
                return nameBot;
            }
            set
            {
                if(string.IsNullOrWhiteSpace(nameBot))
                {
                    throw new ArgumentNullException("Bot name cannot be empty");
                }
            }
        }
    }
}
