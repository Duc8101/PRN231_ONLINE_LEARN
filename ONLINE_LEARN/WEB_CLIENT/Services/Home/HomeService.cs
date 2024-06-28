using Common.Base;
using Common.Const;
using Common.Entity;
using DataAccess.Model.IDAO;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace WEB_CLIENT.Services.Home
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
