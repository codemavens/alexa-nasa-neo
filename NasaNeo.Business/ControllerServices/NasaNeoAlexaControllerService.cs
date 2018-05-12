using NasaNeo.Business.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace NasaNeo.Business.ControllerServices
{
    public class NasaNeoAlexaControllerService
    {
        IServiceProvider _serviceProvider;
        public NasaNeoAlexaControllerService(IServiceProvider serviceProvider)
        {            
            _serviceProvider = serviceProvider;
        }

        //public VerifyViewModel GetVerifyViewModel()
        //{
        //    var result = new VerifyViewModel();

        //    // test the db connection
        //    using (var dbContext = _serviceProvider.GetRequiredService<MemoryGameContext>())
        //    {
        //        var count = dbContext.Games.Count();
        //        result.DBConnectionVerified = true;
        //    }

        //    return result;
        //}
    }
}
