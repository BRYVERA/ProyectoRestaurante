using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;
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

            //conexion.Open();
            //OracleCommand command = new OracleCommand("mi_procedimiento", conexion);
            //command.CommandType = System.Data.CommandType.StoredProcedure;

            //// Crear parámetros de salida
            //OracleParameter idParam = new OracleParameter("ids", OracleType.VarChar, 100);
            //idParam.Direction = ParameterDirection.Output;
            //command.Parameters.Add(idParam);

            //OracleParameter nombreParam = new OracleParameter("nom", OracleType.VarChar, 300);
            //nombreParam.Direction = ParameterDirection.Output;
            //command.Parameters.Add(nombreParam);

            //OracleParameter apellidoParam = new OracleParameter("prim", OracleType.VarChar, 20);
            //apellidoParam.Direction = ParameterDirection.Output;
            //command.Parameters.Add(apellidoParam);

            //OracleParameter horaParam = new OracleParameter("horas", OracleType.VarChar, 20);
            //horaParam.Direction = ParameterDirection.Output;
            //command.Parameters.Add(horaParam);

            //OracleParameter fechaParam = new OracleParameter("fechas", OracleType.VarChar, 20);
            //fechaParam.Direction = ParameterDirection.Output;
            //command.Parameters.Add(fechaParam);

            //OracleParameter capacidadParam = new OracleParameter("cap", OracleType.Number);
            //capacidadParam.Direction = ParameterDirection.Output;
            //command.Parameters.Add(capacidadParam);

            //OracleParameter tipoParam = new OracleParameter("tip", OracleType.VarChar, 50);
            //tipoParam.Direction = ParameterDirection.Output;
            //command.Parameters.Add(tipoParam);

            //command.ExecuteNonQuery();

            //DataTable dataTable = new DataTable();
            //dataTable.Columns.Add("ID", typeof(string));
            //dataTable.Columns.Add("Nombre", typeof(string));
            //dataTable.Columns.Add("Apellido", typeof(string));
            //dataTable.Columns.Add("Hora", typeof(string));
            //dataTable.Columns.Add("Fecha", typeof(string));
            //dataTable.Columns.Add("Capacidad", typeof(int));
            //dataTable.Columns.Add("Tipo", typeof(string));

            //DataRow row = dataTable.NewRow();
            //row["ID"] = idParam.Value;
            //row["Nombre"] = nombreParam.Value;
            //row["Apellido"] = apellidoParam.Value;
            //row["Hora"] = horaParam.Value;
            //row["Fecha"] = fechaParam.Value;
            //row["Capacidad"] = capacidadParam.Value;
            //row["Tipo"] = tipoParam.Value;
            //dataTable.Rows.Add(row);

            //dgvReservas.DataSource = dataTable;

            //conexion.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAdministrarReservas_Click(object sender, EventArgs e)
        {

        }
    }
}
