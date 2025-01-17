﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Tester
{
	public partial class UserForm : Form
	{
		public Student student { get; set; }
		public UserForm()
		{
			InitializeComponent();
		}
		//закрытие окна
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

		//При клике он поменяет добавит бэкграунд
		private void voidLabelMouseClick(object sender, System.EventArgs e)
		{
			var label = (Label)sender;
			label.BackColor = Color.DarkCyan;
		}
		//При двойном клике у нас переходит к самому тесту
		private void LabelMouseDoubleClick(object sender, System.EventArgs e)
		{

			string connectionString = "TestBook";
			MongoCRUD db = new MongoCRUD(connectionString);

			var label = (Label)sender;
			label.BackColor = Color.FromArgb(69, 69, 97);
			var test = db.SearchTest<Test>("Test", label.Text);
			var question_termin = test.questions_termin;
			var question_choise_answer = test.questions_choise_answer;
			var question_insert_word = test.questions_insert_word;
			List<object> list_question = new List<object>();

			List<TestQuestion> test_question_user = new List<TestQuestion>();
			Object[] tests_question_user = new Object[test.count_question];

			PassedTest passed_test = new PassedTest();
			passed_test.test = test;
			passed_test.student = student;

			foreach (var i in question_termin)
			{
				list_question.Add(i);
			}
			foreach (var i in question_choise_answer)
			{
				list_question.Add(i);
			}
			foreach (var i in question_insert_word)
			{
				list_question.Add(i);
			}


			for (int i = 0; i < test.count_question; i++)
			{
				test_question_user.Add(new TestQuestion());
			}

			for (int i = 0; i < test.count_question; i++)
			{
				test_question_user[i].Text = Convert.ToString(i);
				test_question_user[i].test_object_questions = test_question_user;
				test_question_user[i].test_count_question = test.count_question;
				test_question_user[i].passed_test = passed_test;
				test_question_user[i].calculate_figure = test.figure_to_count;
				Console.WriteLine(list_question.ToJson());
				list_question.IndexOf(list_question[i]);


				//Console.WriteLine("====================");
				//Console.WriteLine(list_question[i].GetType());
				//Console.WriteLine(list_question[i].GetType() == typeof(CustiomizeInstertWordQuestion));
				//Console.WriteLine(list_question[i].GetType() == typeof(Tester.QuestionInsertWordQuestion));
				//Console.WriteLine("====================");

				if (list_question[i].GetType() == typeof(QuestionChoiseAnser))
				{
					Console.WriteLine("QuestionChoiseAnser");
					test_question_user[i].bool_question_insert_word = false;
					test_question_user[i].bool_question_termin = false;
					test_question_user[i].bool_question_choise_answer = true;
					QuestionChoiseAnser question = (QuestionChoiseAnser)list_question[i];
					test_question_user[i].question_choise_answer = question;
				}
				else if (list_question[i].GetType() == typeof(QuestionTermin))
				{
					Console.WriteLine("QuestionTermin");
					test_question_user[i].bool_question_termin = true;
					test_question_user[i].bool_question_insert_word = false;
					test_question_user[i].bool_question_choise_answer = false;
					QuestionTermin question = (QuestionTermin)list_question[i];
					test_question_user[i].question_termin = question;
				}
				else if (list_question[i].GetType() == typeof(Tester.QuestionInsertWordQuestion))
				{
					Console.WriteLine("CustiomizeInstertWordQuestion");
					test_question_user[i].bool_question_insert_word = true;
					test_question_user[i].bool_question_termin = false;
					test_question_user[i].bool_question_choise_answer = false;
					QuestionInsertWordQuestion question = (QuestionInsertWordQuestion)list_question[i];
					test_question_user[i].question_insert_word = question;
				}
				test_question_user[0].Show();
			}


			this.Hide();
			Console.WriteLine(test.ToJson());
		}


		//создание списка ссылок на тесты
		private void UserForm_Load(object sender, EventArgs e)
		{
			string connectionString = "TestBook";
			MongoCRUD db = new MongoCRUD(connectionString);
			List<Test> list_tests = db.ListTests<Test>("Test");
			foreach (Test s in list_tests)
			{
				Label label_element = new Label();
				label_element.Text = s.name;
				label_element.ForeColor = Color.White;
				label_element.Font = new Font(label1.Font.Name, Convert.ToSingle(20), label1.Font.Style);
				label_element.Width = 450;
				label_element.Height = 50;
				label_element.MouseClick += voidLabelMouseClick;
				label_element.MouseDoubleClick += LabelMouseDoubleClick;
				flowLayoutPanel1.Controls.Add(label_element);
			}
		}

		private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
		{

		}

		private void label2_Click(object sender, EventArgs e)
		{

		}

		private void button2_Click(object sender, EventArgs e)
		{

		}

		private void button4_Click(object sender, EventArgs e)
		{
			this.WindowState = FormWindowState.Minimized;
		}
	}
}
