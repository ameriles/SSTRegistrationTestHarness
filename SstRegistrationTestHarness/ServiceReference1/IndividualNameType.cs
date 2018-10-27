// Decompiled with JetBrains decompiler
// Type: WebService_Tester.ServiceReference1.IndividualNameType
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
  public class IndividualNameType : INotifyPropertyChanged
  {
    private string firstNameField;
    private string middleInitialField;
    private string lastNameField;
    private GenerationalNameSuffixType nameSuffixField;
    private bool nameSuffixFieldSpecified;

    [XmlElement(Order = 0)]
    public string FirstName
    {
      get
      {
        return this.firstNameField;
      }
      set
      {
        this.firstNameField = value;
        this.RaisePropertyChanged(nameof (FirstName));
      }
    }

    [XmlElement(Order = 1)]
    public string MiddleInitial
    {
      get
      {
        return this.middleInitialField;
      }
      set
      {
        this.middleInitialField = value;
        this.RaisePropertyChanged(nameof (MiddleInitial));
      }
    }

    [XmlElement(Order = 2)]
    public string LastName
    {
      get
      {
        return this.lastNameField;
      }
      set
      {
        this.lastNameField = value;
        this.RaisePropertyChanged(nameof (LastName));
      }
    }

    [XmlElement(Order = 3)]
    public GenerationalNameSuffixType NameSuffix
    {
      get
      {
        return this.nameSuffixField;
      }
      set
      {
        this.nameSuffixField = value;
        this.RaisePropertyChanged(nameof (NameSuffix));
      }
    }

    [XmlIgnore]
    public bool NameSuffixSpecified
    {
      get
      {
        return this.nameSuffixFieldSpecified;
      }
      set
      {
        this.nameSuffixFieldSpecified = value;
        this.RaisePropertyChanged(nameof (NameSuffixSpecified));
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
