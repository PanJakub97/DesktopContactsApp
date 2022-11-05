using DesktopContactsApp.Classes;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopContactsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Contact> contacts;
        public MainWindow()
        {
            InitializeComponent();

            contacts = new List<Contact>();

            ReadDataBase();
        }

        private void NewContactButton_Click(object sender, RoutedEventArgs e)
        {
            NewContactWIndow newContactWIndow = new NewContactWIndow();
            newContactWIndow.ShowDialog();

            ReadDataBase();
        }

        void ReadDataBase()
        {
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.databasePath))
            {
                conn.CreateTable<Contact>();
                contacts = conn.Table<Contact>().OrderBy(c => c.Name).ToList();
            }

            if (contacts != null)
            {
                contactsListView.ItemsSource = contacts;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Filtering 

            TextBox searchTextBox = sender as TextBox;

            var filteredList = contacts.Where(c => (c.Name.ToLower().Contains(searchTextBox.Text.ToLower()))
                                                || (c.Email.Contains(searchTextBox.Text.ToLower()))
                                                || (c.Phone.Contains(searchTextBox.Text.ToLower())));

            contactsListView.ItemsSource = filteredList;
        }

        private void contactsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Selecting position from list to update/delete

            Contact selectedContact = (Contact)contactsListView.SelectedItem;

            if (selectedContact != null)
            {
                ContactDetailsWindow contactDetailsWindow = new ContactDetailsWindow(selectedContact);
                contactDetailsWindow.ShowDialog();

                ReadDataBase();
            }
        }
    }
}
