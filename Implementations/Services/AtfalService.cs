using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Atfal360.Contracts.Repositories;
using Atfal360.Contracts.Services;
using Atfal360.Dto;
using Atfal360.Entities;
using Atfal360.Paging;
using CsvHelper;

namespace Atfal360.Implementations.Services
{
    public class AtfalService : IAtfalService
    {
        private readonly IAtfalRepository _atfalRepository;
        public AtfalService(IAtfalRepository atfalRepository)
        {
            _atfalRepository = atfalRepository;
        }
        public async Task<AtfalDto> Create(CreateAtfalRequestModel atfal)
        {
            var atfalObj = new Atfal
            {
                Name = $"{atfal.FirstName} {atfal.LastName}",
                Muqami = atfal.Muqami,
                Dila = atfal.Dila,
                Region = atfal.Region,
                State = atfal.State,
                Age = atfal.Age
            };
            var atfalCreatedObj = await _atfalRepository.Create(atfalObj);
            return new AtfalDto
            {
                Name = atfalCreatedObj.Name,
                Muqami = atfalCreatedObj.Muqami,
                Dila = atfalCreatedObj.Dila,
                State = atfalCreatedObj.State,
                Region = atfalCreatedObj.Region,
                Age = atfal.Age,
                Id = atfalCreatedObj.Id
            };
        }

        public async Task<byte[]> DownloadCsv(GetAtfalRequestModel request)
        {
            using (var memoryStream = new MemoryStream())
            using (var writer = new StreamWriter(memoryStream))
            using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csvWriter.WriteField("Name");
                csvWriter.WriteField("Region");
                csvWriter.WriteField("State");
                csvWriter.WriteField("Dila");
                csvWriter.WriteField("Muqami");
                csvWriter.WriteField("Age");
                csvWriter.NextRecord();
                 var items = await _atfalRepository.Get(a => request.Name != null ? a.Name.ToLower().Contains(request.Name.ToLower()) : true
                    &&
                    request.Muqami != null ? a.Muqami.ToLower().Contains(request.Muqami.ToLower()) : true
                    &&
                    request.State != null ? a.State.ToLower().Contains(request.State.ToLower()) : true && request.Region != null ? a.Region.ToLower().Contains(request.Region.ToLower()) : true
                    &&
                    request.Dila != null ? a.Dila.ToLower().Contains(request.Dila.ToLower()) : true
                    &&
                    request.Region != null ? a.Region.ToLower().Contains(request.Region.ToLower()) : true
                    &&
                    request.Age > 0 ? a.Age == request.Age : true,
                    request); 
                foreach (var item in items.Items)
                {
                    csvWriter.WriteField(item.Name);
                    csvWriter.WriteField(item.Region);
                    csvWriter.WriteField(item.State);
                    csvWriter.WriteField(item.Dila);
                    csvWriter.WriteField(item.Muqami);
                    csvWriter.WriteField(item.Age);
                    csvWriter.NextRecord();
                }

                // Flush and return the CSV as byte array
                writer.Flush();
                return memoryStream.ToArray();
            }

        }

        public async Task<PaginatedList<AtfalDto>> GetAtfal(GetAtfalRequestModel request)
        {
            var items = await _atfalRepository.Get(a => request.Name != null ? a.Name.ToLower().Contains(request.Name.ToLower()) : true
            &&
            request.Muqami != null ? a.Muqami.ToLower().Contains(request.Muqami.ToLower()) : true
            &&
            request.State != null ? a.State.ToLower().Contains(request.State.ToLower()) : true && request.Region != null ? a.Region.ToLower().Contains(request.Region.ToLower()) : true
            &&
            request.Dila != null ? a.Dila.ToLower().Contains(request.Dila.ToLower()) : true
            &&
            request.Region != null ? a.Region.ToLower().Contains(request.Region.ToLower()) : true
            &&
            request.Age > 0 ? a.Age == request.Age : true,
            request);
            return new PaginatedList<AtfalDto>
            {
                Items = items.Items.Select(a => new AtfalDto
                {
                    Name = a.Name,
                    Muqami = a.Muqami,
                    Dila = a.Dila,
                    State = a.State,
                    Region = a.Region
                }).ToList(),
                Page = items.Page,
                PageSize = items.PageSize,
                TotalItems = items.TotalItems,
            };
        }

        public GetAtfalStatisticsResponse GetAtfalStatistics()
        {
            var query = _atfalRepository.GetAtfals();
            return new GetAtfalStatisticsResponse
            {
                PreschoolSchoolCount = query.Where(a => a.Age > 0 && a.Age < 6).Count(),
                PreTeenCount = query.Where(a => a.Age > 10 && a.Age < 14).Count(),
                EarlyChildhoodCount = query.Where(a => a.Age > 5 && a.Age < 11).Count(),
                TeenCount = query.Where(a => a.Age > 13 && a.Age < 18).Count(),
                TotalCount = query.Count(),
            };
        }
    }
}