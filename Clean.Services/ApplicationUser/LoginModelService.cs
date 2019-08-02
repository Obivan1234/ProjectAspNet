using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Core.Data;
using Clean.Core.Domain.ApplicationUser;

namespace Clean.Services.ApplicationUser
{
    public class LoginModelService : ILoginModelService
    {
        private IRepository<LoginModel> _loginRepository;

        public LoginModelService(IRepository<LoginModel> loginRepository)
        {
            this._loginRepository = loginRepository;
        }

        public void Delete(LoginModel loginModel)
        {
            _loginRepository.Delete(loginModel);
        }

        public IEnumerable<LoginModel> GetAllLogins()
        {
            return _loginRepository.Get();
        }

        public LoginModel GetById(int id)
        {
            return _loginRepository.GetById(id);
        }

        public void InsertLogin(LoginModel loginModel)
        {
            if (loginModel == null)
                throw new ArgumentNullException("loginModel", "Does Not conSis");
            else
                _loginRepository.Insert(loginModel);
        }
    }
}
