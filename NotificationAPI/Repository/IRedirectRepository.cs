/*
 * @ Jeremy Goold
 * @ IRedirectRepository.cs
 * 
 * Notes:
 *  This file offeres the Redirect acess to the third parts API.  Both classes implement the IRedirectRespositoy.  
 *  The idea was to approach it through OOP and then the more complex is became the easier it was to implement new features.
 * 
 *  Normally I would not have the interface and the classes implementing them to be in the same file.  Though this poject is small and I did not want to clutter it.
 *  
 *  
 *  This class and interface model would make more sense the more complicated the project was.  If I was using the restsharp package for both methods it would 
 *  probably come out very clean. Though in this case I wanted to show that I knew how to use either method.
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotificationAPI.Models;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using RestSharp;


namespace NotificationAPI.Repositories
{
    public interface IRedirectRepository
    {
        public String Post(Object o);

    }


    public class RedirectText : IRedirectRepository
    {


        public String Post(Object o)
        {
            SendTextModel newTextData = (SendTextModel) o;

            try
            {
                //Log the requests that we are making if the Twilio app does not already store this information.  

                TwilioClient.Init(Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID"), Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN"));
                var message = MessageResource.Create(
                    body: newTextData.Body,
                    from: new Twilio.Types.PhoneNumber(newTextData.From),
                    to: new Twilio.Types.PhoneNumber(newTextData.To)
                    );

                return message.Sid;    //honestly a better error capture system would be better but with the time I have this works 
            }
            catch(Exception)
            {
                //could send error to Logger with details
                return "Error in Twilio connection";
            }
           
        }
    }


    public class RedirectMail : IRedirectRepository
    {
        public String Post(Object o)
        {
            SendMailModel newMailData = (SendMailModel) o;
            try
            {
               //I decided to use the RestClient library here to show that there are multiple ways to complete this project.  
                //It seems that there is also a package for this as well.  

                var client = new RestClient(Environment.GetEnvironmentVariable("SENDGRID_URI"));
                var request = new RestRequest(Method.POST);
                request.AddHeader("Authorization", Environment.GetEnvironmentVariable("SENDGRID_API_KEY"));
                request.AddHeader("Content-Type", "application/json");


                request.AddJsonBody(newMailData);

                var response = client.Post(request);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception("error with URI / AUTH");
                }

                return response.StatusCode.ToString();   //again I would of liked to use a better error code system but with a better layout out of the project it could be done

            }
            catch (Exception e)
            {
                return e.Message;   //could add dynamic error messages.  Did not spend the time to perfect the erroring system as it could take 
                                    //some time and not needed to show success of the project
            }

        }

    }
}
