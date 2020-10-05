using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using ModelLibrary;

namespace SimpleRestService.DButil
{
    public class ManageItems{
        private const string connectionstring = "Data Source=bxq.one,31433;Initial Catalog=TestDb;User ID=sa;Password=Sugo11asd;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private const string getAll = "SELECT * FROM dbo.items";
        

        public IEnumerable<Item> Get(){
            List<Item> items = new List<Item>();

            using (SqlConnection conn = new SqlConnection(connectionstring)){
                conn.Open();
                using (SqlCommand com = new SqlCommand(getAll, conn)){
                    SqlDataReader sr = com.ExecuteReader();

                    while (sr.Read())
                    {
                        Item item = ReadNextElement(sr);

                        items.Add(item);
                    }
                    sr.Close();
                }
                    
            }

            return items;
            
            //throw new NotImplementedException();
        }

        public Item Get(int id){
            return Get().ToList().Find(i => i.ID == id);
        }

        public void Post(Item value){

        string query = $"INSERT INTO dbo.items (Name, Quality, Quantity) VALUES ('{value.Name}', '{value.Quality}', {value.Quantity})";

            using (SqlConnection conn = new SqlConnection(connectionstring)){
                conn.Open();
                SqlCommand cmd = new SqlCommand(query,conn);
                cmd.ExecuteNonQuery();
            }

            //throw new NotImplementedException();
        }

        public void Put(int id, Item value){

            string query = $"UPDATE dbo.items SET Name='{value.Name}', Quality='{value.Quality}', Quantity='{value.Quantity}' WHERE Id={id}";
            using (SqlConnection conn = new SqlConnection(connectionstring)){
                conn.Open();
                SqlCommand cmd = new SqlCommand(query,conn);
                cmd.ExecuteNonQuery();
            }

            //throw new NotImplementedException();
        }

        public void Delete(int id){
            string query = $"DELETE FROM dbo.items WHERE Id={id}";
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }

            //throw new NotImplementedException();
        }

        protected Item ReadNextElement(SqlDataReader sr){
            Item item = new Item(){
                ID = sr.GetInt32(0),
                Name = sr.GetString(1),
                Quality = sr.GetString(2),
                Quantity = sr.GetDouble(3)

            };

            return item;
        }
    }
}
