//Not used ignore



using System;
using System.ComponentModel.DataAnnotations;
using SendGrid.Helpers.Mail;


namespace NotificationAPI.Models
{
    public class EmailNotificationModel
    {


        [Required]
        [EmailAddress]
        
        public String Recipent { set; get; }


        [Required]
        [EmailAddress]
        public String Sender { set; get; }


        [Required]
        [StringLength(600)]
        public String Body { set; get; }


        public String Subject { set; get; }
    }
}
