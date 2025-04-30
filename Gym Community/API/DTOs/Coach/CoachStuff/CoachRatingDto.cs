namespace Gym_Community.API.DTOs.Coach.CoachStuff
{
    public class CoachRatingDto
    {
        public int Id { get; set; }

        public string ClientFirstName { get; set; }
        public string ClientLastName
        {
            get; set;
        }
        public string clientimg { get; set; }
        public string ClientId { get; set; }
        public string CoachId { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
    }
}
