using DasBookStore.Domain.Interfaces;
using DasBookStore.Domain.Services;
using DasBookStore.Infrastructure.Context;
using DasBookStore.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DasBookStore.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<BookStoreDbContext>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IBookRepository, BookRepository>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IBookService, BookService>();

            return services;
        }
    }
}

//ResolveDependencies it’s an extension method from IServiceCollection. Inside of this method we are adding the Dependency Injection configuration.

//There are three lifetimes that can be used to register a service:

//Transient — Transient lifetime services (AddTransient) are created each time they’re requested from the service container.
//Scoped — Scoped lifetime services (AddScoped) are created once per client request (connection).
//Singleton — Singleton lifetime services (AddSingleton) are created the first time they’re requested.

//We are using Scoped because then the objects are created just once per request, this way we avoid to use more memory, once it will always refer to the same memory location.