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
    public partial class Login : Form
    {

        OracleConnection conexion = new OracleConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);
        public Login()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {

            try
            {
                conexion.Open();

                OracleCommand comandos = new OracleCommand("sp_login", conexion);
                comandos.CommandType = System.Data.CommandType.StoredProcedure;

                comandos.Parameters.Add("p_empleado_id", OracleType.Number).Value = Convert.ToInt32(txtID.Text);
                comandos.Parameters.Add("p_password", OracleType.VarChar).Value = txtPassword.Text;


                comandos.Parameters.Add("p_role", OracleType.Number, 4);
                comandos.Parameters["p_role"].Direction = ParameterDirection.Output;


                comandos.ExecuteNonQuery();

                string rolString = comandos.Parameters["p_role"].Value.ToString();
                int rol = Int32.Parse(rolString);


                if (rol == 1)
                {
                    Form form1 = new Clientes();
                    form1.Show();
                }
                else if (rol == 2)
                {
                    Form form2 = new reporteHorario();
                    form2.Show();
                }
                else if (rol == 3)
                {
                    Form form3 = new Stock();
                    form3.Show();
                }
                else if (rol == 4)
                {
                    Form form3 = new Productos();
                    form3.Show();
                }

                else
                {
                    MessageBox.Show("Datos incorrectos");
                }

            }
            catch (OracleException ex)
            {
                MessageBox.Show("Ocurrio un error en el sistema, intenta de nuevo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error en el sistema, intenta de nuevo");
            }

                conexion.Close();
                                    
            }
    }
}
