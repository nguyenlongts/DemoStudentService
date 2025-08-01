
using DemoCQRS_MediatR.Infrastructure;

namespace DemoCQRS_MediatR.APP.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseServiceCollectionConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddMediatorConfiguration()
                .AddKafkaConfiguration(configuration)
                .AddPersistanceConfiguration(configuration)
                .AddScopeService();
        }

        private static IServiceCollection AddMediatorConfiguration(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
            return services;
        }
        private static IServiceCollection AddPersistanceConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var dbConnectionString = configuration.GetConnectionString("OracleDB");
            services.AddDbContext<ModelContext>((options) =>
            {
                if (!String.IsNullOrEmpty(dbConnectionString))
                {
                    options.UseOracle(dbConnectionString);
                }
            });
            return services;
        }
        private static IServiceCollection AddKafkaConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = configuration["Kafka:BootstrapServers"]
            };

            services.AddSingleton<IProducer<Null, string>>(sp =>
            {
                return new ProducerBuilder<Null, string>(producerConfig).Build();
            });

            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = configuration["Kafka:BootstrapServers"],
                GroupId = configuration["Kafka:GroupId"] ?? "demo-consumer-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };


            services.AddSingleton<IConsumer<Null, string>>(sp =>
            {
                return new ConsumerBuilder<Null, string>(consumerConfig).Build();
            });

            return services;
        }
        private static IServiceCollection AddScopeService(this IServiceCollection services)
        {
            services.AddScoped<StudentRepository>();

            return services;
        }
    }
}
