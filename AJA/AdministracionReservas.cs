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
    public partial class AdministracionReservas : Form
    {
        OracleConnection conexion = new OracleConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);
        public AdministracionReservas()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AdministracionReservas_Load(object sender, EventArgs e)
        {
           // conexion.Open();
           // OracleCommand comando = new OracleCommand("editar_reserva", conexion);
           // comando.CommandType = System.Data.CommandType.StoredProcedure;

           // comando.Parameters.Add("p_id", OracleType.Number).Value = "656";

           //comando.Parameters.Add("p_hora", OracleType.VarChar).Value = "2:00";
           // comando.Parameters.Add("p_fecha", OracleType.VarChar).Value = "32";
           // comando.Parameters.Add("p_mesa_id", OracleType.Number).Value = 4;
           //comando.Parameters.Add("p_identificacion", OracleType.VarChar).Value = "34865432";

           // comando.ExecuteNonQuery();
           // conexion.Close();
        }
    }
}
