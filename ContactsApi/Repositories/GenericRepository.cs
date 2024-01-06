using System.Collections.Generic;
using System.Threading.Tasks;
using ContactsApi.Data;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;

namespace ContactsApi.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly ContactsApiContext _context;

        public GenericRepository(ContactsApiContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }



        public async Task<TEntity> GetById(int id)
        {
            TEntity entity = await _context.Set<TEntity>().FindAsync(id) ;

            if (entity == null)
            {
                throw new NotFoundException($"Entity with ID {id} not found");
            }

            return entity;
        }


        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task Create(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        // Implementa el resto de los métodos de la interfaz según sea necesario

        public async Task<IEnumerable<TEntity>> SearchByEmailOrPhone(string searchTerm)
        {
            // Implementa la lógica de búsqueda por email o teléfono aquí
            return null;
        }

        public async Task<IEnumerable<TEntity>> GetByLocation(string state, string city)
        {
            // Implementa la lógica de búsqueda por ubicación aquí
            return null;
        }
    }
}
