using System.Threading.Tasks;

namespace Domain {
    public interface IUserRepository: IAsyncRepository<User> {
        Task<User> FindUserByUsername(string username);
    }
}