using System.Collections.Generic;
using SendGrid.Helpers.Mail;
using System.ComponentModel.DataAnnotations;


namespace NotificationAPI.Models
{

    
    public class To
    {
        [EmailAddress]
        [StringLength(64)]
        public string email { get; set; }
    }

    public class Personalization
    {
        [Required]
        [MaxLength(1000)]                                           //another limitation by good old sendgrid
        public List<To> to { get; set; }

        [StringLength(64)]
        public string subject { get; set; }
    }

    public class From
    {
        [EmailAddress]
        [StringLength(64)]
        public string email { get; set; }
    }

    public class Content
    {
        public string type { get; set; }
        [MaxLength(600)]                                                //sendgrid only takes emails of up to 600 so lets not cause a problem
        public string value { get; set; }
    }

    public class SendMailModel
    {
        public List<Personalization> personalizations { get; set; }
        public From from { get; set; }
        public List<Content> content { get; set; }
    }
}
