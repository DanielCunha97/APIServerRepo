using System;
using System.Collections.Generic;
using System.Text;

namespace APIServer.Domain
{
    public class EmailModel
    {
        public EmailModel(
            string emailToName,
            string emailToId,
            string emailSubject,
            string emailContent)
        {
            this.EmailBody = emailContent;
            this.EmailToId = emailToId;
            this.EmailToName = emailToName;
            this.EmailSubject = emailSubject;
        }

        public string EmailToId
        {
            get; set;
        }
        public string EmailToName
        {
            get; set;
        }
        public string EmailSubject
        {
            get; set;
        }
        public string EmailBody
        {
            get; set;
        }
    }
}
