using System;
using System.Data.SqlClient;
using System.Diagnostics;

string connetionString = null;

string  ServerName    = "qfrog.de";
string  DatabaseName  = "froggy_chatbot";
string  UserName      = "Froggy";
string  Password      = "t4stE9~2";

TraceWriter log = new TraceWriter();

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
