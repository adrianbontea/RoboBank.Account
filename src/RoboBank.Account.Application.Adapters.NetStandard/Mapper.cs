namespace RoboBank.Account.Application.Adapters.NetStandard
{
    public class Mapper : Ports.IMapper
    {
        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return AutoMapper.Mapper.Map<TSource, TDestination>(source);
        }
    }
}
