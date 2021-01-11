using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment4.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Assignment4Controller : ControllerBase
    {

        [HttpGet("GetInvent")]
        public ActionResult<List<InventoryItem>> Get()
        {
            return Ok(DataContext.Inventory);
        }

        [HttpGet("GetCart")]
        public ActionResult<List<Product>> GetC()
        {
            return Ok(DataContext.Cart);
        }

        [HttpPost("AddOrUpdate")]
        public ActionResult<InventoryItem> AddOrUpdate([FromBody] InventoryItem prod)
        {
            if (prod == null)
            {
                return BadRequest();
            }

            //if (DataContext.Cart.Any(i => i.Id.Equals(prod.Id))) //increment
            if (DataContext.Cart.Any(i => i.Name == prod.Name))
            {
                DataContext.Cart.FirstOrDefault(i => i.Name == prod.Name).Units++;
                //DataContext.Cart.FirstOrDefault(i => i.Id.Equals(prod.Id)).Units++;

            }
            else // add new item in cart
            {
                if (prod.type == 0) // add new prodByWeight
                {
                    DataContext.Cart.Add(new ProductByWeight { Name = prod.Name, Description = prod.Description, UnitPrice = prod.Price, Units = 1, Id = prod.Id });
                }
                else
                {
                    DataContext.Cart.Add(new ProductByQuantity { Name = prod.Name, Description = prod.Description, UnitPrice = prod.Price, Units = 1, Id = prod.Id });
                }
            }


            return Ok(prod);
        }

        [HttpPost("Delete")]
        public ActionResult<Product> Delete([FromBody] string name)
        {
            //var prodToRemove = DataContext.Cart.FirstOrDefault(t => t.Id.Equals(id));
            if (name != null)
            {
                if (DataContext.Cart.FirstOrDefault(i => i.Name == name).Units == 1) // if last one, remove
                {
                    DataContext.Cart.Remove(DataContext.Cart.FirstOrDefault(i => i.Name == name));
                }
                else //decrement
                {
                    DataContext.Cart.FirstOrDefault(i => i.Name == name).Units--;
                }
            }

            return Ok(name);
        }
    }
}
