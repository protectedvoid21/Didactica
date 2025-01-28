using Microsoft.EntityFrameworkCore;
using NSubstitute;

namespace Didactica.Tests;

internal static class TestHelper
{
    public static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
    {
        var queryable = sourceList.AsQueryable();

        var dbSet = Substitute.For<DbSet<T>, IQueryable<T>>();
        ((IQueryable<T>)dbSet).Provider.Returns(queryable.Provider);
        ((IQueryable<T>)dbSet).Expression.Returns(queryable.Expression);
        ((IQueryable<T>)dbSet).ElementType.Returns(queryable.ElementType);
        ((IQueryable<T>)dbSet).GetEnumerator().Returns(queryable.GetEnumerator());
        dbSet.When(d => d.Add(Arg.Any<T>())).Do(callInfo => sourceList.Add(callInfo.Arg<T>()));

        return dbSet;
    }
}