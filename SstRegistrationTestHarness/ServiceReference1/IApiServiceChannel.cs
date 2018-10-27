// Decompiled with JetBrains decompiler
// Type: WebService_Tester.ServiceReference1.IApiServiceChannel
// Assembly: WebService Tester, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA9F4582-6811-45A0-93FE-F9F57B337E46
// Assembly location: C:\Users\david.dango\Downloads\SST-API-master\SST-API-master\Web Service Tool (For Service Providers)\WebService Tester.exe

using System;
using System.CodeDom.Compiler;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace WebService_Tester.ServiceReference1
{
  [GeneratedCode("System.ServiceModel", "4.0.0.0")]
  public interface IApiServiceChannel : IApiService, IClientChannel, IContextChannel, IChannel, ICommunicationObject, IExtensibleObject<IContextChannel>, IDisposable
  {
  }
}
