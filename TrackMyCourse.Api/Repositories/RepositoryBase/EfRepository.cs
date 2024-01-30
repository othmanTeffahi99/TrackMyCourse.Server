using Microsoft.EntityFrameworkCore;
using TrackMyCourseApi.Data;
using TrackMyCourseApi.Repositories.Interfaces;

namespace TrackMyCourseApi.Repositories.RepositoryBase;

public class EfRepository<T>(AppDbContext dbContext) : RepositoriesBase<T>(dbContext) , IRepository<T> where T : class
{
    
}