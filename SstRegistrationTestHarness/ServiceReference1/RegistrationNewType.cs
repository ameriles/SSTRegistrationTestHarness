// Decompiled with JetBrains decompiler
// Type: WebService_Tester.ServiceReference1.RegistrationNewType
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
  [XmlRoot("StreamlinedRegistrationNew", IsNullable = false, Namespace = "")]
  [Serializable]
  public class RegistrationNewType : INotifyPropertyChanged
  {
    private RegistrationNewTypeActionCode actionCodeField;
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
    private RegistrationNewTypeRegistrationIndicator registrationIndicatorField;
    private RegistrationNewTypeRemoteSellerID remoteSellerIDField;
    private DateTime remoteEffDateField;
    private bool remoteEffDateFieldSpecified;
    private RegistrationNewTypeSSTPAllowanceIndicator sSTPAllowanceIndicatorField;
    private string newPassField;
    private DateTime firstSalesDateField;
    private DateTime registrationDateField;

    [XmlElement(Order = 0)]
    public RegistrationNewTypeActionCode ActionCode
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

    [XmlElement(Order = 12)]
    public RegistrationNewTypeRegistrationIndicator RegistrationIndicator
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

    [XmlElement(Order = 13)]
    public RegistrationNewTypeRemoteSellerID RemoteSellerID
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

    [XmlElement(DataType = "date", Order = 14)]
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

    [XmlElement(Order = 15)]
    public RegistrationNewTypeSSTPAllowanceIndicator SSTPAllowanceIndicator
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

    [XmlElement(Order = 16)]
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

    [XmlElement(DataType = "date", Order = 17)]
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

    [XmlElement(DataType = "date", Order = 18)]
    public DateTime RegistrationDate
    {
      get
      {
        return this.registrationDateField;
      }
      set
      {
        this.registrationDateField = value;
        this.RaisePropertyChanged(nameof (RegistrationDate));
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
