// Decompiled with JetBrains decompiler
// Type: WebService_Tester.ServiceReference1.TechModelType
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
  [DebuggerStepThrough]
  [Serializable]
  public class TechModelType : INotifyPropertyChanged
  {
    private object itemField;
    private ItemChoiceType2 itemElementNameField;

    [XmlElement("ModelThree", typeof (object), Order = 0)]
    [XmlChoiceIdentifier("ItemElementName")]
    [XmlElement("ModelOne", typeof (ModelOneType), Order = 0)]
    [XmlElement("ModelTwo", typeof (ModelTwoType), Order = 0)]
    [XmlElement("None", typeof (object), Order = 0)]
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

    [XmlElement(Order = 1)]
    [XmlIgnore]
    public ItemChoiceType2 ItemElementName
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
