using System;

namespace ClientTCP
{
    class User
    {
        #region username

        public string userName { get; set; }

        #endregion

        #region parameters

        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(userName))
                {
                    var ErorNullName = "Name cannot be empty!";

                    for (var i = 0; i == 0;)
                    {
                        Console.WriteLine("The name cannot be empty, try entering the name again");

                        string NameRepeat = Console.ReadLine();
                        userName = NameRepeat;

                        if (NameRepeat != "")
                        {
                            userName = NameRepeat;

                            break;
                        }
                    }
                }

                if (userName.Equals("@bot", StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.WriteLine("This name cannot be taken by the user.");

                    for (var i = 0; i == 0;)
                    {
                        Console.WriteLine("try to enter a name again");

                        string NameRepeat = Console.ReadLine();
                        userName = NameRepeat;

                        if (NameRepeat != "@bot")
                        {
                            userName = NameRepeat;

                            break;
                        }
                    }
                }

                if (userName.Equals("bot", StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.WriteLine("This name cannot be taken by the user.");

                    for (var i = 0; i == 0;)
                    {
                        Console.WriteLine("try to enter a name again");

                        string NameRepeat = Console.ReadLine();
                        userName = NameRepeat;

                        if (NameRepeat != "bot")
                        {
                            userName = NameRepeat;

                            break;
                        }
                    }
                }

                if (userName.Equals("@SC9", StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.WriteLine("This name cannot be taken by the user.");

                    for (var i = 0; i == 0;)
                    {
                        Console.WriteLine("try to enter a name again");

                        string NameRepeat = Console.ReadLine();
                        userName = NameRepeat;

                        if (NameRepeat != "@SC9")
                        {
                            userName = NameRepeat;

                            break;
                        }
                    }
                }

                if (userName.Equals("SC9", StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.WriteLine("This name cannot be taken by the user.");

                    for (var i = 0; i == 0;)
                    {
                        Console.WriteLine("try to enter a name again");

                        string NameRepeat = Console.ReadLine();
                        userName = NameRepeat;

                        if (NameRepeat != "SC9")
                        {
                            userName = NameRepeat;

                            break;
                        }
                    }
                }
            }
        }
        #endregion
    }
}
