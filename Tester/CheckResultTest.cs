﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tester
{
	public partial class CheckResultTest : Form
	{
		public CheckResultTest()
		{
			InitializeComponent();
		}

		private void button6_Click(object sender, EventArgs e)
		{
			this.Hide();
		}

		private void panel2_MouseDown(object sender, MouseEventArgs e)
		{
			panel2.Capture = false;
			Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
			this.WndProc(ref m);
		}

		private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
		{

		}

		private void CheckResultTest_Load(object sender, EventArgs e)
		{
			
		}
	}
}
