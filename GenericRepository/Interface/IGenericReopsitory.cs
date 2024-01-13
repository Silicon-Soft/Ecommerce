
namespace Ecommerce.GenericRepository.Interface
{
    public interface IGenericReopsitory<T> where T : class
    {
        void Add(T obj);
        List<T> GetAll();
        T GetById(int id);
        void Delete(int id);
        void Update(T obj);
        IQueryable<T> GetDatas();
    }
}
