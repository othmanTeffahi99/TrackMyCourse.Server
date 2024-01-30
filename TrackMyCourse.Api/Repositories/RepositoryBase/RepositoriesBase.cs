

using Microsoft.EntityFrameworkCore;
using TrackMyCourseApi.Data;
using TrackMyCourseApi.Repositories.Interfaces;

namespace TrackMyCourseApi.Repositories.RepositoryBase;

public class RepositoriesBase<T>(AppDbContext appDbContext)  : IRepositoryBase<T> where T : class 
{
    
    
    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
       return await appDbContext.Set<T>().ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await appDbContext.Set<T>().FindAsync(new object[] { id }, cancellationToken: cancellationToken);    }

    public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
         await appDbContext.Set<T>().AddAsync(entity, cancellationToken: cancellationToken);
         
         return entity;
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        await Task.Run( () =>
        {
            appDbContext.Set<T>().Update(entity);
        }, cancellationToken);
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        await Task.Run( () =>
        {
            appDbContext.Set<T>().Update(entity);
        }, cancellationToken);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await appDbContext.SaveChangesAsync(cancellationToken);
    }
}