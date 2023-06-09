using System;
using System.Threading.Tasks;
using Diplomna.Common.Dtos;
using Diplomna.Services.Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Diplomna.App
{
    public class BarcodeFunctions
    {
        private readonly ILogger<BarcodeFunctions> _logger;
        private readonly IBarcodeService _barcodeService;

        public BarcodeFunctions(ILogger<BarcodeFunctions> logger, IBarcodeService barcodeService)
        {
            _logger = logger;
            _barcodeService = barcodeService;
        }

        [Function("Barcode")]
        public async Task GetBarcodeAsync([ServiceBusTrigger("%ServiceBusSettings:BarcodeQueueName%")] BarcodeDto input)
        {
            var result = await _barcodeService.LogScanInfo(input);
            if (!result.IsSuccessful)
            {
                if (result.Error == "Invalid token data")
                {
                    _logger.LogCritical(result.Error);
                }
                else
                {
                    _logger.LogError(result.Error);
                }

                throw new Exception(result.Error);
            }
        }
    }
}
