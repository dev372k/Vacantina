using Shared.DTOs.AppDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IAppRepo
    {
        Task Contact(ContactDTO dto);
    }
}
