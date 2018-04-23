namespace RoboBank.Account.Application.Adapters
{
    public class Mapper :AccountApplicationService.IMapper
    {
        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return AutoMapper.Mapper.Map<TSource, TDestination>(source);
        }
    }
}
