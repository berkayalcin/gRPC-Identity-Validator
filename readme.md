# gRPC ile Kimlik Numarası Doğrulama Servisi

**Konular**

 - gRPC Nedir?
 - Protobuf Nedir ?
 - gRPC .Net Core Server'ı nasıl ayağa kaldırılır ?
 - .Net Core ile gRPC Server'ı nasıl kullanılır ?

### gRPC Nedir ?

gRPC , Client uygulamalarının Server uygulamadaki bir methodu sanki o method Client üzerinde tanımlıymış gibi uzaktan çağırmasına denilir.
gRPC Servisi Interface Definition Language olarak Protocol Buffer'ı kullanılır.

### Protocol Buffer (Protobuf) Nedir ?

Protocol Buffer , Google'ın dil bağımsız , platform bağımsız dilidir.Protocol Bufferlar Yapısal Verileride (Nesneler) Serialization için kullanılır.
Protocol Bufferlar Dil bağımsız olduğu için burada üretilen sınıflar desteklediği diller üzerinde objelere dönüştürülürler.

[**Daha Detaylı Bilgi İçin**](https://developers.google.com/protocol-buffers/docs/proto3)

#### Örnek Protobuf Kodumuza Bakalım


```protobuf
// Burada Protobuf Syntax Versiyonunu Tanımlıyoruz.
syntax = "proto3";  
// Üretilen Sınıfımızın Namespace'ini tanımlıyoruz.
option csharp_namespace = "gRPC_Identity_Validator";  
// Üretilen Sınıfın Paketini tanımlıyoruz.
package identity;  

// Burada Üretilecek olan Servisimizi ve Methodunu signature ve return value'su ile birlikte tanımlıyoruz.
service IdentityValidatorService{  
  rpc ValidateAsync(IdentityValidateRequest) returns (IdentityValidationResult); }  

// Request Objemiz
message IdentityValidateRequest{  
  // Fieldlara verilen numaralar bu fieldların unique indexlerini temsil ediyor.
  // NOT : Protobufta bütün fieldlar optionaldır (nullable).
  string identityNumber = 1;  
}  
// Response Objemiz
message IdentityValidationResult{  
  bool isSuccess = 1;  
  string message = 2;  
}
```

### gRPC .Net Core Server'ı nasıl ayağa kaldırılır ?

gRPC Servisini Visual Studio'da oluşturmak için AspNetCore Web Application'ı seçip sonrasında proje template'inden gRPCyi seçerek boilerplate gRPC projesini elde edebiliriz.
MacOS için Rider'da aynı aşamalar'ı yaparak aynı şekilde proje oluşturulabilir.

**MacOS için uyarı** : MacOS üzerinde TLS Ip Port binding support olmadığı için Kestrel üzerinde ufak bir değişiklik yapmak gerekiyor.Program.cs dosyamızda CreateHostBuilder methodunu aşağıdaki gibi değiştirmemiz gerekiyor.

```csharp
public static IHostBuilder CreateHostBuilder(string[] args) =>  
    Host.CreateDefaultBuilder(args)  
		.ConfigureWebHostDefaults(webBuilder =>  
        {  
			  webBuilder.ConfigureKestrel(options =>  
			  {  
					 options.ListenLocalhost(5000, o => o.Protocols =  
					  HttpProtocols.Http2);  
			 }); 
	 webBuilder.UseStartup<Startup>();  
 });
```

### .Net Core ile gRPC Server'ı nasıl kullanılır ?

Herhangi bir .Net Core uygulamamıza Server için oluşturduğumuz proto dosyamızı Projede Protos klasörü açarak içerisine ekleyelim.
Ardından projemizin {projectName}.csproj dosyasında aşağıdaki kodu yapıştıralım ve kaydedelim.


```xml
<ItemGroup>  
	  <!--Burada Server'ımızda tanımladığımız Proto dosyamızı , bu proje için Client olarak tanımlıyoruz.-->  
	  <Protobuf Include="Protos\identity.proto" GrpcServices="Client" />  
	  <!--Burada gerekli Nuget Paketlerimizi kuruyoruz.-->  
	  <PackageReference Include="Google.Protobuf" Version="3.11.4" />  
	 <PackageReference Include="Grpc.Net.Client" Version="2.28.0" />  
	 <PackageReference Include="Grpc.Tools" Version="2.28.1"/>  
</ItemGroup>
```

Remote Procedure Call yapmak istediğimiz herhangi bir servis / sınıfımızda aşağıdaki gibi Client oluşturarak tanımladığımız method çağrısını yapabiliriz.


```csharp
#region Sadece MacOS İçin Gereklidir
	// MacOS'ta http protokolünü kullandığımız için uygulamada bunun tanımlamasını yapmamız gerekmekte.
	AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);  
#endregion
var channel = GrpcChannel.ForAddress("http://localhost:5000");  
return new IdentityValidatorService.IdentityValidatorServiceClient(channel);
```