using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Payment
{
    public partial class select_dop_rekvisit : Form
    {
        SqlConnection conn = new SqlConnection(@"Password=000;Persist Security Info=True;User ID=sa;Initial Catalog=PAYMENT;Data Source=T1212-W00079\MSSQLSERVER2012");

        public main m;

        public select_dop_rekvisit(main mm)
        {
            InitializeComponent();
            m = mm;
        }

        //Заполнение DataGridView наименованиями полей 
        public void fill_gridview()
        {
            dataGridView1.Columns["NAIMENOVAN_KONTR"].HeaderText = "NAIMENOVAN_KONTR";
            dataGridView1.Columns["NAIMENOVAN_KONTR"].Width = 10;
            dataGridView1.Columns["NAIMENOVAN_KONTR"].Visible = false;            
            dataGridView1.Columns["INN"].HeaderText = "INN";
            dataGridView1.Columns["INN"].Width = 10;
            dataGridView1.Columns["INN"].Visible = false;
            dataGridView1.Columns["KPP"].HeaderText = "КПП";
            dataGridView1.Columns["KPP"].Width = 80;
            dataGridView1.Columns["ACCOUNT"].HeaderText = "Р/счет";
            dataGridView1.Columns["ACCOUNT"].Width = 100;
            dataGridView1.Columns["BANK_NAIMENOVAN"].HeaderText = "Наименование банка";
            dataGridView1.Columns["BANK_NAIMENOVAN"].Width =150;
            dataGridView1.Columns["BIK"].HeaderText = "БИК";
            dataGridView1.Columns["BIK"].Width = 100;
            dataGridView1.Columns["KORR_COUNT"].HeaderText = "Корр./счет";
            dataGridView1.Columns["KORR_COUNT"].Width = 100;
            
        }

        private void select_dop_rekvisit_Load(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("select CATALOG_KONTRAGENT.NAIMENOVAN_KONTR, CATALOG_KONTRAGENT_DOP_REKVISIT.INN, CATALOG_KONTRAGENT_DOP_REKVISIT.KPP, CATALOG_KONTRAGENT_DOP_REKVISIT.ACCOUNT, CATALOG_KONTRAGENT_DOP_REKVISIT.BANK_NAIMENOVAN,CATALOG_KONTRAGENT_DOP_REKVISIT.BIK, CATALOG_KONTRAGENT_DOP_REKVISIT.KORR_COUNT from CATALOG_KONTRAGENT_DOP_REKVISIT, CATALOG_KONTRAGENT where [CATALOG_KONTRAGENT].ID=CATALOG_KONTRAGENT_DOP_REKVISIT.ID_CATALOG_KONTRAGENT and CATALOG_KONTRAGENT.INN='" + main.rekv + "'", conn);
            SqlDataAdapter da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            DataSet ds = new DataSet();
            conn.Close();
            //Заполнение DataGridView наименованиями полей 
            da.Fill(ds, "CATALOG_KONTRAGENT_DOP_REKVISIT");
            dataGridView1.DataSource = ds.Tables[0];

            fill_gridview();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {           
            m.comboBox2.Text = "";
            m.comboBox2.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            m.comboBox7.Text = "";
            m.comboBox7.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            m.textBox8.Clear();
            m.textBox8.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            m.textBox9.Clear();
            m.textBox9.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            m.textBox10.Clear();
            m.textBox10.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            m.textBox11.Clear();
            m.textBox11.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            m.textBox12.Clear();
            m.textBox12.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();


            
            this.Close();
        }
    }
}
