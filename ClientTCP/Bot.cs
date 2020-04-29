using System;

namespace ClientTCP
{
    class Bot
    {
        #region botname

        public string nameBot = "SC9";

        #endregion

        #region parameters

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

        #endregion
    }
}
