// Decompiled with JetBrains decompiler
// Type: WebService_Tester.ServiceReference1.ContactType
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
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [Serializable]
  public class ContactType : INotifyPropertyChanged
  {
    private IndividualNameType contactNameField;
    private string contactPhoneField;
    private string contactPhoneExtField;
    private string contactEmailField;

    [XmlElement(Order = 0)]
    public IndividualNameType ContactName
    {
      get
      {
        return this.contactNameField;
      }
      set
      {
        this.contactNameField = value;
        this.RaisePropertyChanged(nameof (ContactName));
      }
    }

    [XmlElement(Order = 1)]
    public string ContactPhone
    {
      get
      {
        return this.contactPhoneField;
      }
      set
      {
        this.contactPhoneField = value;
        this.RaisePropertyChanged(nameof (ContactPhone));
      }
    }

    [XmlElement(Order = 2)]
    public string ContactPhoneExt
    {
      get
      {
        return this.contactPhoneExtField;
      }
      set
      {
        this.contactPhoneExtField = value;
        this.RaisePropertyChanged(nameof (ContactPhoneExt));
      }
    }

    [XmlElement(Order = 3)]
    public string ContactEmail
    {
      get
      {
        return this.contactEmailField;
      }
      set
      {
        this.contactEmailField = value;
        this.RaisePropertyChanged(nameof (ContactEmail));
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
