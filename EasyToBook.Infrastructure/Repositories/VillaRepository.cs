using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EasyToBook.Application.Common.Interfaces;
using EasyToBook.Domain.Entities;
using EasyToBook.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EasyToBook.Infrastructure.Repositories
{
    public class VillaRepository : Repository<Villa> ,IVillaRepository
    {
        private readonly ApplicationDbContext _context;
        public VillaRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Villa entity)
        {
            _context.Villas.Update(entity);
        }
    }
}
