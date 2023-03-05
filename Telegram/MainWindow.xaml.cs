using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace Telegram
{ 
    public partial class MainWindow : Window
    {

        public List<Person?>? People { get; set; }

        public Person? CurrentPerson { get; set; }

        public ObservableCollection<string?>? Messages { get; set; } = new();

        public int ActualWidthh { get; set; }


        public MainWindow()
        {
            InitializeComponent();

            People = new List<Person?>{
                new Person { Name = "Ali", Image = @"https://media.sproutsocial.com/uploads/2022/06/profile-picture.jpeg" },
                new Person { Name = "Aqil", Image = @"https://m.media-amazon.com/images/M/MV5BMTk3M2YxMTUtMDg0OC00ZmMzLWFkNWQtZWU5YjE1ZDBlODMwXkEyXkFqcGdeQXVyNjUxMjc1OTM@._V1_.jpg" },
                new Person { Name = "Xosu", Image = @"https://images.newscientist.com/wp-content/uploads/2019/06/18142824/einstein.jpg" },
            };

            DataContext = this;
        }

        private void PeopleList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Person? p = PeopleList.SelectedItem as Person;

            foreach (var item in People)
            {
                if (item.Name == p.Name)
                {
                    CurrentPerson = item;
                    break;
                }
            }

            //messageList.ItemsSource = CurrentPerson.Messages;  

            txtblock_name.Text = CurrentPerson.Name;

            Messages = CurrentPerson.Messages;

            txtblock_status.Text = "online";

        }


        private void btn_send_Click(object sender, RoutedEventArgs e)
        {
            CurrentPerson.Messages.Add(WriteMesTextBox.Text);

            ActualWidthh = WriteMesTextBox.Text.Length;

            //textblockborder.Text = WriteMesTextBox.Text;

            WriteMesTextBox.Clear();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            var json = JsonSerializer.Serialize(People);
            File.WriteAllText("People.json", json);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var json = File.ReadAllText("People.json");
            People = JsonSerializer.Deserialize<List<Person>>(json);
        }

        private void txtbox_searchname_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Person> temp = new List<Person>();
            
            if (txtbox_searchname.Text != "")
            {
                foreach (var item in People)
                {
                    var a = item.Name.First();
                    char? b = txtbox_searchname.Text.ToString().ToUpper().First();
                    if (a == b)
                        temp.Add(item);
                }
                PeopleList.ItemsSource = temp;
            }
            else
                PeopleList.ItemsSource = People;
        }

    }
}