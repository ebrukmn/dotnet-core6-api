using NLayerCore.UnitOfWorks;

namespace NLayerRepository.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _appDbContext;

    public UnitOfWork(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    
    public async Task CommitAsync()
    {
        await _appDbContext.SaveChangesAsync();
    }

    public void Commit()
    {
        _appDbContext.SaveChanges();
    }
}