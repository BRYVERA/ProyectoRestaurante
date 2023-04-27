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
    public partial class Mesas : Form
    {

        int indexRow;

        OracleConnection conexion = new OracleConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

        public Mesas()
        {
            InitializeComponent();
        }

        private void Metodo1()
        {
            Mesas_Load(this, EventArgs.Empty);
        }

        private void MetodoLimpiar()
        {
            txtNumMesa.Text = "";
            txtCapacidad.Text = "";
            txtTipo.Text = "";
            txtEstado.Text = "";
            txtUbicacion.Text = "";
        }

        private void Mesas_Load(object sender, EventArgs e)
        {

            try
            {
                conexion.Open();
                OracleCommand comandos = new OracleCommand("mostrar_mesas", conexion);
                comandos.CommandType = System.Data.CommandType.StoredProcedure;
                comandos.Parameters.Add("registros", OracleType.Cursor).Direction = ParameterDirection.Output;

                OracleDataAdapter adaptador = new OracleDataAdapter();
                adaptador.SelectCommand = comandos;
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dgvMesas.DataSource = tabla;

            }

            catch (OracleException ex)
            {
                MessageBox.Show("Lo siento, los datos de las mesas no estan disponibles");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error en el sistema, intenta de nuevo");
            }

            conexion.Close();
        }

        private void btnReservar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult resul = MessageBox.Show("Seguro que quieres registrar la mesa", "Registro", MessageBoxButtons.YesNo);
                if (resul == DialogResult.Yes)
                {
                    conexion.Open();
                    OracleCommand comando = new OracleCommand("registrar_mesa", conexion);
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.Add("p_mesa_id", OracleType.VarChar).Value = Convert.ToInt32(txtNumMesa.Text);
                    comando.Parameters.Add("p_capacidad", OracleType.VarChar).Value = Convert.ToInt32(txtCapacidad.Text);
                    comando.Parameters.Add("p_tipo", OracleType.VarChar).Value = txtTipo.Text;
                    comando.Parameters.Add("p_estado", OracleType.VarChar).Value = comboBox1.Text;
                    comando.Parameters.Add("p_ubicacion", OracleType.VarChar).Value = txtUbicacion.Text;

                    comando.ExecuteNonQuery();
                    conexion.Close();
                    MetodoLimpiar();

                }

            }
            catch (OracleException ex)
            {
                MessageBox.Show("Mesa ya se encuntra registrada");
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

                DialogResult resul = MessageBox.Show("Seguro que quiere eliminar la mesa?", "Eliminar Mesa", MessageBoxButtons.YesNo);
                if (resul == DialogResult.Yes)
                {
                    conexion.Open();
                    OracleCommand comando = new OracleCommand("borrar_mesas", conexion);
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.Add("P_MESA_ID", OracleType.Number).Value = Convert.ToInt32(txtNumMesa.Text); ;
                    comando.ExecuteNonQuery();


                    MetodoLimpiar();

                }
            }
            catch (OracleException ex)
            {
                MessageBox.Show("Registro no encontrado");
                MessageBox.Show("Ingrese # Mesa ");
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

                DialogResult resul = MessageBox.Show("Seguro que quiere actualizar la mesa?", "Actualizar", MessageBoxButtons.YesNo);
                if (resul == DialogResult.Yes)
                {
                    conexion.Open();
                    OracleCommand comando = new OracleCommand("actualizar_mesa", conexion);
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.Add("p_mesa_id", OracleType.VarChar).Value = Convert.ToInt32(txtNumMesa.Text);
                    comando.Parameters.Add("p_capacidad", OracleType.VarChar).Value = Convert.ToInt32(txtCapacidad.Text);
                    comando.Parameters.Add("p_tipo", OracleType.VarChar).Value = txtTipo.Text;
                    comando.Parameters.Add("p_estado", OracleType.VarChar).Value = comboBox1.Text;
                    comando.Parameters.Add("p_ubicacion", OracleType.VarChar).Value = txtUbicacion.Text;

                    comando.ExecuteNonQuery();
                    conexion.Close();

                    MetodoLimpiar();

                }
            }
            catch (OracleException ex)
            {
                MessageBox.Show("Verifique el numero que desea modificar");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error en el sistema, intenta de nuevo");
            }

            conexion.Close();
            Metodo1();
        }

        private void dgvMesas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

           try
            {

                indexRow = e.RowIndex;
                DataGridViewRow row = dgvMesas.Rows[indexRow];
                txtNumMesa.Text = row.Cells[0].Value.ToString();
                txtCapacidad.Text = row.Cells[1].Value.ToString();
                txtTipo.Text = row.Cells[2].Value.ToString();
                string valor = row.Cells[3].Value.ToString();
                comboBox1.Items.Add(valor);
                txtUbicacion.Text = row.Cells[4].Value.ToString();
            }
            catch
            {
                MessageBox.Show("Lo siento, error al enviar los datos de la tabla");
            }

}

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ya te encuentras en esta parte del menú ");
        }

        private void label3_Click(object sender, EventArgs e)
        {

            Reservas Reservas = new Reservas();
            Reservas.Show();

            Mesas Mesas = new Mesas();
            Mesas.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Clientes Clientes = new Clientes();
            Clientes.Show();

            Mesas Mesas = new Mesas();
            Mesas.Close();

        }
    }
}
