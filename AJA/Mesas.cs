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

namespace AJA
{
    public partial class Mesas : Form
    {

        OracleConnection conexion = new OracleConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

        public Mesas()
        {
            InitializeComponent();
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
                    comando.Parameters.Add("p_estado", OracleType.VarChar).Value = Convert.ToInt32(txtEstado.Text);
                    comando.Parameters.Add("p_ubicacion", OracleType.VarChar).Value = txtUbicacion.Text;
  
                    comando.ExecuteNonQuery();
                    conexion.Close();

                    txtNumMesa.Text = "";
                    txtCapacidad.Text = "";
                    txtTipo.Text = "";
                    txtEstado.Text = "";
                    txtUbicacion.Text = "";
                 
                }

            }
            catch (Exception)
            {

                MessageBox.Show("Lo siento,algo fallo");
            }
        }

        private void Mesas_Load(object sender, EventArgs e)
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

            conexion.Close();
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
                    conexion.Close();

                    txtUbicacion.Text = "";
                    txtNumMesa.Text = "";
                    txtTipo.Text = "";
                    txtEstado.Text = "";
                    txtCapacidad.Text = "";
                    
                }
            }
            catch (Exception)
           {
               MessageBox.Show("Lo siento,algo fallo");
           }
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
                    comando.Parameters.Add("p_estado", OracleType.VarChar).Value = Convert.ToInt32(txtEstado.Text);
                    comando.Parameters.Add("p_ubicacion", OracleType.VarChar).Value = txtUbicacion.Text;

                    comando.ExecuteNonQuery();
                    conexion.Close();

                    txtNumMesa.Text = "";
                    txtCapacidad.Text = "";
                    txtTipo.Text = "";
                    txtEstado.Text = "";
                    txtUbicacion.Text = "";

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lo siento,algo fallo");
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
