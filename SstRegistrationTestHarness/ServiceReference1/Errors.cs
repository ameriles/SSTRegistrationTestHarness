// Decompiled with JetBrains decompiler
// Type: WebService_Tester.ServiceReference1.Errors
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
  public class Errors : INotifyPropertyChanged
  {
    private WebService_Tester.ServiceReference1.Error[] errorField;
    private string errorCountField;

    [XmlElement("Error", Order = 0)]
    public WebService_Tester.ServiceReference1.Error[] Error
    {
      get
      {
        return this.errorField;
      }
      set
      {
        this.errorField = value;
        this.RaisePropertyChanged(nameof (Error));
      }
    }

    [XmlAttribute(DataType = "nonNegativeInteger")]
    public string errorCount
    {
      get
      {
        return this.errorCountField;
      }
      set
      {
        this.errorCountField = value;
        this.RaisePropertyChanged(nameof (errorCount));
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
