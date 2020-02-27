using AutoMapper;
using Microservice.Core3.Basic.Configurations.Exceptions;

namespace Microservice.Core3.Basic.Services
{
    public class ValueService
    {
        private readonly IMapper _mapper;

        public ValueService(IMapper mapper) => _mapper = mapper;

        public void CustomExceptionVoid() => throw new ConflictCustomException();

        public void CustomExceptionMessage() => throw new ConflictCustomException("This is a known exception, yei!");
    }
}
