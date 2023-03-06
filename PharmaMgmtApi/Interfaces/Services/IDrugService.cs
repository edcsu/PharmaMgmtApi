using System.Linq.Expressions;
using PharmaMgmtApi.Commons.Utils;
using PharmaMgmtApi.Models;
using PharmaMgmtApi.ViewModels.Drugs;

namespace PharmaMgmtApi.Interfaces.Services;

public interface IDrugService
{
    Task<DrugViewModel> CreateAsync(DrugCreateViewModel drugCreate);
    
    Task<bool> UpdateAsync(Guid id, DrugCreateViewModel drugUpdate);
    
    Task<bool> DeleteAsync(Expression<Func<Drug, bool>> expression);
    
    Task<DrugViewModel?> GetAsync(Expression<Func<Drug, bool>> expression);
    
    Task<IEnumerable<DrugViewModel>> GetAllAsync(Expression<Func<Drug, bool>>? expression = null,
        PaginationParams? @params = null);
}