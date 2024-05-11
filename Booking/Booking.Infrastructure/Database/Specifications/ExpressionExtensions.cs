using System.Linq.Expressions;

namespace Booking.Infrastructure.Database.Specifications;

public static class ExpressionExtensions
{
    // Метод розширення для створення логічного І між двома виразами
    public static Expression<Func<T, bool>> AndAlso<T>(
        this Expression<Func<T, bool>> left,
        Expression<Func<T, bool>> right)
    {
        return left.Compose(right, Expression.AndAlso);
    }

    // Метод розширення для створення логічного АБО між двома виразами
    public static Expression<Func<T, bool>> OrElse<T>(
        this Expression<Func<T, bool>> left,
        Expression<Func<T, bool>> right)
    {
        return left.Compose(right, Expression.OrElse);
    }

    // Приватний метод для комбінування двох виразів за допомогою заданої функції злиття
    private static Expression<T> Compose<T>(
        this Expression<T> left,
        Expression<T> right,
        Func<Expression, Expression, Expression> merge)
    {
        // Створюємо словник для заміни параметрів в правому виразі
        var map = left.Parameters
            .Select((expr, index) => new { Expression = expr, Parameter = right.Parameters[index] })
            .ToDictionary(p => p.Parameter, p => p.Expression);

        // Замінюємо параметри в тілі правого виразу
        var rightBody = ParameterRebinder.ReplaceParameters(map, right.Body);

        // Повертаємо новий вираз, що є результатом злиття тіл двох виразів
        return Expression.Lambda<T>(merge(left.Body, rightBody), left.Parameters);
    }
}