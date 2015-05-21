using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Net;

namespace RefurbishExe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }
        System.Timers.Timer myTimer;

        void Form1_Load(object sender,EventArgs e)
        {
            var timeSpanceConfig = ConfigurationManager.AppSettings["timeSpan"];
            this.textBox1.Text = timeSpanceConfig;
            var timeSpance =Convert.ToInt32(timeSpanceConfig) * 1000;//秒
            myTimer = new System.Timers.Timer(timeSpance);//定时周期2秒
            myTimer.Elapsed += myTimer_Elapsed;//到2秒了做的事件
            myTimer.AutoReset = true; //是否不断重复定时器操作

        }

          void myTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
              //var rurl = "http://yewu.tuhu.cn/Article/Refurbish";
              //httpSend.HttpSend(rurl, "");
            Invoke((MethodInvoker)delegate
            {
                wb.Document.InvokeScript("dorefresh");
            });
        }
       


        //启用按钮
        private void button1_Click(object sender, EventArgs e)
        {
            myTimer.Enabled = true; //定时器开始用
            //如果不写下面这句会有一个异常。
            //异常：线程间操作无效: 从不是创建控件"richtextbox"的线程访问它
            //但这不是最好的方法。如果只有一个进程调用richtextbox而已。就可以用下面这句
            //如果有多个线程调用richtextbox等控件。就要用委托。具体百度
            //一篇参考博客http://www.cnblogs.com/zyh-nhy/archive/2008/01/28/1056194.html
            Control.CheckForIllegalCrossThreadCalls = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (myTimer.Enabled)
            {
                myTimer.Enabled = false; //定时器停止
                button2.Text = "继续";
            }
            else
            {
                myTimer.Enabled = true;
                button2.Text = "暂停";
            }

        }

    }

    public static class httpSend
    {
        public static string HttpSend(this string apiurl, string args = null, string method = "POST", string contentype = "application/x-www-form-urlencoded")
        {
            if (string.IsNullOrEmpty(apiurl))
            {
                return string.Empty;
            }
            WebClient webClients = new WebClient();
            webClients.Headers.Add("Content-Type", contentype);
            byte[] encodedUrl = null;
            if (args != null)
            {
                encodedUrl = Encoding.UTF8.GetBytes(args);
            }
            byte[] responseDatas = webClients.UploadData(apiurl, "POST", encodedUrl);
            string src = Encoding.UTF8.GetString(responseDatas);
            return src;
        }
    }
}
