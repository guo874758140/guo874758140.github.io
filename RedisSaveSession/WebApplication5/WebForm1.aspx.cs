using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;

namespace WebApplication5
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            test();
        }
        public void test()
        {
            SessionHelper Session = new SessionHelper();
            if (Session["user"] == null)
            {
                Session["user"] = "eric";
            }
            //为了展示两个session的过期时间不一样，让线程休眠2s
            System.Threading.Thread.Sleep(2000);
            if (Session["url"] == null)
            {
                Session["url"] = "http://baidu.com";
            }
            //Session["user"] = "qwewrwe";
            string text = string.Format("session['user']的值为:{0},距离过期时间：{1}秒", Session["user"].ToString(), Session.Redis.TTL(Session.SessionId + "_user"));
            text += "<br />";
            text += string.Format("session['url']的值为:{0},距离过期时间：{1}秒", Session["url"].ToString(), Session.Redis.TTL(Session.SessionId + "_url"));
            Response.Write(text);

        }
    }
}