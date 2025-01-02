using Labratory.Domain.Configs;
using Labratory.Domain.Dtos;
using Labratory.Domain.Exceptions;
using Labratory.Service.BlobStorage;
using Labratory.Service.ListItems;
using Labratory.Service.Lists;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Laboratory.API.Controllers
{
    [Route("api/v1/lists-items")]
    [ApiController]
    public class ListItemsController : ControllerBase
    {

        private readonly IListItemService service;
        private readonly IBlobStorageService blobStorageService;
        private readonly CommonConfig commonConfig;

        public ListItemsController(IListItemService service,
            IBlobStorageService blobStorageService,
            IOptions<CommonConfig> options)
        {
            this.service = service;
            this.blobStorageService = blobStorageService;
            this.commonConfig = options.Value;
        }

        [HttpGet("{listId}")]
        public async Task<IActionResult> Index(int listId)
        {
            var list = await service.GetListsItemsAsync(listId);

            return Ok(list);
        }

        [HttpGet("{listId}/{listItemId}")]
        public async Task<IActionResult> GetById(int listId, int listItemId)
        {
            var item = await service.GetListsItemsAsync(listId, listItemId);

            return Ok(item);
        }

        [HttpPost("{listId}")]
        public async Task<IActionResult> Create(int listId, [FromBody] ListsItemsDto listsItemsDto)
        {
            listsItemsDto.ListId = listId;
            var list = await service.CreateListsItemsAsync(listsItemsDto);

            return Ok(list);
        }

        [HttpPut("{listId}/{listItemId}")]
        public async Task<IActionResult> Update(int listId, int listItemId, [FromBody] ListsItemsDto listsItemsDto)
        {
            listsItemsDto.Id = listItemId;
            listsItemsDto.ListId = listId;
            var list = await service.UpdateListsItemsAsync(listsItemsDto);

            return Ok(list);
        }

        [HttpDelete("{listId}/{listItemId}")]
        public async Task<IActionResult> Delete(int listId, int listItemId, [FromBody] ListsItemsDto listsItemsDto)
        {
            listsItemsDto.Id = listItemId;
            listsItemsDto.ListId = listId;
            var list = await service.DeleteListsItemsAsync(listsItemsDto);

            return Ok(list);
        }

        [HttpPost("{listId}/update-image/{listItemId}")]
        public async Task<IActionResult> LoadImageForItem(int listId, int listItemId, [FromBody] IFormFile image)
        {
            try
            {
                string extention = Path.GetExtension(image.FileName).ToLowerInvariant();
                if (!this.commonConfig.AllowedImageExt.Contains(extention.Replace(".", "")))
                {
                    return BadRequest("File not allowed");
                }

                var result = await this.service.UpdateListsItemImageAsync(listId, listItemId, extention, image.OpenReadStream());
                // var ruta = await this.blobStorageService.SaveImageIntoBlobStorage(image.FileName, name, image.OpenReadStream());

                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{listId}/upload-list")]
        public async Task<IActionResult> LoadImageForItem(int listId, IFormFile fileCSV)
        {
            try
            {
                string extention = Path.GetExtension(fileCSV.FileName).ToLowerInvariant();
                if (extention.Replace(".", "") != "csv")
                {
                    return BadRequest("File not allowed");
                }

                var result = await this.service.LoadListsItemAsync(listId, extention, fileCSV.OpenReadStream());
                // var ruta = await this.blobStorageService.SaveImageIntoBlobStorage(image.FileName, name, image.OpenReadStream());

                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("{listId}/filter")]
        public async Task<IActionResult> Index(int listId,[FromBody] FilterEnvelop<FilterSearch> filter)
        {
            try
            {
                var list = await service.GetFilteredListsItemsAsync(listId, filter);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
