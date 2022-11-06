using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Exceptions;
using BusinessLogic.Interfaces;
using BusinessLogic.Resources;
using DataAccess;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class PhoneService : IPhoneService
    {
        private readonly StoreDbContext context;
        private readonly IMapper mapper;

        public PhoneService(StoreDbContext context, IMapper mapper)
        {
            this.context = context;
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

            context.Phones.Add(mapper.Map<Phone>(phone));
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var phone = context.Phones.Find(id);

            if (phone == null) 
                throw new HttpException(ErrorMessages.PhoneNotFound, HttpStatusCode.NotFound);

            context.Phones.Remove(phone);
            context.SaveChanges();
        }

        public void Edit(PhoneDTO phone)
        {
            context.Phones.Update(mapper.Map<Phone>(phone));
            context.SaveChanges();
        }

        public PhoneDTO? Get(int id)
        {
            var phone = context.Phones.Find(id);

            if (phone == null) 
                throw new HttpException(ErrorMessages.PhoneNotFound, HttpStatusCode.NotFound);

            return mapper.Map<PhoneDTO>(phone);
        }

        public IEnumerable<PhoneDTO> GetAll()
        {
            var phones = context.Phones.Include(p => p.Color).ToList();
            return mapper.Map<IEnumerable<PhoneDTO>>(phones);
        }
    }
}
