namespace Application.Interfaces
{
    public interface IApplicationUserRepository {
        ApplicationUser FindApplicationUserByUsername(string username);
        ApplicationUser CreateNewApplicationUser(ApplicationUser user);
    }
}