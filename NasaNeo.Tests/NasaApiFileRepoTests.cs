using NasaNeo.Business.NasaApi;
using System;
using Xunit;

namespace NasaNeo.Tests
{
    public class NasaApiFileRepoTests
    {
        [Fact]
        public void GetNeoForDateRange_SingleDateTest()
        {
            var testDate = DateTime.Parse("2018-05-17");
            var result = new NasaApiFileRepo().GetNeoForDateRange(testDate, testDate);

            Assert.NotNull(result);

        }
    }
}
