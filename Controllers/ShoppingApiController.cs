using Microsoft.AspNetCore.Mvc;
using ShoppingApp.EfCore;
using ShoppingApp.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingApp.Controllers
{
    [Route("api/[controller]/products")]
    [ApiController]
    public class ShoppingApiController : ControllerBase
    {
        private readonly DbHelper _db;
        public ShoppingApiController(EF_DataContext eF_DataContext)
        {
            _db = new DbHelper(eF_DataContext);
        }
        // GET: api/<ShoppingApiController>
        [HttpGet]
        //[Route("api/[controller]/products")]
        public IActionResult Get()
        {
            ResponseType type = ResponseType.Success;
            try
            {
                IEnumerable<ProductModel> data = _db.GetProducts();
                
                if (!data.Any())
                {
                    type = ResponseType.NotFound;
                    return BadRequest();
                }
                return Ok(ResponseHandler.GetAppResponse(type, data));
            } catch (Exception ex)
            {
                type = ResponseType.Failure;
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // GET api/<ShoppingApiController>/5
        [HttpGet("{Id}")]
        //[Route("api/[controller]/products/{Id}")]
        public IActionResult Get(int Id)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                ProductModel data = _db.GetProductById(Id);

                if (data == null)
                {
                    type = ResponseType.NotFound;
                    return BadRequest();
                }
                return Ok(ResponseHandler.GetAppResponse(type, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // POST api/<ShoppingApiController>
        [HttpPost]
        //[Route("api/[controller]/products")]
        public IActionResult Post([FromBody] OrderModel model)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.SaveOrder(model);
                return Ok(ResponseHandler.GetAppResponse(type, model));
            }
            catch(Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        //// PUT api/<ShoppingApiController>/5
        //[HttpPut("{Id}")]
        ////[Route("api/[controller]/products/{Id}")]
        //public IActionResult Put(int Id, [FromBody] OrderModel model)
        //{
        //    try
        //    {
        //        ResponseType type = ResponseType.Success;
        //        _db.SaveOrder(model);
        //        return Ok(ResponseHandler.GetAppResponse(type, model));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ResponseHandler.GetExceptionResponse(ex));
        //    }
        //}

        // DELETE api/<ShoppingApiController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.DeleteOrder(Id);
                return Ok(ResponseHandler.GetAppResponse(type, "Delete Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}
