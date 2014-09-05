using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.ServiceModel;

[DataContract(Name = "ContactDetails", Namespace = "http://KnockKnock.readify.net")]
[Serializable]
public class ContactDetails : object, IExtensibleDataObject, INotifyPropertyChanged
{
    [OptionalField] private string _emailAddressField;

    [OptionalField] private string _familyNameField;

    [OptionalField] private string _givenNameField;

    [OptionalField] private string _phoneNumberField;
    [NonSerialized] private ExtensionDataObject _extensionDataField;

    [DataMember]
    public string EmailAddress
    {
        get { return _emailAddressField; }
        set
        {
            if ((ReferenceEquals(_emailAddressField, value) != true))
            {
                _emailAddressField = value;
                RaisePropertyChanged("EmailAddress");
            }
        }
    }

    [DataMember]
    public string FamilyName
    {
        get { return _familyNameField; }
        set
        {
            if ((ReferenceEquals(_familyNameField, value) != true))
            {
                _familyNameField = value;
                RaisePropertyChanged("FamilyName");
            }
        }
    }

    [DataMember]
    public string GivenName
    {
        get { return _givenNameField; }
        set
        {
            if ((ReferenceEquals(_givenNameField, value) != true))
            {
                _givenNameField = value;
                RaisePropertyChanged("GivenName");
            }
        }
    }

    [DataMember]
    public string PhoneNumber
    {
        get { return _phoneNumberField; }
        set
        {
            if ((ReferenceEquals(_phoneNumberField, value) != true))
            {
                _phoneNumberField = value;
                RaisePropertyChanged("PhoneNumber");
            }
        }
    }

    [Browsable(false)]
    public ExtensionDataObject ExtensionData
    {
        get { return _extensionDataField; }
        set { _extensionDataField = value; }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void RaisePropertyChanged(string propertyName)
    {
        PropertyChangedEventHandler propertyChanged = PropertyChanged;
        if ((propertyChanged != null))
        {
            propertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

[DataContract(Name = "TriangleType", Namespace = "http://KnockKnock.readify.net")]
public enum TriangleType
{
    [EnumMember] Error = 0,

    [EnumMember] Equilateral = 1,

    [EnumMember] Isosceles = 2,

    [EnumMember] Scalene = 3,
}

[ServiceContract(Namespace = "http://KnockKnock.readify.net")]
public interface IRedPill
{
    [OperationContract(Action = "http://KnockKnock.readify.net/IRedPill/WhoAreYou", ReplyAction = "http://KnockKnock.readify.net/IRedPill/WhoAreYouResponse")]
    ContactDetails WhoAreYou();

    [OperationContract(Action = "http://KnockKnock.readify.net/IRedPill/FibonacciNumber",
        ReplyAction = "http://KnockKnock.readify.net/IRedPill/FibonacciNumberResponse")]
    [FaultContract(typeof (ArgumentOutOfRangeException),
        Action = "http://KnockKnock.readify.net/IRedPill/FibonacciNumberArgumentOutOfRangeExceptionFault", Name = "ArgumentOutOfRangeException",
        Namespace = "http://schemas.datacontract.org/2004/07/System")]
    long FibonacciNumber(long n);

    [OperationContract(Action = "http://KnockKnock.readify.net/IRedPill/WhatShapeIsThis", ReplyAction = "http://KnockKnock.readify.net/IRedPill/WhatShapeIsThisResponse")]
    TriangleType WhatShapeIsThis(int a, int b, int c);

    [OperationContract(Action = "http://KnockKnock.readify.net/IRedPill/ReverseWords", ReplyAction = "http://KnockKnock.readify.net/IRedPill/ReverseWordsResponse")]
    [FaultContract(typeof (ArgumentNullException),
        Action = "http://KnockKnock.readify.net/IRedPill/ReverseWordsArgumentNullExceptionFault", Name = "ArgumentNullException", Namespace = "http://schemas.datacontract.org/2004/07/System")]
    string ReverseWords(string s);
}