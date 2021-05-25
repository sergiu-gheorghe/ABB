using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Application.Albums.Repositories;
using Application.Users.Repositories;
using Application.Photos.Repositories;
using Infrastructure.Repositories.Caching;
using Polly;
using System.Net.Http;
using Polly.Extensions.Http;
using System;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddHttpClient<AlbumRepository, AlbumRepository>()
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler(GetRetryPolicy());

            services.AddHttpClient<PhotoRepository, PhotoRepository>()
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler(GetRetryPolicy());

            services.AddHttpClient<PhotoRepository, PhotoRepository>()
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler(GetRetryPolicy());

            services.AddLazyCache();

            services.AddScoped<AlbumRepository, AlbumRepository>();
            services.AddScoped<PhotoRepository, PhotoRepository>();
            services.AddScoped<UserRepository, UserRepository>();
            services.AddScoped<IAlbumRepository, AlbumCachedRepository>();
            services.AddScoped<IPhotoRepository, PhotoCachedRepository>();
            services.AddScoped<IUserRepository, UserCachedRepository>();
            
            return services;
        }

        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
    }
}
