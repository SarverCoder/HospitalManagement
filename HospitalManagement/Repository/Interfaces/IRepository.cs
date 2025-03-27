namespace HospitalManagement.Repository.Interfaces
{
    public interface IRepository<TEntity>
    {
        void Add(TEntity entity);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        TEntity GetById(int id);
        Task<TEntity> GetByIdAsync(int id);
        IQueryable<TEntity> GetAll();
        Task<IQueryable> GetAllAsync();

        Task<int> SaveChangesAsync();
        int SaveChanges();

    }
}
