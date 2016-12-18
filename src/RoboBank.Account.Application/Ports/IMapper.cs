namespace RoboBank.Account.Application.Ports
{
    public interface IMapper
    {
        TDestination Map<TSource, TDestination>(TSource source);
    }
}
