using APIServer.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIServer.Command.Share.Commands
{
    public class ShareEmailCommand : CommandBase
    {
        public ShareEmailCommand(string destinationEmail, string contentUrl)
        {
            this.DestinationEmail = destinationEmail;
            this.ContentUrl = contentUrl;
        }

        public string DestinationEmail
        {
            get; set;
        }
        public string ContentUrl
        {
            get; set;
        }
    }
}
