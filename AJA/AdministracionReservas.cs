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
        int indexRow;
        bool status;

        public AdministracionReservas()
        {
            InitializeComponent();
        }

        private void Metodo1()
        {
            AdministracionReservas_Load(this, EventArgs.Empty);
        }

        private void AdministracionReservas_Load(object sender, EventArgs e)
        {
            try
            {
                conexion.Open();
                OracleCommand comandos = new OracleCommand("mostrar_reserva_cliente", conexion);
                comandos.CommandType = System.Data.CommandType.StoredProcedure;
                comandos.Parameters.Add("reservas", OracleType.Cursor).Direction = ParameterDirection.Output;


                OracleDataAdapter adaptador = new OracleDataAdapter();
                adaptador.SelectCommand = comandos;
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dgvAdminReserva.DataSource = tabla;

                // conexion.Close();


                // Listo
                // Cargo las mesas que se encuntran unicamente libres en un combobox
                // Listo

                //conexion.Open();
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
            }
            catch (OracleException ex)
            {
                MessageBox.Show("Lo siento, los datos de la reserva no estan disponibles");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error en el sistema al cargar los datos, intenta de nuevo");
            }

            conexion.Close();
        }


        private void btnReservar_Click(object sender, EventArgs e)
        {
           

            try
          {
                status = false;

                DialogResult resul = MessageBox.Show("Seguro que quieres registrar", "Registro", MessageBoxButtons.YesNo);
                if (resul == DialogResult.Yes)
                {
                    conexion.Open();
                    OracleCommand comando = new OracleCommand("agregar_reserva", conexion);
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.Add("P_FECHA", OracleType.VarChar).Value = dateTimePicker1.Value.ToString("dd-MMM-yyyy");
                    comando.Parameters.Add("P_MESA_ID", OracleType.Number).Value = Convert.ToInt32(comboBox1.Text);
                    comando.Parameters.Add("P_IDENTIFICACION", OracleType.VarChar).Value = txtID.Text;
                    comando.Parameters.Add("P_HORA", OracleType.VarChar).Value = txtHora.Text;

                    comando.ExecuteNonQuery();
                    status = true;

                    if (status == true)
                    {
                        OracleCommand comand = new OracleCommand("cambiar_estado_mesa", conexion);
                        comand.CommandType = System.Data.CommandType.StoredProcedure;
                        comand.Parameters.Add("P_MESA_ID", OracleType.Number).Value = Convert.ToInt32(comboBox1.Text);
                        comand.Parameters.Add("p_estado", OracleType.VarChar).Value = "Ocupado";

                        comand.ExecuteNonQuery();
                    }

                    txtID.Text = "";
                    txtHora.Text = "";
                    comboBox1.Text = "";
                    comboBox1.Items.Clear();
                }

             }

                catch (OracleException ex)
            {
                MessageBox.Show("El cliente ya se encuentra registrado o mesa no disponible");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            conexion.Close();
            Metodo1();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Listo
            // Delete una reserva
            // Listo
           

            try
            {
                status = false;

                DialogResult resul = MessageBox.Show("Seguro que quieres eliminar", "Eliminar", MessageBoxButtons.YesNo);
                if (resul == DialogResult.Yes)
                {

                    conexion.Open();

                    OracleCommand comando = new OracleCommand("eliminar_reserva", conexion);

                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("p_reserva_id", OracleType.Number).Value = txtNumReserva.Text;

                    comando.Parameters.Add("RETURN_VALUE", OracleType.Int32).Direction = ParameterDirection.ReturnValue;

                    comando.ExecuteNonQuery();

                    valorRetornoDos = (int)comando.Parameters["RETURN_VALUE"].Value;

                    status = true;

                    if (status == true)
                    {
                        OracleCommand comand = new OracleCommand("cambiar_estado_mesa", conexion);
                        comand.CommandType = System.Data.CommandType.StoredProcedure;
                        comand.Parameters.Add("P_MESA_ID", OracleType.Number).Value = Convert.ToInt32(comboBox1.Text);
                        comand.Parameters.Add("p_estado", OracleType.VarChar).Value = "Libre";

                        comand.ExecuteNonQuery();
                    }

                    txtID.Text = "";
                    txtHora.Text = "";
                    comboBox1.Text = "";
                    comboBox1.Items.Clear();
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

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            // LISTO
            // Update una reserva
            try
            {
                DialogResult resul = MessageBox.Show("Seguro que quieres actualizar", "Actualizar", MessageBoxButtons.YesNo);
                if (resul == DialogResult.Yes)
                {

                    conexion.Open();
                    OracleCommand command = new OracleCommand("editar_reserva", conexion);


                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("p_reserva_id", OracleType.Int32).Value = txtNumReserva.Text;
                    command.Parameters.Add("p_hora", OracleType.VarChar).Value = txtHora.Text;
                    command.Parameters.Add("P_FECHA", OracleType.VarChar).Value = dateTimePicker1.Value.ToString("dd-MMM-yyyy");
                    command.Parameters.Add("P_MESA_ID", OracleType.Number).Value = Convert.ToInt32(comboBox1.Text);
                    command.Parameters.Add("p_identificacion", OracleType.VarChar).Value = txtID.Text;

                    command.Parameters.Add("RETURN_VALUE", OracleType.Int32).Direction = ParameterDirection.ReturnValue;

                     command.ExecuteNonQuery();

                    valorRetorno = (int)command.Parameters["RETURN_VALUE"].Value;


                    txtID.Text = "";
                    txtHora.Text = "";
                    comboBox1.Text = "";
                    comboBox1.Items.Clear();

                }
            }
            catch (OracleException ex)
            {
                MessageBox.Show("Registro de la reservacion no encontrado");
                MessageBox.Show("Revise los datos ingresados previamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error en el sistema, intenta de nuevo");
            }

            conexion.Close();
            Metodo1();

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult resul = MessageBox.Show("Ver datos de la reserva?", "Reservas", MessageBoxButtons.YesNo);
                if (resul == DialogResult.Yes)
                {

                    conexion.Open();
                    OracleCommand comandos = new OracleCommand("mostrar_reserva_cliente", conexion);
                    comandos.CommandType = System.Data.CommandType.StoredProcedure;
                    comandos.Parameters.Add("reservas", OracleType.Cursor).Direction = ParameterDirection.Output;
                    comandos.Parameters.Add("PID", OracleType.VarChar).Value = txtID.Text;

                    OracleDataAdapter adaptador = new OracleDataAdapter();
                    adaptador.SelectCommand = comandos;
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dgvAdminReserva.DataSource = tabla;
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

        }

        private void dgvAdminReserva_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try { 


                indexRow = e.RowIndex;
                DataGridViewRow row = dgvAdminReserva.Rows[indexRow];
                txtNumReserva.Text = row.Cells[0].Value.ToString();
                txtID.Text = row.Cells[1].Value.ToString();

                string valor = row.Cells[2].Value.ToString();
                comboBox1.Items.Add(valor);
              

                string valor2 = row.Cells[3].Value.ToString();

                DateTime fecha;
                DateTime.TryParse(valor2, out fecha);
                dateTimePicker1.Value = fecha;

                txtHora.Text = row.Cells[4].Value.ToString();

            }
            catch
            {
                MessageBox.Show("La carga de los datos esta durando mas de lo esperado...., ");
            }

        }


        private void label1_Click(object sender, EventArgs e)
        {
            Clientes Clientes = new Clientes();
            Clientes.Show();

            AdministracionReservas AdministracionReservas = new AdministracionReservas();
            AdministracionReservas.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Reservas Reservas = new Reservas();
            Reservas.Show();

            AdministracionReservas AdministracionReservas = new AdministracionReservas();
            AdministracionReservas.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Mesas Mesas = new Mesas();
            Mesas.Show();

            AdministracionReservas AdministracionReservas = new AdministracionReservas();
            AdministracionReservas.Close();

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox9_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Login Login = new Login();
            Login.Show();
        }
    }
    
}

