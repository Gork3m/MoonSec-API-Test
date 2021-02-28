using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Web;
using System.Net;
using System.IO;
using System.Text;

namespace MoonSecAPITest
{
	
	public partial class MainForm : Form
	{
		public MainForm()
		{
			
			InitializeComponent();
		
		}
		void Button1Click(object sender, EventArgs e)
		{
			RadioButton[] bytecode = { radioButton1, radioButton2, radioButton3, radioButton4, radioButton5, radioButton6, radioButton7 };
			int bytecodeindex = -1;
			for (int i = 0; i < bytecode.Length; i++) {
				if (bytecode[i].Checked) {
					bytecodeindex = i;
					break;
				}
			}
			
			string script = richTextBox1.Text;
			string platform = comboBox1.Text;
			
			CheckBox[] boxes = { checkBox1, checkBox2, checkBox3, checkBox4 };
			string[] paramname = { "StringEncryption", "ConstantEncryption", "AntiDump", "SmallOutput" };
			
			string options_payload = "";
			for (int i = 0; i < boxes.Length; i++) {
				if (boxes[i].Checked) {
					options_payload += paramname[i] + "+"; // i know, but it doesn't matter if it has + at the very end.
				}
			}
			
			var request = (HttpWebRequest)WebRequest.Create("https://api.f3d.at/v1/obfuscate.php?key=" + textBox1.Text + "&options=" + options_payload + "&bytecode=" + bytecodeindex + "&platform=" + platform);


			byte[] _byteScript = Encoding.UTF8.GetBytes(script);
			request.Method = "POST";
			request.ContentType = "text/html charset=utf-8;";
			request.ContentLength = _byteScript.Length;
			using (var stream = request.GetRequestStream())
			{
    			stream.Write(_byteScript, 0, _byteScript.Length);
			}
			
			
			try{
			var response = (HttpWebResponse)request.GetResponse();
			var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
			richTextBox2.Text = responseString;
			}catch(WebException ex){ MessageBox.Show(ex.Message); }
			
			
	
		}
	}
}
