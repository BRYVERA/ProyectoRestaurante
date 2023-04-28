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
            try
            {
                conexion.Open();
                OracleCommand comando = new OracleCommand("mostrar_reservas", conexion);
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

            catch (OracleException ex)
            {
                MessageBox.Show("Lo siento, no se encuentran los datos disponibles");
            }
            catch (Exception ex)
            {
                MessageBox.Show ("Ocurrio un error en el sistema, intenta de nuevo");
            }

            try
            {
                conexion.Open();
            OracleCommand comandos = new OracleCommand("mostrar_auditoria_reserva", conexion);
            comandos.CommandType = System.Data.CommandType.StoredProcedure;
            comandos.Parameters.Add("auditoria", OracleType.Cursor).Direction = ParameterDirection.Output;

            OracleDataAdapter adaptador2 = new OracleDataAdapter();
            adaptador2.SelectCommand = comandos;
            DataTable tablas = new DataTable();
            adaptador2.Fill(tablas);
            data2.DataSource = tablas;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error en el sistema, intenta de nuevo");
            }

            conexion.Close();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAdministrarReservas_Click(object sender, EventArgs e)
        {
            AdministracionReservas AdministracionReservas = new AdministracionReservas();
            AdministracionReservas.Show();

            Reservas Reservas = new Reservas();
            Reservas.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            try
            { 
            conexion.Open();
            OracleCommand comando = new OracleCommand("mostrar_reservas", conexion);
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
            catch (OracleException ex)
            {
                MessageBox.Show("Lo siento, la reserva no se encuntra en nuestro sistema");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error en el sistema, intenta de nuevo");
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ya te encuentras en esta parte del menú ");
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Clientes Clientes = new Clientes();
            Clientes.Show();

            Reservas Reservas = new Reservas();
            Reservas.Close();

        }

        private void label4_Click(object sender, EventArgs e)
        {
            Mesas Mesas = new Mesas();
            Mesas.Show();

            Reservas Reservas = new Reservas();
            Reservas.Close();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Login Login = new Login();
            Login.Show();
        }
    }
}

