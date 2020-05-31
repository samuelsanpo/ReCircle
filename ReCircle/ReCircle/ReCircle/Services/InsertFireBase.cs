using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using ReCircle.Model;
using ReCircle.Model.Adapter;

namespace ReCircle.Services
{
    public class InsertFireBase
    {
        public InsertFireBase()
        {
        }

        private readonly string ChildName = "Clients";

        readonly FirebaseClient firebase = new FirebaseClient("https://recircle-d8492.firebaseio.com/");

        public async Task<List<Client>> GetAllClients()
        {
            return (await firebase
                .Child(ChildName)
                .OnceAsync<Client>()).Select(item => new Client
                {
                    Name = item.Object.Name,
                    UserId = item.Object.UserId,
                    Role = item.Object.Role,
                    LastName = item.Object.LastName,
                    Mobile = item.Object.Mobile,
                    Address = item.Object.Address,
                    Birthday = item.Object.Birthday,
                    Email = item.Object.Email,
                    Document  = item.Object.Document,
                    Calification = item.Object.Gender,
                    IsActive =  item.Object.IsActive,
                    seeds = item.Object.seeds
                }).ToList();
        }

        public async Task AddClient(string userId,string name,int role,string lastname
            , string mobile, string address,DateTime birth,string email,string document,string gender
            ,string verification,bool isActive)
        {
            await firebase
                .Child(ChildName)
                .Child(userId)
                .PostAsync(new Client()
                {
                    Name = name,
                    UserId = userId,
                    Role = role,
                    LastName = lastname,
                    Mobile = mobile,
                    Address = address,
                    Birthday = birth,
                    Email = email,
                    Document = document,
                    Calification = gender,
                    VerificationCode = verification,
                    IsActive = false,
                    seeds = 100
                });
        }

        public async Task<Client> GetClient(string personId)
        {
            var allPersons = await GetAllClients();
            await firebase
                .Child(ChildName)
                .OnceAsync<Client>();
            return allPersons.FirstOrDefault(a => a.UserId == personId);
        }

        public async Task<Client> GetClientByName(string name)
        {
            var allPersons = await GetAllClients();
            await firebase
                .Child(ChildName)
                .OnceAsync<Client>();
            return allPersons.FirstOrDefault(a => a.Name == name);
        }

        public async Task UpdateClient(string userId, string name, int role, string lastname
            , string mobile, string address, DateTime birth, string email, string document, string gender
            , string verification, bool isActive, int seeds)
        {
            var toUpdatePerson = (await firebase
                .Child(ChildName)
                .OnceAsync<Client>()).FirstOrDefault(a => a.Object.UserId == userId);

            await firebase
                .Child(ChildName)
                .Child(userId)
                .PutAsync(new Client()
                {
                    Name = name,
                    UserId = userId,
                    Role = role,
                    LastName = lastname,
                    Mobile = mobile,
                    Address = address,
                    Birthday = birth,
                    Email = email,
                    Document = document,
                    Calification = gender,
                    VerificationCode = verification,
                    IsActive = isActive,
                    seeds = seeds
                });
        }

        public async Task DeleteClient(string personId)
        {
            var toDeletePerson = (await firebase
                .Child(ChildName)
                .OnceAsync<Client>()).FirstOrDefault(a => a.Object.UserId == personId);
            await firebase.Child(ChildName).Child(toDeletePerson.Key).DeleteAsync();
        }
    }
}
