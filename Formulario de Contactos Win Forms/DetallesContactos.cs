using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Formulario_de_Contactos_Win_Forms
{
    public partial class DetallesContactos : Form
    {
        private CapaDeNegocios _capaDeNegocios;
        private Contacto _contacto;

        public DetallesContactos()
        {
            InitializeComponent();
            _capaDeNegocios = new CapaDeNegocios();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            GuardarContacto();
            this.Close();
            ((Main)this.Owner).ContactosPopulares();
        }
   
        private void GuardarContacto(){
            Contacto contacto = new Contacto();
            contacto.Nombre = txtNombre.Text;
            contacto.Apellido = txtApellido.Text;
            contacto.Telefono = txtTelefono.Text;
            contacto.Direccion = txtDireccion.Text;
            contacto.FechaInscripcion = fechaInscripcion.Value;

            contacto.Id =_contacto != null ? _contacto.Id : 0;

            _capaDeNegocios.GuardarContacto(contacto);

        }
        public void CargarContacto(Contacto contacto) {
            _contacto = contacto;
        if(contacto != null)
            {
                LimpiarFrom();
                txtNombre.Text = contacto.Nombre;
                txtApellido.Text = contacto.Apellido;
                txtTelefono.Text = contacto.Telefono;
                txtDireccion.Text = contacto.Direccion;
                fechaInscripcion.Value = contacto.FechaInscripcion;
            }
        }
        private void LimpiarFrom()
        {
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            fechaInscripcion.Value = DateTime.Now;
        }

        private void DetallesContactos_Load(object sender, EventArgs e)
        {

        }
    }
}
