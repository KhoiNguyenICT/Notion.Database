using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NotionCore.Infrastructure;
using NotionCore.Internal;
using NotionCore.Properties;

namespace NotionCore.Extensions;

public static class NotionCoreServiceCollectionExtensions
{
    public static IServiceCollection AddDbContext<TContext>(
        this IServiceCollection serviceCollection,
        Action<NotionContextOptionsBuilder>? optionsAction = null,
        ServiceLifetime contextLifetime = ServiceLifetime.Scoped,
        ServiceLifetime optionsLifetime = ServiceLifetime.Scoped)
        where TContext : NotionContext
        => AddDbContext<TContext, TContext>(serviceCollection, optionsAction, contextLifetime, optionsLifetime);

    private static IServiceCollection AddDbContext<TContextService, TContextImplementation>(
        this IServiceCollection serviceCollection,
        Action<NotionContextOptionsBuilder>? optionsAction = null,
        ServiceLifetime contextLifetime = ServiceLifetime.Scoped,
        ServiceLifetime optionsLifetime = ServiceLifetime.Scoped)
        where TContextImplementation : NotionContext, TContextService
        => AddDbContext<TContextService, TContextImplementation>(
            serviceCollection,
            optionsAction == null
                ? null
                : (_, b) => optionsAction(b), contextLifetime, optionsLifetime);

    public static IServiceCollection AddDbContext<TContext>(
        this IServiceCollection serviceCollection,
        ServiceLifetime contextLifetime,
        ServiceLifetime optionsLifetime = ServiceLifetime.Scoped)
        where TContext : NotionContext
        => AddDbContext<TContext, TContext>(serviceCollection, contextLifetime, optionsLifetime);

    private static IServiceCollection AddDbContext<TContextService, TContextImplementation>(
        this IServiceCollection serviceCollection,
        ServiceLifetime contextLifetime,
        ServiceLifetime optionsLifetime = ServiceLifetime.Scoped)
        where TContextImplementation : NotionContext, TContextService
        where TContextService : class
        => AddDbContext<TContextService, TContextImplementation>(
            serviceCollection,
            (Action<IServiceProvider, NotionContextOptionsBuilder>?) null,
            contextLifetime,
            optionsLifetime);

    public static IServiceCollection AddDbContext<TContext>(
        this IServiceCollection serviceCollection,
        Action<IServiceProvider, NotionContextOptionsBuilder>? optionsAction,
        ServiceLifetime contextLifetime = ServiceLifetime.Scoped,
        ServiceLifetime optionsLifetime = ServiceLifetime.Scoped)
        where TContext : NotionContext
        => AddDbContext<TContext, TContext>(serviceCollection, optionsAction, contextLifetime, optionsLifetime);

    private static IServiceCollection AddDbContext<TContextService, TContextImplementation>(
        this IServiceCollection serviceCollection,
        Action<IServiceProvider, NotionContextOptionsBuilder>? optionsAction,
        ServiceLifetime contextLifetime = ServiceLifetime.Scoped,
        ServiceLifetime optionsLifetime = ServiceLifetime.Scoped)
        where TContextImplementation : NotionContext, TContextService
    {
        if (contextLifetime == ServiceLifetime.Singleton)
        {
            optionsLifetime = ServiceLifetime.Singleton;
        }

        if (optionsAction != null)
        {
            CheckContextConstructors<TContextImplementation>();
        }

        AddCoreServices<TContextImplementation>(serviceCollection, optionsAction, optionsLifetime);

        if (serviceCollection.Any(d => d.ServiceType == typeof(IDbContextFactorySource<TContextImplementation>)))
        {
            var serviceDescriptor =
                serviceCollection.FirstOrDefault(d => d.ServiceType == typeof(TContextImplementation));
            if (serviceDescriptor != null)
            {
                serviceCollection.Remove(serviceDescriptor);
            }
        }

        serviceCollection.TryAdd(new ServiceDescriptor(typeof(TContextService), typeof(TContextImplementation),
            contextLifetime));

        if (typeof(TContextService) != typeof(TContextImplementation))
        {
            serviceCollection.TryAdd(
                new ServiceDescriptor(
                    typeof(TContextImplementation),
                    p => (TContextImplementation) p.GetService<TContextService>()!,
                    contextLifetime));
        }

        return serviceCollection;
    }

    private static void AddCoreServices<TContextImplementation>(
        IServiceCollection serviceCollection,
        Action<IServiceProvider, NotionContextOptionsBuilder>? optionsAction,
        ServiceLifetime optionsLifetime)
        where TContextImplementation : NotionContext
    {
        serviceCollection.TryAdd(
            new ServiceDescriptor(
                typeof(NotionContextOptions<TContextImplementation>),
                p => CreateDbContextOptions<TContextImplementation>(p, optionsAction),
                optionsLifetime));

        serviceCollection.Add(
            new ServiceDescriptor(
                typeof(NotionContextOptions),
                p => p.GetRequiredService<NotionContextOptions<TContextImplementation>>(),
                optionsLifetime));
    }

    private static NotionContextOptions<TContext> CreateDbContextOptions<TContext>(
        IServiceProvider applicationServiceProvider,
        Action<IServiceProvider, NotionContextOptionsBuilder>? optionsAction)
        where TContext : NotionContext
    {
        return default!;
    }

    private static void CheckContextConstructors<TContext>()
        where TContext : NotionContext
    {
        var declaredConstructors = typeof(TContext).GetTypeInfo().DeclaredConstructors.ToList();
        if (declaredConstructors.Count == 1
            && declaredConstructors[0].GetParameters().Length == 0)
        {
            throw new ArgumentException(CoreStrings.DbContextMissingConstructor(typeof(TContext).ShortDisplayName()));
        }
    }
}