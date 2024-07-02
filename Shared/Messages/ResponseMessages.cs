using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Messages
{
    public class ResponseMessages
    {
        public const string USER_ADDED = "User registered successfully";
        public const string USER_DELETED = "User deleted successfully";
        public const string FORGET_PASSWORD_EMAIL = "New password sent to your email.";
        public const string PASSWORD_CHANGED = "Password changed successfully";

        public const string ROLE_ADDED = "Role added successfully";
        public const string ROLE_DELETED = "Role deleted successfully";
        public const string ROLE_UPDATED = "Role updated successfully";
    }
}
