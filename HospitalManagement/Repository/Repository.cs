using HospitalManagement.DataAccess;
using HospitalManagement.Repository.Interfaces;

namespace HospitalManagement.Repository
{
    public abstract class Repository<TEntity>(HospitalContext context)
        : IRepository<TEntity> where TEntity : class
    {
        
        public void Add(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
        }

        public async Task AddAsync(TEntity entity)
        {
          await context.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            context.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
        }

        public TEntity GetById(int id)
        {
          return context.Set<TEntity>().Find(id);
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public IQueryable GetAll()
        {
          return context.Set<TEntity>().AsQueryable();
        }

        public async Task<IQueryable> GetAllAsync()
        {
            return await Task.FromResult(context.Set<TEntity>().AsQueryable());
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }
    }
}
