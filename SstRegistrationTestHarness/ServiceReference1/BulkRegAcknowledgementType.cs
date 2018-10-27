// Decompiled with JetBrains decompiler
// Type: WebService_Tester.ServiceReference1.BulkRegAcknowledgementType
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
  [XmlRoot("BulkRegAcknowledgement", IsNullable = false, Namespace = "")]
  [DebuggerStepThrough]
  [Serializable]
  public class BulkRegAcknowledgementType : INotifyPropertyChanged
  {
    private string documentIdField;
    private string sSTPIDField;
    private string newPassField;
    private BulkRegAcknowledgementTypeDocumentType documentTypeField;
    private StatusType documentStatusField;
    private Errors errorsField;
    private string documentVersionField;

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
    public string SSTPID
    {
      get
      {
        return this.sSTPIDField;
      }
      set
      {
        this.sSTPIDField = value;
        this.RaisePropertyChanged(nameof (SSTPID));
      }
    }

    [XmlElement(Order = 2)]
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

    [XmlElement(Order = 3)]
    public BulkRegAcknowledgementTypeDocumentType DocumentType
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

    [XmlAttribute]
    public string documentVersion
    {
      get
      {
        return this.documentVersionField;
      }
      set
      {
        this.documentVersionField = value;
        this.RaisePropertyChanged(nameof (documentVersion));
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
