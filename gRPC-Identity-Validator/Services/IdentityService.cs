using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace gRPC_Identity_Validator
{
    public class IdentityService : IdentityValidatorService.IdentityValidatorServiceBase
    {
        private readonly ILogger<IdentityService> _logger;

        public IdentityService(ILogger<IdentityService> logger)
        {
            _logger = logger;
        }

        public override Task<IdentityValidationResult> ValidateAsync(IdentityValidateRequest request,
            ServerCallContext context)
        {
            // Do Some Auth Check
            var validationResult = TcIdentityNumberValidationAlgorithm.Validate(request.IdentityNumber);
            return Task.FromResult(validationResult);
        }
    }
}