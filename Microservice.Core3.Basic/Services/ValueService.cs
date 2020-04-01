using AutoMapper;
using Microservice.Core3.Basic.Configurations.Exceptions;

namespace Microservice.Core3.Basic.Services
{
    public class ValueService
    {
        private readonly IMapper _mapper;

        public ValueService(IMapper mapper) => _mapper = mapper;

        public void CustomExceptionVoid() => throw new CustomException(Type.Conflict);

        public void CustomExceptionMessage() => throw new CustomException(Type.Conflict, "This is a known exception, yei!");
    }
}
