using AutoMapper;

namespace Identity.Core.Application.Mappings;

public interface IMapTo<T>
{
    public virtual void Mapping(Profile profile)
    {
        profile.CreateMap(GetType(), typeof(T));
    }
}