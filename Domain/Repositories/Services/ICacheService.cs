using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Services;

public interface ICacheService
{
    void Set<T>(string key, T value);
    T Get<T>(string key);
    bool Remove(string key);
}
