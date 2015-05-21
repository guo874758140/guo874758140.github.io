using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;

namespace WebApplication4
{
    public partial class Default : System.Web.UI.Page
    {
        SessionHelper Session = new SessionHelper();

        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = Session["user"].ToString() + "___" + Session["url"].ToString();

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            //改变Session["user"]的值
            Session["user"] = TextBox1.Text;
            Label1.Text = Session["user"].ToString() + "___" + Session["url"].ToString();
        }

      
    }
}