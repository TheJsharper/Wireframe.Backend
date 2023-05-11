using Microsoft.AspNetCore.Mvc;
using Wireframe.Backend.Models;
using Wireframe.Backend.Repositories;

namespace Wireframe.Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WireframeController : ControllerBase

    {
        public readonly IWireFrameRepository wireFrameRepository;

        private readonly ILogger<WireframeController> _logger;
        public WireframeController(ILogger<WireframeController> logger, IWireFrameRepository wireFrameRepository)
        {
            this.wireFrameRepository = wireFrameRepository;
            this._logger = logger;
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<WireframeModel>))]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await wireFrameRepository.GetAll();

                return  Ok(result);
            }
            catch (ArgumentException e)
            {
                return NotFound(new { e.Message });
            }


        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WireframeModel))]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var result = await wireFrameRepository.GetById(id);

                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return  NotFound(new { e.Message });
            }


        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WireframeModel))]
        public async Task<IActionResult> Post(WireframeModel wireframe)
        {
            try
            {

            var  result = await wireFrameRepository.Post(wireframe);
                return  Ok(result);
            }catch(Exception e)
            {
                return NotFound(new  { e.Message});
            }    

            
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Device))]
        public async Task<IActionResult> Put(string id, Device device)
        {
            try
            {
                var result = await wireFrameRepository.Put(id, device);

                return  CreatedAtAction(nameof(Put), result);
            }
            catch (Exception e)
            {
                return  NotFound(new {  e.Message });
            }

        }
        [HttpPut("ModifyDevice/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Device))]
        public async Task<IActionResult> ModifyDevice(string id, Device device)
        {
            try
            {
                var result = await wireFrameRepository.ModifyDevice(id, device);

                return CreatedAtAction(nameof(ModifyDevice), result);

            }catch(Exception e)
            {
                return NotFound(new { e.Message });
            }
        }


    }
}
