using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Exceptions;

namespace TaskFlow.Application.Validations
{
    public static class ValidateParameters
    {
        public static void ValidateId(long id)
        {
            if (id <= 0)
                throw new BadRequestException("O Id informado é invalido");
        }
    }
}
