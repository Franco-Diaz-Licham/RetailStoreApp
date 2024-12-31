namespace RetailStore.Server.Data.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly DataContext _db;
    public IMapper _mapper { get; }

    public GenericRepository(DataContext db, IMapper mapper)
    {
        _mapper = mapper;
        _db = db;
    }

    public void Add(T entity)
    {
        _db.Set<T>().Add(entity);
    }

    public async Task<int> CountAsync(ISpecification<T> spec)
    {
        var output = await ApplySpecification(spec).CountAsync();
        return output;
    }

    public void Delete(T entity)
    {
        _db.Set<T>().Remove(entity);
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        var output = await _db.Set<T>().FindAsync(id);
        return output;
    }

    public async Task<U?> GetByIdAndMapAsync<U>(int id)
    {
        var output = await _db.Set<T>().Where(x => x.Id == id).ProjectTo<U>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
        return output;
    }

    public async Task<T?> GetEntityWithSpec(ISpecification<T> spec)
    {
        var output = await ApplySpecification(spec).FirstOrDefaultAsync();
        return output;
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        var output = await _db.Set<T>().ToListAsync();
        return output;
    }

    public async Task<IReadOnlyList<T>> GetAllAsync(ISpecification<T> spec)
    {
        var output = await ApplySpecification(spec).ToListAsync();
        return output;
    }

    public void Update(T entity)
    {
        _db.Set<T>().Attach(entity);
        _db.Entry(entity).State = EntityState.Modified;
    }

    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        var output = SpecificationEvaluator<T>.GetQuery(_db.Set<T>().AsQueryable(), spec);
        return output;
    }
}
