namespace TopCourseWorkBl.BusinessLayer.CheckStrategy.Abstractions
{
    public abstract class BaseCheck : ICheckStrategy
    {
        private ICheckStrategy? _nextStrategy;

        public ICheckStrategy SetNext(ICheckStrategy checkStrategy)
            => _nextStrategy = checkStrategy;

        public abstract (bool, ICheckStrategy) Check(ICheckResponse checkResponse);

        protected (bool, ICheckStrategy) CheckNext(ICheckResponse checkResponse)
            => _nextStrategy?.Check(checkResponse) ?? (true, this);
    }
}