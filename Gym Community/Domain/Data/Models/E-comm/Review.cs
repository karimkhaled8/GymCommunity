﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Gym_Community.Data;
using Gym_Community.Domain.Data.Models.E_comms;

namespace Gym_Community.Domain.Data.Models.E_comm
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }

        [ForeignKey("Product")]
        public int? ProductID { get; set; }
        public Product? Product { get; set; }

        [ForeignKey("ApplicationUser")]
        public string? UserID { get; set; }
        public AppUser? AppUser { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        public string Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
