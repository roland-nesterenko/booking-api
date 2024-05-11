using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking.Infrastructure.Database;

public abstract class BaseEntity<TKey>
{
    // Атрибут [Key] вказує, що властивість 'Id' є первинним ключем в базі даних
    [Key]
    // Атрибут [DatabaseGenerated(DatabaseGeneratedOption.Identity)] вказує, 
    // що значення для 'Id' буде автоматично генеруватися базою даних при створенні нового екземпляру сутності
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    // Властивість 'Id' представляє унікальний ідентифікатор сутності
    public TKey Id { get; set; } = default!;
}