using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.Core.HelperFunctions
{
    public class EmailTemplate
    {
        public static string UserConfirmationMail(string userName, string confirmationLink)
        {
            var message = new StringBuilder();
            message.AppendFormat(@"
                    <html>
                    <head>
                        <title>Confirmation Mail</title>
                    </head>
                    <body>
                        <p>Hi {0},</p>
                        <p>In order to start using your new account, you need to confirm your email address.</p>
                        <p>Please confirm your email by clicking <a href='{1}'>here</a>.</p>
                        <p>If you did not sign up for this account, you can ignore this email and the account will be deleted.</p>
                    </body>
                    </html>",userName,confirmationLink);
            return message.ToString();
        }
    }
}
