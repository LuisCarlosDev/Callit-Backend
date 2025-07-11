using Callit.Domain.Repositories;

namespace Callit.Infrastructure.DataAccess;

internal class UnitOfWork : IUnitOfWork
{
  private readonly CallitDbContext _dbContext;

  public UnitOfWork(CallitDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task Commit() => await _dbContext.SaveChangesAsync();
}
