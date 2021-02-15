using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Newtonsoft.Json;
using System.Web.Services;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public static string GetEmpData()
        {
            con.Open();
            string _data = "";
            SqlCommand cmd = new SqlCommand("sp_tblEmployees_Select", con);
            cmd.CommandType = CommandType.StoredProcedure;
            
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                _data = JsonConvert.SerializeObject(ds.Tables[0]);
            }
            
            return _data;
        }
    }
}