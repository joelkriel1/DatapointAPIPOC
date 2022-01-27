using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatapointAPIPOC.Interfaces;
using DatapointAPIPOC.Models;
using IronPython.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Scripting.Hosting;
using Newtonsoft.Json;

namespace DatapointAPIPOC.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class NaaController : Controller
    {
        private readonly INaaProvider _naaProvider;

        public NaaController(INaaProvider naaProvider)
        {
            _naaProvider = naaProvider;
        }

        [HttpGet("{id}", Name = "GetTileById")]
        public async Task<IActionResult> GetTileCardAsync(int id)
        {
            var result = await _naaProvider.GetTileCard1Async(id);
            if (result.IsSuccess)
            {
                return Ok(result.TileCard1);
            }
            return NotFound(result.ErrorMessage);
        }

        [HttpPost("PostTileCard1")]
        [Produces("application/json")]
        public async Task<IActionResult> GetTileCardAsync([FromBody] TileRequestObject tileRequestObject)
        {
            var result = await _naaProvider.GetTileCard1Async(tileRequestObject);
            if (result.IsSuccess)
            {
                return Ok(result.TileCard1);
            }
            return NotFound(result.ErrorMessage);
        }

        [HttpPost("PostTileCard2")]
        [Produces("application/json")]
        public async Task<IActionResult> GetTileCard2Async([FromBody] TileRequestObject tileRequestObject)
        {
            var result = await _naaProvider.GetTileCard2Async(tileRequestObject);
            if (result.IsSuccess)
            {
                return Ok(result.TileCard2);
            }
            return NotFound(result.ErrorMessage);
        }

        [HttpPost("PostTileCard3")]
        [Produces("application/json")]
        public async Task<IActionResult> GetTileCard3Async([FromBody] TileRequestObject tileRequestObject)
        {
            var result = await _naaProvider.GetTileCard3Async(tileRequestObject);
            if (result.IsSuccess)
            {
                return Ok(result.TileCard3);
            }
            return NotFound(result.ErrorMessage);
        }

        [HttpPost("PostTileCard4")]
        [Produces("application/json")]
        public async Task<IActionResult> GetTileCard4Async([FromBody] TileRequestObject tileRequestObject)
        {
            var result = await _naaProvider.GetTileCard4Async(tileRequestObject);
            if (result.IsSuccess)
            {
                return Ok(result.TileCard4);
            }
            return NotFound(result.ErrorMessage);
        }

        [HttpPost("PostTileCard5")]
        [Produces("application/json")]
        public async Task<IActionResult> GetTileCard5Async([FromBody] TileRequestObject tileRequestObject)
        {
            var result = await _naaProvider.GetTileCard5Async(tileRequestObject);
            if (result.IsSuccess)
            {
                return Ok(result.TileCard5);
            }
            return NotFound(result.ErrorMessage);
        }

        [HttpPost("PostTableData")]
        [Produces("application/json")]
        public async Task<IActionResult> GetTableDataAsync([FromBody] TileRequestObject tileRequestObject)
        {
            var result = await _naaProvider.GetTabledDataAsync(tileRequestObject);
            if (result.IsSuccess)
            {
                return Ok(result.TabledData);
            }
            return NotFound(result.ErrorMessage);
        }

        [HttpPost("PostTileRequestObjectAPI")]
        [Produces("application/json")]
        public IActionResult GetTileCardAsyncAPI([FromBody] TileRequestObject tileRequestObject)
        {
            ScriptEngine engine = Python.CreateEngine();
            var searchPaths = engine.GetSearchPaths();
            searchPaths.Add(@"C:\Users\krielj\AppData\Local\Programs\Python\Python310\Lib");
            searchPaths.Add(@"C:\Users\krielj\AppData\Local\Programs\Python\Python310\Lib\site-packages");
            //searchPaths.Add(@"C:\Users\krielj\AppData\Local\Programs\Python\Python310\libs");
            engine.SetSearchPaths(searchPaths);
            return Ok(engine.ExecuteFile(@"C:\Users\krielj\Downloads\Python-Snowflake-API\Working-Code-api\generate-api-jwt.py").ToString());

            //var result = await _naaProvider.GetTileCard1Async(tileRequestObject);
            //if (result.IsSuccess)
            //{
            //    return Ok(result.TileCard1);
            //}
            //return NotFound(result.ErrorMessage);
        }

    }
}