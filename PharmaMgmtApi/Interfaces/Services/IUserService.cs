using System.Linq.Expressions;
using PharmaMgmtApi.Commons.Utils;
using PharmaMgmtApi.Models;
using PharmaMgmtApi.ViewModels.Users;

namespace PharmaMgmtApi.Interfaces.Services;

public interface IUserService
{
    Task<UserViewModel> UpdateAsync(Guid id, UserCreateViewModel userCreateViewModel);
    
    Task<bool> DeleteAsync(Expression<Func<User, bool>> expression);
    
    Task<IEnumerable<UserViewModel>> GetAllAsync(Expression<Func<User, bool>>? expression = null,
        PaginationParams? @params = null);
    
    Task<UserViewModel?> GetAsync(Expression<Func<User, bool>> expression);
}