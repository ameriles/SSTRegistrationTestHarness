// Decompiled with JetBrains decompiler
// Type: WebService_Tester.ServiceReference1.BulkRegistrationHeaderType
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
  [XmlRoot("BulkRegistrationHeader", IsNullable = false, Namespace = "")]
  [DesignerCategory("code")]
  [DebuggerStepThrough]
  [Serializable]
  public class BulkRegistrationHeaderType : INotifyPropertyChanged
  {
    private BulkRegistrationHeaderTypeElectronicPostmark electronicPostmarkField;
    private DateTime dateReceivedField;
    private bool dateReceivedFieldSpecified;
    private BulkRegistrationHeaderTypeFilingType filingTypeField;
    private TINType tINField;

    [XmlElement(Order = 0)]
    public BulkRegistrationHeaderTypeElectronicPostmark ElectronicPostmark
    {
      get
      {
        return this.electronicPostmarkField;
      }
      set
      {
        this.electronicPostmarkField = value;
        this.RaisePropertyChanged(nameof (ElectronicPostmark));
      }
    }

    [XmlElement(DataType = "date", Order = 1)]
    public DateTime DateReceived
    {
      get
      {
        return this.dateReceivedField;
      }
      set
      {
        this.dateReceivedField = value;
        this.RaisePropertyChanged(nameof (DateReceived));
      }
    }

    [XmlIgnore]
    public bool DateReceivedSpecified
    {
      get
      {
        return this.dateReceivedFieldSpecified;
      }
      set
      {
        this.dateReceivedFieldSpecified = value;
        this.RaisePropertyChanged(nameof (DateReceivedSpecified));
      }
    }

    [XmlElement(Order = 2)]
    public BulkRegistrationHeaderTypeFilingType FilingType
    {
      get
      {
        return this.filingTypeField;
      }
      set
      {
        this.filingTypeField = value;
        this.RaisePropertyChanged(nameof (FilingType));
      }
    }

    [XmlElement(Order = 3)]
    public TINType TIN
    {
      get
      {
        return this.tINField;
      }
      set
      {
        this.tINField = value;
        this.RaisePropertyChanged(nameof (TIN));
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
