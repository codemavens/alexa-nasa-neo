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

        /// <summary>
        /// Rounds a number to something more pleasant. So something like 173,236 will round to 173,000, etc
        /// </summary>
        /// <param name="numToRound"></param>
        public int RoundWholeNumber(int numToRound)
        {
            var result = numToRound;

            if(numToRound > 100000000) //123,456,789 -> 123,000,000
            {
                result = (int)(numToRound / 1000000) * 1000000;
            }
            else if (numToRound > 10000000) //12,345,678 -> 12,000,000
            {
                result = (int)(numToRound / 1000000) * 1000000;
            }
            else if (numToRound > 1000000) //1,234,567 -> 1,200,000
            {
                result = (int)(numToRound / 100000) * 100000;
            }
            else if (numToRound > 100000) //123,456 -> 123,000
            {
                result = (int)(numToRound / 1000) * 1000;
            }
            else if (numToRound > 10000) //12,345 -> 12,000
            {
                result = (int)(numToRound / 1000) * 1000;
            }
            else if (numToRound > 1000) //1,234 -> 1,200
            {
                result = (int)(numToRound / 100) * 100;
            }
            else if (numToRound > 100) //123 -> 120
            {
                result = (int)(numToRound / 10) * 10;
            }

            return result;
        }

        public int RoundWholeNumber(float numToRound)
        {
            return RoundWholeNumber((int)numToRound);
        }
    }
}
