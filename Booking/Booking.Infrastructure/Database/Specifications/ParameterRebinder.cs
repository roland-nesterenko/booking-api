using System.Linq.Expressions;

namespace Booking.Infrastructure.Database.Specifications;

public sealed class ParameterRebinder : ExpressionVisitor
{
    // Словник для зберігання відповідностей між параметрами
    private readonly IDictionary<ParameterExpression, ParameterExpression> _map;

    // Конструктор, який приймає словник відповідностей між параметрами
    private ParameterRebinder(IDictionary<ParameterExpression, ParameterExpression>? map)
    {
        // Ініціалізуємо словник, якщо він не переданий
        _map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
    }

    // Статичний метод для заміни параметрів у виразі
    public static Expression ReplaceParameters(
        IDictionary<ParameterExpression, ParameterExpression> map,
        Expression expression)
    {
        // Створюємо новий екземпляр ParameterRebinder та викликаємо метод Visit
        return new ParameterRebinder(map).Visit(expression);
    }

    // Перевизначений метод для відвідування параметрів у виразі
    protected override Expression VisitParameter(ParameterExpression node)
    {
        // Якщо параметр присутній у словнику, замінюємо його
        if (_map.TryGetValue(node, out var replacement))
        {
            node = replacement;
        }

        // Викликаємо базовий метод VisitParameter
        return base.VisitParameter(node);
    }
}