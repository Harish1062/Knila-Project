using System;
using System.Collections.Generic;

namespace KnilaProject.DataModel;

public partial class Contact
{
    public int Id { get; set; }

    public string Firstname { get; set; }

    public string Lastname { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string Address { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string Country { get; set; }

    public string PostalCode { get; set; }
}
