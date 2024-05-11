using System.Linq.Expressions;

namespace Booking.Infrastructure.Database.Specifications;

public abstract class Specification<T>
{
    // Статичні поля, що представляють істинну та хибну специфікації
    public static readonly Specification<T> True = new TrueSpecification<T>();

    public static readonly Specification<T> False = new NotSpecification<T>(new TrueSpecification<T>());
    
    // Абстрактний метод для перетворення специфікації в вираз
    public abstract Expression<Func<T, bool>> ToExpression();

    // Метод для перетворення специфікації в предикат
    public virtual Func<T, bool> ToPredicate()
    {
        return ToExpression().Compile();
    }

    // Метод для перевірки, чи задовольняє об'єкт специфікацію
    public bool IsSatisfiedBy(T obj)
    {
        ArgumentNullException.ThrowIfNull(obj);

        var predicate = ToPredicate();
        return predicate(obj);
    }

    // Оператори для перевірок, комбінацій та інверсії специфікації
    public static bool operator true(Specification<T> spec)
    {
        return true;
    }

    public static bool operator false(Specification<T> spec)
    {
        return false;
    }

    public static Specification<T> operator &(Specification<T> left, Specification<T> right)
    {
        return new AndSpecification<T>(left, right);
    }

    public static Specification<T> operator |(Specification<T> left, Specification<T> right)
    {
        return new OrSpecification<T>(left, right);
    }

    public static Specification<T> operator !(Specification<T> spec)
    {
        return new NotSpecification<T>(spec);
    }
}