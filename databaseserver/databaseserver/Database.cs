using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Text;

namespace databaseserver
{
    class Database
    {
        private SQLiteConnection sqlcon_;
        private SQLiteCommand com_;
        private SQLiteDataAdapter sqlda_;
        private DataSet DS_ = new DataSet();
        private DataTable DT_ = new DataTable();
        private void Connect() {
           sqlcon_ = new SQLiteConnection(@"data source=I:\COMP134Project\mmoments\databaseserver\songdatabase.mmd");
        }
        public void ExecuteQuery(string txtQuery)
        {
            this.Connect();
            sqlcon_.Open();
            com_ = sqlcon_.CreateCommand();
            com_.CommandText = txtQuery;
            com_.ExecuteNonQuery();
            sqlcon_.Close();
        }
        public DataTable GetData(string query)
        {
            this.Connect();
            sqlcon_.Open();
            com_ = sqlcon_.CreateCommand();
            string CommandText = query;
            sqlda_ = new SQLiteDataAdapter(CommandText, sqlcon_);
            DS_.Reset();
            sqlda_.Fill(DS_);
            DT_ = DS_.Tables[0];
            sqlcon_.Close();
            return DT_;
        }
    }
}
