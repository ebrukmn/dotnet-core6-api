using System.Linq.Expressions;

namespace NLayerCore.Services;

public interface IService<T> where T : class
{
    Task<T> GetByIdAsync(int id);

    Task<IEnumerable<T>> GetAll();
    
    IQueryable<T> Where(Expression<Func<T, bool>> expression);

    Task<T> AddAsync(T entity);
    
    Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
    
    //EF Core'un update ve remove için async versiyonu yok fakat servis versiyonunda async kullanacağız.
    Task<T> UpdateAsync(T entity);
    
    Task DeleteAsync(T entity);

    Task RemoveRangeAsync(IEnumerable<T> entities);

    Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
}