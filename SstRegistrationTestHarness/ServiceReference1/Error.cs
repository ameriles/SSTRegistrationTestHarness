// Decompiled with JetBrains decompiler
// Type: WebService_Tester.ServiceReference1.Error
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
  public class Error : INotifyPropertyChanged
  {
    private string itemField;
    private ItemChoiceType itemElementNameField;
    private string errorMessageField;
    private string additionalErrorMessageField;
    private string severityField;
    private string dataValueField;
    private string errorIdField;

    [XmlChoiceIdentifier("ItemElementName")]
    [XmlElement("FieldIdentifier", typeof (string), Order = 0)]
    [XmlElement("XPath", typeof (string), Order = 0)]
    public string Item
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

    [XmlElement(Order = 1)]
    [XmlIgnore]
    public ItemChoiceType ItemElementName
    {
      get
      {
        return this.itemElementNameField;
      }
      set
      {
        this.itemElementNameField = value;
        this.RaisePropertyChanged(nameof (ItemElementName));
      }
    }

    [XmlElement(Order = 2)]
    public string ErrorMessage
    {
      get
      {
        return this.errorMessageField;
      }
      set
      {
        this.errorMessageField = value;
        this.RaisePropertyChanged(nameof (ErrorMessage));
      }
    }

    [XmlElement(Order = 3)]
    public string AdditionalErrorMessage
    {
      get
      {
        return this.additionalErrorMessageField;
      }
      set
      {
        this.additionalErrorMessageField = value;
        this.RaisePropertyChanged(nameof (AdditionalErrorMessage));
      }
    }

    [XmlElement(Order = 4)]
    public string Severity
    {
      get
      {
        return this.severityField;
      }
      set
      {
        this.severityField = value;
        this.RaisePropertyChanged(nameof (Severity));
      }
    }

    [XmlElement(Order = 5)]
    public string DataValue
    {
      get
      {
        return this.dataValueField;
      }
      set
      {
        this.dataValueField = value;
        this.RaisePropertyChanged(nameof (DataValue));
      }
    }

    [XmlAttribute(DataType = "positiveInteger")]
    public string errorId
    {
      get
      {
        return this.errorIdField;
      }
      set
      {
        this.errorIdField = value;
        this.RaisePropertyChanged(nameof (errorId));
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
