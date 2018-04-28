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
        // Data source address
        static string connectionString = @"Data Source = C:\Users\flash\Source\repos\GeekHunter\GeekHunter\GeekHunter\GeekHunter.sqlite;Version=3;";

        // Load all the Candidates with Id, FirstName & LastName from Candidate table
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

        // Load all the Skills with Id, Name from Skill table
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
 
        /// <summary>
        /// Create a new Candidate with Skills
        /// </summary>
        /// <param name="fName"></param>
        /// <param name="lName"></param>
        /// <param name="skillIds"></param>
        /// <returns></returns>
        public int addCandidate_Skill(string fName, string lName, List<int> skillIds)
        {
            int candidateId;
            candidateId = LoadAllCandidates().Max(c => c.Id) + 1;

            AddCandidate(candidateId, fName, lName);

            foreach (int skillId in skillIds)
            {
                AddCandidateSkills(candidateId, skillId);
            }
            return candidateId;
        }

        // Insert Id, FirstName & LastName into Candidate table
        private void AddCandidate(int id, string fName, string lName)
        {
            string query;
            query = $"insert into Candidate(Id, FirstName, LastName) values({id}, '{fName}', '{lName}')";

            ExecuteNonQuery(query);
        }

        // Insert CandidateId & SkillId into Candidate_Skill table
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

                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                SQLiteDataReader reader = cmd.ExecuteReader();
                
                string fname;
                string lname;
                
                while (reader.Read())
                {                  
                    fname = reader.GetString(0);
                    lname = reader.GetString(1);
                    
                    candidate.FirstName = fname;
                    candidate.LastName = lname;          
                }
            }
            catch (System.Exception e) { MessageBox.Show(e.ToString()); }
            finally { conn.Close(); }
            return candidate;
        }

        //Get 1 Candidate with Skills by Id
        public static List<Skills> GetOneCandidateSkill(int id)
        {
            SQLiteConnection conn;
            conn = new SQLiteConnection(connectionString);
         
            List<Skills> skills = new List<Skills>();
            try
            {
                conn.Open();

                string sql = $"SELECT SkillId FROM Candidate_Skill WHERE CandidateId = {id} ";

                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                SQLiteDataReader reader = cmd.ExecuteReader();
                int sid;

                while (reader.Read())
                {
                    sid = reader.GetInt32(0);

                    Skills skill = new Skills();
                    skill.Id = sid;
                    skills.Add(skill);
                }
            }
            catch (System.Exception e) { MessageBox.Show(e.ToString()); }
            finally { conn.Close(); }
            return skills;
        }

        //Search all candidates
        public static List<Candidate> SearchCandidate()
        {
            SQLiteConnection conn;
            conn = new SQLiteConnection(connectionString);

            List<Candidate> candidatesList = new List<Candidate>();
            try
            {
                conn.Open();

                string sql = "SELECT c.Id, c.FirstName, c.LastName, s.Name FROM Candidate c INNER JOIN Candidate_Skill cs ON c.id = cs.CandidateId INNER JOIN Skill s ON s.Id = cs.SkillId ";

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

        // Select technologies candidate has experience in from the predefined list
        public static List<Candidate> SearchCandidateSkills(List<int> skillIds)
        {
            SQLiteConnection conn;
            conn = new SQLiteConnection(connectionString);

            List<Candidate> candidatesList = new List<Candidate>();
            try
            {
                conn.Open();
                String sCondition = "";

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

                //string sql = "SELECT c.Id, c.FirstName, c.LastName, s.Name " +
                //     "FROM (SELECT LST1.CandidateId FROM (SELECT COUNT(*) CNT, cs1.CandidateId FROM Candidate_Skill cs1 " +
                //        "WHERE cs1.SkillId IN (" + sCondition + ") " +
                //        "GROUP BY cs1.CandidateId) LST1 " +
                //         "WHERE LST1.CNT = " + iSkillCount +
                //        ") LST INNER JOIN " +
                //        "Candidate c ON LST.CandidateId = c.Id INNER JOIN Candidate_Skill cs ON c.id = cs.CandidateId INNER JOIN Skill s ON s.Id = cs.SkillId ORDER BY c.id ASC; ";

                string sql = "SELECT c.Id, c.FirstName, c.LastName, s.Name " +
                     "FROM (SELECT LST1.CandidateId FROM (SELECT COUNT(*) CNT, cs1.CandidateId FROM Candidate_Skill cs1 " +
                        "WHERE cs1.SkillId IN (" + sCondition + ") " +
                        "GROUP BY cs1.CandidateId) LST1 " +
                         "WHERE LST1.CNT = " + iSkillCount +
                        ") LST INNER JOIN " +
                        "Candidate c ON LST.CandidateId = c.Id INNER JOIN Candidate_Skill cs ON c.id = cs.CandidateId INNER JOIN Skill s ON s.Id = cs.SkillId " +
                        "WHERE cs.SkillId IN (" + sCondition + ") " +
                        "ORDER BY c.id ASC; ";

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

        //Update Candidate details & their Skills at the same time
        public void updateCandidate_Skill(int canId, string fName, string lName, List<int> skillIds)
        {    
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
        
        // Delete a Candidate
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
