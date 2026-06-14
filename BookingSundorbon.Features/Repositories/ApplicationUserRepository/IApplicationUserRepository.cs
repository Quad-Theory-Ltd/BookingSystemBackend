using BookingSundorbon.Views.DTOs.ApplicationUserView;

namespace BookingSundorbon.Features.Repositories.ApplicationUserRepository
{
    public interface IApplicationUserRepository
    {
        Task<IEnumerable<AdminUserDetailView>> GetAdminUserDetailsAsync();
    }
}
