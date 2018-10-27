// Decompiled with JetBrains decompiler
// Type: WebService_Tester.ServiceReference1.BulkRegistrationNewTypeStateRegistrationIndicator
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
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [GeneratedCode("System.Xml", "4.6.1064.2")]
  [Serializable]
  public class BulkRegistrationNewTypeStateRegistrationIndicator : INotifyPropertyChanged
  {
    private string stateField;
    private BulkRegistrationNewTypeStateRegistrationIndicatorRegistrationIndicator registrationIndicatorField;
    private DateTime firstSalesDateField;
    private BulkRegistrationNewTypeStateRegistrationIndicatorRemoteSellerID remoteSellerIDField;
    private DateTime remoteEffDateField;
    private bool remoteEffDateFieldSpecified;
    private BulkRegistrationNewTypeStateRegistrationIndicatorSSTPAllowanceIndicator sSTPAllowanceIndicatorField;
    private bool sSTPAllowanceIndicatorFieldSpecified;

    [XmlElement(Order = 0)]
    public string State
    {
      get
      {
        return this.stateField;
      }
      set
      {
        this.stateField = value;
        this.RaisePropertyChanged(nameof (State));
      }
    }

    [XmlElement(Order = 1)]
    public BulkRegistrationNewTypeStateRegistrationIndicatorRegistrationIndicator RegistrationIndicator
    {
      get
      {
        return this.registrationIndicatorField;
      }
      set
      {
        this.registrationIndicatorField = value;
        this.RaisePropertyChanged(nameof (RegistrationIndicator));
      }
    }

    [XmlElement(DataType = "date", Order = 2)]
    public DateTime FirstSalesDate
    {
      get
      {
        return this.firstSalesDateField;
      }
      set
      {
        this.firstSalesDateField = value;
        this.RaisePropertyChanged(nameof (FirstSalesDate));
      }
    }

    [XmlElement(Order = 3)]
    public BulkRegistrationNewTypeStateRegistrationIndicatorRemoteSellerID RemoteSellerID
    {
      get
      {
        return this.remoteSellerIDField;
      }
      set
      {
        this.remoteSellerIDField = value;
        this.RaisePropertyChanged(nameof (RemoteSellerID));
      }
    }

    [XmlElement(DataType = "date", Order = 4)]
    public DateTime RemoteEffDate
    {
      get
      {
        return this.remoteEffDateField;
      }
      set
      {
        this.remoteEffDateField = value;
        this.RaisePropertyChanged(nameof (RemoteEffDate));
      }
    }

    [XmlIgnore]
    public bool RemoteEffDateSpecified
    {
      get
      {
        return this.remoteEffDateFieldSpecified;
      }
      set
      {
        this.remoteEffDateFieldSpecified = value;
        this.RaisePropertyChanged(nameof (RemoteEffDateSpecified));
      }
    }

    [XmlElement(Order = 5)]
    public BulkRegistrationNewTypeStateRegistrationIndicatorSSTPAllowanceIndicator SSTPAllowanceIndicator
    {
      get
      {
        return this.sSTPAllowanceIndicatorField;
      }
      set
      {
        this.sSTPAllowanceIndicatorField = value;
        this.RaisePropertyChanged(nameof (SSTPAllowanceIndicator));
      }
    }

    [XmlIgnore]
    public bool SSTPAllowanceIndicatorSpecified
    {
      get
      {
        return this.sSTPAllowanceIndicatorFieldSpecified;
      }
      set
      {
        this.sSTPAllowanceIndicatorFieldSpecified = value;
        this.RaisePropertyChanged(nameof (SSTPAllowanceIndicatorSpecified));
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
