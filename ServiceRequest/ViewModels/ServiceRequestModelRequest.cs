using System;
using Newtonsoft.Json;
using ServiceRequest.DataModel.Enums;

namespace ServiceRequest.ViewModels
{
    public class ServiceRequestModelRequest
    {
        public ServiceRequestModelRequest(string buildingCode, string description, CurrentStatus currentStatus,
            string createdBy, DateTime createdDate, string lastModifiedBy, DateTime lastModifiedDate)
        {
            BuildingCode = buildingCode;
            Description = description;
            CurrentStatus = currentStatus;
            CreatedBy = createdBy;
            CreatedDate = createdDate;
            LastModifiedBy = lastModifiedBy;
            LastModifiedDate = lastModifiedDate;
        }

        public ServiceRequestModelRequest()
        {
            
        }

        [JsonProperty("buildingCode")]
        public string BuildingCode { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        public CurrentStatus CurrentStatus { get; }
        [JsonProperty("createdBy")]
        public string CreatedBy { get; set; }
        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }
        [JsonProperty("lastModifiedBy")]
        public string LastModifiedBy { get; set; }
        [JsonProperty("lastModifiedDate")]
        public DateTime LastModifiedDate { get; set; }
    }
}
