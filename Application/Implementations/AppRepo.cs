using Application.Implementations.Base;
using Domain.Documents;
using Domain.Repositories;
using Domain.Repositories.Services;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Shared.DTOs.AppDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Implementations
{
    public class AppRepo : BaseRepo<Contact>, IAppRepo
    {
        public AppRepo(
        IMongoClient mongoClient,
        IClientSessionHandle clientSessionHandle) : base(mongoClient, clientSessionHandle, "contact") { }

        public async Task Contact(ContactDTO dto)
        {
            await InsertAsync(new Contact
            (
                dto.Name,
                dto.Email,
                dto.Message
            ));
        }
    }
}
