﻿using System.Linq.Expressions;

namespace Booking.Infrastructure.Database.Specifications;

public class TrueSpecification<T> : Specification<T>
{
    public override Expression<Func<T, bool>> ToExpression()
    {
        return x => true;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;

        if (ReferenceEquals(this, obj)) return true;

        return GetType() == obj.GetType();
    }

    public override int GetHashCode()
    {
        return GetType().GetHashCode();
    }
}