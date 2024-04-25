using System;
using System.Windows;


namespace ClientProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {    
        public MainWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void Log_enter_Click(object sender, RoutedEventArgs e)
        {            
            try
            {
                //поиск совпадений по введеному логину и паролю
                var userLogin = "Admin";
                var userPassword = "123";

                if (userLogin == login_box.Text && password_box.Password == userPassword) //если совпадения есть
                {
                    //создание и открытие нового окна
                    DataInfo info = new DataInfo();
                    info.Show();
                    this.Close(); //после успешного входа, это окно уже не требуется
                }
                else
                {
                    MessageBox.Show("Неверно введены логин или пароль", "Ошибка");
                }
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message); //вывод об ошибке и возврат
                return;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Логин: Admin \nПароль: 123", "Краткое руководство");
        }
    }
}
