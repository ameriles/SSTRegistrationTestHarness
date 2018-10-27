// Decompiled with JetBrains decompiler
// Type: WebService_Tester.ServiceReference1.BulkRegistrationHeaderTypeElectronicPostmark
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
  public class BulkRegistrationHeaderTypeElectronicPostmark : INotifyPropertyChanged
  {
    private string cSPIDField;
    private DateTime valueField;

    [XmlAttribute]
    public string CSPID
    {
      get
      {
        return this.cSPIDField;
      }
      set
      {
        this.cSPIDField = value;
        this.RaisePropertyChanged(nameof (CSPID));
      }
    }

    [XmlText(DataType = "date")]
    public DateTime Value
    {
      get
      {
        return this.valueField;
      }
      set
      {
        this.valueField = value;
        this.RaisePropertyChanged(nameof (Value));
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
