using System;
using System.Threading.Tasks;
using BlazorDapperAPI.Data;
using BlazorDapperShared.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Collections.Generic;

namespace BlazorDapperAPI.Models
{
    public class VideoRepository : IVideoRepository
    {
        private readonly SqlConnectionConfiguration _sqlConnectionConfiguration;
        public VideoRepository(SqlConnectionConfiguration sqlConnectionConfiguration)
        {
            this._sqlConnectionConfiguration = sqlConnectionConfiguration;
        }

        public async Task<bool> DeleteVideo(int videoID)
        {
            try
            {
                using (var conn = new SqlConnection(_sqlConnectionConfiguration.Value))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("VideoID", videoID, DbType.Int32);
                    const string query = "sp_Video_Delete";
                    await conn.QueryAsync(query, parameters, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> InsertVideo(Video video)
        {
            using(var conn = new SqlConnection(_sqlConnectionConfiguration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("Title", video.Title,DbType.String);
                parameters.Add("DatePublished", video.DatePublished, DbType.Date);
                parameters.Add("IsActive", video.IsActive, DbType.Boolean);
                const string query = "sp_Video_Insert";
                await conn.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
                
            }
            return true;
        }
        public async Task<IEnumerable<Video>> GetAll()
        {
            IEnumerable<Video> videos;
            using (var conn = new SqlConnection(_sqlConnectionConfiguration.Value))
            {
                 videos = await conn.QueryAsync<Video>("sp_Video_GetAll", commandType: CommandType.StoredProcedure);

            }
            return videos;
        }
        public async Task<IEnumerable<Video>> GetActive()
        {
            IEnumerable<Video> videos;
            using (var conn = new SqlConnection(_sqlConnectionConfiguration.Value))
            {
                videos = await conn.QueryAsync<Video>("sp_Video_GetActive", commandType: CommandType.StoredProcedure);

            }
            return videos;
        }
        public async Task<bool> UpdateVideo(Video video)
        {
            try
            {
                using (var conn = new SqlConnection(_sqlConnectionConfiguration.Value))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("VideoID", video.VideoID, DbType.Int32);
                    parameters.Add("VideoTitle", video.Title, DbType.String);
                    parameters.Add("DatePublished", video.DatePublished, DbType.Date);
                    parameters.Add("IsActive", video.IsActive, DbType.Boolean);
                    const string query = "sp_Video_Update";
                    await conn.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<Video> GetOne(int videoID)
        {
            Video newVideo = new Video();
            try
            {
                using(var conn = new SqlConnection(_sqlConnectionConfiguration.Value))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("VideoID", videoID, DbType.Int32);
                    const string query = "sp_Video_GetByID";
                    newVideo = await conn.QueryFirstOrDefaultAsync<Video>(query, parameters, commandType: CommandType.StoredProcedure);
                    return newVideo;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return newVideo;
            }
        }
    }
}
