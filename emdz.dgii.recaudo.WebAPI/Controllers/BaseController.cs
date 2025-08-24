using emdz.dgii.recaudo.Domain.Excepciones;
using Microsoft.AspNetCore.Mvc;

namespace emdz.dgii.recaudo.WebAPI.Controllers
{
    public class BaseController : Controller
    {
        internal ObjectResult CustomException(Exception exception)
        {
            // Handle specific custom exceptions
            if (exception is DocumentTypeException docTypeEx)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    code = docTypeEx.Code,
                    message = docTypeEx.Message
                });
            }

            // Default to a generic 500 error for unhandled exceptions
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                message = "Uh oh, something went wrong on our end!"
            });
        }
    }
}
