using DTO;
using TransportesAPI.Models;

namespace TransportesAPI.Services
{
    public interface ICamiones
    {
        //es una estructura que define un contrato o conjunto de métodos y
        //propiedades que una clase debe implementar.
        //Una interfaz establece un conjunto de requisitos que cualquier clase
        //que la implemente debe seguir. Estos requisitos son declarados en la
        //interfaz en forma de firmas de métodos y propiedades,
        //pero la interfaz en sí misma no proporciona ninguna implementación
        //de estos métodos o propiedades.Es responsabilidad de las clases que
        //implementan la interfaz proporcionar las implementaciones concretas de
        //estos miembros.

        //Las interfaces son útiles para lograr la abstracción y la reutilización
        //de código en C#.

        //GET
        List<Camiones_DTO> GetCamiones();
        //GETbyId
        Camiones_DTO GetCamiones(int id);
        //INSERT (POST)
        string InsertCamion(Camiones_DTO camion);
        //UPDATE (PUT)
        string UpdateCamion(Camiones_DTO camion);
        //DELETE (DELETE)
        string DeleteCamion(int id);
    }

    //la clase wue implementa la interfaz y declara la imprlementacion d ela ligica d elos etodos existentes 
    public class CamionesService : ICamiones
    {
        //variable para crear el contexto (Inyeccion de dependencias )
        private readonly TransportesContext _context;
        //constructor para inicializar el contexto
        public CamionesService(TransportesContext context)
        {
            _context = context;
        }
        //Implementacion de metodos
        public string DeleteCamion(int id)
        {
            try
            {
                //obtengo el camion de base de datos
                Camiones _camion = _context.Camiones.Find(id);
                if(_camion == null)
                {
                    return $"No se encontro algun objeto con identificaador {id}";
                }
                //remuevo el objeto del contexto
                _context.Camiones.Remove(_camion);
                //inpacto la BD
                _context.SaveChanges();
                //respondo
                return $"Camion {id} eliminado con exito";
            }
            catch(Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public List<Camiones_DTO> GetCamiones()
        {
            try
            {
                //lista de camiones del original
                List<Camiones> lista_original = _context.Camiones.ToList();
                //lista de DTOs
                List<Camiones_DTO> lista_salida = new List<Camiones_DTO>();
                //recorro cada camion y genero un nuvo DTO con DinamycMapper
                foreach (var cam in lista_original) {
                    //usamos el dinamycmapper para convertitr
                    Camiones_DTO  DTO= DynamicMapper.Map<Camiones, Camiones_DTO>(cam);
                    lista_salida.Add(DTO);
                }
                //retorno la lista con los objetos ya mapeados
                return lista_salida;
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
        }

        public Camiones_DTO GetCamiones(int id)
        {
            Camiones origen = _context.Camiones.Find(id);
            //DynamicMapper
            Camiones_DTO resultado = DynamicMapper.Map<Camiones, Camiones_DTO>(origen);
            return resultado;
        }

        public string InsertCamion(Camiones_DTO camion)
        {
            try
            {
                //creo un camion del modelo original
                Camiones _camion = new Camiones();
                //asigno los valores del objrto DTO del parametro al objeto del modelo original
                _camion = DynamicMapper.Map<Camiones_DTO, Camiones>(camion);
                //añadimo el objeto al contexto
                _context.Camiones.Add(_camion);
                //impactamos la BD
                _context.SaveChanges();
                //respondo 
                return "Camion insertado con exito";
            }catch(Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string UpdateCamion(Camiones_DTO camion)
        {
            try
            {
                //creo un camion del modelo original
                Camiones _camion = new Camiones();
                //asigno los valores del objrto DTO del parametro al objeto del modelo original
                _camion = DynamicMapper.Map<Camiones_DTO, Camiones>(camion);
                //modifico el estado del modelo en el contexto 
                _context.Entry(_camion).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                //impactamos la BD
                _context.SaveChanges();
                //respondo 
                return $"Camion {_camion.ID_Camion} Actualizado con exito";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}
