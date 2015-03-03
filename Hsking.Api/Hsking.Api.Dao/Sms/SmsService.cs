using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Twilio;

namespace Hsking.Api.Dao.Sms
{
    public class SmsService : IIdentityMessageService
    {
        private string _sid;
        private readonly string _from;
        private string _token;

        public SmsService(SmsServiceInitializator inititializator)
        {
            _token = inititializator.Token;
            _sid = inititializator.Sid;
            _from = inititializator.From;
        }

        public Task SendAsync(IdentityMessage message)
        {
            var twilio = new TwilioRestClient(_sid,_token);
            var result = twilio.SendMessage(_from,
               message.Destination, message.Body);

            // Status is one of Queued, Sending, Sent, Failed or null if the number is not valid
            Trace.TraceInformation(result.Status);

            // Twilio doesn't currently have an async API, so return success.
            return Task.FromResult(0);
        }
    }
}
