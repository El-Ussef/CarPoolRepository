using System.ComponentModel.DataAnnotations;

namespace Identity.Core.Domain.Common;

public interface IEntity
{
    
}

public interface ITimeModification
{
    DateTime CreatedAt { get; set; }
    DateTime? ModifiedAt { get; set; }
}
public abstract class BaseEntity<TId>: IEntity,ITimeModification
{
    [Key]
    public TId Id { get; protected set; }

    protected BaseEntity()
    {
        // If TId is of type Guid, set a new Guid by default
        if (typeof(TId) == typeof(Guid))
        {
            Id = (TId)(object)Guid.NewGuid();
        }
    }

    public override bool Equals(object obj)
    {
        if (obj == null || obj.GetType() != GetType())
            return false;

        var entity = (BaseEntity<TId>)obj;
        return Equals(Id, entity.Id);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public static bool operator ==(BaseEntity<TId> left, BaseEntity<TId> right)
    {
        if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
            return true;

        if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(BaseEntity<TId> left, BaseEntity<TId> right)
    {
        return !(left == right);
    }

    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
}