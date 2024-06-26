﻿using System.Collections;
using Bogus;

namespace Booking.Tests.Helpers;

public abstract class ObjectFakeProviderBase<TModel>(int amount) : IEnumerable<object[]>
    where TModel : class
{
    protected virtual int FakesNumber { get; set; } = amount;

    protected virtual Func<Faker<TModel>> DefaultFactory => () => throw new NotImplementedException();

    internal TModel Get()
    {
        ArgumentNullException.ThrowIfNull(DefaultFactory);

        var faker = DefaultFactory();
        return faker.Generate();
    }

    protected virtual IEnumerable<TModel> GetFakes(
        Func<Faker<TModel>> fakerFactory)
    {
        fakerFactory ??= DefaultFactory;
        ArgumentNullException.ThrowIfNull(fakerFactory);

        var faker = fakerFactory();

        foreach (var fake in faker.Generate(FakesNumber))
        {
            yield return fake;
        }
    }

    public IEnumerator<object[]> GetEnumerator()
        => GetFakes(DefaultFactory)
            .Select(fake => (object[])[fake])
            .GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}