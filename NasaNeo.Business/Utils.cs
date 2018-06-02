using System;
using System.Collections.Generic;
using System.Text;

namespace NasaNeo.Business
{
    public class Utils
    {
        Random randomizer = new Random();

        public string GetRandomMessage(List<string> messages)
        {
            return messages[randomizer.Next(messages.Count)];
        }
    }
}
