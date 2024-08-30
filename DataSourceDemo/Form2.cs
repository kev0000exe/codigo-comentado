using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataSourceDemo
{
    public partial class Form2 : Form
    {
        // Constructor del formulario
        public Form2()
        {
            InitializeComponent(); // Inicializa los componentes del formulario
        }

        // Evento que se activa al hacer clic en el botón de guardar en el BindingNavigator
        private void customersBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate(); // Valida los datos del formulario
            this.customersBindingSource.EndEdit(); // Finaliza cualquier edición en curso en el BindingSource
            this.tableAdapterManager.UpdateAll(this.northwindDataSet); // Actualiza todos los cambios realizados en el DataSet 'northwindDataSet'
        }

        // Evento que se activa cuando el formulario se carga
        private void Form2_Load(object sender, EventArgs e)
        {
            // Carga los datos en la tabla 'Customers' del DataSet 'northwindDataSet'
            // Esta línea puede ser movida o eliminada según sea necesario
            this.customersTableAdapter.Fill(this.northwindDataSet.Customers);
        }

        // Evento que se activa cuando se hace clic en el cuadro de texto 'cajaTextoID'
        private void cajaTextoID_Click(object sender, EventArgs e)
        {
            // Este evento está vacío, pero podría ser utilizado para manejar acciones
            // cuando se hace clic en el cuadro de texto, como seleccionar todo el texto.
        }

        // Evento que se activa cuando se presiona una tecla en el cuadro de texto 'cajaTextoID'
        private void cajaTextoID_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Si se presiona la tecla 'Enter' (char 13)
            if (e.KeyChar == (char)13)
            {
                // Busca el índice del elemento en el BindingSource cuyo campo 'customerID' coincide con el valor en 'cajaTextoID'
                var index = customersBindingSource.Find("customerID", cajaTextoID);

                // Si se encuentra un elemento coincidente, establece la posición del BindingSource en ese índice
                if (index > -1)
                {
                    customersBindingSource.Position = index; // Cambia la posición del BindingSource al índice encontrado
                    return; // Sale del método, ya que no es necesario hacer más
                }
                else
                {
                    // Si no se encuentra el elemento, muestra un mensaje de error
                    MessageBox.Show("Elemento no encontrado");
                }
            }
        }
    }
}
