using System.Windows;


namespace ClientProject
{
    /// <summary>
    /// Логика взаимодействия для AddNewClient.xaml
    /// </summary>
    public partial class AddNewClient : Window
    {
        public AddNewClient()
        {
            InitializeComponent();
        }
        public AddNewClient(Clients new_client) : this()
        {
            //события после нажатия кнопки
            add.Click += delegate
            {
                //проверка на заполненность 
                if (NewName.Text == string.Empty ||
                NewLastname.Text == string.Empty ||
                NewMiddlename.Text == string.Empty ||
                NewEmail.Text == string.Empty)
                {
                    MessageBox.Show("Заполните все обязательные поля");
                    return;
                }

                new_client.name = NewName.Text;
                new_client.lastname = NewLastname.Text;
                new_client.middlename = NewMiddlename.Text;
                if (NewPhonenumber.Text != string.Empty)
                {
                    new_client.phonenumber = NewPhonenumber.Text;
                }                
                new_client.email = NewEmail.Text;
                this.DialogResult = true;
            };
        }
   
    }
}
