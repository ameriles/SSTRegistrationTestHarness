// Decompiled with JetBrains decompiler
// Type: WebService_Tester.ServiceReference1.SSTRegistrationTransmissionType
// Assembly: WebService Tester, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA9F4582-6811-45A0-93FE-F9F57B337E46
// Assembly location: C:\Users\david.dango\Downloads\SST-API-master\SST-API-master\Web Service Tool (For Service Providers)\WebService Tester.exe

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace WebService_Tester.ServiceReference1
{
  [XmlRoot("SSTRegistrationTransmission", IsNullable = false, Namespace = "")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [GeneratedCode("System.Xml", "4.6.1064.2")]
  [Serializable]
  public class SSTRegistrationTransmissionType : INotifyPropertyChanged
  {
    private TransmissionHeaderType transmissionHeaderField;
    private RegistrationDocumentType[] registrationDocumentField;
    private string transmissionVersionField;

    [XmlElement(Order = 0)]
    public TransmissionHeaderType TransmissionHeader
    {
      get
      {
        return this.transmissionHeaderField;
      }
      set
      {
        this.transmissionHeaderField = value;
        this.RaisePropertyChanged(nameof (TransmissionHeader));
      }
    }

    [XmlElement("RegistrationDocument", Order = 1)]
    public RegistrationDocumentType[] RegistrationDocument
    {
      get
      {
        return this.registrationDocumentField;
      }
      set
      {
        this.registrationDocumentField = value;
        this.RaisePropertyChanged(nameof (RegistrationDocument));
      }
    }

    [XmlAttribute]
    public string transmissionVersion
    {
      get
      {
        return this.transmissionVersionField;
      }
      set
      {
        this.transmissionVersionField = value;
        this.RaisePropertyChanged(nameof (transmissionVersion));
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void RaisePropertyChanged(string propertyName)
    {
      PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
      if (propertyChanged == null)
        return;
      propertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
