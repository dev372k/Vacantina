using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Messages
{
    public class ExceptionMessages
    {
        public const string USER_DOESNOT_EXIST = "User does not exist.";
        public const string USER_ALREADY_EXIST = "User already exists.";

        public const string FOUNDER_DOESNOT_EXIST = "Founder does not exist.";
        public const string REFFERAL_EXCEED = "Refferal count exceed.";
        public const string FOUNDER_ALREADY_EXIST = "Founder already exists.";
        public const string FOUNDER_ALREADY_PHONE_EXIST = "Phone number already exists.";
        public const string NO_REFFERER_EXIST = "Provided URL is wrong.";

        public const string INVALID_CREDENTIALS = "Invalid credentials.";
        public const string UNVERIFIED_USER = "User not verified.";
    }
}
