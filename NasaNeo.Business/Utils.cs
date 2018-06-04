using System;
using System.Collections.Generic;
using System.Text;

namespace NasaNeo.Business
{
    public class Utils
    {
        const int percentOfTimeToPlayRandomMessage = 85;

        private Random randomizer = new Random();

        public string GetRandomMessage(List<string> messages, bool allowEmptyReturn = true)
        {
            var result = string.Empty;

            if (allowEmptyReturn)
            {
                // add a bit of a percentage onto the count so that we don't always get a random message
                var max = messages.Count + (int)Math.Ceiling((((100M - percentOfTimeToPlayRandomMessage) / 100) * messages.Count));
                var randIndex = randomizer.Next(max);

                if (randIndex < messages.Count)
                {
                    result = messages[randIndex];
                }
            }
            else
            {
                result = messages[randomizer.Next(messages.Count)];
            }

            return result;
        }
    }
}
