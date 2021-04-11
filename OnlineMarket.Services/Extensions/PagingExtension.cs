using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineMarket.Helpers;

namespace OnlineMarket.Services.Extensions
{
    public static class PagingExtension
    {
        public static async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, int pageNumber, int pageSize)
        {
            int count = await source.CountAsync();

            List<T> items = await source
                .AsQueryable()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }

        public static IQueryable<T> WhereEq<T, TKey>(
                this IQueryable<T> source,
                Expression<Func<T, TKey>> expression,
                TKey value,
                bool ignoreNull = true
                )
        {
            if (ignoreNull && value == null)
            {
                return source;
            }

            UnaryExpression constant = Expression.Convert(Expression.Constant(value), expression.ReturnType);
            ParameterExpression param = expression.Parameters[0];
            Expression<Func<T, bool>> predicate = Expression.Lambda<Func<T, bool>>(
                Expression.Equal(
                    expression.Body,
                    constant
                ),
                new[] { param }
            );
            return source.Where(predicate);
        }

        public static IQueryable<T> WhereGt<T, TKey>(
                this IQueryable<T> source,
                Expression<Func<T, TKey>> expression,
                TKey value,
                bool ignoreNull = true
                )
        {
            if (ignoreNull && value == null)
            {
                return source;
            }

            UnaryExpression constant = Expression.Convert(Expression.Constant(value), expression.ReturnType);
            ParameterExpression param = expression.Parameters[0];
            Expression<Func<T, bool>> predicate = Expression.Lambda<Func<T, bool>>(
                Expression.GreaterThan(
                    expression.Body,
                    constant
                ),
                new[] { param }
            );
            return source.Where(predicate);
        }

        public static IQueryable<T> WhereLt<T, TKey>(
                this IQueryable<T> source,
                Expression<Func<T, TKey>> expression,
                TKey value,
                bool ignoreNull = true
                )
        {
            if (ignoreNull && value == null)
            {
                return source;
            }

            UnaryExpression constant = Expression.Convert(Expression.Constant(value), expression.ReturnType);
            ParameterExpression param = expression.Parameters[0];
            Expression<Func<T, bool>> predicate = Expression.Lambda<Func<T, bool>>(
                Expression.LessThan(
                    expression.Body,
                    constant
                ),
                new[] { param }
            );
            return source.Where(predicate);
        }

        public static IQueryable<T> WhereGtEq<T, TKey>(
                this IQueryable<T> source,
                Expression<Func<T, TKey>> expression,
                TKey value,
                bool ignoreNull = true
                )
        {
            if (ignoreNull && value == null)
            {
                return source;
            }

            UnaryExpression constant = Expression.Convert(Expression.Constant(value), expression.ReturnType);
            ParameterExpression param = expression.Parameters[0];
            Expression<Func<T, bool>> predicate = Expression.Lambda<Func<T, bool>>(
                Expression.GreaterThanOrEqual(
                    expression.Body,
                    constant
                ),
                new[] { param }
            );
            return source.Where(predicate);
        }

        public static IQueryable<T> WhereLtEq<T, TKey>(
                this IQueryable<T> source,
                Expression<Func<T, TKey>> expression,
                TKey value,
                bool ignoreNull = true
                )
        {
            if (ignoreNull && value == null)
            {
                return source;
            }

            UnaryExpression constant = Expression.Convert(Expression.Constant(value), expression.ReturnType);
            ParameterExpression param = expression.Parameters[0];
            Expression<Func<T, bool>> predicate = Expression.Lambda<Func<T, bool>>(
                Expression.LessThanOrEqual(
                    expression.Body,
                    constant
                ),
                new[] { param }
            );
            return source.Where(predicate);
        }
    }
}