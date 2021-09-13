using System;
using ServiceRequest.DataModel.Enums;

namespace ServiceRequest.DataModel
{
    public class ServiceRequestModel
    {
        public ServiceRequestModel(Guid id, string buildingCode, string description, CurrentStatus currentStatus,
            string createdBy, DateTime createdDate, string lastModifiedBy, DateTime lastModifiedDate)
        {
            Id = id;
            BuildingCode = buildingCode;
            Description = description;
            CurrentStatus = currentStatus;
            CreatedBy = createdBy;
            CreatedDate = createdDate;
            LastModifiedBy = lastModifiedBy;
            LastModifiedDate = lastModifiedDate;
        }

        public ServiceRequestModel()
        {
            
        }

        public Guid Id { get; }
        public string BuildingCode { get; private set; }
        public string Description { get; private set; }
        public CurrentStatus CurrentStatus { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public string LastModifiedBy { get; private set; }
        public DateTime LastModifiedDate { get; private set; }
    }
}
