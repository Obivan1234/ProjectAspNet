using Clean.Core.Domain.ApplicationUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Services.ApplicationUser
{
    public interface ILoginModelService
    {
        void InsertLogin(LoginModel loginModel);
        IEnumerable<LoginModel> GetAllLogins();
        void Delete(LoginModel loginModel);
        LoginModel GetById(int id);
    }
}
