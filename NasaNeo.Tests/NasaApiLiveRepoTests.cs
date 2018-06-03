using Microsoft.Extensions.Configuration;
using NasaNeo.Business.NasaApi;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NasaNeo.Tests
{
    public class NasaApiLiveRepoTests
    {
        public IConfiguration _config;

        public NasaApiLiveRepoTests()
        {
            _config = TestHelper.GetApplicationConfiguration();
        }

        [Fact]
        public async void GetNeoForDateAsync_SingleDateTest()
        {
            var testDate = DateTime.Parse("2018-05-17");
            var result = await new NasaApiLiveRepo(_config).GetNeoForDateAsync(testDate);

            Assert.NotNull(result);

        }

        [Theory]
        [InlineData("2018-03-24")]
        [InlineData("2018-03-25")]
        [InlineData("2018-03-23")]
        public async void GetNeoForDateAsync_MultipleEntriesReturnsSingleItemForGivenDate(string testDateParm)
        {
            var testDate = DateTime.Parse(testDateParm);
            var testItem = await new NasaApiLiveRepo(_config).GetNeoForDateAsync(testDate);

            Assert.True(testItem.ItemsByDate.Count == 1);
            Assert.True(testItem.ItemsByDate[0].Date == testDate);

        }

    }
}
