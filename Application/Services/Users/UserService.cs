using Application.Contracts.Repos;
using Application.Contracts.Services;
using Application.DTOs.Response;
using AutoMapper;
using Domain.Entity.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public UserService(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<IEnumerable<ApplicationUser>>> GetAllAsync()
        {

            try
            {
                var entities = await _accountRepository.GetAllAsync();


                return entities != null && entities.Any()
                   ? ServiceResult<IEnumerable<ApplicationUser>>.Success(entities)
                   : ServiceResult<IEnumerable<ApplicationUser>>.Failure("No user entities found.");
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

        public async Task<ServiceResult<ApplicationUser>> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _accountRepository.FindByIdAsync(id);

                return entity != null
                    ? ServiceResult<ApplicationUser>.Success(entity)
                    : ServiceResult<ApplicationUser>.Failure($"Entity with ID {id} not found.");
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

        public async Task<ServiceResult<ApplicationUser>> GetByEmailAsync(string email)
        {
            try
            {
                var entity = await _accountRepository.FindByEmailAsync(email);

                return entity != null
                    ? ServiceResult<ApplicationUser>.Success(entity)
                    : ServiceResult<ApplicationUser>.Failure($"Entity with Email {email} not found.");
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

        public async Task<ServiceResult<bool>> AddAsync(ApplicationUser user, string password)
        {
            try
            {
                // await _accountRepository.AddAsync(user, password);

                var creationResult = await _accountRepository.AddAsync(user, password);

                if (!creationResult.Succeeded) return null!; //or throw exception

                var result = await _accountRepository.FindByEmailAsync(user.Email);

                return result != null
                    ? ServiceResult<bool>.Success(true, "User successfully added.")
                    : ServiceResult<bool>.Failure($"User creation failed.");
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

        public async Task<ServiceResult<bool>> UpdateAsync(ApplicationUser user, string password)
        {
            try
            {
                var passwordHasher = new PasswordHasher<ApplicationUser>();
                user.PasswordHash = passwordHasher.HashPassword(user, password);

                var updateResult = await _accountRepository.UpdateAsync(user);

                if (!updateResult.Succeeded) return null!; //or throw exception

                if (updateResult.Succeeded)
                    return ServiceResult<bool>.Success(true, "User successfully updated.");
                else
                    return ServiceResult<bool>.Failure("User update failed.");

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


        public async Task<ServiceResult<bool>> DeleteAsync(ApplicationUser user)
        {
            try
            {
                var identityResult = await _accountRepository.DeleteAsync(user);

                if (identityResult.Succeeded)
                    return ServiceResult<bool>.Success(true, "User successfully deleted.");
                else
                    return ServiceResult<bool>.Failure("User deletion failed.");

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
