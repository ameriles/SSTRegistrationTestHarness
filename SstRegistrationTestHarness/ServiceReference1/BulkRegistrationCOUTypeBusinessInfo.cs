// Decompiled with JetBrains decompiler
// Type: WebService_Tester.ServiceReference1.BulkRegistrationCOUTypeBusinessInfo
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
  [GeneratedCode("System.Xml", "4.6.1064.2")]
  [DesignerCategory("code")]
  [Serializable]
  public class BulkRegistrationCOUTypeBusinessInfo : INotifyPropertyChanged
  {
    private string dBANameField;
    private string nAICSCodeField;
    private AddressType physicalAddressField;
    private AddressType mailingAddressField;
    private string sellerPhoneField;
    private string sellerPhoneExtField;
    private ContactType sSTPContactField;
    private string stateIncorporatedField;

    [XmlElement(Order = 0)]
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

    [XmlElement(Order = 1)]
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

    [XmlElement(Order = 2)]
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

    [XmlElement(Order = 3)]
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

    [XmlElement(Order = 4)]
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

    [XmlElement(Order = 5)]
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

    [XmlElement(Order = 6)]
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

    [XmlElement(Order = 7)]
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
