// Decompiled with JetBrains decompiler
// Type: WebService_Tester.ServiceReference1.TransmissionAcknowledgementType
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
  [GeneratedCode("System.Xml", "4.6.1064.2")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [XmlRoot("TransmissionAcknowledgement", IsNullable = false, Namespace = "")]
  [Serializable]
  public class TransmissionAcknowledgementType : INotifyPropertyChanged
  {
    private string transmissionIdField;
    private DateTime transmissionTimestampField;
    private bool transmissionTimestampFieldSpecified;
    private StatusType transmissionStatusField;
    private Errors errorsField;

    [XmlElement(Order = 0)]
    public string TransmissionId
    {
      get
      {
        return this.transmissionIdField;
      }
      set
      {
        this.transmissionIdField = value;
        this.RaisePropertyChanged(nameof (TransmissionId));
      }
    }

    [XmlElement(Order = 1)]
    public DateTime TransmissionTimestamp
    {
      get
      {
        return this.transmissionTimestampField;
      }
      set
      {
        this.transmissionTimestampField = value;
        this.RaisePropertyChanged(nameof (TransmissionTimestamp));
      }
    }

    [XmlIgnore]
    public bool TransmissionTimestampSpecified
    {
      get
      {
        return this.transmissionTimestampFieldSpecified;
      }
      set
      {
        this.transmissionTimestampFieldSpecified = value;
        this.RaisePropertyChanged(nameof (TransmissionTimestampSpecified));
      }
    }

    [XmlElement(Order = 2)]
    public StatusType TransmissionStatus
    {
      get
      {
        return this.transmissionStatusField;
      }
      set
      {
        this.transmissionStatusField = value;
        this.RaisePropertyChanged(nameof (TransmissionStatus));
      }
    }

    [XmlElement(Order = 3)]
    public Errors Errors
    {
      get
      {
        return this.errorsField;
      }
      set
      {
        this.errorsField = value;
        this.RaisePropertyChanged(nameof (Errors));
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
