namespace Application
{
    public class HandlerResult<T> where T : class
    {
        public T Entity { get; set; }
    }

    public class HandlerListResult<IList> where IList : class
    {
        public IList Entity { get; set; }
    }
}
