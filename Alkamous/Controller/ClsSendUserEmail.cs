using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Alkamous.Controller
{
    public class ClsSendUserEmail
    {
        public static async Task<bool> SendEmailAsync(string UserName,string recipientEmail, string otpCode)
        {
            string subject = "OTP Password ALKAMOUS Application";
            string body = $@"
    <!DOCTYPE html>
    <html>
    <head>
        <style>
            body {{
                font-family: Arial, sans-serif;
                text-align: center;
                color: #333333;
            }}
            .container {{
                max-width: 400px;
                margin: auto;
                padding: 20px;
                border-radius: 10px;
                box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
                background-color: #f9f9f9;
            }}
            .title {{
                font-size: 24px;
                color: #4285f4;
                margin-bottom: 10px;
            }}
            .otp {{
                font-size: 36px;
                color: #34a853;
                margin: 20px 0;
            }}
            .footer {{
                font-size: 12px;
                color: #888888;
                margin-top: 20px;
            }}
            .footer a {{
                color: #888888;
                text-decoration: none;
            }}
            hr {{
                border: none;
                border-top: 1px solid #dddddd;
                margin: 20px 0;
            }}
        </style>
    </head>
    <body>
        <div class='container'>
            <div class='title'>Email OTP</div>
            <hr>
            <p>Dear {UserName},</p>
            <p>Your One-Time Password (OTP) is:</p>
            <div class='otp'>{otpCode}</div>
            <p>Please use this OTP to complete your Forget Password. Do not share this code with anyone.</p>
            
            <div class='footer'>
                Hassan Ali Alkamous. All rights reserved.
            </div>
            
        </div>
    </body>
    </html>";

            string fromEmail = "hassanwin8.ha@gmail.com";
            string password = "lypt uols hyab ospb";

            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(fromEmail);
                    mail.To.Add(recipientEmail);                    
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true; // تأكد من تفعيل هذا الخيار لتنسيق HTML
                    
                    using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtpClient.Credentials = new NetworkCredential(fromEmail, password);
                        smtpClient.EnableSsl = true;
                        await smtpClient.SendMailAsync(mail);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
