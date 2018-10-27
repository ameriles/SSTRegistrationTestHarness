// Decompiled with JetBrains decompiler
// Type: WebService_Tester.ServiceReference1.SSTReceiptType
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
  [DebuggerStepThrough]
  [GeneratedCode("System.Xml", "4.6.1064.2")]
  [Serializable]
  public class SSTReceiptType : INotifyPropertyChanged
  {
    private SSTReceiptTypeReceiptHeader receiptHeaderField;
    private SSTReceiptTypeTransmissionReceipt transmissionReceiptField;
    private string receiptVersionField;

    [XmlElement(Order = 0)]
    public SSTReceiptTypeReceiptHeader ReceiptHeader
    {
      get
      {
        return this.receiptHeaderField;
      }
      set
      {
        this.receiptHeaderField = value;
        this.RaisePropertyChanged(nameof (ReceiptHeader));
      }
    }

    [XmlElement(Order = 1)]
    public SSTReceiptTypeTransmissionReceipt TransmissionReceipt
    {
      get
      {
        return this.transmissionReceiptField;
      }
      set
      {
        this.transmissionReceiptField = value;
        this.RaisePropertyChanged(nameof (TransmissionReceipt));
      }
    }

    [XmlAttribute]
    public string receiptVersion
    {
      get
      {
        return this.receiptVersionField;
      }
      set
      {
        this.receiptVersionField = value;
        this.RaisePropertyChanged(nameof (receiptVersion));
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
