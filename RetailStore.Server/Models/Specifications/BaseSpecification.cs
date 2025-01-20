namespace RetailStore.Server.Models.Specifications;

/// <summary>
/// Parent specification which defines common properties used to build an IQuerable for entity framework.
/// </summary>
/// <typeparam name="T"></typeparam>
public class BaseSpecification<T> : ISpecification<T>
{
    public BaseSpecification() { }
    public BaseSpecification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }

    /// <summary>
    /// Criteria to query with entity framework. Used in the WHERE function call.
    /// </summary>
    public Expression<Func<T, bool>> Criteria { get; }

    /// <summary>
    /// Eager loading criteria.
    /// </summary>
    public List<Expression<Func<T, object>>> Includes { get; } = new();

    /// <summary>
    /// Order by criteria.
    /// </summary>
    public Expression<Func<T, object>> OrderBy { get; private set; }

    /// <summary>
    /// Order by descending criteria.
    /// </summary>
    public Expression<Func<T, object>> OrderByDescending { get; private set; }

    /// <summary>
    /// Specifies the number of tiems to take. Defined by items per page.
    /// </summary>
    public int Take { get; private set; }

    /// <summary>
    /// Specifies how many items to skip.
    /// </summary>
    public int Skip { get; private set; }
    public bool IsPagingEnabled { get; private set; }

    /// <summary>
    /// Method which adds include to the Includes property.
    /// </summary>
    /// <param name="expression"></param>
    protected void AddInclude(Expression<Func<T, object>> expression)
    {
        Includes.Add(expression);
    }

    /// <summary>
    /// Method which adds an order by expression to the OderBy property.
    /// </summary>
    /// <param name="expression"></param>
    protected void AddOrderBy(Expression<Func<T, object>> expression)
    {
        OrderBy = expression;
    }

    /// <summary>
    /// Method which adds an order by descending expression to the OderByDescending property.
    /// </summary>
    /// <param name="expression"></param>
    protected void AddOrderByDescending(Expression<Func<T, object>> expression)
    {
        OrderByDescending = expression;
    }

    /// <summary>
    /// Method which updates related properties for paging including Skip, Take and IsPagingEnabled.
    /// </summary>
    /// <param name="skip">Number of items to skip.</param>
    /// <param name="take">Number of items to query.</param>
    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
    }
}
