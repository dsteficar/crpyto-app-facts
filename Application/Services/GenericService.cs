using Application.Contracts.Repos;
using Application.Contracts.Services;
using Application.DTOs.Response;

namespace Application.Services
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly IGenericRepository<T> _repository;

        public GenericService(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResult<IEnumerable<T>>> GetAllAsync()
        {
            try
            {
                var entities = await _repository.GetAllAsync();

                return entities != null && entities.Any()
                   ? ServiceResult<IEnumerable<T>>.Success(entities)
                   : ServiceResult<IEnumerable<T>>.Failure("No entities found.");
            }
            catch (Exception ex) when(ex is not ApplicationException)
            {
                throw new ApplicationException(ex.Message);
            }
            catch (ApplicationException)
            {
                throw;
            }
        }

        public async Task<ServiceResult<IEnumerable<T>>> GetPagedAsync(int skip, int top)
        {
            try
            {
                var entities = await _repository.GetPagedAsync(skip, top);

                return entities != null && entities.Any()
                    ? ServiceResult<IEnumerable<T>>.Success(entities)
                    : ServiceResult<IEnumerable<T>>.Failure("No entities found for the specified range.");
            }
            catch (Exception ex) when (ex is not ApplicationException)
            {
                throw new ApplicationException(ex.Message);
            }
            catch (ApplicationException)
            {
                throw;
            }
        }

        public async Task<ServiceResult<T>> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);

                return entity != null
                    ? ServiceResult<T>.Success(entity)
                    : ServiceResult<T>.Failure($"Entity with ID {id} not found.");
            }
            catch (Exception ex) when (ex is not ApplicationException)
            {
                throw new ApplicationException(ex.Message);
            }
            catch (ApplicationException)
            {
                throw;
            }
            
        }

        public async Task<ServiceResult<bool>> AddAsync(T entity)
        {
            try 
            { 
                await _repository.AddAsync(entity);
                await _repository.SaveChangesAsync();

                return ServiceResult<bool>.Success(true, "Entity successfully added.");
            }
            catch (Exception ex) when(ex is not ApplicationException)
            {
                throw new ApplicationException(ex.Message);
            }
            catch (ApplicationException)
            {
                throw;
            }
        }

        public async Task<ServiceResult<bool>> UpdateAsync(T entity)
        {
            try
            {
                await _repository.UpdateAsync(entity);
                await _repository.SaveChangesAsync();

                return ServiceResult<bool>.Success(true, "Entity successfully updated.");
            }
            catch (Exception ex) when (ex is not ApplicationException)
            {
                throw new ApplicationException(ex.Message);
            }
            catch (ApplicationException)
            {
                throw;
            }
        }

        public async Task<ServiceResult<bool>> DeleteAsync(T entity)
        {
            try
            {
                await _repository.DeleteAsync(entity);
                await _repository.SaveChangesAsync();

                return ServiceResult<bool>.Success(true, "Entity successfully deleted.");
            }
            catch (Exception ex) when (ex is not ApplicationException)
            {
                throw new ApplicationException(ex.Message);
            }
            catch (ApplicationException)
            {
                throw;
            }
        }

        public async Task<ServiceResult<int>> GetCountAsync()
        {
            try
            {
                var count = await _repository.GetCountAsync();
                return ServiceResult<int>.Success(count);
            }
            catch (Exception ex) when (ex is not ApplicationException)
            {
                throw new ApplicationException(ex.Message);
            }
            catch (ApplicationException)
            {
                throw;
            }
        }
    }
}
