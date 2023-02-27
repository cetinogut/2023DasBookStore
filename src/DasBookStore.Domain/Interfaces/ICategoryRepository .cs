using DasBookStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DasBookStore.Domain.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        //not need any specific methods, so we will just create the interface that inherits from IRepository
    }
}
