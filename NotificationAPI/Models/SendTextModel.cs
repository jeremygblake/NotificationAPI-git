/*
 * @ Jeremy Goold
 * SendTextModel.cs
 * 
 * Notes:
 *      data model that is sent from our front end, filled by the API controller 
 *      then validated, filterd, and logged to then be sent off to Twilio services
 * 
 */



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
        public String Body { set; get; }  //holds the message itself being sent

        [MinLength(12)]
        [MaxLength(12)]
        public String From { set; get; }  //holds the number of the sender
        [MinLength(12)]
        [MaxLength(12)]
        public String To { set; get; }    //holder Recipent number.
    }
}


