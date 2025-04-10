using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym_Community.Domain.Models.CoachStuff
{
    public class CoachCertificate
    {
        [Key]
        public int Id { get; set; }
        public string ImageUrl { get; set; }

        [ForeignKey("Protofolio")]
        public int ProtofolioId { get; set; }
        public CoachPortfolio Protofolio { get; set; }
    }
}
