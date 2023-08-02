using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AzFunctionServiceBus
{
    public static class Function1
    {
        //private readonly ILogger<Function1> _logger;

        //public Function1(ILogger<Function1> log)
        //{
        //    _logger = log;
        //}

        [FunctionName("Function1")]
        public static void Run([ServiceBusTrigger("facturacion-topic", "facturacion-subscript", Connection = "FacturacionConnString")]string mySbMsg, ILogger logger)
        {
            logger.LogInformation($"C# ServiceBus topic trigger function processed message: {mySbMsg}");
        }
    }
}
