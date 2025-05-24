using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natafa.Api.Models.AuthenticationModel
{
    public class MailData
    {
        public required string ReceiverName { get; set; }
        public required string ToEmail { get; set; }
        public required string Subject { get; set; }
        public required string Body { get; set; }
    }
}
