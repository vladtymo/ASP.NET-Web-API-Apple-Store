using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class PhoneService : IPhoneService
    {
        private readonly IRepository<Phone> phoneRepo;
        private readonly IMapper mapper;

        public PhoneService(IRepository<Phone> phoneRepo, IMapper mapper)
        {
            this.phoneRepo = phoneRepo;
            this.mapper = mapper;
        }

        public void Create(PhoneDTO phone)
        {
            // we used AutoMapper instead
            //Phone entity = new()
            //{
            //    Id = phone.Id,
            //    Model = phone.Model,
            //    ColorId = phone.ColorId,
            //    Price = phone.Price,
            //    Memory = phone.Memory,
            //    Description = phone.Description,
            //    ImagePath = phone.ImagePath
            //};

            phoneRepo.Insert(mapper.Map<Phone>(phone));
            phoneRepo.Save();
        }

        public void Delete(int id)
        {
            var phone = phoneRepo.GetByID(id);

            if (phone == null) 
                throw new HttpException(ErrorMessages.PhoneNotFound, HttpStatusCode.NotFound);

            phoneRepo.Delete(phone);
            phoneRepo.Save();
        }

        public void Edit(PhoneDTO phone)
        {
            phoneRepo.Update(mapper.Map<Phone>(phone));
            phoneRepo.Save();
        }

        public PhoneDTO? Get(int id)
        {
            var phone = phoneRepo.GetByID(id);

            if (phone == null) 
                throw new HttpException(ErrorMessages.PhoneNotFound, HttpStatusCode.NotFound);

            return mapper.Map<PhoneDTO>(phone);
        }

        public IEnumerable<PhoneDTO> GetAll()
        {
            var phones = phoneRepo.Get(includeProperties: $"{nameof(Phone.Color)}");
            return mapper.Map<IEnumerable<PhoneDTO>>(phones);
        }
    }
}
