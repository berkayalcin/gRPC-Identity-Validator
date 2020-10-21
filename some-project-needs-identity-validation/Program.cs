using System;
using gRPC_Identity_Validator;
using Grpc.Net.Client;

namespace some_project_needs_identity_validation
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = GetClient();

            string input = "";
            string errorMessage = null;
            IdentityValidationResult result = null;

            Console.WriteLine("Welcome to identity validator.");
            Console.WriteLine("Write identity to validate then press enter." +
                              "\nTo Quit Press q");
            while (!string.IsNullOrEmpty((input = Console.ReadLine())))
            {
                if(input.Equals("q"))
                    break;
                
                try
                {
                    result = client.ValidateAsync(new IdentityValidateRequest()
                    {
                        IdentityNumber = input
                    });
                }
                catch (Exception e)
                {
                    errorMessage = e.Message;
                }

                Console.WriteLine($"Your Input {input} - Validation Response {errorMessage ?? result.Message}");
            }
        }

        private static IdentityValidatorService.IdentityValidatorServiceClient GetClient()
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            var channel = GrpcChannel.ForAddress("http://localhost:5000");
            return new IdentityValidatorService.IdentityValidatorServiceClient(channel);
        }
    }
}