using BusinessLogic.DTOs;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IPhoneService
    {
        IEnumerable<PhoneDTO> GetAll();
        PhoneDTO? Get(int id);
        void Create(PhoneDTO phone);
        void Edit(PhoneDTO phone);
        void Delete(int id);
    }
}
