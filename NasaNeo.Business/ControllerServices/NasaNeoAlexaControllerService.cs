using NasaNeo.Business.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NasaNeo.Business.NasaApi;
using System.Threading.Tasks;
using Alexa.NET.Response;
using Alexa.NET;

namespace NasaNeo.Business.ControllerServices
{
    public class NasaNeoAlexaControllerService
    {
        IServiceProvider _serviceProvider;
        INasaNeoRepo _repo;

        public NasaNeoAlexaControllerService(IServiceProvider serviceProvider, INasaNeoRepo repo)
        {            
            _serviceProvider = serviceProvider;
        }

        public SkillResponse GetNeoForDateRange(DateTime startDate, DateTime endDate)
        {
            var neoForRange = _repo.GetNeoForDateRange(startDate, endDate);

            if(neoForRange.Count == 0)
            {
                return NoResultsResponse(startDate, endDate);
            }

            return NoResultsResponse(startDate, endDate);            
        }

        private SkillResponse NoResultsResponse(DateTime startDate, DateTime endDate)
        {
            var response = BuildResponse("Yay! There are no threats to earth on that date!");
            return response;
        }

        private SkillResponse BuildResponse(string message)
        {
            var speech = new Alexa.NET.Response.SsmlOutputSpeech();
            speech.Ssml = $"<speak>{message}</speak>";

            // create the speech reprompt
            var repromptMessage = new Alexa.NET.Response.PlainTextOutputSpeech();
            repromptMessage.Text = message;

            // create the reprompt
            var repromptBody = new Alexa.NET.Response.Reprompt();
            repromptBody.OutputSpeech = repromptMessage;

            var finalResponse = ResponseBuilder.Ask(speech, repromptBody);
            return finalResponse;
        }
        //public VerifyViewModel GetVerifyViewModel()
        //{
        //    var result = new VerifyViewModel();

        //    // test the db connection
        //    using (var dbContext = _serviceProvider.GetRequiredService<MemoryGameContext>())
        //    {
        //        var count = dbContext.Games.Count();
        //        result.DBConnectionVerified = true;
        //    }

        //    return result;
        //}
    }
}
