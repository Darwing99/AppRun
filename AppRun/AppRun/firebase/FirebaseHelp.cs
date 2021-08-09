using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace AppRun.Firebase
{
    class FirebaseHelp
    {
        FirebaseClient firebase = new FirebaseClient("https://app-running-317003-default-rtdb.firebaseio.com");

        public async Task<List<Usuarios>> GetAllPersons()
        {

            return (await firebase
              .Child("Usuario")
              .OnceAsync<Usuarios>()).Select(item => new Usuarios
              {
                  Name = item.Object.Name,
                  UserId = item.Object.UserId,
                  correo = item.Object.correo,
                  password = item.Object.password

              }).ToList();
        }

        public async Task AddPerson(int _UserId, string name, string Correo, string _password)
        {

            await firebase
               .Child("Usuarios")
              .PostAsync(new Usuarios() { UserId =_UserId, Name = name, correo = Correo, password = _password });
        }

        public async Task<Usuarios> GetPerson(int _UserId)
        {
            var allPersons = await GetAllPersons();
            await firebase
              .Child("Usuarios")
              .OnceAsync<Usuarios>();
            return allPersons.Where(a => a.UserId == _UserId).FirstOrDefault();
        }

        public async Task UpdatePerson(int _UserId, string name, string Correo, string _password)
        {
            var toUpdatePerson = (await firebase
              .Child("Usuarios")
              .OnceAsync<Usuarios>()).Where(a => a.Object.UserId == _UserId).FirstOrDefault();

            await firebase
              .Child("Usuarios")
              .Child(toUpdatePerson.Key)
              .PutAsync(new Usuarios() { UserId = _UserId, Name = name,correo=Correo, password=_password });
        }

        public async Task DeletePerson(int _UserId)
        {
            var toDeletePerson = (await firebase
              .Child("Persons")
              .OnceAsync<Usuarios>()).Where(a => a.Object.UserId == _UserId).FirstOrDefault();
            await firebase.Child("Persons").Child(toDeletePerson.Key).DeleteAsync();

        }
    }
}
