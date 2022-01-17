using System;

namespace Infrastructure.DDD
{
    public abstract class TypedIdValueBase<T>  : IEquatable<TypedIdValueBase<T>>  where  T: IComparable
    {
        public TypedIdValueBase()
        {
            
        }
        public T Value { get; }
        
        public TypedIdValueBase(T value)
        {
            Value = value;
        }

        public static bool operator ==(int rhs,TypedIdValueBase<T> lhs)
        {
            if (lhs is null)
            {
                // if (rhs is null)
                // {
                //     return true;
                // }
                return false;
            }

            return lhs.Value.Equals(rhs);
        }
        public static bool operator !=(int rhs,TypedIdValueBase<T> lhs)
        {
            return !(rhs == lhs);
        }
        public static bool operator ==(TypedIdValueBase<T> lhs, T rhs)
        {
            if (lhs is null)
            {
                if (rhs is null)
                {
                    return true;
                }
                return false;
            }

            return lhs.Value.Equals(rhs);
        }

        public static bool operator !=(TypedIdValueBase<T> lhs, T rhs)
        {
            return !(lhs == rhs);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is TypedIdValueBase<T> other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public bool Equals(TypedIdValueBase<T> other)
        {
            return this.Value.Equals(other.Value);
        }

        public static bool operator ==(TypedIdValueBase<T> obj1, TypedIdValueBase<T> obj2)
        {
            if (object.Equals(obj1, null))
            {
                if (object.Equals(obj2, null))
                {
                    return true;
                }
                return false;
            }
            return obj1.Equals(obj2);
        }
        public static bool operator !=(TypedIdValueBase<T> x, TypedIdValueBase<T> y) 
        {
            return !(x == y);
        }

        public static implicit operator T(TypedIdValueBase<T> src)
        {
            return src.Value;
        }
    }
}