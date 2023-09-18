using AutoMapper;
using Mapster;
using System.Reflection;

namespace Extensions.Mapper
{
    public class ObjectMapper<T> where T : Profile, new()
    {
        static public MapperConfigurationExpression CreateConfigurationExpression()
        {
            var configurationExpression = new MapperConfigurationExpression();
            configurationExpression.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
            configurationExpression.AddProfile<T>();
            return configurationExpression;
        }
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() => {
            var config = new MapperConfiguration(CreateConfigurationExpression());
            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper mapper => Lazy.Value;
    }
    class ObjectMappingProfile<TSource, TDestination> : Profile where TSource : class where TDestination : class
    {
        public ObjectMappingProfile()
        {
            TraverseObject(typeof(TSource), typeof(TDestination));
        }
        public void TraverseObject(Type source, Type dest)
        {
            if (!typeof(IEnumerable<Object>).IsAssignableFrom(source))
                CreateMap(source, dest).ReverseMap();
            var sourceProperties = source.GetProperties();
            var destProperties = dest.GetProperties();
            foreach (PropertyInfo sp in sourceProperties)
            {
                foreach (PropertyInfo dp in destProperties)
                {
                    if (sp.Name == dp.Name && !IsSimple(sp.PropertyType))
                    {
                        bool isEnumerable = typeof(IEnumerable<Object>).IsAssignableFrom(sp.PropertyType);
                        if (isEnumerable)
                            TraverseObject(sp.PropertyType.GetGenericArguments().Single(),
                                dp.PropertyType.GetGenericArguments().Single());
                        else
                            TraverseObject(sp.PropertyType, dp.PropertyType);
                    }
                }
            }
        }
        bool IsSimple(Type type)
        {
            return type.IsPrimitive || type.IsEnum || type.Equals(typeof(string)) || type.Equals(typeof(decimal))
                || type.Equals(typeof(int)) || type.IsValueType;
        }
    }
    public class Mapper
    {
        public static TDestination Map<TSource, TDestination>(TSource sourceObject) where TSource : class where TDestination : class
        {
            return sourceObject.Adapt<TDestination>();
        }
    }
}
