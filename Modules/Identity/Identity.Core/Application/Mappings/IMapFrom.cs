using AutoMapper;

namespace Identity.Core.Application.Mappings;

public interface IMapFrom<T>
{
    public virtual void Mapping(Profile profile)
    {
        profile.CreateMap(typeof(T), GetType());
    }
}