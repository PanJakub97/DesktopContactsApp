using DesktopContactsApp.Classes;
using SQLite;
using System;
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
using System.Windows.Shapes;
using static SQLite.SQLite3;

namespace DesktopContactsApp
{
    /// <summary>
    /// Interaction logic for NewContactWIndow.xaml
    /// </summary>
    public partial class ContactDetailsWindow : Window
    {
        Contact _contact;
        public ContactDetailsWindow(Contact contact)
        {
            InitializeComponent();

            Owner = Application.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            _contact = contact;

            nameTextBox.Text = contact.Name;
            emailTextBox.Text = contact.Email;
            phoneTextBox.Text = contact.Phone;
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            _contact.Name = nameTextBox.Text;
            _contact.Email = emailTextBox.Text;
            _contact.Phone = phoneTextBox.Text;

            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure, that you want to update?",
                "Update Warning!", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
                {
                    connection.CreateTable<Contact>();
                    connection.Update(_contact);
                }
            }
            else if (messageBoxResult == MessageBoxResult.No)
                return;

            Close();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure, that you want to delete this contact?",
                "Delete Warning!", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
                {
                    connection.CreateTable<Contact>();
                    connection.Delete(_contact);
                }
            }
            else if (messageBoxResult == MessageBoxResult.No)
                return;

            Close();
        }
    }
}
