using ShopBridgeDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShopBridge.Controllers
{
    public class ProductsController : ApiController
    {
        [HttpGet]
        public IEnumerable<Product> LoadAllProducts()
        {
            using (InventoryDBEntities inventoryDBEntities = new InventoryDBEntities())
            {
                return inventoryDBEntities.Products.ToList();
            }
        }
        [HttpGet]
        public HttpResponseMessage LoadAllProductsByID(int ID)
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
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product with ID =" + ID.ToString() + " not exists!");
                }
            }
        }
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
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product with ID " + ID.ToString() + " not found to be deleted!");
                    }
                    else
                    {
                        inventoryEntity.Products.Remove(entity);
                        inventoryEntity.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
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
    }

}
