/*
 *  @ Jeremy Goold
 *  SendEmailModel.cs
 *  
 *  Notes:
 *      data model that is sent from our front end, filled by the API controller 
 *      then validated, filterd, and logged to then be sent off to SendGrid services
 * 
 * 
 */



using System.Collections.Generic;
using SendGrid.Helpers.Mail;
using System.ComponentModel.DataAnnotations;


namespace NotificationAPI.Models
{

    
    public class To
    {
        [EmailAddress]
        [StringLength(64)]
        public string email { get; set; }  //recipient email
    }


    //This holds the recievers 
    public class Personalization
    {
        [Required]
        [MaxLength(1000)]    //another limitation by good old sendgrid
        public List<To> to { get; set; }  //recipient email

        [StringLength(64)]
        public string subject { get; set; }  //subject matter just like in any email client.
    }


    //holds the senders email
    public class From
    {
        [EmailAddress]
        [StringLength(64)]
        public string email { get; set; }  //senders email
    }


    //holds the message and the format it is it... such as "plain\text"  images should be encoded in Base64 for this to work properly
    public class Content
    {
        public string type { get; set; }
        [MaxLength(600)]                                                //sendgrid only takes emails of up to 600 so lets not cause a problem
        public string value { get; set; }
    }


    //this is the main data model that is used in the post requests.  
    //The others are just layers of abstraction to form the full model.

    public class SendMailModel
    {
        public List<Personalization> personalizations { get; set; }
        public From from { get; set; }
        public List<Content> content { get; set; }
    }
}
