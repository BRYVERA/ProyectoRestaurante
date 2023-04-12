using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OracleClient;
using System.Configuration;

namespace AJA
{
    public class DBoracle
    {


        string conexionstring = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

        public void conectar()
        {
            OracleConnection conexion = new OracleConnection(conexionstring); 
            conexion.Open();
            System.Windows.Forms.MessageBox.Show("conectado a oracle"); 
        }
    }
}
