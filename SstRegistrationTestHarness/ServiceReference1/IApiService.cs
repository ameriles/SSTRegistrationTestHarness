// Decompiled with JetBrains decompiler
// Type: WebService_Tester.ServiceReference1.IApiService
// Assembly: WebService Tester, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA9F4582-6811-45A0-93FE-F9F57B337E46
// Assembly location: C:\Users\david.dango\Downloads\SST-API-master\SST-API-master\Web Service Tool (For Service Providers)\WebService Tester.exe

using System.CodeDom.Compiler;
using System.ServiceModel;
using System.Threading.Tasks;

namespace WebService_Tester.ServiceReference1
{
  [GeneratedCode("System.ServiceModel", "4.0.0.0")]
  [ServiceContract(ConfigurationName = "ServiceReference1.IApiService", Namespace = "http://SST")]
  public interface IApiService
  {
    [OperationContract(Action = "http://SST/IApiService/BulkRegistration", ReplyAction = "http://SST/IApiService/BulkRegistrationResponse")]
    [XmlSerializerFormat(SupportFaults = true)]
    BulkRegistrationAcknowledgementType BulkRegistration(BulkRegistrationTransmissionType bulkRegistrationTransmission);

    [OperationContract(Action = "http://SST/IApiService/BulkRegistration", ReplyAction = "http://SST/IApiService/BulkRegistrationResponse")]
    Task<BulkRegistrationAcknowledgementType> BulkRegistrationAsync(BulkRegistrationTransmissionType bulkRegistrationTransmission);

    [OperationContract(Action = "http://SST/IApiService/GetDocuments", ReplyAction = "http://SST/IApiService/GetDocumentsResponse")]
    [XmlSerializerFormat(SupportFaults = true)]
    SSTRegistrationTransmissionType GetDocuments(string AcknowledgementStatus);

    [OperationContract(Action = "http://SST/IApiService/GetDocuments", ReplyAction = "http://SST/IApiService/GetDocumentsResponse")]
    Task<SSTRegistrationTransmissionType> GetDocumentsAsync(string AcknowledgementStatus);

    [OperationContract(Action = "http://SST/IApiService/GetTransmission", ReplyAction = "http://SST/IApiService/GetTransmissionResponse")]
    [XmlSerializerFormat(SupportFaults = true)]
    SSTRegistrationTransmissionType GetTransmission(string TransmissionId);

    [OperationContract(Action = "http://SST/IApiService/GetTransmission", ReplyAction = "http://SST/IApiService/GetTransmissionResponse")]
    Task<SSTRegistrationTransmissionType> GetTransmissionAsync(string TransmissionId);

    [XmlSerializerFormat(SupportFaults = true)]
    [OperationContract(Action = "http://SST/IApiService/AcknowledgeTransmission", ReplyAction = "http://SST/IApiService/AcknowledgeTransmissionResponse")]
    SSTReceiptType AcknowledgeTransmission(SSTPAcknowledgementType acknowledgement);

    [OperationContract(Action = "http://SST/IApiService/AcknowledgeTransmission", ReplyAction = "http://SST/IApiService/AcknowledgeTransmissionResponse")]
    Task<SSTReceiptType> AcknowledgeTransmissionAsync(SSTPAcknowledgementType acknowledgement);
  }
}
