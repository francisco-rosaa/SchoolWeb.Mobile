using SchoolWebMobile.Models;
using System;

namespace SchoolWebMobile.ViewModels
{
    public class MainViewModel
    {
        private static MainViewModel instance;

        public TokenResponse Token { get; set; }

        public MainViewModel()
        {
            instance = this;
        }

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }

        public bool IsTokenValid()
        {
            if (Token != null)
            {
                if (!string.IsNullOrEmpty(Token.Token) && !string.IsNullOrEmpty(Token.Expiration))
                {
                    DateTime _expiration = new DateTime();

                    if (DateTime.TryParse(Token.Expiration, out _expiration))
                    {
                        if (_expiration >= DateTime.Now)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
