// Decompiled with JetBrains decompiler
// Type: WebService_Tester.ServiceReference1.BulkRegistrationDocumentType
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
  [XmlRoot("BulkRegistrationDocument", IsNullable = false, Namespace = "")]
  [GeneratedCode("System.Xml", "4.6.1064.2")]
  [DesignerCategory("code")]
  [Serializable]
  public class BulkRegistrationDocumentType : INotifyPropertyChanged
  {
    private string documentIdField;
    private BulkRegistrationDocumentTypeDocumentType documentTypeField;
    private BulkRegistrationHeaderType bulkRegistrationHeaderField;
    private object itemField;

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

    [XmlElement(Order = 1)]
    public BulkRegistrationDocumentTypeDocumentType DocumentType
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

    [XmlElement(Order = 2)]
    public BulkRegistrationHeaderType BulkRegistrationHeader
    {
      get
      {
        return this.bulkRegistrationHeaderField;
      }
      set
      {
        this.bulkRegistrationHeaderField = value;
        this.RaisePropertyChanged(nameof (BulkRegistrationHeader));
      }
    }

    [XmlElement("BulkRegistrationCOU", typeof (BulkRegistrationCOUType), Order = 3)]
    [XmlElement("BulkRegistrationNew", typeof (BulkRegistrationNewType), Order = 3)]
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
