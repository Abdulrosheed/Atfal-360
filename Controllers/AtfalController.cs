using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Atfal360.Contracts.Services;
using Atfal360.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Atfal360.Controllers
{
    [Route("api/atfals")]
    [ApiController]
    public class AtfalController : ControllerBase
    {
        private readonly IAtfalService _atfalService;
        public AtfalController(IAtfalService atfalService)
        {
            _atfalService = atfalService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAtfalRequestModel model)
        {
            var response = await _atfalService.Create(model);
            return CreatedAtAction(nameof(Create), new { id = response.Id }, response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAtfals([FromQuery] GetAtfalRequestModel model)
        {
            var response = await _atfalService.GetAtfal(model);
            return Ok(response);
        }
        [HttpGet("statistics")]
        public IActionResult GetAtfalStatistics()
        {
            var response = _atfalService.GetAtfalStatistics();
            return Ok(response);
        }
        [HttpGet("download-csv")]
        public async Task<IActionResult> DowloadAtfalRecordsCsv([FromQuery] GetAtfalRequestModel model)
        {
            model.UsePaging = false;
            var fileContent = await _atfalService.DownloadCsv(model);
            var contentType = "text/csv";
            var fileName = "data.csv";
            return File(fileContent, contentType, fileName);
        }
    }
}