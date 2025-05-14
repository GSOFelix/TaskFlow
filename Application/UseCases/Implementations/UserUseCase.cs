using Microsoft.AspNetCore.Http.HttpResults;
using TaskFlow.Application.Dtos.UserDto;
using TaskFlow.Application.Mappers;
using TaskFlow.Application.UseCases.Interfaces;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Exceptions;
using TaskFlow.Domain.Interfaces.Repository;
using TaskFlow.Domain.Interfaces.Services;

namespace TaskFlow.Application.UseCases.Implementations
{
    public class UserUseCase : IUserUseCase
    {
        private readonly IUserRepository _repository;
        private readonly IPassWordService _passWordService;

        public UserUseCase(IUserRepository repository, IPassWordService passWordService)
        {
            _repository = repository;
            _passWordService = passWordService;
        }

        public async Task<long> CreateUser(UserRequestDto request, CancellationToken token)
        {
            try
            {
                var passWordHash = _passWordService.Hash(request.Password);

                var user = new User(request.Name, request.Email, passWordHash);

                return await _repository.InsertAsync(user, token);
            }
            catch (Exception ex)
            {
                throw new InternalErrorException("Erro ao criar: ", ex.InnerException);
            }
        }

        public async Task<UserResponseDto> GetUserById(long id, CancellationToken token)
        {
            if (id == 0)
                throw new BadRequestException("A Id deve ser um valor válido");

            var user = await _repository.GetByIdAsync(id, token) 
                ?? throw new NotFoundException("Usuário não encontrado");

            return user.ToDto();
        }
    }
}
