using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OracleClient;
using System.Configuration;

namespace AJA
{
    public partial class HorariosForm : Form
    {
        string selected_id = "0";
        string editando_horario = "Editando Horario";
        string horario_selected_dias = "0";
        string horario_selected_entrada = "tmp";
        string horario_selected_salida = "tmp";
        OracleConnection conexion = new OracleConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);
        public HorariosForm()
        {
            InitializeComponent();
            LoadHorarios();
        }

        private void LoadHorarios()
        {
            try
            {
                string query = "SELECT * from VW_Turnos_Lista";
                OracleCommand cmd = new OracleCommand(query, conexion);
                conexion.Open();
                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                   

                    ListViewItem item = new ListViewItem(reader["Horario_ID"].ToString());
                    item.SubItems.Add(reader["dias_laborales"].ToString());
                    item.SubItems.Add(reader["hora_entrada"].ToString());
                    item.SubItems.Add(reader["hora_salida"].ToString());
                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void DeleteHorario(int Id)
        {
            try
            {
                string query = "BEGIN SP_Turnos_Borrar(:id);" +
                    " END; ";
                OracleCommand cmd = new OracleCommand(query, conexion);
                cmd.Parameters.Add(":id", OracleType.Number).Value = Id;
                conexion.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Turno eliminado con exito!");
                }
                else
                {
                    MessageBox.Show("No se encontró el turno con el ID especificado.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }

        private void EditarHorario(string id)
        {
            if (int.TryParse(id, out int horario_id))
            {
                try
                {
                    string query = "BEGIN SP_Turnos_Actualizar(:id,:d_laborales,:h_entra,:h_salida);" +
                                   " END; ";
                    OracleCommand cmd = new OracleCommand(query, conexion);
                    cmd.Parameters.Add(":id", OracleType.Number).Value = horario_id;
                    cmd.Parameters.Add(":d_laborales", OracleType.VarChar).Value = int.Parse(textBox1.Text);
                    cmd.Parameters.Add(":h_entra", OracleType.VarChar).Value = textBox2.Text;
                    cmd.Parameters.Add(":h_salida", OracleType.VarChar).Value = textBox3.Text;
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Horario actualizado con éxito!");
                    conexion.Close();
                    LoadHorarios();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Invalid horario_id: " + id);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Editar")
            {
                EditarHorario(selected_id);
                listView1.Items.Clear();
                LoadHorarios();
                
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                selected_id = "0";
                groupBox1.Text = "Nuevo horario";
                button1.Text = "Agregar";

            }
            else if (button1.Text == "Agregar")
            {
                try
                {


                    int dias_laborales = int.Parse(textBox1.Text);
                    string hora_entrada = textBox2.Text;
                    string hora_salida = textBox3.Text;

                    string query = "BEGIN SP_Turnos_Insertar(:p_dias_laborales," +
                            "  :p_hora_entrada, :p_hora_salida);" +
                            " END; ";
                    OracleCommand cmd = new OracleCommand(query, conexion);

                    cmd.Parameters.Add(":p_dias_laborales", OracleType.Number).Value = dias_laborales;
                    cmd.Parameters.Add(":p_hora_entrada", OracleType.VarChar).Value = hora_entrada;
                    cmd.Parameters.Add(":p_hora_salida", OracleType.VarChar).Value = hora_salida;

                    conexion.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Turno agregado con éxito!");
                    conexion.Close();
                    listView1.Items.Clear();
                    LoadHorarios();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    conexion.Close();

                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                try
                {
                    string selectedRolId = listView1.SelectedItems[0].Text;
                    selected_id = selectedRolId;
                    horario_selected_dias = listView1.SelectedItems[0].SubItems[1].Text; 
                    horario_selected_entrada = listView1.SelectedItems[0].SubItems[2].Text; 
                    horario_selected_salida = listView1.SelectedItems[0].SubItems[3].Text; 
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    conexion.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DeleteHorario(int.Parse(selected_id));
            listView1.Items.Clear();
            LoadHorarios();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button1.Text = "Editar";
            groupBox1.Text = "Editando horario";
            textBox1.Text = horario_selected_dias.ToString();
            textBox2.Text = horario_selected_entrada;
            textBox3.Text = horario_selected_salida;
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Login Login = new Login();
            Login.Show();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ya te encuentras en esta parte del menú ");
        }

        private void label15_Click(object sender, EventArgs e)
        {

            EmpleadosForm EmpleadosForm = new EmpleadosForm();
            EmpleadosForm.Show();

            HorariosForm HorariosForm = new HorariosForm();
            HorariosForm.Close();

        }

        private void label14_Click(object sender, EventArgs e)
        {
            RolesForm RolesForm = new RolesForm();
            RolesForm.Show();

            HorariosForm HorariosForm = new HorariosForm();
            HorariosForm.Close();
        }
    }
}
