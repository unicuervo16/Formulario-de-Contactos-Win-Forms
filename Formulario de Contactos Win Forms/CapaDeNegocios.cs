using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario_de_Contactos_Win_Forms
{
    internal class CapaDeNegocios
    {
        private CapaDeAccesoBD _capaDeAccesoBD;

        public CapaDeNegocios()
        {
            _capaDeAccesoBD = new CapaDeAccesoBD();
        }
        public Contacto GuardarContacto(Contacto contacto)
        {
            if(contacto.Id == 0)
            {
                _capaDeAccesoBD.InsertarContacto(contacto);
            }
            else
                _capaDeAccesoBD.ActualizarContacto(contacto);
            return contacto;
        }
        public List<Contacto> TraerContactos(string textoBusqueda = null)
        {
           return _capaDeAccesoBD.TraerContactos(textoBusqueda);
        }
        public void EliminarContacto(int id)
        {
            _capaDeAccesoBD.EliminarContacto(id);
        }
    }
}
