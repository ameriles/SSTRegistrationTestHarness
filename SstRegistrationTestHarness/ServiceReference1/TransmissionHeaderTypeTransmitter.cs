﻿// Decompiled with JetBrains decompiler
// Type: WebService_Tester.ServiceReference1.TransmissionHeaderTypeTransmitter
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
  [DebuggerStepThrough]
  [Serializable]
  public class TransmissionHeaderTypeTransmitter : INotifyPropertyChanged
  {
    private string eTINField;

    [XmlElement(Order = 0)]
    public string ETIN
    {
      get
      {
        return this.eTINField;
      }
      set
      {
        this.eTINField = value;
        this.RaisePropertyChanged(nameof (ETIN));
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
