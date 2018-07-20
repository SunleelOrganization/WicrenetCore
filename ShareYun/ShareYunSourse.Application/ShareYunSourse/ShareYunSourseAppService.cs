using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace ShareYunSourse.Application.ShareYunSourse
{
    public class ShareYunSourseAppService :IShareYunSourseAppService
    {
        private readonly IRepository<YunSourse> _yunsourseIRepository;
        private readonly IUserManager UserManager;

        public ShareYunSourseAppService(IRepository<YunSourse> yunsourseIRepository, IUserManager userManager)
        {
            _yunsourseIRepository = yunsourseIRepository;
            UserManager = userManager;
        }
        public async Task GetName()
        {
             var list = _yunsourseIRepository.GetAll().Where(m => !string.IsNullOrEmpty(m.Content)).ToList();
        }
    }
}
