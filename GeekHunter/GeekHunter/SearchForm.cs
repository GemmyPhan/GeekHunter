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
    public partial class SearchForm : Form
    {
        //int MaxId = 0;
        public SearchForm()
        {
            InitializeComponent();
            List<Skills> listSkills = DB.LoadAllSkills();
           
            foreach (Skills s in listSkills)
            {
                lstBox1.Items.Add(s);
            }
        }

        private void add_c(object sender, EventArgs e)
        {
            

        }

        private void ADD()
        {
            int c = lstBox1.Items.Count - 1;

            for (int i = c; i >= 0; i--)
            {
                if (lstBox1.GetSelected(i))
                {
                    lstBox2.Items.Add(lstBox1.Items[i]);
                    lstBox1.Items.RemoveAt(i);
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void REMOVE()
        {
            int c = lstBox2.Items.Count - 1;

            for (int i = c; i >= 0; i--)
            {
                if (lstBox2.GetSelected(i))
                {
                    lstBox1.Items.Add(lstBox2.Items[i]);
                    lstBox2.Items.RemoveAt(i);
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SearchForm_Load(object sender, EventArgs e)
        {
            

        }

        private void btnSearchCandidate_Click(object sender, EventArgs e)
        {

            lstViewCanSkill.Items.Clear();
            if (lstBox2.Items.Count <= 0)
            {
                // Search all Candidate

                List<Candidate> listCandidate = DB.SearchCandidate();

                for (int i = 0; i < listCandidate.Count; i++)
                {
                    ListViewItem lvi = new ListViewItem(listCandidate[i].Id.ToString());

                    lvi.SubItems.Add(listCandidate[i].FirstName);
                    lvi.SubItems.Add(listCandidate[i].LastName);
                    lvi.SubItems.Add(listCandidate[i].SkillNames);
                    lstViewCanSkill.Items.Add(lvi);
                    
                }
            }
            else 
            {
                // Skills skillSelected = (Skills)lstBox2.Items[lstBox2.SelectedIndex];
                //MessageBox.Show(skillSelected.Id.ToString());

                Candidate c = new Candidate();

                c.SkillIds = new List<int>();

                for (int i = 0; i < lstBox2.Items.Count; i++)
                {
                    Skills skillSelected = (Skills)lstBox2.Items[i];
                    //MessageBox.Show(skillSelected.Id.ToString());


                    c.SkillIds.Add(skillSelected.Id);
                }
                //MessageBox.Show("uuuu: " + c.SkillIds.Count.ToString()); 
                //DB db = new DB();
                //DB.SearchCandidateSkills(c.SkillIds);
                //lstViewCanSkill.Clear();

                List<Candidate> listCandidateSkill = DB.SearchCandidateSkills(c.SkillIds);

                for (int i = 0; i < listCandidateSkill.Count; i++)
                {
                    ListViewItem lvi = new ListViewItem(listCandidateSkill[i].Id.ToString());

                    lvi.SubItems.Add(listCandidateSkill[i].FirstName);
                    lvi.SubItems.Add(listCandidateSkill[i].LastName);
                    lvi.SubItems.Add(listCandidateSkill[i].SkillNames);
                    lstViewCanSkill.Items.Add(lvi);
                }

            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            ADD();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            REMOVE();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            this.Hide();
            InputForm ipFrm = new InputForm();

            if (ipFrm.ShowDialog() == DialogResult.OK)
            {

            }
            // We get here when newform's DialogResult gets set
            this.Show();
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {


            if (lstViewCanSkill.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Please select an item in the list below.");
                return;
            }


            int id = int.Parse(lstViewCanSkill.SelectedItems[0].SubItems[0].Text);

            DialogResult result = MessageBox.Show("Are you sure to delete this record?", "caption", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                DB.deleteCandidate(id);
                lstViewCanSkill.SelectedItems[0].Remove();
            }
            else if (result == DialogResult.No)
            {
                return;
            }

            
            //if (id == MaxId)
            //{
            //    MaxId--;
            //}

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
            if (lstViewCanSkill.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Please select an item in the list below.");
                return;
            }

            //MessageBox.Show("aaa: " +lstViewCanSkill.SelectedItems[0].SubItems[0].Text);
            this.Hide();
            InputForm frm = new InputForm();
           
            frm.iMyId = int.Parse(lstViewCanSkill.SelectedItems[0].SubItems[0].Text);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                //if (.Text == "")
                //{
                //    MessageBox.Show("DVD Name can not be null.");
                //    return;
                //}    
            }
            // We get here when newform's DialogResult gets set
            this.Show();
           
        
            //DB.updateCandidate(frm.Candidate);
            //lstViewCanSkill.SelectedItems[0].SubItems[1].Text = frm.Candidate.FirstName;

        }
    }
}
