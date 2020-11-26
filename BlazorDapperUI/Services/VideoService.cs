using System;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorDapperShared.Entities;
using System.Net.Http.Json;
using BlazorDapperUI.IHttpClient;
using System.Collections;
using System.Collections.Generic;

namespace BlazorDapperUI.Services
{
    public class VideoService : IVideoService
    {
        private readonly HttpClient _httpClient;
        public VideoService(VideoHttpClient videoHttpClient)
        {
            this._httpClient = videoHttpClient.value;
        }

        public async Task<IEnumerable<Video>> GetActive()
        {
            var result = await _httpClient.GetFromJsonAsync<IEnumerable<Video>>("api/video/getactive");
            return result;
        }

        public async Task<IEnumerable<Video>> GetAll()
        {
            var result = await _httpClient.GetFromJsonAsync<IEnumerable<Video>>("api/video/getall");
            return result;

        }

        public async Task<bool> InsertVideo(Video video)
        {
            var result = await _httpClient.PostAsJsonAsync<Video>("api/video", video);
            if(result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }
        
    }
}
