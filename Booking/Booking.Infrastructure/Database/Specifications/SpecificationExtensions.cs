namespace Booking.Infrastructure.Database.Specifications;

public static class SpecificationExtensions
{
    // Метод розширення для перетворення масиву значень в колекцію специфікацій
    public static IEnumerable<Specification<TEntity>> Map<TEntity, TValue>(
        this TValue[] values,
        Func<TValue, Specification<TEntity>> selector,
        bool defaultValue = true)
    {
        // Якщо масив порожній, повертаємо специфікацію за замовчуванням
        // Інакше, перетворюємо кожне значення в специфікацію
        return values.Length is 0
            ? Enumerable.Repeat(FromBool<TEntity>(defaultValue), 1)
            : values.Select(selector);
    }
    
    // Метод розширення для комбінації специфікацій за допомогою логічного І
    public static Specification<T> CombineAnd<T>(this IEnumerable<Specification<T>> specs)
    {
        return specs.Aggregate((seed, spec) => seed &= spec);
    }

    // Метод розширення для комбінації специфікацій за допомогою логічного АБО
    public static Specification<T> CombineOr<T>(this IEnumerable<Specification<T>> specs)
    {
        return specs.Aggregate((seed, spec) => seed |= spec);
    }

    // Приватний метод для створення специфікації з булевого значення
    private static Specification<T> FromBool<T>(bool defaultValue)
    {
        return defaultValue
            ? Specification<T>.True
            : Specification<T>.False;
    }
}