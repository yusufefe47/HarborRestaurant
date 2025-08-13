using HarborRestaurant.Business.Abstract;
using HarborRestaurant.DataAccess.Abstract;
using HarborRestaurant.Entities.Concrete;

namespace HarborRestaurant.Business.Concrete
{
    public class HomePageService : IHomePageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomePageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<HomePage?> GetActiveHomePageAsync()
        {
            return await _unitOfWork.HomePages.GetActiveHomePageAsync();
        }

        public async Task<HomePage?> GetByIdAsync(int id)
        {
            return await _unitOfWork.HomePages.GetByIdAsync(id);
        }

        public async Task<IEnumerable<HomePage>> GetAllAsync()
        {
            return await _unitOfWork.HomePages.GetAllAsync();
        }

        public async Task AddAsync(HomePage homePage)
        {
            // Yeni homepage eklerken diğerlerini pasif yap
            var existingHomePages = await _unitOfWork.HomePages.GetAllAsync();
            foreach (var existing in existingHomePages)
            {
                existing.IsActive = false;
                existing.UpdatedDate = DateTime.Now;
                _unitOfWork.HomePages.Update(existing);
            }

            homePage.IsActive = true;
            homePage.CreatedDate = DateTime.Now;
            await _unitOfWork.HomePages.AddAsync(homePage);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<HomePage> CreateAsync(HomePage homePage)
        {
            // Yeni homepage eklerken diğerlerini pasif yap
            var existingHomePages = await _unitOfWork.HomePages.GetAllAsync();
            foreach (var existing in existingHomePages)
            {
                existing.IsActive = false;
                existing.UpdatedDate = DateTime.Now;
                _unitOfWork.HomePages.Update(existing);
            }

            homePage.IsActive = true;
            homePage.CreatedDate = DateTime.Now;
            await _unitOfWork.HomePages.AddAsync(homePage);
            await _unitOfWork.SaveChangesAsync();
            return homePage;
        }

        public async Task UpdateAsync(HomePage homePage)
        {
            homePage.UpdatedDate = DateTime.Now;
            
            // Eğer bu homepage aktif ediliyorsa diğerlerini pasif yap
            if (homePage.IsActive)
            {
                var existingHomePages = await _unitOfWork.HomePages.FindAsync(h => h.HomePageId != homePage.HomePageId);
                foreach (var existing in existingHomePages)
                {
                    existing.IsActive = false;
                    existing.UpdatedDate = DateTime.Now;
                    _unitOfWork.HomePages.Update(existing);
                }
            }

            _unitOfWork.HomePages.Update(homePage);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var homePage = await _unitOfWork.HomePages.GetByIdAsync(id);
            if (homePage != null)
            {
                _unitOfWork.HomePages.Remove(homePage);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
