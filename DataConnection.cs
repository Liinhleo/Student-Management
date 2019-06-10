using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMOMANAGEMENT
{
    class DataConnection
    {
       
        //tao ket noi toi SQL Server

       
        string conStr; // Ten bien 
        public DataConnection() 
        {
            //dinh nghia chuoi ket noi
            conStr = "Data Source = DESKTOP-6D8HBV8; Initial Catalog = quanly; UID=sa; PASSWORD = 123456"; // gan = ten server name
        }
        // lay chuoi ket noi tu SQL Connection
        public SqlConnection getConnect() //lay chuoi ket noi
        {
            return new SqlConnection(conStr); // truyen chuoi ket noi o tren vao
        }
    }
}
