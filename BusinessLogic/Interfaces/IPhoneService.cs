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
        IEnumerable<Phone> GetAll();
        Phone? Get(int id);
        void Create(Phone phone);
        void Edit(Phone phone);
        void Delete(int id);
    }
}
