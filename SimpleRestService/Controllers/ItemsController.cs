using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ModelLibrary;
using SimpleRestService.DButil;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SimpleRestService.Controllers
{
    [Route("localitems")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private static readonly List<Item> items = new List<Item>(){
            new Item(1,"Bread","Low",33),
            new Item(2,"Bread","Middle",21),
            new Item(3,"Beer","low",70.5),
            new Item(4,"Soda","High",21.4),
            new Item(5,"Milk","Low",55.8)
        };

        

        // GET: api/<ItemsController>
        [HttpGet]
        public IEnumerable<Item> Get(){
            return new ManageItems().Get();
            //return items;
        }

        // GET api/<ItemsController>/5
        [HttpGet("{id}")]
        public Item Get(int id)
        {
            return new ManageItems().Get(id);
            //return items.Find(item => item.ID == id);
        }

        // POST api/<ItemsController>
        [HttpPost]
        public void Post([FromBody] Item value)
        {
            new ManageItems().Post(value);
            //items.Add(value);
        }

        // PUT api/<ItemsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Item value){

            new ManageItems().Put(id,value);
            /*
            Item item = Get(id);
            if (item != null){
                item.ID = value.ID;
                item.Name = value.Name;
                item.Quality = value.Quality;
                item.Quantity = value.Quantity;
            }
            */
        }

        // DELETE api/<ItemsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id){

            new ManageItems().Delete(id);
            /*
            Item item = Get(id);
            items.Remove(item);
            */
        }

        //GetFromSubstring api/substring/
        [HttpGet("name/{substring}")]
        public IEnumerable<Item> GetByName(string substring){
            return items.FindAll(item => item.Name.ToLower().Contains(substring));
        }


        // GET by quality
        [HttpGet("quality/{query}")]
        public IEnumerable<Item> GetByQuality(string query){
            //throw new NotImplementedException();
            return items.FindAll(item => item.Quality.ToLower().Contains(query));
        }

        //Get by Filter
        /// <summary>
        /// Uses filter to fetch items by quantity
        /// </summary>
        /// <param name="filter">class with an upper and a lower boundry</param>
        /// <returns>array of item withing the boundries</returns>
        [HttpGet("getbyfilter")]
        public IEnumerable<Item> GetByFilter([FromQuery] FilterClass filter){
            
            return items.FindAll(item => item.Quantity > filter.LowQuantity && item.Quantity < filter.HighQuantity);

        }
    }
}
