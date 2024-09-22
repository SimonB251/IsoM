using IsoMetrix.BL.StringManipulation;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IsoMetrix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StringController : ControllerBase
    {
        private readonly ILogger<StringController> _logger;
        private readonly IStringCalculator _stringCalculator;

        public StringController(ILogger<StringController> logger, IStringCalculator stringCalculator)
        {
            _logger = logger;
            _stringCalculator = stringCalculator;
        }

        [HttpGet("{stringValue}")]
        public int AddDigitsWithinString(string stringValue)
        {
            try
            {
                return _stringCalculator.Add(stringValue);
            }
            catch (InvalidDataException ex)
            {
                _logger.LogError(ex, "StringCalculator failed to add digits within string input");
                return -1;
            }
        }
    }
}