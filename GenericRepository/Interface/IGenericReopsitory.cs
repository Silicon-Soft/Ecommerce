
namespace Ecommerce.GenericRepository.Interface
{
    public interface IGenericReopsitory<T> where T : class
    {
        T Add(T entity);
        List<T> GetAll();
        T GetById(int id);
        void Delete(int id);
        T Update(T entity);
        IQueryable<T> GetDatas();
    }
}
