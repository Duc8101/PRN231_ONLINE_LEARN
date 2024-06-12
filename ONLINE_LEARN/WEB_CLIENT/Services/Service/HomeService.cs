using Common.Base;
using Common.Consts;
using Common.Entity;
using DataAccess.Model.IDAO;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WEB_CLIENT.Services.IService;

namespace WEB_CLIENT.Services.Service
{
    public class HomeService : IHomeService
    {
        private readonly ICommonDAO<User> _daoUser;

        public HomeService(ICommonDAO<User> daoUser)
        {
            _daoUser = daoUser;
        }

        public async Task<ResponseBase<List<User>?>> Index()
        {
            try
            {
                // get top 4 teacher
                List<User> list = await _daoUser.FindAll(u => u.RoleName == UserConst.ROLE_TEACHER && u.IsDeleted == false).Take(4).ToListAsync();
                return new ResponseBase<List<User>?>(list, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<User>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }

        }
    }
}
