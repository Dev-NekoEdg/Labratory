using Labratory.Domain.Dtos;
using Labratory.Service.Lists;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Laboratory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListsController : ControllerBase
    {
        private readonly IListsService service;

        public ListsController(IListsService service)
        {
            this.service = service;
        }


        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var result = await this.service.GetLists();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await this.service.GetList(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateList([FromBody] ListsDto listsDto)
        {
            try
            {
                var result = await this.service.CreateList(listsDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateList(int id, [FromBody] ListsDto listsDto)
        {
            try
            {
                listsDto.Id = id;
                var result = await this.service.UpdateList(listsDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteList(int id)
        {
            try
            {
                var result = await this.service.DeleteList(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("filtered")]
        public async Task<IActionResult> Index([FromBody] FilterEnvelop<FilterSearch> filter)
        {
            try
            {
                var result = await this.service.GetFilteredLists(filter);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
