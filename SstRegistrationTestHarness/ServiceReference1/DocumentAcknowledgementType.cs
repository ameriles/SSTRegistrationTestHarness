// Decompiled with JetBrains decompiler
// Type: WebService_Tester.ServiceReference1.DocumentAcknowledgementType
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
  [XmlRoot("DocumentAcknowledgement", IsNullable = false, Namespace = "")]
  [GeneratedCode("System.Xml", "4.6.1064.2")]
  [Serializable]
  public class DocumentAcknowledgementType : INotifyPropertyChanged
  {
    private string documentIdField;
    private string itemField;
    private ItemChoiceType1 itemElementNameField;
    private DocumentAcknowledgementTypeDocumentType documentTypeField;
    private StatusType documentStatusField;
    private PaymentIndicatorType paymentIndicatorField;
    private Errors errorsField;

    [XmlElement(Order = 0)]
    public string DocumentId
    {
      get
      {
        return this.documentIdField;
      }
      set
      {
        this.documentIdField = value;
        this.RaisePropertyChanged(nameof (DocumentId));
      }
    }

    [XmlElement("StateID", typeof (string), Order = 1)]
    [XmlChoiceIdentifier("ItemElementName")]
    [XmlElement("SSTPID", typeof (string), Order = 1)]
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

    [XmlElement(Order = 2)]
    [XmlIgnore]
    public ItemChoiceType1 ItemElementName
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

    [XmlElement(Order = 3)]
    public DocumentAcknowledgementTypeDocumentType DocumentType
    {
      get
      {
        return this.documentTypeField;
      }
      set
      {
        this.documentTypeField = value;
        this.RaisePropertyChanged(nameof (DocumentType));
      }
    }

    [XmlElement(Order = 4)]
    public StatusType DocumentStatus
    {
      get
      {
        return this.documentStatusField;
      }
      set
      {
        this.documentStatusField = value;
        this.RaisePropertyChanged(nameof (DocumentStatus));
      }
    }

    [XmlElement(Order = 5)]
    public PaymentIndicatorType PaymentIndicator
    {
      get
      {
        return this.paymentIndicatorField;
      }
      set
      {
        this.paymentIndicatorField = value;
        this.RaisePropertyChanged(nameof (PaymentIndicator));
      }
    }

    [XmlElement(Order = 6)]
    public Errors Errors
    {
      get
      {
        return this.errorsField;
      }
      set
      {
        this.errorsField = value;
        this.RaisePropertyChanged(nameof (Errors));
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
