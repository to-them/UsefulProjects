using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendMessageLibrary
{
    public class Email
    {
        /// <summary>
        /// Sender email address (required)
        /// </summary>
        public string sender_email { get; set; }

        /// <summary>
        /// Sender full name (required)
        /// </summary>
        public string sender_name { get; set; }

        /// <summary>
        /// Sender phone number (optional)
        /// </summary>
        public string sender_phone { get; set; }

        /// <summary>
        /// Sender message / inquiry (required)
        /// </summary>
        public string sender_message { get; set; }

        /// <summary>
        /// Email subject (required)
        /// </summary>
        public string email_subject { get; set; }

        /// <summary>
        /// Recipient email address (required)
        /// </summary>
        public string recipient_email { get; set; }

        /// <summary>
        /// Recipient name (optional)
        /// </summary>
        public string recipient_name { get; set; }
    }
}
