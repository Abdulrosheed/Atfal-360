using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Atfal360.Dto;
using Atfal360.Paging;

namespace Atfal360.Contracts.Services
{
    public interface IAtfalService
    {
        Task<AtfalDto> Create (CreateAtfalRequestModel atfal);
        Task<PaginatedList<AtfalDto>> GetAtfal (GetAtfalRequestModel request);
        GetAtfalStatisticsResponse GetAtfalStatistics();
        Task<byte[]> DownloadCsv(GetAtfalRequestModel request);

    }
}