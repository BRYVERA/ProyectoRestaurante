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
    public partial class Reservas : Form
    {

        OracleConnection conexion = new OracleConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);
        public Reservas()
        {
            InitializeComponent();
        }


        private void dgvReservaCombinado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Reservas_Load(object sender, EventArgs e)
        {

            conexion.Open();
            OracleCommand cmd = new OracleCommand("mi_procedimiento", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("ids", OracleType.VarChar, 20).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("nom", OracleType.VarChar, 20).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("prim", OracleType.VarChar, 20).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("horas", OracleType.VarChar, 10).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("fechas", OracleType.DateTime).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("cap", OracleType.Number, 2).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("tip", OracleType.VarChar, 15).Direction = ParameterDirection.Output;
            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[1];

           

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
