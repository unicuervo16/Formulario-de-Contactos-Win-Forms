using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Formulario_de_Contactos_Win_Forms
{
    public partial class Main : Form
    {   
        private CapaDeNegocios _capaDeNegocios;
        public Main()
        {
            InitializeComponent();
            _capaDeNegocios = new CapaDeNegocios();
        }

        #region EVENTOS
        private void button1_Click(object sender, EventArgs e)
        {
            AbrirFormularioDeContactos();
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ContactosPopulares(textBusqueda.Text);
            textBusqueda.Text = string.Empty;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();

        }
        private void EliminarContacto(int id)
        {
            _capaDeNegocios.EliminarContacto(id);
        }
        #endregion

        #region METODOS PRIVADOS
        private void AbrirFormularioDeContactos()
        {
            DetallesContactos detalleDeContactos = new DetallesContactos();
            detalleDeContactos.ShowDialog(this);

        }
        private void Main_Load(object sender, EventArgs e)
        {
            ContactosPopulares();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            List<Contacto> contactos = _capaDeNegocios.TraerContactos();
            gridContacts.DataSource = contactos;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewLinkCell cell = (DataGridViewLinkCell)gridContacts.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (cell.Value.ToString() == "Editar")
            {
                DetallesContactos contactDetails = new DetallesContactos();
                contactDetails.CargarContacto(new Contacto
                {
                    Id = int.Parse(gridContacts.Rows[e.RowIndex].Cells[0].Value.ToString()),
                    Nombre = gridContacts.Rows[e.RowIndex].Cells[1].Value.ToString(),
                    Apellido = gridContacts.Rows[e.RowIndex].Cells[2].Value.ToString(),
                    Telefono = gridContacts.Rows[e.RowIndex].Cells[3].Value.ToString(),
                    Direccion = gridContacts.Rows[e.RowIndex].Cells[4].Value.ToString(),
                    FechaInscripcion = DateTime.Parse(gridContacts.Rows[e.RowIndex].Cells[5].Value.ToString())
                });
                contactDetails.ShowDialog(this);

            }
            else if (cell.Value.ToString() == "Eliminar")
            {
                //int id = int.Parse(gridContacts.Rows[e.RowIndex].Cells[0].Value.ToString());
                EliminarContacto(int.Parse(gridContacts.Rows[e.RowIndex].Cells[0].Value.ToString()));
                ContactosPopulares();
            }

        }



















        #endregion

        public void ContactosPopulares(string textoBusqueda = null)
        {
            List<Contacto> contactos = _capaDeNegocios.TraerContactos(textoBusqueda);
            gridContacts.DataSource = contactos;
        }
    }

}
