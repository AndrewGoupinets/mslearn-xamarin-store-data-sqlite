using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using People.Models;
using SQLite;

namespace People
{
    public class PersonRepository
    {
        public string StatusMessage { get; set; }

        private SQLiteAsyncConnection conn;
        public PersonRepository(string dbPath)
        {
            // TODO: Initialize a new SQLiteConnection
            conn = new SQLiteAsyncConnection(dbPath);

            // TODO: Create the Person table
            conn.CreateTableAsync<Person>().Wait();
        }

        public async Task AddNewPersonAsync(string name)
        {
            int result = 0;
            try
            {
                //basic validation to ensure a name was entered
                if (string.IsNullOrEmpty(name))
                    throw new Exception("Valid name required");

                // TODO: insert a new person into the Person table
                result = await conn.InsertAsync(new Person { Name = name });

                StatusMessage = string.Format("{0} record(s) added [Name: {1})", result, name);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
            }

        }

        public async Task<List<Person>> GetAllPeopleAsync()
        {
            // TODO: return a list of people saved to the Person table in the database
            try
            {
                return await conn.Table<Person>().ToListAsync();
            }
            catch (Exception e) 
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", e);
                return new List<Person>();
            }

        }
    }
}