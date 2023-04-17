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
    public partial class AdministracionReservas : Form
    {
        OracleConnection conexion = new OracleConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

        int valorRetorno;
        int valorRetornoDos;

        public AdministracionReservas()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AdministracionReservas_Load(object sender, EventArgs e)
        {
            // Cargo las mesas que se encuntran desocupadas

            conexion.Open();
            OracleCommand comando = new OracleCommand("listar_mesas_disponibles", conexion);
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.Add("mesas_disponibles", OracleType.Cursor).Direction = ParameterDirection.Output;

            using (OracleDataReader reader = comando.ExecuteReader())
            {
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader["mesa_id"].ToString());
                }
            }

            conexion.Close();

            // Cargar Datagridview // 

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Delete una reserva

            conexion.Open();

            OracleCommand comando = new OracleCommand("eliminar_reserva", conexion);

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.Add("p_identificacion", OracleType.VarChar).Value = txtID.Text;

            comando.Parameters.Add("RETURN_VALUE", OracleType.Int32).Direction = ParameterDirection.ReturnValue;

            comando.ExecuteNonQuery();

            valorRetornoDos = (int)comando.Parameters["RETURN_VALUE"].Value;

            conexion.Close();

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            // Update una reserva

            conexion.Open();
            OracleCommand command = new OracleCommand("editar_reserva", conexion);


            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("p_reserva_id", OracleType.Int32).Value = txtNumReserva.Text;
            command.Parameters.Add("p_hora", OracleType.VarChar).Value = txtHora.Text;
            command.Parameters.Add("p_fecha", OracleType.VarChar).Value = txtFecha.Text;  
            command.Parameters.Add("p_mesa_id", OracleType.Int32).Value = txtMesa.Text;
            command.Parameters.Add("p_identificacion", OracleType.Number).Value = txtID.Text;

            command.Parameters.Add("RETURN_VALUE", OracleType.Int32).Direction = ParameterDirection.ReturnValue;

             command.ExecuteNonQuery();

            valorRetorno = (int)command.Parameters["RETURN_VALUE"].Value;

            conexion.Close();

        }

        private void btnReservar_Click(object sender, EventArgs e)
        {

        }
    }
    
}

