/* 
 * @ Jeremy Goold
 * NotificationController.cs
 * 
 * 
 * Notes:
 *  This is the API handler for the project.  It acts as the entry point and all
 *  data should be validated and filtered as it is a hole in the system.
 * 
 */




using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NotificationAPI.Models;
using Microsoft.AspNetCore.Http;
using NotificationAPI.Repositories;




namespace NotificationAPI.Controllers
{
    [Route("api/SendMail")]
    [ApiController]
    public class MailNotificationController : ControllerBase
    {
        //this object allows us to redirect REST requests to SendGrid
        private readonly IRedirectRepository _redirect = new RedirectMail();

        [HttpGet]
        public IActionResult testMethod()                                                                           // for testing purposes
        {
            return Ok("GET Endpoint");
        }
       

        [HttpPost]
        public async Task<ActionResult<SendMailModel>> SendMail(SendMailModel newMailData )
        {
            //data validation and filtering
            //log data from the request and what they attempted to request with.  Might want to encode this is you are not validation before logging.


            if (newMailData.from.email == null || newMailData.from.email == "")                                      //auto generate a noreply address if there was no data filled in in the call
            {                                                                                                        //could potentially create a service to create custom messages based on the occasion
                newMailData.from.email = "noreply@companydomain.com";
            }

            //This will redirect the data that has been filtered to our Redirecting service.
            var response = await Task.Run(() =>
                _redirect.Post(newMailData)
            );


            if (response != "Success")
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "could not contact third party service");
            }
            return Ok(response);
        }

    }


    [Route("api/SendText")]
    [ApiController]
    public class TEXTNotificationController : ControllerBase
    {
        //Redirect access for Twilio
        private readonly IRedirectRepository _redirect = new RedirectText();


        [HttpPost]
        public async Task<ActionResult<SendTextModel>> SendText(SendTextModel newTextData)
        {
            //data validation and filtering.  if going into a database one could encode on entry
            //log data from the request and what they attempted to request with.  Might want to encode this is you are not validation before logging.
            var response = await Task.Run( ()=> _redirect.Post(newTextData));
           
            return Ok(response);

        }


    }
}







/*
 * 
 * Considerations:
 *      using the raw json api or the package offered by sendmail extension in visual...  validators from sendmail could be used on this api as well with some more effort.
 *      [HttpPost]
       public async Task<ActionResult<EmailNotificationModel>> CreateEmailNotification(EmailNotificationModel emailmodel)
        {
            //[logger]  API was access at DateType.Now
            try
            {
                string API_KEY = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");                            //grab the api key from the IDE's enviorment variables
                //var client = new SendGridClient(API_KEY);

                var signature = "<b> Thank you for using Jeremy Goold's Middleware</b>";                            //custom signature embeded in the email

                var msg = MailHelper.CreateSingleEmail( new EmailAddress(emailmodel.Sender),
                                                        new EmailAddress(emailmodel.Recipent),
                                                        emailmodel.Subject, emailmodel.Body,
                                                        signature);
                //[Logger] 
                //var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
                
                
                return Ok(emailmodel);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in contacting third party service");
            }
        }
 */