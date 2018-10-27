// Decompiled with JetBrains decompiler
// Type: WebService_Tester.ServiceReference1.RegistrationCOUType
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
  [XmlRoot("StreamlinedRegistrationCOU", IsNullable = false, Namespace = "")]
  [GeneratedCode("System.Xml", "4.6.1064.2")]
  [Serializable]
  public class RegistrationCOUType : INotifyPropertyChanged
  {
    private RegistrationCOUTypeActionCode actionCodeField;
    private EntityType registrationEntityField;
    private object itemField;
    private string dBANameField;
    private string nAICSCodeField;
    private AddressType physicalAddressField;
    private AddressType mailingAddressField;
    private string sellerPhoneField;
    private string sellerPhoneExtField;
    private ContactType sSTPContactField;
    private string stateIncorporatedField;
    private TechModelType technologyModelField;
    private RegistrationCOUTypeRegistrationIndicator registrationIndicatorField;
    private bool registrationIndicatorFieldSpecified;
    private DateTime lastSaleDateField;
    private bool lastSaleDateFieldSpecified;
    private RegistrationCOUTypeStateAcctInd stateAcctIndField;
    private bool stateAcctIndFieldSpecified;
    private RegistrationCOUTypeRemoteSellerID remoteSellerIDField;
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
    private RegistrationCOUTypeSSTPAllowanceIndicator sSTPAllowanceIndicatorField;
    private bool sSTPAllowanceIndicatorFieldSpecified;
    private string newPassField;
    private DateTime effectiveDateField;

    [XmlElement(Order = 0)]
    public RegistrationCOUTypeActionCode ActionCode
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
    public EntityType RegistrationEntity
    {
      get
      {
        return this.registrationEntityField;
      }
      set
      {
        this.registrationEntityField = value;
        this.RaisePropertyChanged(nameof (RegistrationEntity));
      }
    }

    [XmlElement("BusinessName", typeof (string), Order = 2)]
    [XmlElement("IndividualName", typeof (IndividualNameType), Order = 2)]
    public object Item
    {
      get
      {
        return this.itemField;
      }
      set
      {
        this.itemField = value;
        this.RaisePropertyChanged(nameof (Item));
      }
    }

    [XmlElement(Order = 3)]
    public string DBAName
    {
      get
      {
        return this.dBANameField;
      }
      set
      {
        this.dBANameField = value;
        this.RaisePropertyChanged(nameof (DBAName));
      }
    }

    [XmlElement(Order = 4)]
    public string NAICSCode
    {
      get
      {
        return this.nAICSCodeField;
      }
      set
      {
        this.nAICSCodeField = value;
        this.RaisePropertyChanged(nameof (NAICSCode));
      }
    }

    [XmlElement(Order = 5)]
    public AddressType PhysicalAddress
    {
      get
      {
        return this.physicalAddressField;
      }
      set
      {
        this.physicalAddressField = value;
        this.RaisePropertyChanged(nameof (PhysicalAddress));
      }
    }

    [XmlElement(Order = 6)]
    public AddressType MailingAddress
    {
      get
      {
        return this.mailingAddressField;
      }
      set
      {
        this.mailingAddressField = value;
        this.RaisePropertyChanged(nameof (MailingAddress));
      }
    }

    [XmlElement(Order = 7)]
    public string SellerPhone
    {
      get
      {
        return this.sellerPhoneField;
      }
      set
      {
        this.sellerPhoneField = value;
        this.RaisePropertyChanged(nameof (SellerPhone));
      }
    }

    [XmlElement(Order = 8)]
    public string SellerPhoneExt
    {
      get
      {
        return this.sellerPhoneExtField;
      }
      set
      {
        this.sellerPhoneExtField = value;
        this.RaisePropertyChanged(nameof (SellerPhoneExt));
      }
    }

    [XmlElement(Order = 9)]
    public ContactType SSTPContact
    {
      get
      {
        return this.sSTPContactField;
      }
      set
      {
        this.sSTPContactField = value;
        this.RaisePropertyChanged(nameof (SSTPContact));
      }
    }

    [XmlElement(Order = 10)]
    public string StateIncorporated
    {
      get
      {
        return this.stateIncorporatedField;
      }
      set
      {
        this.stateIncorporatedField = value;
        this.RaisePropertyChanged(nameof (StateIncorporated));
      }
    }

    [XmlElement(Order = 11)]
    public TechModelType TechnologyModel
    {
      get
      {
        return this.technologyModelField;
      }
      set
      {
        this.technologyModelField = value;
        this.RaisePropertyChanged(nameof (TechnologyModel));
      }
    }

    [XmlElement(Order = 12)]
    public RegistrationCOUTypeRegistrationIndicator RegistrationIndicator
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

    [XmlElement(DataType = "date", Order = 13)]
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

    [XmlElement(Order = 14)]
    public RegistrationCOUTypeStateAcctInd StateAcctInd
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

    [XmlElement(Order = 15)]
    public RegistrationCOUTypeRemoteSellerID RemoteSellerID
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

    [XmlElement(DataType = "date", Order = 16)]
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

    [XmlElement(DataType = "date", Order = 17)]
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

    [XmlElement(DataType = "date", Order = 18)]
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

    [XmlElement(DataType = "gYearMonth", Order = 19)]
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

    [XmlElement(DataType = "date", Order = 20)]
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

    [XmlElement(DataType = "date", Order = 21)]
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

    [XmlElement(Order = 22)]
    public RegistrationCOUTypeSSTPAllowanceIndicator SSTPAllowanceIndicator
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

    [XmlElement(Order = 23)]
    public string NewPass
    {
      get
      {
        return this.newPassField;
      }
      set
      {
        this.newPassField = value;
        this.RaisePropertyChanged(nameof (NewPass));
      }
    }

    [XmlElement(DataType = "date", Order = 24)]
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
