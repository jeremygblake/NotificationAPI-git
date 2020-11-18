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


       [HttpPost]
       public async Task<ActionResult<EmailNotificationModel>> CreateEmailNotification(EmailNotificationModel emailmodel)
        {
            
            try
            {
                //
                var API_KEY = "";
                var client = new SendGridClient(API_KEY);
                //data

                //send the sendgrid request

                var from = new EmailAddress();   //Todo: populate emails
                var to = new EmailAddress(); 
                var subject = "";
                var plainTextContent = "";
                var htmlContent = "";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

                var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in contacting third party service");
            }
        }
        

    }

}
