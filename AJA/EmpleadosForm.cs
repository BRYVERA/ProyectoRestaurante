
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
    public partial class EmpleadosForm : Form
    {
        string selected_id = "0";
        string editando_empleado = "Editando Empleado";
        string employee_selected_name = "tmp";
        string employee_selected_ap1 = "tmp";
        string employee_selected_ap2 = "tmp";
        string employee_selected_email = "tmp";
        string employee_selected_password = "tmp";
        string employee_selected_telefono = "tmp";
        string employee_selected_rol = "tmp";
        string employee_selected_horario = "tmp";
        OracleConnection conexion = new OracleConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);
        public EmpleadosForm()
        {
            InitializeComponent();
            LoadEmpleados();
            LoadComboBoxes();
        }

        private void LoadComboBoxes()
        {
            string puestoQuery = "SELECT * from VIEW_Rol";
            string horarioQuery = "SELECT * from VW_Turnos_Lista";
            OracleCommand puestoCmd = new OracleCommand(puestoQuery, conexion);
            OracleCommand horarioCmd = new OracleCommand(horarioQuery, conexion);
            conexion.Open();
            OracleDataReader puestoReader = puestoCmd.ExecuteReader();
            OracleDataReader horarioReader = horarioCmd.ExecuteReader();
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            while (puestoReader.Read())
            {
                comboBox1.Items.Add(new KeyValuePair<int, string>(puestoReader.GetInt32(0), puestoReader.GetString(1)));
            }
            while (horarioReader.Read())
            {
                comboBox2.Items.Add(horarioReader.GetInt32(0));
            }
            puestoReader.Close();
            horarioReader.Close();
            conexion.Close();
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";

        }

        private void DeleteEmpleado(int Id)
        {
            try
            {
                string query = "BEGIN eliminar_empleado(:id);" +
                    " END; ";
                OracleCommand cmd = new OracleCommand(query, conexion);
                cmd.Parameters.Add(":id", OracleType.Number).Value = Id;
                conexion.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Empleado eliminado con exito!");
                }
                else
                {
                    MessageBox.Show("No se encontró el empleado con el ID especificado.");
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

        private void EditarEmpleado(string id)
        {
            int telefono;
            int horario;
            int rolId = 1;
            if (int.TryParse(textBox7.Text, out telefono) && int.TryParse(comboBox2.Text, out horario))
            {
                try
                {
                    string query = "BEGIN actualizar_empleado(:id,:name,:a1,:a2,:email,:tel,:password,:rol,:turno);" +
                                   " END; ";
                    OracleCommand cmd = new OracleCommand(query, conexion);
                    cmd.Parameters.Add(":id", OracleType.Number).Value = int.Parse(id);
                    cmd.Parameters.Add(":name", OracleType.VarChar).Value = textBox2.Text;
                    cmd.Parameters.Add(":a1", OracleType.VarChar).Value = textBox3.Text;
                    cmd.Parameters.Add(":a2", OracleType.VarChar).Value = textBox4.Text;
                    cmd.Parameters.Add(":email", OracleType.VarChar).Value = textBox3.Text;
                    cmd.Parameters.Add(":tel", OracleType.Number).Value = telefono;
                    cmd.Parameters.Add(":password", OracleType.VarChar).Value = textBox3.Text;
                    cmd.Parameters.Add(":rol", OracleType.Number).Value = rolId;
                    cmd.Parameters.Add(":turno", OracleType.Number).Value = horario;


                    conexion.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Empleado actualizado con éxito!");
                    conexion.Close();
                    LoadEmpleados();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Invalid empleado_id: " + id);
            }
        }

        private void LoadEmpleados()
        {
            try
            {
                //ver empleados(es una vista)
                string query = "SELECT * FROM ver_empleados";
                OracleCommand cmd = new OracleCommand(query, conexion);
                conexion.Open();
                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    ListViewItem item = new ListViewItem(reader["EMPLEADO_ID"].ToString());
                    item.SubItems.Add(reader["nombre"].ToString());
                    item.SubItems.Add(reader["primer_apellido"].ToString());
                    item.SubItems.Add(reader["segundo_apellido"].ToString());
                    item.SubItems.Add(reader["email"].ToString());
                    item.SubItems.Add(reader["telefono"].ToString());
                    item.SubItems.Add(reader["horario_id"].ToString());
                    listView2.Items.Add(item);
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

        private string ObtenerRolEmpleado()
        {
            string rol = "";

            {
                try
                {

                    string query = "SELECT obtener_rol_empleado(:id) FROM dual";
                    OracleCommand cmd = new OracleCommand(query, conexion);
                    cmd.Parameters.Add(":id", OracleType.Number).Value = int.Parse(selected_id);
                    conexion.Open();
                    OracleDataReader reader = cmd.ExecuteReader();
                    
                    while (reader.Read())
                    {
                        rol = reader["obtener_rol_empleado(:id)"].ToString();
                    }
                     
                    return rol;
                    
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    
                    return rol;
                }
                finally
                {
                    conexion.Close();
                }
            }
            

        }
    

            
        

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Roles_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void EmpleadosForm_Load(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Editar")
            {
                EditarEmpleado(selected_id);
                listView2.Items.Clear();
                LoadEmpleados();
                groupBox2.Text = "Crear empleado";
                textBox7.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                comboBox1.Text = "";
                comboBox2.Text = "";
                selected_id = "0";
                
                button1.Text = "Agregar";

            }
            else if (button1.Text == "Agregar")
            {
                try
                {
                    string query = "SELECT * FROM ver_empleados";
                    OracleCommand cmd = new OracleCommand(query, conexion);
                    conexion.Open();
                    OracleDataReader reader = cmd.ExecuteReader();
                    string empleado_id = "999";
                    while (reader.Read())
                    {
                        empleado_id = reader["EMPLEADO_ID"].ToString();

                    }
                    conexion.Close();
                    int empleados_number;
                    int telefono;
                    int horario;
                    int rolId = ((KeyValuePair<int, string>)comboBox1.SelectedItem).Key;
                    if (int.TryParse(empleado_id, out empleados_number) && int.TryParse(textBox7.Text, out telefono) && int.TryParse(comboBox2.Text, out horario))
                    {
                        empleados_number++;

                        query = "BEGIN crear_empleado(:empleado_id, :nombre, :apellido1," +
                            " :apellido2, :email, :telefono, :password, :rol_id, :horario);" +
                            " END; ";
                        cmd = new OracleCommand(query, conexion);

                        cmd.Parameters.Add(":empleado_id", OracleType.Number).Value = empleados_number;
                        cmd.Parameters.Add(":nombre", OracleType.VarChar).Value = textBox2.Text;
                        cmd.Parameters.Add(":apellido1", OracleType.VarChar).Value = textBox3.Text;
                        cmd.Parameters.Add(":apellido2", OracleType.VarChar).Value = textBox4.Text;
                        cmd.Parameters.Add(":email", OracleType.VarChar).Value = textBox5.Text;
                        cmd.Parameters.Add(":telefono", OracleType.Number).Value = telefono;
                        cmd.Parameters.Add(":password", OracleType.VarChar).Value = textBox6.Text;
                        cmd.Parameters.Add(":rol_id", OracleType.Number).Value = rolId;
                        cmd.Parameters.Add(":horario", OracleType.Number).Value = horario;
                        conexion.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Nuevo empleado agregado con exito!");
                        conexion.Close();
                        listView2.Items.Clear();
                        LoadEmpleados();

                    }
                    else
                    {
                        MessageBox.Show("Invalid action");
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
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {
                try
                {
                    string selectedId = listView2.SelectedItems[0].Text;
                    selected_id = selectedId;
                    employee_selected_name = listView2.SelectedItems[0].SubItems[1].Text;
                    employee_selected_ap1 = listView2.SelectedItems[0].SubItems[2].Text;
                    employee_selected_ap2 = listView2.SelectedItems[0].SubItems[3].Text;
                    employee_selected_email = listView2.SelectedItems[0].SubItems[4].Text;
                    employee_selected_password = "Editar contraseña..";
                    employee_selected_telefono = listView2.SelectedItems[0].SubItems[5].Text;
                    employee_selected_rol = "";
                    employee_selected_horario = listView2.SelectedItems[0].SubItems[6].Text;
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

        private void button3_Click(object sender, EventArgs e)
        {
            //Borrar
            DeleteEmpleado(int.Parse(selected_id));
            listView2.Items.Clear();
            LoadEmpleados();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Editar
            button1.Text = "Editar";
            groupBox2.Text = "Editando";


            employee_selected_rol = ObtenerRolEmpleado();

            textBox2.Text = employee_selected_name;
            textBox3.Text = employee_selected_ap1;
            textBox4.Text = employee_selected_ap2;
            textBox5.Text = employee_selected_email;
            textBox6.Text = employee_selected_password;
            textBox7.Text = employee_selected_telefono;
            comboBox1.Text = employee_selected_rol;
            comboBox2.Text = employee_selected_horario;

        }
    }
}
