using System.Threading.Tasks;

namespace bLibrary.Interfaces
{
    public interface IRepository<T, TParam>
    {
       Task<T> Create(TParam param);
       Task<T> Edit(TParam param);
       Task<T> Delete(int? id);
    }
}
