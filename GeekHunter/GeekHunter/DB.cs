using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace GeekHunter
{
    class DB
    {
        static string connectionString = @"Data Source = C:\Users\user\source\repos\GeekHunter\GeekHunter\GeekHunter\GeekHunter.sqlite;Version=3;";

        public static List<Candidate> LoadAllCandidates()
        {
            SQLiteConnection conn;
            conn = new SQLiteConnection(connectionString);

            List<Candidate> candidatesList = new List<Candidate>();
            try
            {
                conn.Open();
                string sql = "select Id, FirstName, LastName from Candidate";

                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                SQLiteDataReader reader = cmd.ExecuteReader();
                int id;
                string fname;
                string lname;
                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                    fname = reader.GetString(1);
                    lname = reader.GetString(2);
                    Candidate candidate = new Candidate();
                    candidate.Id = id;
                    candidate.FirstName = fname;
                    candidate.LastName = lname;
                    candidatesList.Add(candidate);
                }

            }
            catch (System.Exception e) { MessageBox.Show(e.ToString()); }
            finally { conn.Close(); }
            return candidatesList;
        }

        public static List<Skills> LoadAllSkills()
        {

            SQLiteConnection conn;
            conn = new SQLiteConnection(connectionString);

            List<Skills> skillsList = new List<Skills>();
            try
            {
                conn.Open();
                string sql = "select Id, Name from Skill";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                SQLiteDataReader reader = cmd.ExecuteReader();
                int id;
                string name;

                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                    name = reader.GetString(1);

                    Skills skills = new Skills();
                    skills.Id = id;
                    skills.Name = name;
                    skillsList.Add(skills);
                }

            }
            catch (System.Exception e) { MessageBox.Show(e.ToString()); }
            finally { conn.Close(); }
            return skillsList;
        }


        public int addCandidate_Skill(string fName, string lName, List<int> skillIds)
        {
            int candidateId;
            candidateId = LoadAllCandidates().Max(c => c.Id) + 1;

            // 
            AddCandidate(candidateId, fName, lName);

            foreach (int skillId in skillIds)
            {
                AddCandidateSkills(candidateId, skillId);
            }
            return candidateId;
        }

        private void AddCandidate(int id, string fName, string lName)
        {
            string query;
            query = $"insert into Candidate(Id, FirstName, LastName) values({id}, '{fName}', '{lName}')";

            ExecuteNonQuery(query);
        }

        private void AddCandidateSkills(int Candidateid, int SkillId)
        {
            string query;
            query = $"insert into Candidate_Skill(CandidateId, SkillId) values({Candidateid}, {SkillId})";
            ExecuteNonQuery(query);
        }
        private void ExecuteNonQuery(string query)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(conn))
                    {
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (System.Exception e) { MessageBox.Show(e.ToString()); }
                finally { conn.Close(); }

            }
        }

        //Search 1 Candidate by Id
        public static Candidate GetOneCandidate(int id)
        {
            //
            SQLiteConnection conn;
            conn = new SQLiteConnection(connectionString);

            Candidate candidate = new Candidate();
            try
            {
                conn.Open();
                

                string sql = $"SELECT FirstName, LastName FROM Candidate WHERE Id = {id} ";
                //where cs.SkillId in (1,2,3)

                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                SQLiteDataReader reader = cmd.ExecuteReader();
                //int id;
                string fname;
                string lname;
                //string sname;
                while (reader.Read())
                {
                    //id = reader.GetInt32(0);
                    fname = reader.GetString(0);
                    lname = reader.GetString(1);
                    //sname = reader.GetString(3);
                    //Candidate candidate = new Candidate();
                    //candidate.Id = id;
                    candidate.FirstName = fname;
                    candidate.LastName = lname;          
                }

            }
            catch (System.Exception e) { MessageBox.Show(e.ToString()); }
            finally { conn.Close(); }
            return candidate;
        }

        public static List<Skills> GetOneCandidateSkill(int id)
        {
            //
            SQLiteConnection conn;
            conn = new SQLiteConnection(connectionString);
            //Candidate candidate = new Candidate();
            //List<Candidate> candidatesList = new List<Candidate>();
            List<Skills> skills = new List<Skills>();
            try
            {
                conn.Open();


                string sql = $"SELECT SkillId FROM Candidate_Skill WHERE CandidateId = {id} ";
                //string sql = $"SELECT s.Name FROM Candidate_Skill cs INNER JOIN Skill s ON s.Id = cs.SkillId WHERE cs.CustomerId = {id} ";
                //where cs.SkillId in (1,2,3)

                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                SQLiteDataReader reader = cmd.ExecuteReader();
                int sid;
                //string sname;

                while (reader.Read())
                {
                    sid = reader.GetInt32(0);
                    //MessageBox.Show(sid.ToString());
                    //fname = reader.GetString(0);
                    //lname = reader.GetString(1);
                    //sname = reader.GetString(0);

                    //<SkillIds>.add(sid;
                    Skills skill = new Skills();
                    skill.Id = sid;
                    skills.Add(skill);
                   

                    //
                    //candidate.FirstName = fname;
                    //candidate.LastName = lname;
                }
                

            }
            catch (System.Exception e) { MessageBox.Show(e.ToString()); }
            finally { conn.Close(); }
            return skills;
        }

        //public static List<Candidate> SearchCandidateSkills(List<int> skillIds)
        public static List<Candidate> SearchCandidate()
        {
            //
            SQLiteConnection conn;
            conn = new SQLiteConnection(connectionString);

            List<Candidate> candidatesList = new List<Candidate>();
            try
            {
                conn.Open();
                //MyClass[] myArray = list.ToArray();

                //Khai bao chuoi sA="";
                //String sA = "";
                //for (listObj.cout) sA = "1,2,3";

                //for (int i = 0; i < skillIds.Count; i++)
                //{
                //    sA = skillIds[i].ToString();
                //    sA = string.Join(",", sA);
                //    MessageBox.Show(sA);
                //}

                //select Id, FirstName, LastName from Candidate c inner join Candidate_Skill ck on c.id = ck.Candidate_id where ck.SkillId in (" + sA + ");

                string sql = "SELECT c.Id, c.FirstName, c.LastName, s.Name FROM Candidate c INNER JOIN Candidate_Skill cs ON c.id = cs.CandidateId INNER JOIN Skill s ON s.Id = cs.SkillId ";
                //where cs.SkillId in (1,2,3)

                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                SQLiteDataReader reader = cmd.ExecuteReader();
                int id;
                string fname;
                string lname;
                string sname;
                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                    fname = reader.GetString(1);
                    lname = reader.GetString(2);
                    sname = reader.GetString(3);
                    Candidate candidate = new Candidate();
                    candidate.Id = id;
                    candidate.FirstName = fname;
                    candidate.LastName = lname;
                    candidate.SkillNames = sname;
                    candidatesList.Add(candidate);
                }

            }
            catch (System.Exception e) { MessageBox.Show(e.ToString()); }
            finally { conn.Close(); }
            return candidatesList;
        }

        public static List<Candidate> SearchCandidateSkills(List<int> skillIds)
        {
            //
            SQLiteConnection conn;
            conn = new SQLiteConnection(connectionString);

            List<Candidate> candidatesList = new List<Candidate>();
            try
            {
                conn.Open();
                //MyClass[] myArray = list.ToArray();

                //Khai bao chuoi sA="";
                String sCondition = "";
                //for (listObj.cout) sA = "1,2,3";

                int iSkillCount = skillIds.Count;

                for (int i = 0; i < iSkillCount; i++)
                {
                    if (i == iSkillCount - 1)
                    {
                        sCondition = sCondition + skillIds[i].ToString();
                    }
                    else
                    {
                        sCondition = sCondition + skillIds[i].ToString() + ",";
                    }


                }
              

                string sql = "SELECT c.Id, c.FirstName, c.LastName, s.Name " +
                     "FROM (SELECT LST1.CandidateId FROM (SELECT COUNT(*) CNT, cs1.CandidateId FROM Candidate_Skill cs1 " +
                        "WHERE cs1.SkillId IN (" + sCondition + ") " +
                        "GROUP BY cs1.CandidateId) LST1 " +
                         "WHERE LST1.CNT = " + iSkillCount +
                        ") LST INNER JOIN " +
                        "Candidate c ON LST.CandidateId = c.Id INNER JOIN Candidate_Skill cs ON c.id = cs.CandidateId INNER JOIN Skill s ON s.Id = cs.SkillId ORDER BY c.id ASC; ";
               
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                SQLiteDataReader reader = cmd.ExecuteReader();
                int id;
                string fname;
                string lname;
                string sname;
                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                    fname = reader.GetString(1);
                    lname = reader.GetString(2);
                    sname = reader.GetString(3);
                    Candidate candidate = new Candidate();
                    candidate.Id = id;
                    candidate.FirstName = fname;
                    candidate.LastName = lname;
                    candidate.SkillNames = sname;
                    candidatesList.Add(candidate);
                }
            }
            catch (System.Exception e) { MessageBox.Show(e.ToString()); }
            finally { conn.Close(); }
            return candidatesList;
        }

        //Update Candidate_Skill
        public void updateCandidate_Skill(int canId, string fName, string lName, List<int> skillIds)
        {    
            // 
            UpdateCandidate(canId, fName, lName);
            DeleteCandidateSkills(canId);
            foreach (int skillId in skillIds)
            {
                UpdateCandidateSkills(canId, skillId);
            }
        }

        private void UpdateCandidate(int canId, string fName, string lName)
        {
            string query;
            query = $"UPDATE Candidate SET FirstName='{fName}', LastName='{lName}' WHERE Id={canId}";

            ExecuteNonQuery(query);
         }
        private void DeleteCandidateSkills(int canId)
        {
            string query;
            query = $"DELETE FROM Candidate_Skill WHERE CandidateId={canId}";
            ExecuteNonQuery(query);

        }
       
        private void UpdateCandidateSkills(int canId, int SkillId)
        {
            string query;
            query = $"INSERT INTO Candidate_Skill(CandidateId, SkillId) VALUES({canId}, {SkillId})";
            ExecuteNonQuery(query);
        }



        //public static void updateCandidate(Candidate candidate)
        //{

        //    SQLiteConnection conn;
        //    conn = new SQLiteConnection(connectionString);
        //    try
        //    {
        //        conn.Open();
        //        string id = candidate.Id.ToString();
        //        string sql = "update Candidate set FirstName=@FirstName, LastName=@LastName where id=@id";
        //        SQLiteCommand cmd = new SQLiteCommand(sql, conn);
        //        string fname = candidate.FirstName;
        //        string lname = candidate.LastName;
        //        cmd.Parameters.AddWithValue("id", candidate.Id);
        //        cmd.Parameters.AddWithValue("FirstName", fname);
        //        cmd.Parameters.AddWithValue("LastName", lname);
        //        cmd.ExecuteNonQuery();
        //        cmd.Parameters.Clear();

        //    }
        //    catch (System.Exception e) { MessageBox.Show(e.ToString()); }

        //    finally { conn.Close(); }

        //}

        //public static void updateCandidateSkill(int id)
        //{

        //    SQLiteConnection conn;
        //    conn = new SQLiteConnection(connectionString);
        //    try
        //    {
        //        conn.Open();
        //        string Id = id.ToString();
        //        string sql = "delete from Candidate_Skill where CandidateId=@id";

        //        SQLiteCommand cmd = new SQLiteCommand(sql, conn);


        //        cmd.Parameters.AddWithValue("id", id);
        //        cmd.ExecuteNonQuery();
        //        cmd.Parameters.Clear();

        //        string sql2 = "INSERT INTO Candidate_Skill from Candidate where Id=@id";
        //        SQLiteCommand cmd2 = new SQLiteCommand(sql2, conn);
        //        cmd2.Parameters.AddWithValue("id", id);
        //        cmd2.ExecuteNonQuery();
        //        cmd2.Parameters.Clear();

        //    }
        //    catch (System.Exception e) { MessageBox.Show(e.ToString()); }

        //    finally
        //    {
        //        conn.Close();
        //    }
        //}

        public static void deleteCandidate(int id)
        {

            SQLiteConnection conn;
            conn = new SQLiteConnection(connectionString);
            try
            {
                conn.Open();
                string Id = id.ToString();
                string sql = "delete from Candidate_Skill where CandidateId=@id";
                
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
               

                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();              
                cmd.Parameters.Clear();

                string sql2 = "delete from Candidate where Id=@id";
                SQLiteCommand cmd2 = new SQLiteCommand(sql2, conn);
                cmd2.Parameters.AddWithValue("id", id);
                cmd2.ExecuteNonQuery();
                cmd2.Parameters.Clear();
            }
            catch (System.Exception e) { MessageBox.Show(e.ToString()); }

            finally
            {
                conn.Close();


            }

        }
    }
}
