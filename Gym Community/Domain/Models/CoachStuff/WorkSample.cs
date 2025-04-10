namespace Gym_Community.Domain.Models.CoachStuff
{
    public class WorkSample
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public int ProtofolioId { get; set; }
        public CoachPortfolio Protofolio { get; set; }
    }
}
