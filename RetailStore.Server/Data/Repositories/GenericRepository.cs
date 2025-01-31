namespace RetailStore.Server.Data.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly DataContext _db;
    public GenericRepository(DataContext db)
    {
        _db = db;
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        var output = await _db.Set<T>().FindAsync(id);
        return output;
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        var output = await _db.Set<T>().ToListAsync();
        return output;
    }

    public async Task<T?> GetEntityWithSpec(ISpecification<T> spec)
    {
        var query = ApplySpecification(spec);
        var output = await query.FirstOrDefaultAsync();
        return output;
    }

    public async Task<IReadOnlyList<T>> GetAllAsync(ISpecification<T> spec)
    {
        var query = ApplySpecification(spec);
        var output = await query.ToListAsync();
        return output;
    }

    public async Task<int> CountAsync(ISpecification<T> spec)
    {
        var query = ApplySpecification(spec);
        var output = await query.CountAsync();
        return output;
    }

    public void Add(T entity)
    {
        _db.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        _db.Set<T>().Attach(entity);
        _db.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(T entity)
    {
        _db.Set<T>().Remove(entity);
    }

    /// <summary>
    /// Method which builds an IQueryable to execute in the GET methods in this repository.
    /// </summary>
    /// <param name="spec"></param>
    /// <returns></returns>
    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        var query = _db.Set<T>().AsQueryable();
        var output = SpecificationEvaluator<T>.GetQuery(query, spec);
        return output;
    }
}
