using Gym_Community.API.DTOs.Coach.CoachStuff;
using Gym_Community.Application.Interfaces.CoachStuff;
using Gym_Community.Domain.Models.CoachStuff;
using Gym_Community.Infrastructure.Interfaces.CoachStuff;

namespace Gym_Community.Application.Services.CoachStuff
{
    public class CoachCertificateService :ICoachCertificateService
    {
        private readonly ICoachCertificateRepository _repo;

        public CoachCertificateService(ICoachCertificateRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<CoachCertificateDto>> GetByPortfolioIdAsync(int portfolioId)
        {
            var certificates = await _repo.GetByPortfolioIdAsync(portfolioId);

            return certificates.Select(c => new CoachCertificateDto
            {
                Id = c.Id,
                ImageUrl = c.ImageUrl,
                ProtofolioId = c.ProtofolioId
            });
        }

        public async Task<bool> CreateAsync(CoachCertificateDto dto)
        {
            var entity = new CoachCertificate
            {
                ImageUrl = dto.ImageUrl,
                ProtofolioId = dto.ProtofolioId
            };

            await _repo.AddAsync(entity);
            return await _repo.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var certificate = await _repo.GetByIdAsync(id);
            if (certificate == null) return false;

            _repo.Delete(certificate);
            return await _repo.SaveChangesAsync();
        }
    }
}
