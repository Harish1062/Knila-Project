using KnilaProject.DataModel;
using KnilaProject.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KnilaProject.Controllers
{
    [Route("api/Contact")]
    [ApiController]
    [Authorize]
    public class ContactController : ControllerBase
    {
        private readonly IContact _contact;
        // GET: ContactController
        public ContactController(IContact contact)
        { 
            _contact = contact;
        }


        [HttpPost]
        [Route("get-contact")]
        public async Task<IActionResult> GetContact(int page,int size,bool isAsc,string column)
        {
            return Ok(await _contact.GetContactDetails(page, size,isAsc,column));
        }
        [HttpPost]
        [Route("get-contact-by-id")]
        public async Task<IActionResult> GetContactById(int id)
        {
            return Ok(await _contact.GetContactById(id));
        }
        [HttpPost]
        [Route("add-contact")]
        public async Task<IActionResult> AddContact(Contact request)
        {
            return Ok(await _contact.AddContact(request));
        }
        [HttpPost]
        [Route("delete-contact")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            return Ok(await _contact.DeleteContact(id));
        }
    }
}
