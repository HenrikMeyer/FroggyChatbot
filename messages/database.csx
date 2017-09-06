using System;
using System.Windows.Forms;
using System.Data.SqlClient;


string connetionString = null;

string  ServerName = "froggy_chatbot";
string  UserName   = "Froggy";
string  Password   = "t4stE9~2";

SqlConnection cnn ;
connetionString = "Data Source="+ServerName+";Initial Catalog="+DatabaseName+";User ID="+UserName+";Password="+Password;
cnn = new SqlConnection(connetionString);
try {
  cnn.Open();
  log.Info("Connection Open ! ");
  cnn.Close();
}
catch (Exception ex){
  log.Info("Can not open connection ! ");
}
