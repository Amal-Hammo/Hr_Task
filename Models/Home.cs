using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task4_Hr.DB_Manage;

namespace Task4_Hr.Models
{
    public class Home
    {
        private OracleConnection Ocon;
        private Connection con;
        public Home()
        {
            con = new Connection();
            Ocon = con.GetConnection();
        }
        public DataTable GetData(string TableName)
        {
            DataTable data = new DataTable();
            OracleCommand cmd = new OracleCommand($"SELECT * FROM {TableName}", Ocon);
            cmd.CommandType = CommandType.Text;

            using (OracleDataAdapter da = new OracleDataAdapter(cmd))
            {
                da.Fill(data);
            }
            Close();
            return data;
        }

        public DataTable GetStateData()
        {
            DataTable data = new DataTable();
            OracleCommand cmd = new OracleCommand("SELECT * FROM C_CITIES_TB", Ocon);
            cmd.CommandType = CommandType.Text;

            using (OracleDataAdapter da = new OracleDataAdapter(cmd))
            {
                da.Fill(data);
            }
            Close();
            return data;
        }

        public string SaveEmployee(FormCollection form, OracleTransaction CmdTrans)
        {
            OracleCommand cmd ;
            try
            {
                // Set the command
                cmd = new OracleCommand("HR_USERS_PKG.add_emp_pr", Ocon);
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.StoredProcedure;
                // Bind 
                cmd.Parameters.Clear();

                cmd.Parameters.Add("emp_FullName", OracleDbType.NVarchar2).Value = form["EmployeeName"];
                cmd.Parameters.Add("emp_card_num", OracleDbType.NVarchar2).Value = form["IDCard"];
                cmd.Parameters.Add("emp_gender_id", OracleDbType.Int32).Value = form["Gender"];
                cmd.Parameters.Add("emp_status_id", OracleDbType.Int32).Value = form["Status"];
                cmd.Parameters.Add("emp_dob", OracleDbType.Date).Value = form["DateBirth"];
                cmd.Parameters.Add("emp_datehire", OracleDbType.Date).Value = form["DateHiring"];
                cmd.Parameters.Add("emp_country_id", OracleDbType.Int32).Value = form["Country"];
                cmd.Parameters.Add("emp_state_id", OracleDbType.Int32).Value = form["State"];
                cmd.Parameters.Add("emp_city_id", OracleDbType.Int32).Value = form["City"];
                cmd.Parameters.Add("emp_address", OracleDbType.NVarchar2).Value = form["Address"];
                cmd.Parameters.Add("emp_tele", OracleDbType.NVarchar2).Value = form["Tele"];
                cmd.Parameters.Add("emp_phone", OracleDbType.NVarchar2).Value = form["Phone"];
                cmd.Parameters.Add("emp_email", OracleDbType.NVarchar2).Value = form["Email"];
                cmd.Parameters.Add("emp_jobtitle_id", OracleDbType.Int32).Value = form["jobTitle"];
                cmd.Parameters.Add("emp_deptid", OracleDbType.Int32).Value = form["Department"];
                cmd.Parameters.Add("emp_empstaus_id", OracleDbType.Int32).Value = form["EmployeeStatus"];
                cmd.Parameters.Add("emp_Salary", OracleDbType.Int32).Value = form["BasicSalary"];
                OracleParameter emp_id_out = cmd.Parameters.Add("emp_id_out", OracleDbType.Int32);
                emp_id_out.Direction = ParameterDirection.Output;
                OracleParameter error_status = cmd.Parameters.Add("error_status", OracleDbType.Int32);
                error_status.Direction = ParameterDirection.Output;
                OracleParameter error_description = cmd.Parameters.Add("error_description", OracleDbType.Int32);
                error_description.Direction = ParameterDirection.Output;

                /*********************************************/
                cmd.ExecuteNonQuery();
                if (error_status.Value.ToString() == "0")
                {
                    throw new Exception(error_description.Value.ToString());
                }
                else
                {
                    return emp_id_out.Value.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

        }

        public string UpdateEmployee(FormCollection form, OracleTransaction CmdTrans)
        {
            OracleCommand cmd;
            try
            {
                // Set the command
                cmd = new OracleCommand("HR_USERS_PKG.Edit_emp_pr", Ocon);
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.StoredProcedure;
                // Bind 
                cmd.Parameters.Clear();

                cmd.Parameters.Add("emp_id_in", OracleDbType.Int32).Value = form["EmpID"];
                cmd.Parameters.Add("emp_FullName", OracleDbType.NVarchar2).Value = form["EmployeeName"];
                cmd.Parameters.Add("emp_card_num", OracleDbType.NVarchar2).Value = form["IDCard"];
                cmd.Parameters.Add("emp_gender_id", OracleDbType.Int32).Value = form["Gender"];
                cmd.Parameters.Add("emp_status_id", OracleDbType.Int32).Value = form["Status"];
                cmd.Parameters.Add("emp_dob", OracleDbType.Date).Value = form["DateBirth"];
                cmd.Parameters.Add("emp_datehire", OracleDbType.Date).Value = form["DateHiring"];
                cmd.Parameters.Add("emp_country_id", OracleDbType.Int32).Value = form["Country"];
                cmd.Parameters.Add("emp_state_id", OracleDbType.Int32).Value = form["State"];
                cmd.Parameters.Add("emp_city_id", OracleDbType.Int32).Value = form["City"];
                cmd.Parameters.Add("emp_address", OracleDbType.NVarchar2).Value = form["Address"];
                cmd.Parameters.Add("emp_tele", OracleDbType.NVarchar2).Value = form["Tele"];
                cmd.Parameters.Add("emp_phone", OracleDbType.NVarchar2).Value = form["Phone"];
                cmd.Parameters.Add("emp_email", OracleDbType.NVarchar2).Value = form["Email"];
                cmd.Parameters.Add("emp_jobtitle_id", OracleDbType.Int32).Value = form["jobTitle"];
                cmd.Parameters.Add("emp_deptid", OracleDbType.Int32).Value = form["Department"];
                cmd.Parameters.Add("emp_empstaus_id", OracleDbType.Int32).Value = form["EmployeeStatus"];
                cmd.Parameters.Add("emp_Salary", OracleDbType.Int32).Value = form["BasicSalary"];
                OracleParameter emp_id_out = cmd.Parameters.Add("emp_id_out", OracleDbType.Int32);
                emp_id_out.Direction = ParameterDirection.Output;
                OracleParameter error_status = cmd.Parameters.Add("error_status", OracleDbType.Int32);
                error_status.Direction = ParameterDirection.Output;
                OracleParameter error_description = cmd.Parameters.Add("error_description", OracleDbType.Int32);
                error_description.Direction = ParameterDirection.Output;

                /*********************************************/
                cmd.ExecuteNonQuery();
                if (error_status.Value.ToString() == "0")
                {
                    throw new Exception(error_description.Value.ToString());
                }
                else
                {
                    return form["EmpID"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

        }
        public Boolean DeleteEmployee(int id, OracleTransaction CmdTrans)
        {
            OracleCommand cmd;
            try
            {
                // Set the command
                cmd = new OracleCommand("HR_USERS_PKG.Delete_emp_pr", Ocon);
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.StoredProcedure;
                // Bind 
                cmd.Parameters.Clear();

                cmd.Parameters.Add("emp_id_in", OracleDbType.Int32).Value = id;
               
                OracleParameter error_status = cmd.Parameters.Add("error_status", OracleDbType.Int32);
                error_status.Direction = ParameterDirection.Output;
                OracleParameter error_description = cmd.Parameters.Add("error_description", OracleDbType.Int32);
                error_description.Direction = ParameterDirection.Output;

                /*********************************************/
                cmd.ExecuteNonQuery();
                if (error_status.Value.ToString() == "0")
                {
                    throw new Exception(error_description.Value.ToString());
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

        }
        void Close()
        {
            con.Close(Ocon);
        }
    }
}