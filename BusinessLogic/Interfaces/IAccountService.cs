using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IAccountService
    {
        Task Register(RegisterDTO data);
        Task Login(string email, string password);
        Task Logout();
    }
}
