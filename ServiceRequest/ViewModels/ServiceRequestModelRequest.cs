using System;
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

        public string BuildingCode { get; }
        public string Description { get; }
        public CurrentStatus CurrentStatus { get; }
        public string CreatedBy { get; }
        public DateTime CreatedDate { get; }
        public string LastModifiedBy { get; }
        public DateTime LastModifiedDate { get; }
    }
}
