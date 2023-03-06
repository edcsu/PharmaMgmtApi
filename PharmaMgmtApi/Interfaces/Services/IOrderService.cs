using System.Linq.Expressions;
using PharmaMgmtApi.Commons.Utils;
using PharmaMgmtApi.Models;
using PharmaMgmtApi.ViewModels.Orders;

namespace PharmaMgmtApi.Interfaces.Services;

public interface IOrderService
{
    Task<OrderViewModel> CreateAsync(Guid userid, OrderCreateViewModel orderCreateViewModel);
    
    Task<bool> DeleteAsync(Expression<Func<Order, bool>> expression);
    
    Task<OrderViewModel?> GetAsync(Guid userId, Expression<Func<Order, bool>> expression);
    
    Task<IEnumerable<OrderViewModel>> GetAllAsync(Expression<Func<Order, bool>>? expression = null,
        PaginationParams? @params = null);
}