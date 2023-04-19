using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AJA
{
    public partial class Reservas : Form
    {

        OracleConnection conexion = new OracleConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);
        public Reservas()
        {
            InitializeComponent();
        }


        private void dgvReservaCombinado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Reservas_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAdministrarReservas_Click(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            conexion.Open();
            OracleCommand comando = new OracleCommand("mostrar_reserva_clientesss", conexion);
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.Add("reservas", OracleType.Cursor).Direction = ParameterDirection.Output;
            comando.Parameters.Add("PID", OracleType.VarChar).Value = txtID.Text;

            OracleDataAdapter adaptador = new OracleDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dgvReservas.DataSource = tabla;

            conexion.Close();

            txtID.Text = "";

        }

        private void btnTodos_Click(object sender, EventArgs e)
        {
            conexion.Open();
            OracleCommand comando = new OracleCommand("mostrar_reserva_clientesss", conexion);
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.Add("reservas", OracleType.Cursor).Direction = ParameterDirection.Output;
            comando.Parameters.Add("PID", OracleType.VarChar).Value = txtID.Text;

            OracleDataAdapter adaptador = new OracleDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dgvReservas.DataSource = tabla;

            conexion.Close();
        }
    }
}
