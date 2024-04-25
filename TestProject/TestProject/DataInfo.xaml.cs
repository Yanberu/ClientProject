using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace ClientProject
{
    /// <summary>
    /// Логика взаимодействия для DataInfo.xaml
    /// </summary>
    public partial class DataInfo : Window

    {
        ClientsEntities context;
        ClientsEntities context2;

        Clients row;

        public DataInfo()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            context = new ClientsEntities();
            
            MessageBox.Show($"Приветствуем, {Environment.UserName} ✓");
            FirstCommand();
        }


        /// <summary>
        ///  Метод вывода первичной таблицы на экран
        /// </summary>
        public void FirstCommand()
        {                                  
            try
            {
                //выгрузка всей таблицы и вывод на основной грид
                //context.Clients.Load();

                context.Clients.Load();
                gridView.DataContext = context.Clients.Local.ToBindingList<Clients>();
                string amount = Convert.ToString(context.Clients.Count<Clients>());
                amountText.Text = amount;

            }
            catch (Exception e )
            {
                MessageBox.Show(e.Message);
            }
        }
        public void SearchByPhone()
        {
            gridView2.Items.Refresh();
            string searchString = search_box.Text;
            try
            {
                context2 = new ClientsEntities();
                context2.Clients
                    .Where(b => b.phonenumber == searchString)
                    .FirstOrDefault();
                gridView2.DataContext = context2.Clients.Local.ToBindingList<Clients>();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// Начало редактирования (получение строки до редактирвания)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GVCellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            row = (Clients)gridView.SelectedItem;            
        }



        /// <summary>
        /// обработчик ПКМ - добавить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemAddClick(object sender, RoutedEventArgs e)
        {

            //DataRow r = new DataRow();
            Clients new_client = new Clients();
            AddNewClient add = new AddNewClient(new_client);
            add.ShowDialog();
            if (add.DialogResult.Value)
            {
                context.Clients.Add(new_client); //добавление в таблицу на экране новой строки
                context.SaveChanges();
                gridView.Items.Refresh();
                string amount = Convert.ToString(context.Clients.Count<Clients>());
                amountText.Text = amount;
                pg1.Value = pg1.Maximum;
            }
        }
        /// <summary>
        /// Обработчик ПКМ - удалить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemDeleteClick(object sender, RoutedEventArgs e)
        {
            row = (Clients)gridView.SelectedItem; //выбранный клиент в данный момент (по которой был ПКМ)
            try
            {
                context.Clients.Remove(row);
                context.SaveChanges();
                string amount = Convert.ToString(context.Clients.Count<Clients>());
                amountText.Text = amount;
                pg1.Value -= pg1.Value / Convert.ToInt32(amount);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"\nВыберите клиента для удаления", "Ошибка!");
            }
            
            
        }  
        /// <summary>
        /// Редактирование записи (строка уже с внесёнными изменениями)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GVCurrentCellChanged(object sender, EventArgs e)
        {
            if (row == null) return;
            try
            {
                
                context.SaveChanges();
                row = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                throw;
            }
            
        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            SearchByPhone();
        }
    }
}
