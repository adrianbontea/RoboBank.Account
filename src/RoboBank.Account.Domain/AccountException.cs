using System;

namespace RoboBank.Account.Domain
{
    public class AccountException : Exception
    {
        public AccountException(string message) : base (message)
        {
        }
    }
}
