using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AJA
{
    public partial class Productos : Form
    {

        OracleConnection conexion = new OracleConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

        int indexRow;

        public Productos()
        {
            InitializeComponent();
        }

        private void Metodo1()
        {
            Productos_Load(this, EventArgs.Empty);
        }

        private void Productos_Load(object sender, EventArgs e)
        {
            conexion.Open();

            OracleCommand command = new OracleCommand("seleccionarPRODUCTO", conexion);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add("producto", OracleType.Cursor).Direction = ParameterDirection.Output;

            OracleDataAdapter adaptador = new OracleDataAdapter();
            adaptador.SelectCommand = command;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dataGridProductos.DataSource = tabla;

            conexion.Close();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {

            try
            {

                conexion.Open();

                OracleCommand command = new OracleCommand("INSERTAR_PRODUCTO", conexion);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.Add("ID", OracleType.Number).Value = Convert.ToInt32(txtID.Text);
                command.Parameters.Add("TIPO_PRODUCTO", OracleType.VarChar).Value = txtTipo.Text;
                command.Parameters.Add("COSTO", OracleType.Number).Value = Convert.ToInt32(txtcosto.Text);
                command.Parameters.Add("INVENTARIO_ID", OracleType.Number).Value = Convert.ToInt32(txtInventario.Text);

                command.ExecuteNonQuery();

                txtID.Text = "";
                txtTipo.Text = "";
                txtcosto.Text = "";
                txtInventario.Text = "";

            }

            catch (OracleException ex)
            {
                MessageBox.Show("Lo siento, el codigo del producto ya esta en uso/ codigo inventario no existente ");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error en el sistema, intenta de nuevo");
            }

            conexion.Close();

            Metodo1();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            try
            {

                conexion.Open();

                OracleCommand comando = new OracleCommand("ELIMINAR_PRODUCTO", conexion);

                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.Add("P_ID", OracleType.Number).Value = Convert.ToInt32(txtID.Text);

                comando.ExecuteNonQuery();

                txtID.Text = "";
            }
            catch (OracleException ex)
            {
                MessageBox.Show("Lo siento, el codigo del producto no existe");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error en el sistema, intenta de nuevo");
            }

            conexion.Close();

            Metodo1();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {

            try
            {
                DialogResult resul = MessageBox.Show("Seguro que quieres actualizar", "Actualizar", MessageBoxButtons.YesNo);
                if (resul == DialogResult.Yes)
                {
                    conexion.Open();
                    OracleCommand comando = new OracleCommand("actualizar_PRODUCTOS", conexion);
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.Add("P_ID", OracleType.Number).Value = Convert.ToInt32(txtID.Text);
                    comando.Parameters.Add("P_TIPO_PRODUCTO", OracleType.VarChar).Value = txtTipo.Text;
                    comando.Parameters.Add("P_COSTO", OracleType.Number).Value = Convert.ToInt32(txtcosto.Text);
                    comando.Parameters.Add("P_INVENTARIO_ID", OracleType.Number).Value = Convert.ToInt32(txtInventario.Text);


                    txtID.Text = "";
                    txtTipo.Text = "";
                    txtcosto.Text = "";
                    txtInventario.Text = "";

                    comando.ExecuteNonQuery();
                }
            }
            catch (OracleException ex)
            {
                MessageBox.Show("Lo siento, verifique el codigo del producto antes de actualizar nuevamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error en el sistema, intenta de nuevo");
            }

                conexion.Close();
                Metodo1();
  
        }

        private void dataGridProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Login Login = new Login();
            Login.Show();
        }

        private void MenuClientes_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ya te encuentras en esta parte del menú ");
        }

        private void MenuReservas_Click(object sender, EventArgs e)
        {
           
            Ordenes Ordenes = new Ordenes();
            Ordenes.Show();

            Productos Productos = new Productos();
            Productos.Close();
        }
    }
}
