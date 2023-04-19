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
    public partial class reservaID : Form
    {
        OracleConnection conexion = new OracleConnection("DATA SOURCE=ORCLL;PASSWORD=123;USER ID=TESTER;");
        public reservaID()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ya estas en esta parte del menu");

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        //Registrar un nuevo reporte
        private void btnRegistrarReportesMesa_Click(object sender, EventArgs e)
        {

           try
           {
                conexion.Open();
            OracleCommand comando = new OracleCommand("create_reporte_mesa", conexion);
            comando.Connection = conexion;
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.Add("p_reporte_id", OracleType.Number).Value = Convert.ToInt32(txtReporteID.Text);
            comando.Parameters.Add("p_fecha", OracleType.VarChar).Value = txtFecha.Text;
            comando.Parameters.Add("p_identificacion_cliente", OracleType.Number).Value = Convert.ToInt32(txtIDCliente.Text);
            comando.Parameters.Add("p_reserva_id", OracleType.Number).Value = Convert.ToInt32(txtReservaID.Text);
            comando.Parameters.Add("p_hora", OracleType.VarChar).Value = txtHora.Text;
            comando.ExecuteNonQuery();
            conexion.Close();
            MessageBox.Show("Registro exitoso");

            txtFecha.Text = "";
            txtIDCliente.Text = "";
            txtReporteID.Text = "";
            txtHora.Text = "";
            txtReservaID.Text = "";

            }
            catch (Exception)
            {

                MessageBox.Show("Lo siento,algo fallo");
            }

        }

        //Mostrar Reportes

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conexion.Open();

            OracleCommand comando = new OracleCommand("read_reporte_mesa", conexion);
                            comando.Connection = conexion;
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.Add("p_result", OracleType.Cursor).Direction= ParameterDirection.Output;
            OracleDataAdapter adaptador = new OracleDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dgvReportesMesa.DataSource = tabla;
            conexion.Close();
            }
            catch (Exception)
            {

                MessageBox.Show("Lo siento,algo fallo");
            }
        }

        //Eliminar Reporte
        private void btnEliminarReportesMesa_Click(object sender, EventArgs e)
        {
            try
            {

                DialogResult resul = MessageBox.Show("Seguro que quiere eliminar el Reporte?", "Eliminar Reporte", MessageBoxButtons.YesNo);
                if (resul == DialogResult.Yes)
                {
                    conexion.Open();
                    OracleCommand comando = new OracleCommand("delete_reporte_mesa", conexion);
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.Add("p_reporte_reserva_id", OracleType.Number).Value = Convert.ToInt32(txtReporteID.Text);
                    comando.ExecuteNonQuery();
                    conexion.Close();

                    txtFecha.Text = "";
                    txtIDCliente.Text = "";
                    txtReporteID.Text = "";
                    txtHora.Text = "";
                    txtReservaID.Text = "";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lo siento,algo fallo");
            }
        }

        //Actualizar 

        private void btnActualizarReportesMesa_Click(object sender, EventArgs e)
        {
            try
            {
                conexion.Open();
                OracleCommand comando = new OracleCommand("actualizar", conexion);
                comando.Connection = conexion;
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.Add("p_reporte_id ", OracleType.Number).Value = Convert.ToInt32(txtReporteID.Text);
                comando.Parameters.Add("p_fecha", OracleType.VarChar).Value = txtFecha.Text;
                comando.Parameters.Add("p_identificacion_cliente", OracleType.Number).Value = Convert.ToInt32(txtIDCliente.Text);
                comando.Parameters.Add("p_reserva_id", OracleType.Number).Value = Convert.ToInt32(txtReservaID.Text);
                comando.Parameters.Add("p_hora", OracleType.VarChar).Value = txtHora.Text;


                comando.ExecuteNonQuery();

                conexion.Close();
                MessageBox.Show("Registro actualizado");

                txtFecha.Text = "";
                txtIDCliente.Text = "";
                txtReporteID.Text = "";
                txtHora.Text = "";
                txtReservaID.Text = "";

            }
            catch (Exception)
            {

                MessageBox.Show("Lo siento,algo fallo");
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Stock Stock = new Stock();
            Stock.Show();
            reservaID reporte = new reservaID();
            reporte.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            reporteHorario Stock = new reporteHorario();
            Stock.Show();
            reservaID reporte = new reservaID();
            reporte.Close();
        }
    }

    }
