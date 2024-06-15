using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace p1final2024
{
    public partial class Articulos : Form
    {
        ArticuloDAO articuloDAO = new ArticuloDAO();

        public Articulos()
        {
            InitializeComponent();
            listarArticulos();
        }

        private void listarArticulos()
        {
            dgvArticulos.DataSource = articuloDAO.ReadAll();
            dgvArticulos.Columns["Imagen"].Visible = false;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Archivos de imagen (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp|Todos los archivos (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Obtiene la ruta completa del archivo seleccionado
                    string filePath = openFileDialog.FileName;

                    // Carga la imagen en el PictureBox
                    pcbImagen.Image = Image.FromFile(filePath);
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            guardarNuevo();
            listarArticulos();
        }

        private void guardarNuevo()
        {
            Articulo articulo = new Articulo
            {
                Nombre = txtNombre.Text,
                Descripcion = txtDescripcion.Text,
                Precio = Convert.ToDecimal(txtPrecio.Text),
                Imagen = obtenerImagen()
            };

            articuloDAO.Create(articulo);
        }

        private byte[] obtenerImagen()
        {
            if (pcbImagen.Image == null)
            {
                MessageBox.Show("Por favor, selecciona una imagen.");
                return null;
            }

            using (var ms = new System.IO.MemoryStream())
            {
                pcbImagen.Image.Save(ms, pcbImagen.Image.RawFormat);
                return ms.ToArray();
            }
        }
    }
}

