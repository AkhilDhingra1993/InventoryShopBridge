using ShopBridgeDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShopBridge.Controllers
{
    public class CategoryController : ApiController
    {
        [HttpGet]
        public IEnumerable<Category> LoadAllCategories()
        {
            using (InventoryDBEntities inventoryDBEntities = new InventoryDBEntities())
            {
                return inventoryDBEntities.Categories.ToList();
            }
        }
        [HttpGet]
        public HttpResponseMessage LoadAllCategoriesByID(int ID)
        {
            using (InventoryDBEntities inventoryDBEntities = new InventoryDBEntities())
            {
                var entity = inventoryDBEntities.Categories.FirstOrDefault(Cat => Cat.Id == ID);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Category with ID =" + ID.ToString() + " not exists!");
                }
            }
        }
        [HttpPost]
        public HttpResponseMessage CreateCategory([FromBody] Category category)
        {
            try
            {
                using (InventoryDBEntities inventoryEntity = new InventoryDBEntities())
                {
                    inventoryEntity.Categories.Add(category);
                    inventoryEntity.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, category);
                    message.Headers.Location = new Uri(Request.RequestUri + category.Id.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpDelete]
        public HttpResponseMessage DeleteCategoryByID(int ID)
        {
            try
            {
                using (InventoryDBEntities inventoryEntity = new InventoryDBEntities())
                {
                    var entity = inventoryEntity.Categories.FirstOrDefault(Prod => Prod.Id == ID);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Category with ID " + ID.ToString() + " not found to be deleted!");
                    }
                    else
                    {
                        inventoryEntity.Categories.Remove(entity);
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
        public HttpResponseMessage ModifyCategories(int ID, [FromBody] Category category)
        {
            try
            {
                using (InventoryDBEntities inventoryEntity = new InventoryDBEntities())
                {
                    var entity = inventoryEntity.Categories.FirstOrDefault(cat => cat.Id == ID);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Category with ID = " + ID.ToString() + " not found!");
                    }
                    else
                    {
                        entity.Category_Name = category.Category_Name;
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
