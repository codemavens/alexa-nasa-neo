﻿using NasaNeo.Business.Models;
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
        const int numResultsToReturn = 3;

        //IServiceProvider _serviceProvider;
        private INasaNeoRepo _repo;
        private Utils _util;

        public NasaNeoAlexaControllerService(IServiceProvider serviceProvider, INasaNeoRepo repo)
        {            
            //_serviceProvider = serviceProvider;
            _repo = repo;
            _util = new Utils();
        }

        public async Task<SkillResponse> GetNeoForDate(DateTime neoDate)
        {
            var neoForRange = await _repo.GetNeoForDateAsync(neoDate);

            if(neoForRange.ItemsByDate.Count == 0)
            {
                return NoResultsResponse(neoDate);
            }

            return NeoResponseDetail(neoForRange);            
        }

        private SkillResponse NeoResponseDetail(NasaNeoSet neoForDate)
        {
            var textResult = new StringBuilder(10000);
            var ssmlResult = new StringBuilder(10000);

            textResult.Append($"There are {neoForDate.ElementCount} threats to earth today. Here are the top {numResultsToReturn}.");
            ssmlResult.Append($"<speak>{_util.GetRandomMessage(Globals.SSML.RedAlert)}{textResult.ToString()}");

            for (int i = 0; i < numResultsToReturn && i < neoForDate.ElementCount; i++)
            {
                // right now we're only supporting one item per date. Should change this to not be a list until we support more dates
                textResult.Append(GetTextResponseForNeo(neoForDate.ItemsByDate[0].NeoItems[i]));
                ssmlResult.Append(GetSSMLResponseForNeo(neoForDate.ItemsByDate[0].NeoItems[i]));
            }

            ssmlResult.Append("</speak>");

            return BuildResponse(textResult.ToString(), ssmlResult.ToString());

        }

        //TODO: Make the responses more interesting -vary, add ssml effects or something
        private string GetTextResponseForNeo(NasaNeoItem neo)
        {
      
            var result = new StringBuilder();

            result.Append($"<p>Object {neo.Name}");
            result.Append($" is between {Math.Round(neo.EstimatedDiameter.FeetEstimatedMin, 0)} and {Math.Round(neo.EstimatedDiameter.FeetEstimatedMax, 0)} feet in diameter.");
            result.Append($" It is hurtling towards us at approximately {Math.Round(neo.RelativeVelocity.MilesPerHour, 0)} miles  per hour");
            result.Append($" and it will miss us by a mere {Math.Round(neo.MissDistance.Miles, 0)} miles.");
            result.Append($" Here is the Nasa JPL link for more information: {neo.NasaJplUrl}.</p>");
            
            return result.ToString();
        }
        private string GetSSMLResponseForNeo(NasaNeoItem neo)
        {
            var result = new StringBuilder();
            
            result.Append($"<p>Object {neo.Name} <break strength=\"weak\" /><emphasis level=\"moderate\">{_util.GetRandomMessage(Globals.SSML.Phew)}</emphasis>");
            result.Append($" is between {Math.Round(neo.EstimatedDiameter.FeetEstimatedMin, 0)} and {Math.Round(neo.EstimatedDiameter.FeetEstimatedMax, 0)} feet in diameter.");
            result.Append($" It is hurtling towards us at approximately {Math.Round(neo.RelativeVelocity.MilesPerHour, 0)} miles  per hour");
            result.Append($" and it will miss us by a mere {Math.Round(neo.MissDistance.Miles, 0)} miles <break strength=\"weak\" /><emphasis level=\"strong\">{_util.GetRandomMessage(Globals.SSML.Wow)}</emphasis></p>");
            
            return result.ToString();
        }

        private SkillResponse NoResultsResponse(DateTime startDate)
        {
            var response = BuildResponse($"Yay! There are no threats to earth on {startDate.ToLongDateString()}!");
            return response;
        }

        private SkillResponse BuildResponse(string message, string ssml = "")
        {
            var speech = new Alexa.NET.Response.SsmlOutputSpeech();
            speech.Ssml = (ssml.Length > 0 ? ssml : $"<speak>{message}</speak>");

            // create the speech reprompt
            var repromptMessage = new Alexa.NET.Response.PlainTextOutputSpeech();
            repromptMessage.Text = message;

            // create the reprompt
            var repromptBody = new Alexa.NET.Response.Reprompt();
            repromptBody.OutputSpeech = repromptMessage;

            var finalResponse = ResponseBuilder.Ask(speech, repromptBody);
            return finalResponse;
        }
    }
}
