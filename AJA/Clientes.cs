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
        int Number;

        private void Metodo1()
        {
            Form1_Load(this, EventArgs.Empty);
        }

        //CARGAR LOS DATOS DE LOS CLIENTES POR MEDIO DE UN CURSOR
        private void Form1_Load(object sender, EventArgs e)
        {
            try
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
               
            }

            catch (OracleException ex)
            {
                MessageBox.Show("Lo siento, los datos de los clientes no estan disponibles");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error en el sistema, intenta de nuevo");
            }

            conexion.Close();

            try
            {

                conexion.Open();

                OracleCommand cmd = new OracleCommand("CantidadClientes", conexion);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("RETURN_VALUE", OracleType.Int32).Direction = ParameterDirection.ReturnValue;

                cmd.ExecuteNonQuery();

                int valorRetorno = (int)cmd.Parameters["RETURN_VALUE"].Value;

                textBox1.Text = valorRetorno.ToString();
            }

            catch (OracleException ex)
            {
                MessageBox.Show("Lo siento, los datos de los clientes no estan disponibles");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error en el sistema, intenta de nuevo");
            }

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
               

                    txtID.Text = "";
                    txtNombre.Text = "";
                    txtPrimerApellido.Text = "";
                    txtSegundoApellido.Text = "";
                    txtEmail.Text = "";
                    txtTelefono.Text = "";
                }

            }
           
                catch (OracleException ex)
                {
                        MessageBox.Show("El cliente ya se encuentra registrado");
                }
                catch (Exception ex)
                {
                         MessageBox.Show("Ocurrio un error en el sistema, intenta de nuevo");
            }

           conexion.Close();

            Metodo1();

        }


        // OBTENGO EL ID DEL CLIENTE QUE SE DESEA ELIMINAR

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
                   

                    txtID.Text = "";
                    txtNombre.Text = "";
                    txtPrimerApellido.Text = "";
                    txtSegundoApellido.Text = "";
                    txtEmail.Text = "";
                    txtTelefono.Text = "";
                }
            }
            catch (OracleException ex)
            {
                MessageBox.Show("Registro del cliente no encontrado");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error en el sistema, intenta de nuevo");
            }

            conexion.Close();
            Metodo1();
        }

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
            catch (OracleException ex)
            {
                MessageBox.Show("Registro no encontrado");
                MessageBox.Show("Ingrese una identificación válida ");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error en el sistema, intenta de nuevo");
            }


            conexion.Close();
            Metodo1();
        }


        // OBTENGO EL VALOR DE CADA FILA Y LA ALMACENO EN EL TEXBOX 
        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                indexRow = e.RowIndex;
                DataGridViewRow row = dgvClientes.Rows[indexRow];
                txtID.Text = row.Cells[0].Value.ToString();
                txtPrimerApellido.Text = row.Cells[2].Value.ToString();
                txtSegundoApellido.Text = row.Cells[3].Value.ToString();
                txtEmail.Text = row.Cells[4].Value.ToString();
                txtTelefono.Text = row.Cells[5].Value.ToString();
                txtNombre.Text = row.Cells[1].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("La carga de los datos esta durando mas de lo esperado...., ");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ya te encuentras en esta parte del menú ");
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Reservas Reservas = new Reservas();
            Reservas.Show();

            Clientes Clientes = new Clientes();
            Clientes.Close();

        
        }

        private void MenuMesas_Click(object sender, EventArgs e)
        {
            Mesas Mesas = new Mesas();
            Mesas.Show();

            Clientes Clientes = new Clientes();
            Clientes.Close();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Login Login = new Login();
            Login.Show();
           
        }
    }
}
