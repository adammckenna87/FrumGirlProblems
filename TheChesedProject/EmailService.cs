﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SendGrid;

namespace TheChesedProject
{
    public class SendEmailResult
    {
        
        public SendEmailResult()
        {
            this.Errors = new HashSet<SendEmailError>();
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public ICollection<SendEmailError> Errors { get; set; }


        public class SendEmailError
        {
            public string Message { get; set; }
            public string Field { get; set; }
            public string Help { get; set; }
        }
    }

    public class EmailService
    {
        private SendGrid.SendGridClient _sendGridClient;
        public EmailService(string apiKey)
        {
            this._sendGridClient = new SendGrid.SendGridClient(apiKey);
        }

        public async Task<SendEmailResult> SendEmailAsync(string recipient, string subject, string htmlContent, string plainTextContent)
        {

            var from = new SendGrid.Helpers.Mail.EmailAddress("admin@thechesedproject.com", "The Chesed Project");

            var to = new SendGrid.Helpers.Mail.EmailAddress(recipient);
            var message = SendGrid.Helpers.Mail.MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            message.SetTemplateId("f54eaab7-93e1-48c4-9fc0-706fadd1af38");
            var mailResult = await _sendGridClient.SendEmailAsync(message);

            SendEmailResult result = new SendEmailResult();
            if ((mailResult.StatusCode == System.Net.HttpStatusCode.OK) || (mailResult.StatusCode == System.Net.HttpStatusCode.Accepted))
            {
                result.Success = true;
            }
            else
            {
                var badMailResponse = mailResult.DeserializeResponseBody(mailResult.Body);
                result.Success = false;
                foreach (var error in badMailResponse["errors"])
                {
                    result.Errors.Add(new SendEmailResult.SendEmailError
                    {
                        Message = error.message,
                        Field = error.field,
                        Help = error.help
                    });
                }

            }
            return result;
        }

    }
}
