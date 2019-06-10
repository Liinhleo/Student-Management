using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMOMANAGEMENT
{
    class BussinessLayer // solve major
    {
        DataAccessLayer DAL;

        public BussinessLayer()
        {
            DAL = new DataAccessLayer();
        }

        public DataTable getAllSV()
        {
            return DAL.getAllSV();
        }

        public bool InsertSV(SINHVIEN sv)
        {
            return DAL.InsertSV(sv);
        }

        public bool UpdateSV(SINHVIEN sv)
        {
            return DAL.UpdateSV(sv);
        }

        public bool DeleteSV(SINHVIEN sv)
        {
            return DAL.DeleteSV(sv);
        }
        public DataTable Refresh()
        {
            return DAL.DisplayData();

        }

    }
}
