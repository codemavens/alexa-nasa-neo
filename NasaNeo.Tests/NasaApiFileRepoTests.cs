using NasaNeo.Business.Models;
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
            var result = new NasaApiFileRepo().GetNeoForDate(testDate);

            Assert.NotNull(result);

        }

        [Theory]
        [InlineData("2018-03-24")]
        [InlineData("2018-03-25")]
        [InlineData("2018-03-23")]
        public void ProcessNeoFromJson_MultipleEntriesReturnsGivenDate(string testDateParm)
        {
            var fileContents = new System.IO.StreamReader(@"C:\Dev\CodeMavens\Alexa\nasa-neo\sample-data\multi-sample-2018-03-23-to-2018-03-25.json").ReadToEnd();

            var testDate = DateTime.Parse(testDateParm);
            var testItem = NasaApiHelper.ProcessNeoFromJson(fileContents, testDate);

            Assert.True(testItem.ItemsByDate.Count == 1);
            Assert.True(testItem.ItemsByDate[0].Date == testDate);

        }
    }
}
