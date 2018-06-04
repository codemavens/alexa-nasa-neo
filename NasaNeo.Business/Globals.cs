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

        public static class SSML
        {
            public static List<string> Phew = new List<string>() { "<p><say-as interpret-as=\"interjection\">phew</p>",
                                                                   "", // blank so we don't wear this out
                                                                   "", // another blank for the randonmizer
                                                                   "all righty",
                                                                   "oy" };

            public static List<string> Wow = new List<string>() { "<p><say-as interpret-as=\"interjection\">wow</p>",
                                                                   "", // blank so we don't wear this out
                                                                   "", // another blank for the randonmizer
                                                                   "wowza",
                                                                   "wowzer",
                                                                   "dun dun dun",
                                                                   "woo hoo",
                                                                   "swoosh"};
        }
    }
}
