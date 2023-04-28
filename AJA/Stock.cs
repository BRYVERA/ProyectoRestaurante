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
    public partial class Stock : Form
    {

  OracleConnection conexion = new OracleConnection("DATA SOURCE=XE;PASSWORD=123;USER ID=TESTER;");
        public Stock()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        //Mostrar
        private void btnMostrarStock_Click(object sender, EventArgs e)
        {
            try
            {
                conexion.Open();

                OracleCommand comando = new OracleCommand("read_stocks", conexion);
                comando.Connection = conexion;
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.Add("p_result_stock", OracleType.Cursor).Direction = ParameterDirection.Output;
                OracleDataAdapter adaptador = new OracleDataAdapter();
                adaptador.SelectCommand = comando;
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dgvStocks.DataSource = tabla;
                conexion.Close();
            }
            catch (Exception)
            {

                MessageBox.Show("Lo siento,algo fallo");
            }
        }

        //Registrar
        private void btnRegistrarStock_Click(object sender, EventArgs e)
        {

            try
            {
                conexion.Open();
                    OracleCommand comando = new OracleCommand("ADD_STOCK", conexion);
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.Add("P_INVENTARIO_ID", OracleType.Number).Value = Convert.ToInt32(txtInventarioID.Text);
            comando.Parameters.Add("P_TIPO_PRODUCTO", OracleType.VarChar).Value = txtTipoDeProducto.Text;
                    comando.Parameters.Add("P_STOCK_INICIAL", OracleType.Number).Value = Convert.ToInt32(txtStockInicial.Text);
                    comando.Parameters.Add("P_DESCRIPCION", OracleType.VarChar).Value = txtDescripcion.Text;
                    comando.Parameters.Add("P_STOCK_TOTAL", OracleType.Number).Value = Convert.ToInt32(txtStockTotal.Text);

                    comando.ExecuteNonQuery();
                    conexion.Close();
            MessageBox.Show("Registro exitoso");

            txtInventarioID.Text = "";
                    txtStockInicial.Text = "";
                    txtDescripcion.Text = "";
                    txtStockTotal.Text = "";
                    txtTipoDeProducto.Text = "";

            }
            catch (Exception)
            {
                MessageBox.Show("Lo siento,algo fallo");
            }


        }
        //Eliminar
        private void btnEliminarStock_Click(object sender, EventArgs e)
        {
            try
            {

                DialogResult resul = MessageBox.Show("Seguro que quiere eliminar el Registro?", "Eliminar Registro", MessageBoxButtons.YesNo);
                if (resul == DialogResult.Yes)
                {
                    conexion.Open();
                    OracleCommand comando = new OracleCommand("DELETE_STOCK", conexion);
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.Add("P_INVENTARIO_ID", OracleType.Number).Value = Convert.ToInt32(txtInventarioID.Text);
                    comando.ExecuteNonQuery();
                    conexion.Close();
                    txtInventarioID.Text = "";
                    txtStockInicial.Text = "";
                    txtDescripcion.Text = "";
                    txtStockTotal.Text = "";
                    txtTipoDeProducto.Text = "";

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lo siento,algo fallo");
            }
        }
        //Actualizar
        private void btnActualizarStock_Click(object sender, EventArgs e)
        {
            try
            {
                conexion.Open();
            OracleCommand comando = new OracleCommand("UPDATE_STOCK", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add("P_INVENTARIO_ID", OracleType.Number).Value = Convert.ToInt32(txtInventarioID.Text);
            comando.Parameters.Add("P_TIPO_PRODUCTO", OracleType.VarChar).Value = txtTipoDeProducto.Text;
            comando.Parameters.Add("P_STOCK_INICIAL", OracleType.Number).Value = Convert.ToInt32(txtStockInicial.Text);
            comando.Parameters.Add("P_DESCRIPCION", OracleType.VarChar).Value = txtDescripcion.Text;
            comando.Parameters.Add("P_STOCK_TOTAL", OracleType.Number).Value = Convert.ToInt32(txtStockTotal.Text);

            comando.ExecuteNonQuery();
            conexion.Close();
            MessageBox.Show(" Actualizacion exitosa");


            txtInventarioID.Text = "";
                    txtStockInicial.Text = "";
                    txtDescripcion.Text = "";
                    txtStockTotal.Text = "";
                    txtTipoDeProducto.Text = "";
            }
            catch (Exception)
            {
                MessageBox.Show("Lo siento,algo fallo");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            reservaID Stock = new reservaID();
            Stock.Show();
            Stock reporte = new Stock();
            reporte.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ya estas en esta parte del menu");

        }

        private void label4_Click(object sender, EventArgs e)
        {
            reporteHorario Stock = new reporteHorario();
            Stock.Show();
            Stock reporte = new Stock();
            reporte.Close();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Login Login = new Login();
            Login.Show();
        }
    }
    }
    
    

