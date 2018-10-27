// Decompiled with JetBrains decompiler
// Type: WebService_Tester.ServiceReference1.TransmissionHeaderType
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
  [XmlRoot("TransmissionHeader", IsNullable = false, Namespace = "")]
  [DebuggerStepThrough]
  [GeneratedCode("System.Xml", "4.6.1064.2")]
  [Serializable]
  public class TransmissionHeaderType : INotifyPropertyChanged
  {
    private string transmissionIdField;
    private DateTime timestampField;
    private bool timestampFieldSpecified;
    private TransmissionHeaderTypeTransmitter transmitterField;
    private TransmissionHeaderTypeProcessType processTypeField;
    private string documentCountField;
    private AmountType transmissionPaymentHashField;

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
    public DateTime Timestamp
    {
      get
      {
        return this.timestampField;
      }
      set
      {
        this.timestampField = value;
        this.RaisePropertyChanged(nameof (Timestamp));
      }
    }

    [XmlIgnore]
    public bool TimestampSpecified
    {
      get
      {
        return this.timestampFieldSpecified;
      }
      set
      {
        this.timestampFieldSpecified = value;
        this.RaisePropertyChanged(nameof (TimestampSpecified));
      }
    }

    [XmlElement(Order = 2)]
    public TransmissionHeaderTypeTransmitter Transmitter
    {
      get
      {
        return this.transmitterField;
      }
      set
      {
        this.transmitterField = value;
        this.RaisePropertyChanged(nameof (Transmitter));
      }
    }

    [XmlElement(Order = 3)]
    public TransmissionHeaderTypeProcessType ProcessType
    {
      get
      {
        return this.processTypeField;
      }
      set
      {
        this.processTypeField = value;
        this.RaisePropertyChanged(nameof (ProcessType));
      }
    }

    [XmlElement(DataType = "positiveInteger", Order = 4)]
    public string DocumentCount
    {
      get
      {
        return this.documentCountField;
      }
      set
      {
        this.documentCountField = value;
        this.RaisePropertyChanged(nameof (DocumentCount));
      }
    }

    [XmlElement(Order = 5)]
    public AmountType TransmissionPaymentHash
    {
      get
      {
        return this.transmissionPaymentHashField;
      }
      set
      {
        this.transmissionPaymentHashField = value;
        this.RaisePropertyChanged(nameof (TransmissionPaymentHash));
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
