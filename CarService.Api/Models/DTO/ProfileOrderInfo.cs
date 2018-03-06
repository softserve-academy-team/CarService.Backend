using System;
using CarService.DbAccess.Entities;

namespace CarService.Api.Models.DTO
{
    public class ProfileOrderInfo
    {
        public int OrderId {get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
        public string MarkName { get; set; }
        public string ModelName { get; set; }
        public int Year { get; set; }
        public string PhotoLink { get; set; }
    }
}