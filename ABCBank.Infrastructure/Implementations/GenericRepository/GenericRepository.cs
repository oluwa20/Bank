using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCBank.Dependencies.GenericRepository.Interfaces;
using ABCBank.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace ABCBank.Infrastructure.Implementations.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ABCBankDbContext _context;
        private DbSet<T> _tbl;

        public GenericRepository(ABCBankDbContext context)
        {
            _context = context;
            _tbl=_context.Set<T>();
            
        }
        public async Task<T> Add(T data)
        {
            await _tbl.AddAsync(data);
            return data;
        }

        public async Task<bool> Delete(Guid Id)
        {
             var entity=await GetById(Id);
            if(entity==null){
                throw new NullReferenceException("ENTITY COULD NOT BE FOUND!");
            }
            _tbl.Remove(entity);
            return true;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _tbl.ToListAsync();
        }

        public async Task<T> GetById(Guid Id)
        {
            return await _tbl.FindAsync(Id);
        }

        public async Task<T> Update(Guid Id, T data)
        {
            var entity=await GetById(Id);
            if(entity==null){
                throw new NullReferenceException("ENTITY COULD NOT BE FOUND!");
            }
            _tbl.Update(data);
            return data;
        }
    }
}