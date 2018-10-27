// Decompiled with JetBrains decompiler
// Type: WebService_Tester.ServiceReference1.BulkRegistrationCOUTypeStateIndicators
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
  public class BulkRegistrationCOUTypeStateIndicators : INotifyPropertyChanged
  {
    private string stateField;
    private BulkRegistrationCOUTypeStateIndicatorsRegistrationIndicator registrationIndicatorField;
    private bool registrationIndicatorFieldSpecified;
    private DateTime lastSaleDateField;
    private bool lastSaleDateFieldSpecified;
    private BulkRegistrationCOUTypeStateIndicatorsStateAcctInd stateAcctIndField;
    private bool stateAcctIndFieldSpecified;
    private BulkRegistrationCOUTypeStateIndicatorsRemoteSellerID remoteSellerIDField;
    private bool remoteSellerIDFieldSpecified;
    private DateTime remoteEffDateField;
    private bool remoteEffDateFieldSpecified;
    private DateTime remoteEndDateField;
    private bool remoteEndDateFieldSpecified;
    private DateTime cSPEndDateField;
    private bool cSPEndDateFieldSpecified;
    private string cSPLastFilingPdField;
    private DateTime acctCloseDateField;
    private bool acctCloseDateFieldSpecified;
    private DateTime firstSalesDateField;
    private bool firstSalesDateFieldSpecified;
    private BulkRegistrationCOUTypeStateIndicatorsSSTPAllowanceIndicator sSTPAllowanceIndicatorField;
    private bool sSTPAllowanceIndicatorFieldSpecified;
    private DateTime firstFilingPeriodField;
    private bool firstFilingPeriodFieldSpecified;

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
    public BulkRegistrationCOUTypeStateIndicatorsRegistrationIndicator RegistrationIndicator
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

    [XmlIgnore]
    public bool RegistrationIndicatorSpecified
    {
      get
      {
        return this.registrationIndicatorFieldSpecified;
      }
      set
      {
        this.registrationIndicatorFieldSpecified = value;
        this.RaisePropertyChanged(nameof (RegistrationIndicatorSpecified));
      }
    }

    [XmlElement(DataType = "date", Order = 2)]
    public DateTime LastSaleDate
    {
      get
      {
        return this.lastSaleDateField;
      }
      set
      {
        this.lastSaleDateField = value;
        this.RaisePropertyChanged(nameof (LastSaleDate));
      }
    }

    [XmlIgnore]
    public bool LastSaleDateSpecified
    {
      get
      {
        return this.lastSaleDateFieldSpecified;
      }
      set
      {
        this.lastSaleDateFieldSpecified = value;
        this.RaisePropertyChanged(nameof (LastSaleDateSpecified));
      }
    }

    [XmlElement(Order = 3)]
    public BulkRegistrationCOUTypeStateIndicatorsStateAcctInd StateAcctInd
    {
      get
      {
        return this.stateAcctIndField;
      }
      set
      {
        this.stateAcctIndField = value;
        this.RaisePropertyChanged(nameof (StateAcctInd));
      }
    }

    [XmlIgnore]
    public bool StateAcctIndSpecified
    {
      get
      {
        return this.stateAcctIndFieldSpecified;
      }
      set
      {
        this.stateAcctIndFieldSpecified = value;
        this.RaisePropertyChanged(nameof (StateAcctIndSpecified));
      }
    }

    [XmlElement(Order = 4)]
    public BulkRegistrationCOUTypeStateIndicatorsRemoteSellerID RemoteSellerID
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

    [XmlIgnore]
    public bool RemoteSellerIDSpecified
    {
      get
      {
        return this.remoteSellerIDFieldSpecified;
      }
      set
      {
        this.remoteSellerIDFieldSpecified = value;
        this.RaisePropertyChanged(nameof (RemoteSellerIDSpecified));
      }
    }

    [XmlElement(DataType = "date", Order = 5)]
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

    [XmlElement(DataType = "date", Order = 6)]
    public DateTime RemoteEndDate
    {
      get
      {
        return this.remoteEndDateField;
      }
      set
      {
        this.remoteEndDateField = value;
        this.RaisePropertyChanged(nameof (RemoteEndDate));
      }
    }

    [XmlIgnore]
    public bool RemoteEndDateSpecified
    {
      get
      {
        return this.remoteEndDateFieldSpecified;
      }
      set
      {
        this.remoteEndDateFieldSpecified = value;
        this.RaisePropertyChanged(nameof (RemoteEndDateSpecified));
      }
    }

    [XmlElement(DataType = "date", Order = 7)]
    public DateTime CSPEndDate
    {
      get
      {
        return this.cSPEndDateField;
      }
      set
      {
        this.cSPEndDateField = value;
        this.RaisePropertyChanged(nameof (CSPEndDate));
      }
    }

    [XmlIgnore]
    public bool CSPEndDateSpecified
    {
      get
      {
        return this.cSPEndDateFieldSpecified;
      }
      set
      {
        this.cSPEndDateFieldSpecified = value;
        this.RaisePropertyChanged(nameof (CSPEndDateSpecified));
      }
    }

    [XmlElement(DataType = "gYearMonth", Order = 8)]
    public string CSPLastFilingPd
    {
      get
      {
        return this.cSPLastFilingPdField;
      }
      set
      {
        this.cSPLastFilingPdField = value;
        this.RaisePropertyChanged(nameof (CSPLastFilingPd));
      }
    }

    [XmlElement(DataType = "date", Order = 9)]
    public DateTime AcctCloseDate
    {
      get
      {
        return this.acctCloseDateField;
      }
      set
      {
        this.acctCloseDateField = value;
        this.RaisePropertyChanged(nameof (AcctCloseDate));
      }
    }

    [XmlIgnore]
    public bool AcctCloseDateSpecified
    {
      get
      {
        return this.acctCloseDateFieldSpecified;
      }
      set
      {
        this.acctCloseDateFieldSpecified = value;
        this.RaisePropertyChanged(nameof (AcctCloseDateSpecified));
      }
    }

    [XmlElement(DataType = "date", Order = 10)]
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

    [XmlIgnore]
    public bool FirstSalesDateSpecified
    {
      get
      {
        return this.firstSalesDateFieldSpecified;
      }
      set
      {
        this.firstSalesDateFieldSpecified = value;
        this.RaisePropertyChanged(nameof (FirstSalesDateSpecified));
      }
    }

    [XmlElement(Order = 11)]
    public BulkRegistrationCOUTypeStateIndicatorsSSTPAllowanceIndicator SSTPAllowanceIndicator
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

    [XmlElement(DataType = "date", Order = 12)]
    public DateTime FirstFilingPeriod
    {
      get
      {
        return this.firstFilingPeriodField;
      }
      set
      {
        this.firstFilingPeriodField = value;
        this.RaisePropertyChanged(nameof (FirstFilingPeriod));
      }
    }

    [XmlIgnore]
    public bool FirstFilingPeriodSpecified
    {
      get
      {
        return this.firstFilingPeriodFieldSpecified;
      }
      set
      {
        this.firstFilingPeriodFieldSpecified = value;
        this.RaisePropertyChanged(nameof (FirstFilingPeriodSpecified));
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
