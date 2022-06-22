using System.Linq.Expressions;

namespace NLayerCore.Repositories;

public interface IGenericRepository<T> where T: class
{
    Task<T> GetByIdAsync(int id);

    IQueryable<T> GetAll();
    
    // * Burada IQeryable kullanılmasının sebebi datanın bir anda memory'e alınmamasıdır. Burada List kullansaydık 
    //datanın hepsini çektikten sonra diğer işlemlere devam eder. Queryable bu yüzden daha performanslıdır.
    
    //Expression Func delegesi alan bir classtır. Func Action gibi methodlar built-in gelen delegelerdir.
    //Delegeler methodları işaret eden yapılardır.
    IQueryable<T> Where(Expression<Func<T, bool>> expression);

    Task AddAsync(T entity);
    
    Task AddRangeAsync(IEnumerable<T> entities);

    //Update ve Remove methodlarının async'leri yoktur. Update ve Remove uzun süren işlemler değildir.
    //EF Core update edilecek ve silinecek entityleri zaten biliyor, sil ve veya update et denildiğinde 
    //sadece state'ini değiştiriyor. Add gibi ekleme yapılan bir süreç değil. 
    void Update(T entity);

    //Async var olan threadleri blocklamamak için kullanıldığından Delete ve Update işlemleri de thread bloklayacak 
    //kadar uzun işlemler olmadığından async versiyonları yoktur. 
    void Delete(T entity);

    void RemoveRange(IEnumerable<T> entities);
}