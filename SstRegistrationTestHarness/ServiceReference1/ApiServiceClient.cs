// Decompiled with JetBrains decompiler
// Type: WebService_Tester.ServiceReference1.ApiServiceClient
// Assembly: WebService Tester, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA9F4582-6811-45A0-93FE-F9F57B337E46
// Assembly location: C:\Users\david.dango\Downloads\SST-API-master\SST-API-master\Web Service Tool (For Service Providers)\WebService Tester.exe

using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace WebService_Tester.ServiceReference1
{
  [DebuggerStepThrough]
  [GeneratedCode("System.ServiceModel", "4.0.0.0")]
  public class ApiServiceClient : ClientBase<IApiService>, IApiService
  {
    public ApiServiceClient()
    {
    }

    public ApiServiceClient(string endpointConfigurationName)
      : base(endpointConfigurationName)
    {
    }

    public ApiServiceClient(string endpointConfigurationName, string remoteAddress)
      : base(endpointConfigurationName, remoteAddress)
    {
    }

    public ApiServiceClient(string endpointConfigurationName, EndpointAddress remoteAddress)
      : base(endpointConfigurationName, remoteAddress)
    {
    }

    public ApiServiceClient(Binding binding, EndpointAddress remoteAddress)
      : base(binding, remoteAddress)
    {
    }

    public BulkRegistrationAcknowledgementType BulkRegistration(BulkRegistrationTransmissionType bulkRegistrationTransmission)
    {
      return this.Channel.BulkRegistration(bulkRegistrationTransmission);
    }

    public Task<BulkRegistrationAcknowledgementType> BulkRegistrationAsync(BulkRegistrationTransmissionType bulkRegistrationTransmission)
    {
      return this.Channel.BulkRegistrationAsync(bulkRegistrationTransmission);
    }

    public SSTRegistrationTransmissionType GetDocuments(string AcknowledgementStatus)
    {
      return this.Channel.GetDocuments(AcknowledgementStatus);
    }

    public Task<SSTRegistrationTransmissionType> GetDocumentsAsync(string AcknowledgementStatus)
    {
      return this.Channel.GetDocumentsAsync(AcknowledgementStatus);
    }

    public SSTRegistrationTransmissionType GetTransmission(string TransmissionId)
    {
      return this.Channel.GetTransmission(TransmissionId);
    }

    public Task<SSTRegistrationTransmissionType> GetTransmissionAsync(string TransmissionId)
    {
      return this.Channel.GetTransmissionAsync(TransmissionId);
    }

    public SSTReceiptType AcknowledgeTransmission(SSTPAcknowledgementType acknowledgement)
    {
      return this.Channel.AcknowledgeTransmission(acknowledgement);
    }

    public Task<SSTReceiptType> AcknowledgeTransmissionAsync(SSTPAcknowledgementType acknowledgement)
    {
      return this.Channel.AcknowledgeTransmissionAsync(acknowledgement);
    }
  }
}
