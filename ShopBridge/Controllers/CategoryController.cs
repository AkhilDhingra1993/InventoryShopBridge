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
    /// Category API
    /// </summary>
    public class CategoryController : ApiController
    {
        // Fetching the List of Category
        // GET api/Category
        [HttpGet]
        public IEnumerable<Category> LoadAllCategories()
        {
            using (InventoryDBEntities inventoryDBEntities = new InventoryDBEntities())
            {
                return inventoryDBEntities.Categories.ToList();
            }
        }

        // Fetching the Category on the basis of ID
        // GET api/Category/1
        [HttpGet]
        public HttpResponseMessage LoadAllCategoriesByID(int ID)
        {
            try
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
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Category with ID =" + ID.ToString() + " not found!");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);

            }
        }
        // Creating a Category
        // POST api/Category
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

        //Modifying a Category on the basis of ID
        // PUT api/Category/5
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

        //Deleting a Category on the basis of ID
        // DELETE api/Category/3
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
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Category with ID " + ID.ToString() + " not found!");
                    }
                    else
                    {
                        inventoryEntity.Categories.Remove(entity);
                        inventoryEntity.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "Category with ID = " + ID.ToString() + " deleted");
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
