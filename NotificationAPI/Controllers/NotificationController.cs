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

            if (newMailData.from.email == null || newMailData.from.email == "")              //auto generate a noreply address if there was no data filled in in the call
            {
                newMailData.from.email = "noreply@companydomain.com";
            }


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

        private readonly IRedirectRepository _redirect = new RedirectText();


        [HttpPost]
        public async Task<ActionResult<SendTextModel>> SendText(SendTextModel newTextData)
        {
            //data validation and filtering.  if going into a database one could encode on entry
            var response = await Task.Run( ()=>_redirect.Post(newTextData));
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