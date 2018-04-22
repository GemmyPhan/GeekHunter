using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeekHunter
{
    public partial class MainForm : Form
    {
        int MaxId = 0;
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<Candidate> listCandidate = DB.LoadAllCandidates();


            for (int i = 0; i < listCandidate.Count; i++)
            {
                ListViewItem lvi = new ListViewItem(listCandidate[i].Id.ToString());

                lvi.SubItems.Add(listCandidate[i].FirstName);
                lvi.SubItems.Add(listCandidate[i].LastName);
                listView1.Items.Add(lvi);
                MaxId = listCandidate[i].Id;
            }


        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            InputForm ipFrm = new InputForm();

            if(ipFrm.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchForm sFrm = new SearchForm();

            if (sFrm.ShowDialog() == DialogResult.OK)
            {

            }
        }
    }
}
