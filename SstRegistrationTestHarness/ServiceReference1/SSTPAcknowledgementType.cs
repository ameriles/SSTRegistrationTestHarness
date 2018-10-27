// Decompiled with JetBrains decompiler
// Type: WebService_Tester.ServiceReference1.SSTPAcknowledgementType
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
  [DesignerCategory("code")]
  [GeneratedCode("System.Xml", "4.6.1064.2")]
  [DebuggerStepThrough]
  [Serializable]
  public class SSTPAcknowledgementType : INotifyPropertyChanged
  {
    private AcknowledgementHeaderType acknowledgementHeaderField;
    private TransmissionAcknowledgementType transmissionAcknowledgementField;
    private DocumentAcknowledgementType[] documentAcknowledgementField;
    private string acknowledgementVersionField;

    [XmlElement(Order = 0)]
    public AcknowledgementHeaderType AcknowledgementHeader
    {
      get
      {
        return this.acknowledgementHeaderField;
      }
      set
      {
        this.acknowledgementHeaderField = value;
        this.RaisePropertyChanged(nameof (AcknowledgementHeader));
      }
    }

    [XmlElement(Order = 1)]
    public TransmissionAcknowledgementType TransmissionAcknowledgement
    {
      get
      {
        return this.transmissionAcknowledgementField;
      }
      set
      {
        this.transmissionAcknowledgementField = value;
        this.RaisePropertyChanged(nameof (TransmissionAcknowledgement));
      }
    }

    [XmlElement("DocumentAcknowledgement", Order = 2)]
    public DocumentAcknowledgementType[] DocumentAcknowledgement
    {
      get
      {
        return this.documentAcknowledgementField;
      }
      set
      {
        this.documentAcknowledgementField = value;
        this.RaisePropertyChanged(nameof (DocumentAcknowledgement));
      }
    }

    [XmlAttribute]
    public string acknowledgementVersion
    {
      get
      {
        return this.acknowledgementVersionField;
      }
      set
      {
        this.acknowledgementVersionField = value;
        this.RaisePropertyChanged(nameof (acknowledgementVersion));
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
