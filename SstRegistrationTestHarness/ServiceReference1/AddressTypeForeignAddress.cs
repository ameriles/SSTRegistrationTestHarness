// Decompiled with JetBrains decompiler
// Type: WebService_Tester.ServiceReference1.AddressTypeForeignAddress
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
  [DesignerCategory("code")]
  [DebuggerStepThrough]
  [Serializable]
  public class AddressTypeForeignAddress : INotifyPropertyChanged
  {
    private string addressLine1TxtField;
    private string addressLine2TxtField;
    private string cityNmField;
    private string provinceOrStateNmField;
    private CountryType countryCdField;
    private string foreignPostalCdField;
    private string inCareOfNmField;

    [XmlElement(Order = 0)]
    public string AddressLine1Txt
    {
      get
      {
        return this.addressLine1TxtField;
      }
      set
      {
        this.addressLine1TxtField = value;
        this.RaisePropertyChanged(nameof (AddressLine1Txt));
      }
    }

    [XmlElement(Order = 1)]
    public string AddressLine2Txt
    {
      get
      {
        return this.addressLine2TxtField;
      }
      set
      {
        this.addressLine2TxtField = value;
        this.RaisePropertyChanged(nameof (AddressLine2Txt));
      }
    }

    [XmlElement(Order = 2)]
    public string CityNm
    {
      get
      {
        return this.cityNmField;
      }
      set
      {
        this.cityNmField = value;
        this.RaisePropertyChanged(nameof (CityNm));
      }
    }

    [XmlElement(Order = 3)]
    public string ProvinceOrStateNm
    {
      get
      {
        return this.provinceOrStateNmField;
      }
      set
      {
        this.provinceOrStateNmField = value;
        this.RaisePropertyChanged(nameof (ProvinceOrStateNm));
      }
    }

    [XmlElement(Order = 4)]
    public CountryType CountryCd
    {
      get
      {
        return this.countryCdField;
      }
      set
      {
        this.countryCdField = value;
        this.RaisePropertyChanged(nameof (CountryCd));
      }
    }

    [XmlElement(Order = 5)]
    public string ForeignPostalCd
    {
      get
      {
        return this.foreignPostalCdField;
      }
      set
      {
        this.foreignPostalCdField = value;
        this.RaisePropertyChanged(nameof (ForeignPostalCd));
      }
    }

    [XmlElement(Order = 6)]
    public string InCareOfNm
    {
      get
      {
        return this.inCareOfNmField;
      }
      set
      {
        this.inCareOfNmField = value;
        this.RaisePropertyChanged(nameof (InCareOfNm));
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
