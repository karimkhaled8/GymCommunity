﻿namespace Gym_Community.API.DTOs.Forum
{
    public class SubReadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int PostCount { get; set; }
    }
}
