using KnilaProject.DataModel;
using KnilaProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnilaProject.Domain
{
    public interface IContact
    {
        Task<ContactList> GetContact(int page, int size, int isAsc, string column);
        Task<ResponseContact> GetContactById(int ID);
        Task<Result<int>> AddContact(Contact objContact);
        Task<Result<bool>> DeleteContact(int ID);

        Task<ContactList> GetContactDetails(int page, int size, bool isAsc, string column);
    }
}
