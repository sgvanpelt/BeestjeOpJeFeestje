using BeestjeOpJeFeestje.Core.Interfaces;
using BeestjeOpJeFeestje.Infrastructure;
using BeestjeOpJeFeestje.Infrastructure.Repositories;
using System;
using System.Diagnostics.CodeAnalysis;
using Unity;
using Unity.Injection;

namespace BeestjeOpJeFeestje
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();

            BoefContext context = new BoefContext();

            container.RegisterType<IAnimalRepository, AnimalRepository>();
            container.RegisterType<IAccesoryRepository, AccessoryRepository>();
            container.RegisterType<IBookingRepository, BookingRepository>(new InjectionConstructor(context));
            container.RegisterType<ICustomerRepository, CustomerRepository>();
        }

        public static T MyResolve<T>()
        {
            System.Console.WriteLine();
            var temp = container.Value.Resolve<T>();
            System.Console.WriteLine();
            return temp;
        }
    }
    
}