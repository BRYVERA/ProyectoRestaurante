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
    public partial class Clientes : Form
    {
       
        OracleConnection conexion = new OracleConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

        public Clientes()
        {
            InitializeComponent();

        }

        int indexRow;

        //CARGAR LOS DATOS DE LOS CLIENTES POR MEDIO DE UN CURSOR
        private void Form1_Load(object sender, EventArgs e)
        {
            conexion.Open();
            OracleCommand comando = new OracleCommand("mostrar_clientes", conexion);
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.Add("registros", OracleType.Cursor).Direction = ParameterDirection.Output;

            OracleDataAdapter adaptador = new OracleDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dgvClientes.DataSource = tabla;
            conexion.Close();
        }

            //BOTON REGISTRO DE LOS CLIENTES POR SP

        private void btnCargar_Click(object sender, EventArgs e)
        {

            try
            {
                DialogResult resul = MessageBox.Show("Seguro que quieres registrar", "Registro", MessageBoxButtons.YesNo);
                if (resul == DialogResult.Yes)
                {
                    conexion.Open();
                    OracleCommand comando = new OracleCommand("registrar_clientes", conexion);
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.Add("P_IDENTIFICACION", OracleType.VarChar).Value = txtID.Text;
                    comando.Parameters.Add("P_PRIMER_APELLIDO", OracleType.VarChar).Value = txtPrimerApellido.Text;
                    comando.Parameters.Add("P_SEGUNDO_APELLIDO", OracleType.VarChar).Value = txtSegundoApellido.Text;
                    comando.Parameters.Add("P_EMAIL", OracleType.VarChar).Value = txtEmail.Text;
                    comando.Parameters.Add("P_TELEFONO", OracleType.Number).Value = Convert.ToInt32(txtTelefono.Text);
                    comando.Parameters.Add("P_NOMBRE", OracleType.VarChar).Value = txtNombre.Text;
                    comando.ExecuteNonQuery();
                    conexion.Close();

                    txtID.Text = "";
                    txtNombre.Text = "";
                    txtPrimerApellido.Text = "";
                    txtSegundoApellido.Text = "";
                    txtEmail.Text = "";
                    txtTelefono.Text = "";
                }




            }
            catch (Exception)
            {

                MessageBox.Show("Lo siento,algo fallo");
            }


        }


        // OBTENGO EL VALOR DE CADA FILA Y LA ALMACENO EN EL TEXBOX 
        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            indexRow = e.RowIndex;
            DataGridViewRow row = dgvClientes .Rows[indexRow];
            txtID.Text = row.Cells[0].Value.ToString();
            txtPrimerApellido.Text = row.Cells[2].Value.ToString();
            txtSegundoApellido.Text = row.Cells[3].Value.ToString();
            txtEmail.Text = row.Cells[4].Value.ToString();
            txtTelefono.Text = row.Cells[5].Value.ToString();
            txtNombre.Text = row.Cells[1].Value.ToString();

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        // OBTENGO EL ID DEL CLIENTE QUE SE DESEA ELIMINAR
        private void btnEliminarClientes_Click(object sender, EventArgs e)
        {
            

            try
            {

                DialogResult resul = MessageBox.Show("Seguro que quiere eliminar el Registro?", "Eliminar Registro", MessageBoxButtons.YesNo);
                if (resul == DialogResult.Yes)
                {
                    conexion.Open();
                    OracleCommand comando = new OracleCommand("borrar_clientes", conexion);
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.Add("P_IDENTIFICACION", OracleType.VarChar).Value = txtID.Text;
                    comando.ExecuteNonQuery();
                    conexion.Close();

                    txtID.Text = "";
                    txtNombre.Text = "";
                    txtPrimerApellido.Text = "";
                    txtSegundoApellido.Text = "";
                    txtEmail.Text = "";
                    txtTelefono.Text = "";
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Lo siento,algo fallo");
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void btnActualizarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult resul = MessageBox.Show("Seguro que quieres actualizar", "Actualizar", MessageBoxButtons.YesNo);
                if (resul == DialogResult.Yes)
                {
                    conexion.Open();
                    OracleCommand comando = new OracleCommand("actualizar_clientes", conexion);
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.Add("P_IDENTIFICACION", OracleType.VarChar).Value = txtID.Text;
                    comando.Parameters.Add("P_PRIMER_APELLIDO", OracleType.VarChar).Value = txtPrimerApellido.Text;
                    comando.Parameters.Add("P_SEGUNDO_APELLIDO", OracleType.VarChar).Value = txtSegundoApellido.Text;
                    comando.Parameters.Add("P_EMAIL", OracleType.VarChar).Value = txtEmail.Text;
                    comando.Parameters.Add("P_TELEFONO", OracleType.Number).Value = Convert.ToInt32(txtTelefono.Text);
                    comando.Parameters.Add("P_NOMBRE", OracleType.VarChar).Value = txtNombre.Text;
                    comando.ExecuteNonQuery();
                    conexion.Close();

                    txtID.Text = "";
                    txtNombre.Text = "";
                    txtPrimerApellido.Text = "";
                    txtSegundoApellido.Text = "";
                    txtEmail.Text = "";
                    txtTelefono.Text = "";
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Lo siento,algo fallo");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Reservas Reservas = new Reservas();
            Reservas.Show();
            Clientes Clientes = new Clientes();
            Clientes.Close();
        }
    }
}
