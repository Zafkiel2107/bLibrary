using System.Threading.Tasks;
using System.Web.Mvc;

namespace bLibrary.Interfaces
{
    public interface IRepository<T, TParam> where T : ActionResult
                                            where TParam : class
    {
       Task<T> Create(TParam param);
       Task<T> Edit(TParam param);
       Task<T> Delete(int? id);
    }
}
