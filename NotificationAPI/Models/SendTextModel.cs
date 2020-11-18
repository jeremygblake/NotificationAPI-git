using System;
using System.ComponentModel.DataAnnotations;

//using Twilio.Types;

namespace NotificationAPI.Models
{

    /*
     * 
     * I was testing the idea of serializing the json directly into the object here by usingthe Twilio types but didnt want to spend to much time on it
     * 
     * ALSO...  you could add more data validation here using the DataAnnotations library.
     * 


    */




    public class SendTextModel
    {
        /*SendTextModel(String b, String f)
        {
            this.From = new Twilio.Types.PhoneNumber(b);
            this.To = new Twilio.Types.PhoneNumber(b);
        }*/



        [StringLength(600)]
        public String Body { set; get; }

        [MinLength(12)]
        [MaxLength(12)]
        public String From { set; get; }
        [MinLength(12)]
        [MaxLength(12)]
        public String To { set; get; }
    }
}


