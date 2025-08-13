using HarborRestaurant.Business.Abstract;
using HarborRestaurant.DataAccess.Abstract;
using HarborRestaurant.Entities.Concrete;

namespace HarborRestaurant.Business.Concrete
{
    public class AboutService : IAboutService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AboutService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<About?> GetActiveAboutAsync()
        {
            return await _unitOfWork.Abouts.GetActiveAboutAsync();
        }

        public async Task<About?> GetByIdAsync(int id)
        {
            return await _unitOfWork.Abouts.GetByIdAsync(id);
        }

        public async Task<IEnumerable<About>> GetAllAsync()
        {
            return await _unitOfWork.Abouts.GetAllAsync();
        }

        public async Task AddAsync(About about)
        {
            // Yeni about eklerken diğerlerini pasif yap
            var existingAbouts = await _unitOfWork.Abouts.GetAllAsync();
            foreach (var existing in existingAbouts)
            {
                existing.IsActive = false;
                existing.UpdatedDate = DateTime.Now;
                _unitOfWork.Abouts.Update(existing);
            }

            about.IsActive = true;
            about.CreatedDate = DateTime.Now;
            await _unitOfWork.Abouts.AddAsync(about);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<About> CreateAsync(About about)
        {
            // Yeni about eklerken diğerlerini pasif yap
            var existingAbouts = await _unitOfWork.Abouts.GetAllAsync();
            foreach (var existing in existingAbouts)
            {
                existing.IsActive = false;
                existing.UpdatedDate = DateTime.Now;
                _unitOfWork.Abouts.Update(existing);
            }

            about.IsActive = true;
            about.CreatedDate = DateTime.Now;
            await _unitOfWork.Abouts.AddAsync(about);
            await _unitOfWork.SaveChangesAsync();
            return about;
        }

        public async Task UpdateAsync(About about)
        {
            about.UpdatedDate = DateTime.Now;
            
            // Eğer bu about aktif ediliyorsa diğerlerini pasif yap
            if (about.IsActive)
            {
                var existingAbouts = await _unitOfWork.Abouts.FindAsync(a => a.AboutId != about.AboutId);
                foreach (var existing in existingAbouts)
                {
                    existing.IsActive = false;
                    existing.UpdatedDate = DateTime.Now;
                    _unitOfWork.Abouts.Update(existing);
                }
            }

            _unitOfWork.Abouts.Update(about);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var about = await _unitOfWork.Abouts.GetByIdAsync(id);
            if (about != null)
            {
                _unitOfWork.Abouts.Remove(about);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
