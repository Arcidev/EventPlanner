using EventPlanner.BL.DTO;

namespace EventPlanner.WebApiModels
{
    public class SignUpForEventModel
    {
        public string UserId { get; set; }

        public UserEventDTO Choices { get; set; }
    }
}
