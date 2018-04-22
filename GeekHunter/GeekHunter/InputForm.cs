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
    public partial class InputForm : Form
    {
        Candidate c;
        internal Candidate Candidate
        {
            get
            {
                return c;
            }

            set
            {
                c = value;
            }
        }

        public int iMyId { get; set; }

        public InputForm()
        {           
            InitializeComponent();

            List<Skills> listSkills = DB.LoadAllSkills();
          
            //fill the listBox
            foreach (Skills s in listSkills)
            {
                lstBox1.Items.Add(s);
            }
        }
       
        private void add_c(object sender, EventArgs e)
        {
            ADD();           
        }

        //Add the skills to Box 2 and remove them from Box 1
        private void ADD()
        {
            int c = lstBox1.Items.Count - 1;
            
            for(int i = c; i>=0; i--)
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
            REMOVE();
        }

        // Remove skills that has been chose from Box 2
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

        // Clear all the fields
        private void RESET()
        {
            lblId.Text = "";
            editFirstName.Text = "";
            editLastName.Text = "";
            
            int c = lstBox2.Items.Count - 1;

            for (int i = c; i >= 0; i--)
            {            
                    lstBox1.Items.Add(lstBox2.Items[i]);
                    lstBox2.Items.RemoveAt(i);                
            }
        }

        // When the input form load
        private void InputForm_Load(object sender, EventArgs e)
        {
            if(this.iMyId != 0)
            {
                lblId.Text = this.iMyId.ToString();

                c = DB.GetOneCandidate(iMyId);

                editFirstName.Text = c.FirstName;
                editLastName.Text = c.LastName;

                List<Skills> lstSkill = new List<Skills>();
                    lstSkill = DB.GetOneCandidateSkill(iMyId);

                c = new Candidate();
                c.FirstName = editFirstName.Text;
                c.LastName = editLastName.Text;
                c.SkillIds = new List<int>();

                for (int i = 0; i < lstBox1.Items.Count; i++)
                {
                    Skills skillSelected = (Skills)lstBox1.Items[i];

                    c.SkillIds.Add(skillSelected.Id);
                }

                for (int i = 0; i < lstSkill.Count(); i++)
                {
                    for (int x = 0; x < c.SkillIds.Count; x++)
                    {
                        if (lstSkill[i].Id.ToString().Equals(c.SkillIds[x].ToString()) == true)
                        {
                            lstBox1.SelectedIndex = x;
                            break;
                        }
                    }
                }
                ADD();
            }

        }

        private void lstBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSaveCandidate_Click(object sender, EventArgs e)
        {

        }

        // When click Save button
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (editFirstName.Text.Trim() == "")
            {
                MessageBox.Show("First name can not be null.");
                return;
            }
            if (editLastName.Text.Trim() == "")
            {
                MessageBox.Show("Last name can not be null.");
                return;
            }
            if(lstBox2.Items.Count <= 0)
            {
                MessageBox.Show("Please choose a skill.");
                return;
            }
            else if(lstBox2.Items.Count > 0)
            {
                c = new Candidate();
                c.FirstName = editFirstName.Text;
                c.LastName = editLastName.Text;
                c.SkillIds = new List<int>();

                for (int i = 0; i < lstBox2.Items.Count; i++)
                {
                    Skills skillSelected = (Skills)lstBox2.Items[i];

                    c.SkillIds.Add(skillSelected.Id);

                }

                //Add
                if (lblId.Text.Equals(""))
                {
                    DB db = new DB();
                    int id = db.addCandidate_Skill(c.FirstName, c.LastName, c.SkillIds);
                    lblId.Text = id.ToString();
                    MessageBox.Show("Saved successfully.");

                    RESET();
                }
                //Update
                else
                {
                    DB db = new DB();
                    c.Id = Int32.Parse(lblId.Text);
                    db.updateCandidate_Skill(c.Id, c.FirstName, c.LastName, c.SkillIds);
                    MessageBox.Show("Updated successfully.");
                }
            }    
        }

        private void btnReset_Click(object sender, EventArgs e)
        {   
            RESET();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // This hides the form, and causes ShowDialog() to return in your Form1
            this.DialogResult = DialogResult.OK;
        }
    }
}
