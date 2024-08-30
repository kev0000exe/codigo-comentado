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
    public partial class Form1 : Form
    {
        // Constructor del formulario
        public Form1()
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

        // Otro método manejador para el mismo evento de clic en guardar
        private void customersBindingNavigatorSaveItem_Click_1(object sender, EventArgs e)
        {
            this.Validate(); // Valida los datos del formulario
            this.customersBindingSource.EndEdit(); // Finaliza cualquier edición en curso en el BindingSource
            this.tableAdapterManager.UpdateAll(this.northwindDataSet); // Actualiza todos los cambios realizados en el DataSet 'northwindDataSet'
        }

        // Otro método manejador para el mismo evento de clic en guardar
        private void customersBindingNavigatorSaveItem_Click_2(object sender, EventArgs e)
        {
            this.Validate(); // Valida los datos del formulario
            this.customersBindingSource.EndEdit(); // Finaliza cualquier edición en curso en el BindingSource
            this.tableAdapterManager.UpdateAll(this.northwindDataSet); // Actualiza todos los cambios realizados en el DataSet 'northwindDataSet'
        }

        // Evento que se activa cuando el formulario se carga
        private void Form1_Load(object sender, EventArgs e)
        {
            // Carga los datos en la tabla 'Customers' del DataSet 'northwindDataSet'
            // Esta línea puede ser movida o eliminada según sea necesario
            this.customersTableAdapter.Fill(this.northwindDataSet.Customers);
        }

        // Evento que se activa cuando se hace clic en una celda del DataGridView
        private void customersDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Aquí podrías agregar lógica para manejar el clic en las celdas del DataGridView
        }

        // Evento que se activa cuando se actualizan los ítems del BindingNavigator
        private void customersBindingNavigator_RefreshItems(object sender, EventArgs e)
        {
            // Aquí podrías agregar lógica para manejar la actualización de los ítems del BindingNavigator
        }
    }
}
