using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Reflection;

namespace SstRegistrationTestHarness.Core.EntityFramework.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ApplyConfigurationsFromAssembly(this DbModelBuilder modelBuilder, Assembly assembly)
        {
            var configurations = from typeInfo in assembly.DefinedTypes
                from type in typeInfo.ImplementedInterfaces
                where type.IsGenericType
                      &&
                      type.Name.Equals(typeof(EntityTypeConfiguration<>).Name, StringComparison.InvariantCultureIgnoreCase)
                      &&
                      typeInfo.IsClass
                      &&
                      !typeInfo.IsAbstract
                      &&
                      !typeInfo.IsNested
                select typeInfo;

            foreach (var configuration in configurations)
            {
                var entityType = configuration.ImplementedInterfaces
                    .Single(x => x.Name.Equals(typeof(EntityTypeConfiguration<>).Name))
                    .GenericTypeArguments.SingleOrDefault(x => x.IsClass);

                var applyConfigMethods = modelBuilder.Conventions.GetType().GetMethods().Where(x => x.Name.Equals(nameof(ConventionsConfiguration.Add)) && x.IsGenericMethod);
                var applyConfigMethod = applyConfigMethods.Single(x => x.GetParameters().Any(y => y.ParameterType.Name.Equals(typeof(EntityTypeConfiguration<>).Name)));

                var applyConfigGenericMethod = applyConfigMethod.MakeGenericMethod(entityType);

                applyConfigGenericMethod.Invoke(modelBuilder, new[] { Activator.CreateInstance(configuration) });
            }
        }
    }
}
