using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Messages
{
    public class EmailBody
    {
        public const string REGISTRATION_SUCCESSFUL = "Congratulations!<br> You are now officially registered on <b><a href='https://tope.pk/'>TopE.PK</a></b>. We're thrilled to have you join our community of savvy shoppers.\r\nFeel free to explore our website and discover the latest deals, discounts, and top-quality products. If you have any questions or need assistance, our support team is here to help.         \r\n            Thank you for choosing <b><a href=\"#\">TopE.PK</a></b>. Happy shopping!\r\n            <br><br>\r\n            Best regards,\r\n            <br>\r\n            The TopE.PK Team";
        public const string VERIFICATION = @"Thank you for creating an account with TopE.PK! <br>
To ensure the security of your account and activate its full functionalities, please verify your email address by clicking on the link below:<a href='{0}'>{0}</a>";
        public const string MOBILE_VERIFICATION = @"Thank you for creating an account with TopE.PK! <br>
To ensure the security of your account and activate its full functionalities, To verify, please enter the verification code: <strong>{0}</strong> in the TopE mobile app";
        public const string FOUNDER_REGISTRATION_SUCCESSFUL = "Congratulations!<br> You are now officially registered on <b>TopE.PK</b>. We're thrilled to have you join our community of savvy shoppers.<br>This is your refferal link <a href='{0}'>{0}</a> <br>This is your refferal code <strong>{1}</strong> share it with 5 others to earn Rs 500 discount  \r\n            Feel free to explore our website and discover the latest deals, discounts, and top-quality products. If you have any questions or need assistance, our support team is here to help.         \r\n            Thank you for choosing <b><a href=\"#\">TopE.PK</a></b>. Happy shopping!\r\n            <br><br>\r\n            Best regards,\r\n            <br>\r\n            The TopE.PK Team";
        public const string RESENT_VERIFICATION = @"You've requested to resend the verification link for your TopE.PK account. <br>
To ensure the security of your account and activate its full functionalities, please verify your email address by clicking on the link below:<a href='{0}'>{0}</a>";

        public const string RESENT_VERIFICATION_MOBILE = @"You've requested to resend the verification code for your TopE.PK account. <br>
To ensure the security of your account and activate its full functionalities, please verify your email address using this code: {0}";
        // public const string FOUNDER_REGISTRATION_SUCCESSFUL = "Congratulations!<br> You are now officially registered on <b>TopE.PK</b>. We're thrilled to have you join our community of savvy shoppers.<br>This is your refferal link <a href='{0}'>{0}</a> share it with 5 others to earn Rs 500 discount  \r\n            Feel free to explore our website and discover the latest deals, discounts, and top-quality products. If you have any questions or need assistance, our support team is here to help.         \r\n            Thank you for choosing <b><a href=\"#\">TopE.PK</a></b>. Happy shopping!\r\n            <br><br>\r\n            Best regards,\r\n            <br>\r\n            The TopE.PK Team";
        public const string FORGET_PASSWORD = @"<br>You've successfully reset your password. Your new password has been generated and is provided below.<br><br>New Password: <b>{0}</b><br><br>For security reasons, we recommend you change this password after logging in. Feel free to explore our website and enjoy our latest deals, discounts, and top-quality products. Should you have any questions or require assistance, our support team is always here to help. <br><br> Thank you for choosing <b><a href=\""#\"">TopE.PK</a></b>. Happy shopping!           <br><br>            Best regards,            <br>            The TopE.PK Team";

    }
}
