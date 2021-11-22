namespace Utils.Filtering
{
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T candidate);
        ISpecification<T> And(ISpecification<T> other);
        ISpecification<T> AndNot(ISpecification<T> other);
        ISpecification<T> Or(ISpecification<T> other);
        ISpecification<T> OrNot(ISpecification<T> other);
        ISpecification<T> Not();
    }

    public abstract class CompositeSpecification<T> : ISpecification<T>
    {
        public abstract bool IsSatisfiedBy(T candidate);

        public ISpecification<T> And(ISpecification<T> other)
        {
            return new AndSpecification<T>(this, other);
        }

        public ISpecification<T> AndNot(ISpecification<T> other)
        {
            return new AndNotSpecification<T>(this, other);
        }

        public ISpecification<T> Or(ISpecification<T> other)
        {
            return new OrSpecification<T>(this, other);
        }

        public ISpecification<T> OrNot(ISpecification<T> other)
        {
            return new OrNotSpecification<T>(this, other);
        }

        public ISpecification<T> Not()
        {
            return new NotSpecification<T>(this);
        }
    }

    public class AndSpecification<T> : CompositeSpecification<T>
    {
        private ISpecification<T> leftCondition;
        private ISpecification<T> rightCondition;

        public AndSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            leftCondition = left;
            rightCondition = right;
        }

        public override bool IsSatisfiedBy(T candidate)
        {
            return leftCondition.IsSatisfiedBy(candidate) && rightCondition.IsSatisfiedBy(candidate);
        }
    }

    public class AndNotSpecification<T> : CompositeSpecification<T>
    {
        private ISpecification<T> leftCondition;
        private ISpecification<T> rightCondition;

        public AndNotSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            leftCondition = left;
            rightCondition = right;
        }

        public override bool IsSatisfiedBy(T candidate)
        {
            return leftCondition.IsSatisfiedBy(candidate) && rightCondition.IsSatisfiedBy(candidate) != true;
        }
    }

    public class OrSpecification<T> : CompositeSpecification<T>
    {
        private ISpecification<T> leftCondition;
        private ISpecification<T> rightCondition;

        public OrSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            leftCondition = left;
            rightCondition = right;
        }

        public override bool IsSatisfiedBy(T candidate)
        {
            return leftCondition.IsSatisfiedBy(candidate) || rightCondition.IsSatisfiedBy(candidate);
        }
    }

    public class OrNotSpecification<T> : CompositeSpecification<T>
    {
        private ISpecification<T> leftCondition;
        private ISpecification<T> rightCondition;

        public OrNotSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            leftCondition = left;
            rightCondition = right;
        }

        public override bool IsSatisfiedBy(T candidate)
        {
            return leftCondition.IsSatisfiedBy(candidate) || rightCondition.IsSatisfiedBy(candidate) != true;
        }
    }

    public class NotSpecification<T> : CompositeSpecification<T>
    {
        private ISpecification<T> Wrapped;

        public NotSpecification(ISpecification<T> x)
        {
            Wrapped = x;
        }

        public override bool IsSatisfiedBy(T candidate)
        {
            return !Wrapped.IsSatisfiedBy(candidate);
        }
    }
}
