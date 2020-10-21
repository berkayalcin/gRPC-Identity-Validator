// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/identity.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace gRPC_Identity_Validator {
  public static partial class IdentityValidatorService
  {
    static readonly string __ServiceName = "identity.IdentityValidatorService";

    static readonly grpc::Marshaller<global::gRPC_Identity_Validator.IdentityValidateRequest> __Marshaller_identity_IdentityValidateRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::gRPC_Identity_Validator.IdentityValidateRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::gRPC_Identity_Validator.IdentityValidationResult> __Marshaller_identity_IdentityValidationResult = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::gRPC_Identity_Validator.IdentityValidationResult.Parser.ParseFrom);

    static readonly grpc::Method<global::gRPC_Identity_Validator.IdentityValidateRequest, global::gRPC_Identity_Validator.IdentityValidationResult> __Method_ValidateAsync = new grpc::Method<global::gRPC_Identity_Validator.IdentityValidateRequest, global::gRPC_Identity_Validator.IdentityValidationResult>(
        grpc::MethodType.Unary,
        __ServiceName,
        "ValidateAsync",
        __Marshaller_identity_IdentityValidateRequest,
        __Marshaller_identity_IdentityValidationResult);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::gRPC_Identity_Validator.IdentityReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of IdentityValidatorService</summary>
    [grpc::BindServiceMethod(typeof(IdentityValidatorService), "BindService")]
    public abstract partial class IdentityValidatorServiceBase
    {
      public virtual global::System.Threading.Tasks.Task<global::gRPC_Identity_Validator.IdentityValidationResult> ValidateAsync(global::gRPC_Identity_Validator.IdentityValidateRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(IdentityValidatorServiceBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_ValidateAsync, serviceImpl.ValidateAsync).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static void BindService(grpc::ServiceBinderBase serviceBinder, IdentityValidatorServiceBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_ValidateAsync, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::gRPC_Identity_Validator.IdentityValidateRequest, global::gRPC_Identity_Validator.IdentityValidationResult>(serviceImpl.ValidateAsync));
    }

  }
}
#endregion
