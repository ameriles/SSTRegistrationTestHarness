// Decompiled with JetBrains decompiler
// Type: WebService_Tester.ServiceReference1.BulkRegistrationNewType
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
  [XmlRoot("BulkRegistrationNew", IsNullable = false, Namespace = "")]
  [GeneratedCode("System.Xml", "4.6.1064.2")]
  [Serializable]
  public class BulkRegistrationNewType : INotifyPropertyChanged
  {
    private BulkRegistrationNewTypeActionCode actionCodeField;
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
    private BulkRegistrationNewTypeStateRegistrationIndicator[] stateRegistrationIndicatorField;
    private DateTime effectiveDateField;
    private string firstFilingPeriodField;
    private string newPassField;

    [XmlElement(Order = 0)]
    public BulkRegistrationNewTypeActionCode ActionCode
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

    [XmlElement("IndividualName", typeof (IndividualNameType), Order = 2)]
    [XmlElement("BusinessName", typeof (string), Order = 2)]
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

    [XmlElement("StateRegistrationIndicator", Order = 12)]
    public BulkRegistrationNewTypeStateRegistrationIndicator[] StateRegistrationIndicator
    {
      get
      {
        return this.stateRegistrationIndicatorField;
      }
      set
      {
        this.stateRegistrationIndicatorField = value;
        this.RaisePropertyChanged(nameof (StateRegistrationIndicator));
      }
    }

    [XmlElement(DataType = "date", Order = 13)]
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

    [XmlElement(DataType = "gYearMonth", Order = 14)]
    public string FirstFilingPeriod
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

    [XmlElement(Order = 15)]
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
