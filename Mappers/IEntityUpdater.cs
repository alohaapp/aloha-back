namespace Aloha.Mappers
{
    public interface IEntityUpdater<TEntity>
    {
        void Update(TEntity target, TEntity source);
    }
}