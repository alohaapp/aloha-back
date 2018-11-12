public interface IClassMapping<TInput, TOutput>
{
    TOutput Map(TInput input);
}