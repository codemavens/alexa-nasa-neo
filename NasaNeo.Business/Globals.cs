using System;
using System.Collections.Generic;
using System.Text;

namespace NasaNeo.Business
{
    public static class Globals
    {
        public static string FriendlyAppTitle = "Earth Defense";

        public static List<string> IDidntUnderstand = new List<string>() { "I didn't understand that request. Please try again.",
                                                                           "Hmmm, I'm not sure I can do that. Please try again.",
                                                                           "I'm not sure what you're asking. Can you rephrase that?"};

        public static List<string> GoodBye = new List<string>() { "Good-bye.",
                                                                           "Farewell my friend.",
                                                                           "Check back tomorrow for more threats."};

        public static class SSML
        {
            public static List<string> Phew = new List<string>() { "<say-as interpret-as=\"interjection\">phew</say-as>",
                                                                   "<say-as interpret-as=\"interjection\">all righty</say-as>",
                                                                   "<say-as interpret-as=\"interjection\">oy</say-as>" };

            public static List<string> Wow = new List<string>() { "<say-as interpret-as=\"interjection\">wow</say-as>",
                                                                   //"<say-as interpret-as=\"interjection\">wowza</say-as>",
                                                                   //"<say-as interpret-as=\"interjection\">wowzer</say-as>",
                                                                   "<say-as interpret-as=\"interjection\">dun dun dun</say-as>",
                                                                   "<say-as interpret-as=\"interjection\">woo hoo</say-as>",
                                                                   "<say-as interpret-as=\"interjection\">swoosh</say-as>",
                                                                    "<audio src='https://s3.amazonaws.com/ask-soundlibrary/scifi/amzn_sfx_scifi_incoming_explosion_01.mp3'/>",
                                                                    "<audio src='https://s3.amazonaws.com/ask-soundlibrary/scifi/amzn_sfx_scifi_explosion_2x_01.mp3'/>"};

            public static List<string> Incoming = new List<string>() { "<audio src='https://s3.amazonaws.com/ask-soundlibrary/scifi/amzn_sfx_scifi_incoming_explosion_01.mp3'/>",
                                                                       "<audio src='https://s3.amazonaws.com/ask-soundlibrary/scifi/amzn_sfx_scifi_explosion_2x_01.mp3'/>",
                                                                      };

            public static List<string> RedAlert = new List<string>() { "<audio src='https://s3.amazonaws.com/ask-soundlibrary/scifi/amzn_sfx_scifi_alarm_01.mp3'/>",
                                                                       "<audio src='https://s3.amazonaws.com/ask-soundlibrary/scifi/amzn_sfx_scifi_alarm_02.mp3'/>",
                                                                       "<audio src='https://s3.amazonaws.com/ask-soundlibrary/scifi/amzn_sfx_scifi_alarm_04.mp3'/>",
                                                                        };
        }
    }
}
