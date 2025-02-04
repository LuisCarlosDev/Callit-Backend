namespace Callit.Domain.Repositories;

public interface IUnitOfWork
{
	Task Commit();
}