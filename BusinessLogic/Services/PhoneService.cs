using BusinessLogic.Interfaces;
using DataAccess;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class PhoneService : IPhoneService
    {
        private readonly StoreDbContext context;
        public PhoneService(StoreDbContext context)
        {
            this.context = context;
        }

        public void Create(Phone phone)
        {
            context.Phones.Add(phone);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var phone = context.Phones.Find(id);

            if (phone == null) return; // throw exception

            context.Phones.Remove(phone);
            context.SaveChanges();
        }

        public void Edit(Phone phone)
        {
            context.Phones.Update(phone);
            context.SaveChanges();
        }

        public Phone? Get(int id)
        {
            var phone = context.Phones.Find(id);

            //if (phone == null) return null; // throw exception

            return phone;
        }

        public IEnumerable<Phone> GetAll()
        {
            return context.Phones.ToList();
        }
    }
}
