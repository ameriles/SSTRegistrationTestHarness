// Decompiled with JetBrains decompiler
// Type: WebService_Tester.ServiceReference1.BulkRegistrationCOUType
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
  [XmlRoot("BulkRegistrationCOU", IsNullable = false, Namespace = "")]
  [GeneratedCode("System.Xml", "4.6.1064.2")]
  [DebuggerStepThrough]
  [Serializable]
  public class BulkRegistrationCOUType : INotifyPropertyChanged
  {
    private BulkRegistrationCOUTypeActionCode actionCodeField;
    private string sSTPIDField;
    private object[] itemsField;
    private DateTime effectiveDateField;

    [XmlElement(Order = 0)]
    public BulkRegistrationCOUTypeActionCode ActionCode
    {
      get
      {
        return this.actionCodeField;
      }
      set
      {
        this.actionCodeField = value;
        this.RaisePropertyChanged(nameof (ActionCode));
      }
    }

    [XmlElement(Order = 1)]
    public string SSTPID
    {
      get
      {
        return this.sSTPIDField;
      }
      set
      {
        this.sSTPIDField = value;
        this.RaisePropertyChanged(nameof (SSTPID));
      }
    }

    [XmlElement("BusinessInfo", typeof (BulkRegistrationCOUTypeBusinessInfo), Order = 2)]
    [XmlElement("StateIndicators", typeof (BulkRegistrationCOUTypeStateIndicators), Order = 2)]
    [XmlElement("TechnologyModel", typeof (TechModelType), Order = 2)]
    public object[] Items
    {
      get
      {
        return this.itemsField;
      }
      set
      {
        this.itemsField = value;
        this.RaisePropertyChanged(nameof (Items));
      }
    }

    [XmlElement(DataType = "date", Order = 3)]
    public DateTime EffectiveDate
    {
      get
      {
        return this.effectiveDateField;
      }
      set
      {
        this.effectiveDateField = value;
        this.RaisePropertyChanged(nameof (EffectiveDate));
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
