// Decompiled with JetBrains decompiler
// Type: WebService_Tester.ServiceReference1.RegistrationDocumentType
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
  [XmlRoot("RegistrationDocument", IsNullable = false, Namespace = "")]
  [DebuggerStepThrough]
  [Serializable]
  public class RegistrationDocumentType : INotifyPropertyChanged
  {
    private string documentIdField;
    private RegistrationDocumentTypeDocumentType documentTypeField;
    private SSTRegistrationHeaderType sSTRegistrationHeaderField;
    private RegistrationDocumentTypeRegistrationInformation registrationInformationField;

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
    public RegistrationDocumentTypeDocumentType DocumentType
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
    public SSTRegistrationHeaderType SSTRegistrationHeader
    {
      get
      {
        return this.sSTRegistrationHeaderField;
      }
      set
      {
        this.sSTRegistrationHeaderField = value;
        this.RaisePropertyChanged(nameof (SSTRegistrationHeader));
      }
    }

    [XmlElement(Order = 3)]
    public RegistrationDocumentTypeRegistrationInformation RegistrationInformation
    {
      get
      {
        return this.registrationInformationField;
      }
      set
      {
        this.registrationInformationField = value;
        this.RaisePropertyChanged(nameof (RegistrationInformation));
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
