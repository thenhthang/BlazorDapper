using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorDapperShared.Entities;

namespace BlazorDapperAPI.Models
{
    public interface IVideoRepository
    {
        Task<bool> InsertVideo(Video video);
        Task<bool> DeleteVideo(int videoID);
        Task<Video> GetOne(int videoID);
        Task<bool> UpdateVideo(Video video);
        Task<IEnumerable<Video>> GetAll();
        Task<IEnumerable<Video>> GetActive();
    }
}
