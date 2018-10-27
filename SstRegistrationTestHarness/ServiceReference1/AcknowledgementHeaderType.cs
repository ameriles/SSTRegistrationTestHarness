// Decompiled with JetBrains decompiler
// Type: WebService_Tester.ServiceReference1.AcknowledgementHeaderType
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
  [XmlRoot("AcknowledgementHeader", IsNullable = false, Namespace = "")]
  [DebuggerStepThrough]
  [Serializable]
  public class AcknowledgementHeaderType : INotifyPropertyChanged
  {
    private DateTime acknowledgementTimestampField;
    private string acknowledgementCountField;

    [XmlElement(Order = 0)]
    public DateTime AcknowledgementTimestamp
    {
      get
      {
        return this.acknowledgementTimestampField;
      }
      set
      {
        this.acknowledgementTimestampField = value;
        this.RaisePropertyChanged(nameof (AcknowledgementTimestamp));
      }
    }

    [XmlElement(DataType = "positiveInteger", Order = 1)]
    public string AcknowledgementCount
    {
      get
      {
        return this.acknowledgementCountField;
      }
      set
      {
        this.acknowledgementCountField = value;
        this.RaisePropertyChanged(nameof (AcknowledgementCount));
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
