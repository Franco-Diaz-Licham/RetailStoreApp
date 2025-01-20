namespace RetailStore.Server.Models.Specifications;

/// <summary>
/// Class which to builds an IQueryable used in entity framework based on passed criteria.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
{
    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
    {
        var query = inputQuery;

        // order of specs is important
        if (spec.Criteria != null) query = query.Where(spec.Criteria);
        if (spec.OrderBy != null) query = query.OrderBy(spec.OrderBy);
        if (spec.OrderByDescending != null) query = query.OrderByDescending(spec.OrderByDescending);
        if (spec.IsPagingEnabled) query = query.Skip(spec.Skip).Take(spec.Take);

        // loop through all include citeria returning the built query before adding the next include
        query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
        
        return query;
    }
}
