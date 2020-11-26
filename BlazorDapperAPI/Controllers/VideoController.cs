using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlazorDapperAPI.Models;
using BlazorDapperShared.Entities;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlazorDapperAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideoController : ControllerBase
    {
        private readonly IVideoRepository _videoRepository;
        public VideoController(IVideoRepository videoRepository)
        {
            this._videoRepository = videoRepository;
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Video>> GetOneVideo(int id)
        {
            var result = await _videoRepository.GetOne(id);
            return result;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IEnumerable<Video>> GetAll()
        {
            var result = await _videoRepository.GetAll();
            return result;
        }
        [HttpGet]
        [Route("getactive")]
        public async Task<IEnumerable<Video>> GetActive()
        {
            var result = await _videoRepository.GetAll();
            return result;
        }
        [HttpPost]
        public async Task<ActionResult<bool>> InsertVideo(Video video)
        {
            var ok = await _videoRepository.InsertVideo(video);
            return Ok();
        }
        [HttpPut("{id:int}")]
        public async Task<bool> UpdateVideo(int id,Video video)
        {
            var result = await _videoRepository.UpdateVideo(video);
            return result;
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteVideo(int id)
        {
            var result = await _videoRepository.DeleteVideo(id);
            if(result == true)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Cannot delete video");
            }
        }

    }
}
