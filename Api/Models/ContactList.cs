using KnilaProject.DataModel;
using KnilaProject.Domain;
using System.Collections.Generic;

namespace KnilaProject.Models
{
    public class ContactList
    {
        public List<ResponseContact> contacts { get; set; }
        public int Count { get; set; }
    }
}
