using ShareYunSourse.Core.Dependency;
using System.Threading.Tasks;

namespace ShareYunSourse.Application
{
    public interface IShareYunSourseAppService: IDependency
    {
        Task GetName();
    }
}
