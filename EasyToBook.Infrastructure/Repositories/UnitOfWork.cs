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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IVillaRepository Villa { get; private set; }
        public IVillaNumberRepository VillaNumber { get; private set; }
        public IAmenityRepository Amenity { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Villa = new VillaRepository(_context);
            VillaNumber = new VillaNumberRepository(_context);
            Amenity = new AmenityRepository(_context);
        }
    }
}
