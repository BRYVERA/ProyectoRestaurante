
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
    public partial class RolesForm : Form
    {
        string selected_id = "0";
        string editando_rol = "Editando Rol";
        string rol_selected_name = "tmp";
        OracleConnection conexion = new OracleConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);
        public RolesForm()
        {
            InitializeComponent();
            LoadRoles();
        }

        private void LoadRoles()
        {
            try
            {
                string query = "SELECT * from VIEW_Rol";
                OracleCommand cmd = new OracleCommand(query, conexion);
                conexion.Open();
                OracleDataReader reader = cmd.ExecuteReader();
                listView1.Items.Clear();
                while (reader.Read())
                {
                    
                    ListViewItem item = new ListViewItem(reader["ROL_ID"].ToString());
                    item.SubItems.Add(reader["puesto"].ToString());
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

        private void deleteRol()
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Roles_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                try
                {
                    string selectedRolId = listView1.SelectedItems[0].Text;
                    selected_id = selectedRolId;
                    rol_selected_name = listView1.SelectedItems[0].SubItems[1].Text;
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(button2.Text == "Editar")
            {
                EditarRol(selected_id);
                LoadRoles();
                textBox1.Text = "";
                selected_id = "0";
                label3.Text = "Agregar nuevo rol";
                button2.Text = "Agregar";

            }
            else if(button2.Text == "Agregar")
            {
                try
                {
                    string query = "SELECT * from VIEW_Rol";
                    OracleCommand cmd = new OracleCommand(query, conexion);
                    conexion.Open();
                    OracleDataReader reader = cmd.ExecuteReader();
                    string rol_id = "999";
                    while (reader.Read())
                    {

                        rol_id = reader["ROL_ID"].ToString();

                    }
                    conexion.Close();
                    int rol_number;
                    if (int.TryParse(rol_id, out rol_number))
                    {
                        rol_number++;

                        query = "BEGIN SP_Rol_Insertar(:rol_id,:puesto);" +
                            " END; ";
                        cmd = new OracleCommand(query, conexion);
                        cmd.Parameters.Add(":rol_id", OracleType.Number).Value = rol_number;
                        Console.WriteLine(rol_number);
                        cmd.Parameters.Add(":puesto", OracleType.VarChar).Value = textBox1.Text;
                        conexion.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Nuevo rol agregado con exito!");
                        conexion.Close();
                        LoadRoles();

                    }
                    else
                    {
                        MessageBox.Show("Invalid rol_id: " + rol_id);
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void DeleteRol(int rolId)
        {
            try
            {
                string query = "BEGIN SP_Rol_Eliminar(:rol_id);" +
                    " END; ";
                OracleCommand cmd = new OracleCommand(query, conexion);
                cmd.Parameters.Add(":rol_id", OracleType.Number).Value = rolId;
                conexion.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Rol eliminado con exito!");
                }
                else
                {
                    MessageBox.Show("No se encontró el rol con el ID especificado.");
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


        private void button1_Click(object sender, EventArgs e)
        {
            DeleteRol(int.Parse(selected_id));
            LoadRoles();
        }

        private void EditarRol(string id)
        {
            if (int.TryParse(id, out int rol_id))
            {
                try
                {
                    string query = "BEGIN SP_Rol_Actualizar(:rol_id,:puesto);" +
                                   " END; ";
                    OracleCommand cmd = new OracleCommand(query, conexion);
                    cmd.Parameters.Add(":rol_id", OracleType.Number).Value = rol_id;
                    cmd.Parameters.Add(":puesto", OracleType.VarChar).Value = textBox1.Text;
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Rol actualizado con éxito!");
                    conexion.Close();
                    LoadRoles();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Invalid rol_id: " + id);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label3.Text = editando_rol;
            button2.Text = "Editar";
            textBox1.Text = rol_selected_name;
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Login Login = new Login();
            Login.Show();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ya te encuentras en esta parte del menú ");
        }

        private void label13_Click(object sender, EventArgs e)
        {
            HorariosForm HorariosForm = new HorariosForm();
            HorariosForm.Show();

            RolesForm RolesForm = new RolesForm();
            RolesForm.Close();
        }

        private void label15_Click(object sender, EventArgs e)
        {
            EmpleadosForm EmpleadosForm = new EmpleadosForm();
            EmpleadosForm.Show();

            RolesForm RolesForm = new RolesForm();
            RolesForm.Close();
        }
    }
}
