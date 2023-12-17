using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Atfal360.Paging;

namespace Atfal360.Dto
{
    public record AtfalDto
    {
        public int Id {get;set;}
        public string  Name {get;set;}
        public string Region {get;set;}
        public string State {get;set;}
        public string Dila {get;set;}
        public string Muqami {get;set;}
        public int Age {get;set;}

    }
    public record CreateAtfalRequestModel
    {
        public string  FirstName {get;set;}
        public string  LastName {get;set;}
        public string Region {get;set;}
        public string State {get;set;}
        public string Dila {get;set;}
        public string Muqami {get;set;}
        public int Age {get;set;}
    }
    public record GetAtfalRequestModel : PageRequest
    {
        public string?  Name {get;set;}
        public string? Region {get;set;}
        public string? State {get;set;}
        public string? Dila {get;set;}
        public string? Muqami {get;set;}
        public int? Age {get;set;}

    }
    public record GetAtfalStatisticsResponse
    {
       public int PreschoolSchoolCount {get;set;}
        public int EarlyChildhoodCount {get;set;}
        public int PreTeenCount {get;set;}
        public int TeenCount {get;set;}
        public int TotalCount {get;set;}

    }
}