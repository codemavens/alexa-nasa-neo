using MemoryGame.Business.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MemoryGame.Business.ControllerServices
{
    public class MemoryGameAlexaControllerService
    {
        IServiceProvider _serviceProvider;
        public MemoryGameAlexaControllerService(IServiceProvider serviceProvider)
        {            
            _serviceProvider = serviceProvider;
        }

        public VerifyViewModel GetVerifyViewModel()
        {
            var result = new VerifyViewModel();

            // test the db connection
            using (var dbContext = _serviceProvider.GetRequiredService<MemoryGameContext>())
            {
                var count = dbContext.Games.Count();
                result.DBConnectionVerified = true;
            }

            return result;
        }
    }
}
