using System;
using System.Collections.Generic;

namespace SharedKernel.Pagination;

public class PaginatedResult<TEntity> where TEntity : class
{
    private readonly int _pageIndex;
    private readonly int _pageSize;
    private readonly long _count;
    private readonly IEnumerable<TEntity> _data;
    public PaginatedResult(
        int pageIndex,
        int pageSize,
        long count,
        IEnumerable<TEntity> data)
    {
        _pageIndex = pageIndex;
        _pageSize = pageSize;
        _count = count;
        _data = data ?? throw new ArgumentNullException(nameof(data));
    }

    public int PageIndex => _pageIndex;
    public int PageSize => _pageSize;
    public long Count => _count;
    public IEnumerable<TEntity> Data => _data;

}