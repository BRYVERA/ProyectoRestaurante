using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AJA
{
    public partial class reporteHorario : Form
    {
        OracleConnection conexion = new OracleConnection("DATA SOURCE=ORCLL;PASSWORD=123;USER ID=TESTER;");

        public reporteHorario()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        //Mostrar
        private void btnMostrarReporteHorario_Click(object sender, EventArgs e)
        {
            try
            {
                conexion.Open();

                OracleCommand comando = new OracleCommand("read_reportes_horario", conexion);
                comando.Connection = conexion;
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.Add("p_result_horario", OracleType.Cursor).Direction = ParameterDirection.Output;
                OracleDataAdapter adaptador = new OracleDataAdapter();
                adaptador.SelectCommand = comando;
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dgvReportesHorario.DataSource = tabla;
                conexion.Close();
            }
            catch (Exception)
            {

                MessageBox.Show("Lo siento,algo fallo");
            }
        }
        //Registrar
        private void btnRegistrarReporteHorario_Click(object sender, EventArgs e)
        {
            conexion.Open();
            OracleCommand comando = new OracleCommand("insert_reporte_horario", conexion);
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.Add("p_reporte_horario_id", OracleType.Number).Value = Convert.ToInt32(txtID.Text);
            comando.Parameters.Add("p_horario_id", OracleType.Number).Value = Convert.ToInt32(txtHorarioID.Text);
         

            comando.ExecuteNonQuery();
            conexion.Close();
            MessageBox.Show("Registro exitoso");

            txtID.Text = "";
            txtHorarioID.Text = "";
        }
        //Eliminar
        private void btnEliminarReporteHorario_Click(object sender, EventArgs e)
        {
         
                    conexion.Open();
                    OracleCommand comando = new OracleCommand("delete_reporte_horario", conexion);
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.Add("p_reporte_horario_id", OracleType.Number).Value = Convert.ToInt32(txtID.Text);
                    comando.ExecuteNonQuery();
                    conexion.Close();
                    MessageBox.Show("Eliminado con exito");

                    txtID.Text = "";
                    txtHorarioID.Text = "";

            }
        //Actualizar

        private void btnActualizarReporteHorario_Click(object sender, EventArgs e)
        {
            conexion.Open();
            OracleCommand comando = new OracleCommand("UPDATE_REPORTE_HORARIO", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add("P_REPORTE_HORARIO", OracleType.Number).Value = Convert.ToInt32(txtID.Text);
            comando.Parameters.Add("P_HORARIO_ID", OracleType.Number).Value = Convert.ToInt32(txtHorarioID.Text);


            comando.ExecuteNonQuery();
            conexion.Close();
            MessageBox.Show(" Actualizacion exitosa");
            txtID.Text = "";
            txtHorarioID.Text = "";
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Stock Stock = new Stock();
            Stock.Show();
            reporteHorario reporte = new reporteHorario();
            reporte.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            reservaID Stock = new reservaID();
            Stock.Show();
            reporteHorario reporte = new reporteHorario();
            reporte.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ya estas en esta parte del menu");

        }

        private void label1_Click(object sender, EventArgs e)
        {
            reservaID Stock = new reservaID();
            Stock.Show();
            reporteHorario reporte = new reporteHorario();
            reporte.Close();

        }
    }
    }
    

