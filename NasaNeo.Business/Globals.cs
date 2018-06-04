using System;
using System.Collections.Generic;
using System.Text;

namespace NasaNeo.Business
{
    public static class Globals
    {
        public static string FriendlyAppTitle = "Duck and Cover";

        public static List<string> IDidntUnderstand = new List<string>() { "I didn't understand that request. Please try again.",
                                                                           "Hmmm, I'm not sure I can do that. Please try again.",
                                                                           "I'm not sure what you're asking. Can you rephrase that?"};

        public static List<string> GoodBye = new List<string>() { "Good-bye.",
                                                                           "Farewell my friend.",
                                                                           "Check back tomorrow for more threats."};

        public static class SSML
        {
            public static List<string> Phew = new List<string>() { "<say-as interpret-as=\"interjection\">phew</say-as>",
                                                                   "", // blank so we don't wear this out
                                                                   "", // another blank for the randonmizer
                                                                   "<say-as interpret-as=\"interjection\">all righty</say-as>",
                                                                   "<say-as interpret-as=\"interjection\">oy</say-as>" };

            public static List<string> Wow = new List<string>() { "<say-as interpret-as=\"interjection\">wow</say-as>",
                                                                   "", // blank so we don't wear this out
                                                                   "", // another blank for the randonmizer
                                                                   "<say-as interpret-as=\"interjection\">wowza</say-as>",
                                                                   "<say-as interpret-as=\"interjection\">wowzer</say-as>",
                                                                   "<say-as interpret-as=\"interjection\">dun dun dun</say-as>",
                                                                   "<say-as interpret-as=\"interjection\">woo hoo</say-as>",
                                                                   "<say-as interpret-as=\"interjection\">swoosh</say-as>"};
        }
    }
}
