using ShopBridgeDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShopBridge.Controllers
{
    /// <summary>
    /// Products API 
    /// </summary>
    public class ProductsController : ApiController
    {
        // Fetching the List of Products
        // GET api/Products
        [HttpGet]
        public IEnumerable<Product> LoadAllProducts()
        {
            using (InventoryDBEntities inventoryDBEntities = new InventoryDBEntities())
            {
                return inventoryDBEntities.Products.ToList();
            }
        }

        // Fetching the Product on the basis of ID
        // GET api/Products/1
        [HttpGet]
        public HttpResponseMessage LoadAllProductsByID(int ID)
        {
            try
            {
                using (InventoryDBEntities inventoryDBEntities = new InventoryDBEntities())
                {
                    var entity = inventoryDBEntities.Products.FirstOrDefault(Prod => Prod.Id == ID);
                    if (entity != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product with ID =" + ID.ToString() + "  not found!");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        // Creating a Product
        // POST api/Products
        [HttpPost]
        public HttpResponseMessage CreateProducts([FromBody] Product product)
        {
            try
            {
                using (InventoryDBEntities inventoryEntity = new InventoryDBEntities())
                {
                    inventoryEntity.Products.Add(product);
                    inventoryEntity.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, product);
                    message.Headers.Location = new Uri(Request.RequestUri + product.Id.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //Modifying a product on the basis of ID
        // PUT api/Products/5
        [HttpPut]
        public HttpResponseMessage ModifyProducts(int ID, [FromBody] Product product)
        {
            try
            {
                using (InventoryDBEntities inventoryEntity = new InventoryDBEntities())
                {
                    var entity = inventoryEntity.Products.FirstOrDefault(prod => prod.Id == ID);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product with ID = " + ID.ToString() + " not found!");
                    }
                    else
                    {
                        entity.ProductName = product.ProductName;
                        entity.ProductDescription = product.ProductDescription;
                        entity.Quantity_Available = product.Quantity_Available;
                        entity.Price = product.Price;
                        entity.Category_Id = product.Category_Id;
                        inventoryEntity.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //Deleting a Product on the basis of ID
        // DELETE api/Products/3
        [HttpDelete]
        public HttpResponseMessage DeleteProductsByID(int ID)
        {
            try
            {
                using (InventoryDBEntities inventoryEntity = new InventoryDBEntities())
                {
                    var entity = inventoryEntity.Products.FirstOrDefault(Prod => Prod.Id == ID);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product with ID = " + ID.ToString() + " not found!");
                    }
                    else
                    {
                        inventoryEntity.Products.Remove(entity);
                        inventoryEntity.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK,"Prodcut with ID = " + ID.ToString() +" deleted");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}