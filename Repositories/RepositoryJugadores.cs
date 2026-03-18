using System.Xml.Linq;
using JugadoresNugetXmlAJ.Models;

namespace JugadoresNugetXmlAJ.Repositories;

public class RepositoryJugadores
{
    private XDocument document;

    public RepositoryJugadores()
    {
        //PARA LEER UN RECURSO INCRUSTADO NECESITAMOS
        //EL NAMESPACE DEL PROYECTO Y, SI ESTUVIERA EN UNA CARPETA
        //TAMBIEN, EL NOMBRE DE LA CARPETA Y EL FILE NAME
        
        string resourceName="JugadoresNugetXmlAJ.Jugadores.xml";
        //LOS DATOS SE RECUPERAN MEDIANTE STREAM
        Stream stream = GetType().Assembly.GetManifestResourceStream(resourceName);
        //EL FICHERO XML SE ALMACENA EN DOCUMENT MEDIANTE LOAD
        
        document= XDocument.Load(stream);
    }

    public List<Jugador> GetJugadores()
    {
        var consulta= from datos in document.Descendants("jugador")
            select datos;

        List<Jugador> players = new List<Jugador>();
        foreach (var tag in consulta)
        {
            Jugador j= new Jugador();
            j.Numero = int.Parse(tag.Element("numero").Value);
            j.Nombre = tag.Element("nombre").Value;
            j.Posicion = tag.Element("posicion").Value;
            j.Edad = int.Parse(tag.Element("edad").Value);
            j.Imagen = tag.Element("imagen").Value;
            players.Add(j);
        }
        return players;
    }
}