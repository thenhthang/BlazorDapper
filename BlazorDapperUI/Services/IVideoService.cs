using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorDapperShared.Entities;

namespace BlazorDapperUI.Services
{
    public interface IVideoService
    {
        Task<bool> InsertVideo(Video video);
        Task<IEnumerable<Video>> GetAll();
        Task<IEnumerable<Video>> GetActive();

    }
}
