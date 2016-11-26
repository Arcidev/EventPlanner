
using MongoDB.Bson;
using System.Collections.Generic;

namespace EventPlanner.BL.DTO
{
    public class UserDTO
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public IDictionary<ObjectId, UserEventDTO> UserEvents { get; set; }
    }

}
