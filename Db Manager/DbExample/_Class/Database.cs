﻿using DbExample.Interfaces;
using DbExample.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DbExample._Class
{
    public class Database : IDatabase
    {
        private SqlConnection con = new SqlConnection("Data Source=LAPTOP;Initial Catalog=Sistem;User Id=sa;Password=123");
        private SqlCommand cmd;
        private SqlParameter parameter;
        private SqlDataAdapter adapter;
        private SqlDataReader reader=null;
        private DataTable dt;

        public List<T> veriCek<T>()
        {
            T result = default(T);
            var x  = typeof(T);
            string query = "SELECT * FROM " + x.Name;
            List<T> list = new List<T>();

            IList<PropertyInfo> properties = new List<PropertyInfo>(x.GetProperties());

            try
            {
                con.Open();
                cmd = new SqlCommand(query, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var y = (T)Activator.CreateInstance(typeof(T));
                    foreach (PropertyInfo item in properties)
                    {
                        item.SetValue(y, reader[item.Name], null);
                    }
                    list.Add(y);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }finally
            {
                if(con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return list;

        }


        public void veriEkle(object data)
        {
            string table_name = data.GetType().Name;
            IList<PropertyInfo> prop = new List<PropertyInfo>(data.GetType().GetProperties());
            string query = "INSERT INTO ";
            query = query + table_name + "(";
            string subQuery = "VALUES (";
            cmd = new SqlCommand();
            foreach (PropertyInfo item in prop)
            {
                
                if(item.Name != "id")
                {
                    query += item.Name.ToString() + ",";
                    subQuery += "@" + item.Name + ",";
                    parameter = cmd.Parameters.AddWithValue('@' + item.Name, item.GetValue(data, null));
                }
            }
            query = query.TrimEnd(',');
            subQuery = subQuery.TrimEnd(',');
            query += ")";
            subQuery += ")";
      
            try
            {
                con.Open();
                query = query + subQuery;
                cmd.CommandText = query;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                Console.Write("Başarılı");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }finally
            {
                if(con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
    }
}