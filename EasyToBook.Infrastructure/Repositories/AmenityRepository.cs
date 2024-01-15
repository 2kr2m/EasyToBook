using EasyToBook.Application.Common.Interfaces;
using EasyToBook.Domain.Entities;
using EasyToBook.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyToBook.Infrastructure.Repositories
{
    public class AmenityRepository : Repository<Amenity>, IAmenityRepository
    {
        private readonly ApplicationDbContext _context;
        public AmenityRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Amenity entity)
        {
            _context.Amenities.Update(entity);
        }
    }
}
