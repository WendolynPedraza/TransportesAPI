using System.Net;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransportesAPI.Models;
using TransportesAPI.Services;

namespace TransportesAPI.Controllers
{
    [Route("api/[controller]")]//se declara el espacio de nombre 
    [ApiController]//establece el tratp del controlador 
    public class CamionesController : ControllerBase
    {
        //variables para interfaz y el contexto 
        private readonly ICamiones _service;
        private readonly TransportesContext _context;
        //constructor para iniciar mi servicio y mi contexto
        public CamionesController(ICamiones service, TransportesContext context)

        {
            _service = service;
            _context = context;
        }
        [HttpGet]
        [Route("getCamiones")]
        public List<Camiones_DTO> getCamiones()
        {
            List<Camiones_DTO> lista = _service.GetCamiones();
            return lista;
        }

        //GET ById
        [HttpGet]
        [Route("getCamion/{id}")]
        public Camiones_DTO getCamion( int id)
        {
            Camiones_DTO camion = _service.GetCamiones(id);
            return camion;
        }

        [HttpPost]
        [Route("insertCamion")]
        //los metodos IActionResult retornanuna respuesta API en un fromato establecido, capaz de ser leido
        //por otro lado, la senteciaa [FramBody] determina ue existe contenido en ele cuerpo de la peticion

        public IActionResult insertCamion([FromBody] Camiones_DTO camion)
        {
            //consumo mi servicio
            string respuesta = _service.InsertCamion(camion);
            //retorno un nuevo objto del tipo ok, siendo este tipo de respuesta HTTP
            //Se genera un unevo objeto con la respuesta (new{respuesta}) para que esta tenga un fromato
            return Ok(new { respuesta });
            
        }
        //put (actualizar)
        [HttpPost]
        [Route("updateCamion")]

        public IActionResult updateCamion([FromBody] Camiones_DTO camion)
        {
            //consumo mi servicio
            string respuesta = _service.UpdateCamion(camion);
            
            return Ok(new { respuesta });

        }
        //delete
        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult deleteCamion(int id)
        {
            string respuesta = _service.DeleteCamion(id);
            return Ok(new { respuesta });
        }
    }
}
