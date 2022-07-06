using System;
using System.Data;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataLayer;

namespace PresentationLayer {
  public partial class MainWindow : Window {
    public MainWindow() {
      InitializeComponent();
      // show data when windows loads
      _show(); // display data from db, when app loads.
    }

    private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e) {

    }

    private void TextBox_TextChanged(object sender, TextChangedEventArgs e) {

    }

    public void _clear() {
      // clear textbox fields
      _name.Clear();
      _surname.Clear();
      _gender.Clear();
      _email.Clear();
      _id.Clear();
    }

    // read user data in static properties
    public void _readUserData() {
      DataLayer.DataLayer_._Name = _name.Text;
      DataLayer.DataLayer_._Surname = _surname.Text;
      DataLayer.DataLayer_._Gender = _gender.Text;
      DataLayer.DataLayer_._Email = _email.Text;
    }

    public void _show() { // read data and show in datagrid
      var data = DataLayer.DataLayer_.GetAll();
      if( data != null ) { // if returned data is not null
        _datagrid.ItemsSource = data.DefaultView;
      }
      // if there is problem fetching all data..
      else { MessageBox.Show("Error Fetching All Data"); }
    }

    private void Button_Click(object sender, RoutedEventArgs e) {
      // DELETE the selected data from user
      if( _id.Text != "" ) { // if id is provided.
        var status = DataLayer.DataLayer_.Delete(_id.Text);
        if( status != null ) { // if there is an error
          MessageBox.Show(status);
        }
        else { // if there is no error deleting user data
          MessageBox.Show("User Data Suceessfully Delete");
        }
        _clear(); // to clear input fields
        _show();
      }
    }

    private void Button_Click_1(object sender, RoutedEventArgs e) {
      // INSERT data into database
      if (_name.Text != "" && _surname.Text != "" && _gender.Text != "" && _email.Text != "" ) {
        _readUserData(); // read user data into properties
        _clear(); // clear textbox fields
        var status = DataLayer.DataLayer_.Insert(); // insert data in database
        // if there is an error when inserting user data, display error message.
        if( status != null ) { MessageBox.Show(status); }
        else { MessageBox.Show("Successsfully Added User Data"); }
        _show(); // update data grid display
      }
      else { MessageBox.Show("Please Provide All Data"); }
    }

    private void Button_Click_2(object sender, RoutedEventArgs e) {
      // UPDATE data from database
      if (_name.Text != "" && _surname.Text != "" && _gender.Text != "" && _email.Text != "" && _id.Text != "" ) {
        _readUserData(); // read user data into properties
        var status = DataLayer.DataLayer_.Update(_id.Text); // update data in database
        _clear(); // clear textbox fields
        // if there is an error when updating user data, display error message.
        if (status != null) { MessageBox.Show(status); }
        else { MessageBox.Show("Successsfully Updated User Data"); }
        _show(); // update data grid display
      }
      else { MessageBox.Show("Please Provide All Data"); }
    }

    private void Button_Click_3(object sender, RoutedEventArgs e) {
      // CLEAR data from input fields
      _clear();
      _datagrid.ItemsSource = null;
    }

    private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e) {
      // get user ID input
      if( _id.Text != "" ) { // if provided data is not empty
        try { // pass id to data layer function to get a one user. and return user data.
          int id = int.Parse(_id.Text); // try convert to int
          var user = DataLayer.DataLayer_.GetOne(_id.Text); 
          // fill textbox fields
          _name.Text = user.Rows[0].Field<string>(1); // get user name
          _surname.Text = user.Rows[0].Field<string>(2); // get user surname
          _gender.Text = user.Rows[0].Field<string>(3); // get user gender 
          _email.Text = user.Rows[0].Field<string>(4); // get user email
        }
        catch( Exception ex ) { // if error, print error message.
          MessageBox.Show($"{ex.Message} : Please Provide Int Value");
        }
        finally {
          // 
        }
      }
    }
  }
}
