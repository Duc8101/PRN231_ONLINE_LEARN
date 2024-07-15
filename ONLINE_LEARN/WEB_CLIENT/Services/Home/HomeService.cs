using Common.Base;
using Common.Entity;
using DataAccess.Model.DAO;
using System.Net;

namespace WEB_CLIENT.Services.Home
{
    public class HomeService : IHomeService
    {
        private readonly DAOUser _daoUser;

        public HomeService(DAOUser daoUser)
        {
            _daoUser = daoUser;
        }

        public ResponseBase<List<User>?> Index()
        {
            try
            {
                // get top 4 teacher
                List<User> list = _daoUser.getTop4Teacher();
                return new ResponseBase<List<User>?>(list);
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<User>?>(ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }

        }
    }
}
