using Azure.Core;
using KnilaProject.DataModel;
using KnilaProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace KnilaProject.Domain
{
    public class ContactRepository : IContact
    {
        private readonly KnilaContext _appDBContext;
        public ContactRepository(KnilaContext context)
        {
            _appDBContext = context;
        }

        public async Task<ContactList> GetContact(int page, int size, int isAsc, string column)
        {
            ContactList responseContacts = new ContactList();
            responseContacts.contacts =  await (from app in _appDBContext.Contacts
                                     select new ResponseContact()
                                     {
                                         Address = app.Address,
                                         City = app.City,
                                         Country = app.Country,
                                         PostalCode = app.PostalCode,
                                         Email = app.Email,
                                         FirstName = app.Firstname,
                                         Id = app.Id,
                                         LastName = app.Lastname,
                                         PhoneNumber = app.PhoneNumber,
                                         State = app.State
                                     }).Skip((page - 1) * size).Take(size).ToListAsync();
            responseContacts.Count = await _appDBContext.Contacts.CountAsync();
            return responseContacts;
        }

        public async Task<ContactList> GetContactDetails(int page, int size, bool isAsc, string column)
        {
            ContactList responseContacts = new ContactList();
            var contacts = await (from app in _appDBContext.Contacts
                                               select new ResponseContact()
                                               {
                                                   Address = app.Address,
                                                   City = app.City,
                                                   Country = app.Country,
                                                   PostalCode = app.PostalCode,
                                                   Email = app.Email,
                                                   FirstName = app.Firstname,
                                                   Id = app.Id,
                                                   LastName = app.Lastname,
                                                   PhoneNumber = app.PhoneNumber,
                                                   State = app.State
                                               }).ToListAsync();
            responseContacts.contacts = contacts;
       
                if (isAsc)
                {
                    switch (column)
                    {
                        case "id": responseContacts.contacts = responseContacts.contacts.OrderBy(x => x.Id).ToList(); break;
                        case "firstName": responseContacts.contacts = responseContacts.contacts.OrderBy(x => x.FirstName).ToList(); break;
                        case "lastName": responseContacts.contacts = responseContacts.contacts.OrderBy(x => x.LastName).ToList(); break;
                        case "email": responseContacts.contacts = responseContacts.contacts.OrderBy(x => x.Email).ToList(); break;
                        case "address": responseContacts.contacts = responseContacts.contacts.OrderBy(x => x.Address).ToList(); break;
                        case "phoneNumber": responseContacts.contacts = responseContacts.contacts.OrderBy(x => x.PhoneNumber).ToList(); break;
                        case "city": responseContacts.contacts = responseContacts.contacts.OrderBy(x => x.City).ToList(); break;
                        case "state": responseContacts.contacts = responseContacts.contacts.OrderBy(x => x.State).ToList(); break;
                        case "country": responseContacts.contacts = responseContacts.contacts.OrderBy(x => x.Country).ToList(); break;
                        case "postalCode": responseContacts.contacts = responseContacts.contacts.OrderBy(x => x.PostalCode).ToList(); break;
                    }
                }                
                else
                {
                     switch (column)
                     {
                        case "id": responseContacts.contacts = responseContacts.contacts.OrderByDescending(x => x.Id).ToList(); break;
                        case "firstName": responseContacts.contacts = responseContacts.contacts.OrderByDescending(x => x.FirstName).ToList(); break;
                        case "lastName": responseContacts.contacts = responseContacts.contacts.OrderByDescending(x => x.LastName).ToList(); break;
                        case "email": responseContacts.contacts = responseContacts.contacts.OrderByDescending(x => x.Email).ToList(); break;
                        case "address": responseContacts.contacts = responseContacts.contacts.OrderByDescending(x => x.Address).ToList(); break;
                        case "phoneNumber": responseContacts.contacts = responseContacts.contacts.OrderByDescending(x => x.PhoneNumber).ToList(); break;
                        case "city": responseContacts.contacts = responseContacts.contacts.OrderByDescending(x => x.City).ToList(); break;
                        case "state": responseContacts.contacts = responseContacts.contacts.OrderByDescending(x => x.State).ToList(); break;
                        case "country": responseContacts.contacts = responseContacts.contacts.OrderByDescending(x => x.Country).ToList(); break;
                        case "postalCode": responseContacts.contacts = responseContacts.contacts.OrderByDescending(x => x.PostalCode).ToList(); break;
                     }
                }
           
            responseContacts.contacts = responseContacts.contacts.Skip((page-1) * size).Take(size).ToList();
            responseContacts.Count = await _appDBContext.Contacts.CountAsync();
            return responseContacts;
        }


        public async Task<ResponseContact> GetContactById(int ID)
        {
            ResponseContact responseContact = new();
            responseContact=await (from app in _appDBContext.Contacts
                                   where app.Id==ID
                                   select new ResponseContact()
                                   {
                                       Address=app.Address,
                                       City=app.City,
                                       Country=app.Country,
                                       PostalCode=app.PostalCode,
                                       Email=app.Email,
                                       FirstName=app.Firstname,
                                       Id=app.Id,
                                       LastName=app.Lastname,
                                       PhoneNumber=app.PhoneNumber,
                                       State=app.State
                                   }).FirstOrDefaultAsync();
            return responseContact;
        }
        
        public async Task<Result<bool>> DeleteContact(int id)
        {
            Result<bool> result = new();
            var contact =await _appDBContext.Contacts.FirstOrDefaultAsync(x=>x.Id==id);
            if (contact != null)
            {
                _appDBContext.Entry(contact).State = EntityState.Deleted;
                int res=await _appDBContext.SaveChangesAsync();
                result.Data = res>0? true: false;
                result.Message= res > 0 ? "Contact deleted successfully" : "Contact ";
            }
            return result;
        }

        public async Task<Result<int>> AddContact(Contact objContact)
        {
            Result<int> result = new Result<int>();
            try
            {
                Contact tblContact = _appDBContext.Contacts.FirstOrDefault(x => x.Id == objContact.Id);
                if (tblContact == null)
                {
                    tblContact = new Contact();
                }
                tblContact.Firstname = objContact.Firstname;
                tblContact.Lastname = objContact.Lastname;
                tblContact.Country = objContact.Country;
                tblContact.PostalCode = objContact.PostalCode;
                tblContact.Address = objContact.Address;
                tblContact.City = objContact.City;
                tblContact.State=objContact.State;
                tblContact.Email = objContact.Email;
                tblContact.PhoneNumber = objContact.PhoneNumber;
                if (tblContact.Id > 0)
                    _appDBContext.Contacts.Update(tblContact);
                else
                    await _appDBContext.Contacts.AddAsync(tblContact);

                int res = await _appDBContext.SaveChangesAsync();
                result.Data = res;
                return result;
            }
            catch(Exception ex)
            {
                result.Data = 0;
                return result;
            }
        }

        
    }
}