using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataLayer {
    public class DataLayer_ {
      // configuration string
      static string config = "username=root;password=;database=220072233_ak_lab4;datasource=127.0.0.1;port=3306";
      // create a mysql connection object
      static MySqlConnection connectObj = new MySqlConnection(config);
      // users properties
      static public string _Name { set; get; }
      static public string _Surname { set; get; }
      static public string _Gender { set; get; }
      static public string _Email { set; get; }

      // insert a users data
      static public string Insert() {
        string query = $"insert into `ujstaffportal`(`firstname`,`lastname`,`gender`,`email`) values ('{_Name}','{_Surname}','{_Gender}','{_Email}')"; // query string, using string interpolation
        try {
          connectObj.Open(); // open mysql connection 
          MySqlCommand commandObj = new MySqlCommand(query, connectObj);
          MySqlDataReader readObj = commandObj.ExecuteReader(); // execute the query
          readObj.Close(); // close the reader object
          connectObj.Close(); // close mysql connection
          return null; // return null
        } // when something is not right
        catch (Exception ex) {
          connectObj.Close(); // close mysql connection
          return ex.Message; // return error message..
        }
      }
      
      // update a users data
      static public string Update( string id) {
        // query string, using string interpolation
        string query = $"update `ujstaffportal` set `firstname`='{_Name}',`lastname`='{_Surname}',`gender`='{_Gender}',`email`='{_Email}' where `id`='{id}'";
        try {
          MySqlCommand commandObj = new MySqlCommand(query, connectObj);
          connectObj.Open(); // open mysql connection 
          MySqlDataReader readObj = commandObj.ExecuteReader(); // execute the query
          readObj.Close(); // close the reader object
          connectObj.Close(); // close mysql connection
          return null; // return null
        } // when something is not right ... // return error message..
        catch (Exception ex) {
          connectObj.Close(); // close mysql connection
          return ex.Message;
        }
      }

      // delete a users data
      static public string Delete( string id ) {
        // query string, using string interpolation
        string query = $"delete from `ujstaffportal` where `id`={id}"; 
        try {
          MySqlCommand commandObj = new MySqlCommand(query, connectObj);
          connectObj.Open(); // open mysql connection 
          MySqlDataReader readObj = commandObj.ExecuteReader(); // execute the query
          readObj.Close(); // close the reader object
          connectObj.Close(); // close mysql connection
          return null; // return null
        } // when something is not right
        catch (Exception ex) {
          connectObj.Close(); // close mysql connection
          return ex.Message; // return error message..
        }
      }

      // get all users data
      static public DataTable GetAll() {
        string query = $"select * from `ujstaffportal`";// query string, using string interpolation
        DataTable data = new DataTable(); // create a new datatable object
        try {
          connectObj.Open(); // open mysql connection
          MySqlDataAdapter adp = new MySqlDataAdapter(); // create a new mysql data adapter object
          adp.SelectCommand = new MySqlCommand(query, connectObj);
          adp.Fill(data); // fill the data adapter with data
          connectObj.Close(); // close mysql connection
          return data; // return all data
        } // when something is not right
        catch (Exception ex) {
          Console.WriteLine(ex.Message);
          connectObj.Close(); // close mysql connection
          return null; // return error message..
        }
      }

    // get a users data
    static public DataTable GetOne( string id ) {
      string query = $"select * from `ujstaffportal` where `id`={id}";// query string, using string interpolation
      DataTable data = new DataTable(); // create a new datatable object
      try {
        connectObj.Open(); // open mysql connection
        MySqlDataAdapter adp = new MySqlDataAdapter(); // create a new mysql data adapter object
        adp.SelectCommand = new MySqlCommand(query, connectObj);
        adp.Fill(data); // fill the data adapter with data
        connectObj.Close(); // close mysql connection
        return data; // return all data
      } // when something is not right
      catch (Exception ex) {
        Console.WriteLine(ex.Message);
        connectObj.Close(); // close mysql connection
        return null; // return error message..
      }
    }



  }
}
