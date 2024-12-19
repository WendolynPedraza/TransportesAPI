using System.Reflection;

namespace TransportesAPI.Services
{
    public class DynamicMapper
    {
        //metodo que mapea de forma dinamica diferentes tipos de obetos (modelos originale sa DTO y viceversa
        public static TDestination Map<Tsource, TDestination>(Tsource source) 
            where Tsource : class //se declara una clase abstracta como tipo de dato de entrada
            where TDestination : class, new()//se declara una clase abstracta como tipo de dato de salida

            
        {
            //valida si existe y contiene informacion la cla sdel origel
            if (source == null) throw new ArgumentNullException("source");
            var destination = new TDestination(); //creo una instacia del objeto de salida 

            //recuperar las propiedades (los atributos de mis elementos) usando la biblioteca system.reflexion
            //Mediante reflexión, puedes acceder a las propiedades de un tipo (clase, estructura, etc.) en tiempo de ejecución, incluso si no conoces el tipo exacto en tiempo de compilación.
            //GetProperties: Devuelve un array con todas las propiedades públicas del tipo especificado.
            //BindingFlags: Opciones que especifican qué miembros buscar(públicos, privados, estáticos, etc.).
            //using System.Reflection;

            var sourcePropertis = typeof(Tsource).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var destinationProperties= typeof(TDestination).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            //rrecorro todos los atributos de origen para equipararlos con el objetp de salida 
            foreach(var sourceProperty in destinationProperties)
            {
                var destinationProperty = destinationProperties.FirstOrDefault(dp => dp.Name.ToLower() == sourceProperty.Name.ToLower()
                &&
                dp.PropertyType==sourceProperty.PropertyType);
                if(destinationProperty!=null && destinationProperty.CanWrite)
                {
                    //GETValue: lee el valor actual de la propiedad del origen al destino
                    //SetValue: Establece un nuevo vslor a la propiedad de un objeto
                    var value = sourceProperty.GetValue(source);
                    destinationProperty.SetValue(destination, value);
                }
            }
            //retorno el nuevo tipo de objeto ya mapeado
            return destination;
        }


    }
}
