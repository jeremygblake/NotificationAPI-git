using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotificationAPI.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace NotificationAPI.Controllers
{
    [Route("api/PostEmailNotification")]
    [ApiController]

    public class NotificationController : ControllerBase
    {
        [HttpGet]
        public IActionResult testMethod()
        {
            return Ok("TEST");
        }
       [HttpPost]
       public async Task<ActionResult<EmailNotificationModel>> CreateEmailNotification(EmailNotificationModel emailmodel)
        {
            
            try
            {
                 string api_key = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
                
                //client = ?

                //var to = new EmailAddress(); 
               // var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

               // var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
                return Ok(emailmodel.Recipent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in contacting third party service");
            }
        }
        
        private void createmessage()
        {

        }

    }

}
