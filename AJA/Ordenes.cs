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
    public partial class Ordenes : Form
    {

        OracleConnection conexion = new OracleConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

        int indexRow;
        public Ordenes()
        {
            InitializeComponent();
        }

        private void Metodo1()
        {
            Ordenes_Load(this, EventArgs.Empty);
        }

        private void Ordenes_Load(object sender, EventArgs e)
        {
            conexion.Open();

            OracleCommand command = new OracleCommand("seleccionarORDEN", conexion);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add("orden", OracleType.Cursor).Direction = ParameterDirection.Output;

            OracleDataAdapter adaptador = new OracleDataAdapter();
            adaptador.SelectCommand = command;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dataGridOrden.DataSource = tabla;

            conexion.Close();

            conexion.Open();

            OracleCommand comando = new OracleCommand("listar_mesas_orden", conexion);
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.Add("mesas_orden", OracleType.Cursor).Direction = ParameterDirection.Output;

            using (OracleDataReader reader = comando.ExecuteReader())
            {
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader["mesa_id"].ToString());
                }
            }

            conexion.Close();

        }


        private void btnRegistrarClientes_Click(object sender, EventArgs e)
        {

 
            conexion.Open();

            OracleCommand command = new OracleCommand("insertar_orden", conexion);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.Add("detalle_productos", OracleType.VarChar).Value = txtDetalle.Text;
            command.Parameters.Add("fecha_creacion", OracleType.DateTime).Value = DateTime.Now.ToString("dd-MMM-yyyy");
            command.Parameters.Add("mesa", OracleType.Number).Value = Convert.ToInt32(comboBox1.Text);

            command.ExecuteNonQuery();

            conexion.Close();

            Metodo1();
        }

        private void btnEliminarClientes_Click(object sender, EventArgs e)
        {
            conexion.Open();

            OracleCommand comando = new OracleCommand("SP_ELIMINAR_ORDEN", conexion);

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.Add("P_ID", OracleType.Number).Value = txtID.Text;

            comando.ExecuteNonQuery();


            conexion.Close();

            Metodo1();

        }

        private void btnActualizarCliente_Click(object sender, EventArgs e)
        {
            DialogResult resul = MessageBox.Show("Seguro que quieres actualizar", "Actualizar", MessageBoxButtons.YesNo);
            if (resul == DialogResult.Yes)
            {
                conexion.Open();
                OracleCommand comando = new OracleCommand("actualizar_orden", conexion);
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.Add("P_ID", OracleType.Number).Value = Convert.ToInt32(txtID.Text);
                comando.Parameters.Add("P_DETALLE_PRODUCTOS", OracleType.VarChar).Value = txtDetalle.Text;
                comando.Parameters.Add("P_MESA", OracleType.Number).Value = Convert.ToInt32(comboBox1.Text);

                comando.ExecuteNonQuery();
           
                txtID.Text = "";
                txtDetalle.Text = "";
                comboBox1.Text = "";


                conexion.Close();
                Metodo1();

            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            indexRow = e.RowIndex;
            DataGridViewRow row = dataGridOrden.Rows[indexRow];
            txtID.Text = row.Cells[0].Value.ToString();
            txtDetalle.Text = row.Cells[1].Value.ToString();
           
        }
    }
}
