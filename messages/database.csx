using System;
using System.Data.SqlClient;
using System.Diagnostics;

string connetionString = null;

string  ServerName    = "qfrog.de";
string  DatabaseName  = "froggy_chatbot";
string  UserName      = "Froggy";
string  Password      = "t4stE9~2";

SqlConnection cnn ;
connetionString = "Data Source="+ServerName+";Initial Catalog="+DatabaseName+";User ID="+UserName+";Password="+Password;
cnn = new SqlConnection(connetionString);
try {
  cnn.Open();
  Debug.WriteLine("Connection Open ! ");
  cnn.Close();
}
catch (Exception ex){
  Debug.WriteLine("Can not open connection ! ");
}
