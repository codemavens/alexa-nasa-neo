using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MemoryGame.Business.ControllerServices;
using MemoryGame.WebApi.Controllers;
using Alexa.NET.Response;
using Microsoft.ApplicationInsights.DataContracts;
using Alexa.NET.Request.Type;
using Alexa.NET;
using MemoryGame.Business.Models;

namespace memory.Controllers
{
    [Route("api/alexa/memory-game")]
    public class MemoryGameAlexaController : AlexaBaseController
    {
        private IConfiguration _configuration;
        private MemoryGameAlexaControllerService _alexaControllerService;
        
        public MemoryGameAlexaController(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _configuration = configuration;
            _alexaControllerService = new MemoryGameAlexaControllerService(serviceProvider);
        }

        [HttpGet]
        [Route("verify")]
        public JsonResult Verify()
        {
            var result = _alexaControllerService.GetVerifyViewModel();
            return Json("The service is running");
        }

        [HttpPost]
        [Route("alexa-home")]
        public async Task<SkillResponse> AlexaHome()
        {
            try
            {
                var input = await GetSkillRequestFromPost();
                if (input == null || input.Request == null) { LogMessage("input request is null", SeverityLevel.Error, null); }

                // check what type of a request it is like an IntentRequest or a LaunchRequest
                var requestType = input.GetRequestType();
                LogMessage($"requestType == {requestType}", SeverityLevel.Information, null);

                if (requestType == typeof(IntentRequest))
                {
                    // do some intent-based stuff
                    var intentRequest = input.Request as IntentRequest;

                    // check the name to determine what you should do
                    if (intentRequest.Intent.Name.Equals("ScriptureIntent"))
                    {
                        // get the slots
                        //var firstValue = intentRequest.Intent.Slots["FirstSlot"].Value;

                        //return EncouragingScripture();
                        return null;
                    }
                    else if (intentRequest.Intent.Name.Equals(Alexa.NET.Request.Type.BuiltInIntent.Help))
                    {
                        return Help();
                    }
                    else
                    {
                        return ErrorResponse();
                    }
                }
                else if (requestType == typeof(Alexa.NET.Request.Type.LaunchRequest))
                {
                    return Usage();
                }
                else
                {
                    return ErrorResponse();
                }

            }
            catch (Exception exc)
            {
                return ErrorResponse(exc);
            }
        }

        private SkillResponse ErrorResponse(Exception exc = null)
        {
            if (exc != null)
            {
                LogMessage(exc.Message, SeverityLevel.Error, new Dictionary<string, string>() { { "Stack Trace", exc.StackTrace } });
            }

            // build the speech response 
            var speech = new Alexa.NET.Response.SsmlOutputSpeech();
            speech.Ssml = $"<speak>I didn't understand that request. Please try again.</speak>";

            // create the response using the ResponseBuilder
            var finalResponse = ResponseBuilder.Tell(speech);
            return finalResponse;
        }

        private SkillResponse Usage()
        {
            // create the speech response - cards still need a voice response
            var speech = new Alexa.NET.Response.SsmlOutputSpeech();
            speech.Ssml = "<speak>Welcome to Memory. You can say 'Start game' to start a game or say Help to get more information.</speak>";

            // create the speech reprompt
            var repromptMessage = new Alexa.NET.Response.PlainTextOutputSpeech();
            repromptMessage.Text = "You can say 'Start game' to start a game or say Help to get more information.";

            // create the reprompt
            var repromptBody = new Alexa.NET.Response.Reprompt();
            repromptBody.OutputSpeech = repromptMessage;

            var finalResponse = ResponseBuilder.Ask(speech, repromptBody);
            return finalResponse;
        }

        private SkillResponse Help()
        {
            // create the speech response - cards still need a voice response
            var speech = new Alexa.NET.Response.SsmlOutputSpeech();
            speech.Ssml = "<speak>Memory is a game that tests and improves your memory.</speak>";

            // create the speech reprompt
            var repromptMessage = new Alexa.NET.Response.PlainTextOutputSpeech();
            repromptMessage.Text = "If you would like to start a game, say 'start game'.";

            // create the reprompt
            var repromptBody = new Alexa.NET.Response.Reprompt();
            repromptBody.OutputSpeech = repromptMessage;

            var finalResponse = ResponseBuilder.Ask(speech, repromptBody);
            return finalResponse;
        }


    }
}
