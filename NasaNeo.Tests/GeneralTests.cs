using NasaNeo.Business;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NasaNeo.Tests
{
    public class GeneralTests
    {
        [Fact]
        public void Utils_Random_DifferentResponsesReturned()
        {
            var utils = new Utils();

            var response1 = utils.GetRandomMessage(Globals.SSML.Wow);
            var response2 = utils.GetRandomMessage(Globals.SSML.Wow);
            var response3 = utils.GetRandomMessage(Globals.SSML.Wow);
            var response4 = utils.GetRandomMessage(Globals.SSML.Wow);

            Assert.False(String.Equals(response1, response2) && String.Equals(response3, response4) && String.Equals(response1, response3) && String.Equals(response2, response4));
        }

        [Fact]
        public void Utils_RoundWholeNumber()
        {
            var utils = new Utils();

            Assert.Equal(123000000, utils.RoundWholeNumber(123456789));
            Assert.Equal(12000000, utils.RoundWholeNumber(12345678));
            Assert.Equal(1200000, utils.RoundWholeNumber(1234567));
            Assert.Equal(123000, utils.RoundWholeNumber(123456));
            Assert.Equal(12000, utils.RoundWholeNumber(12345));
            Assert.Equal(1200, utils.RoundWholeNumber(1234));
            Assert.Equal(120, utils.RoundWholeNumber(123));
        }

    }
}
