using ContactsEditor_MVVM.Model;
using ContactsEditor_MVVM.ViewModel;
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

namespace Exam12.View
{
    /// <summary>
    /// Interaction logic for UpdateKommuneWindow.xaml
    /// </summary>
    public partial class UpdateKommuneWindow : Window
    {
        private ContactViewModel model;
        public UpdateKommuneWindow(Contact contact)
        {
            model = new ContactViewModel(contact, MainViewModel.repository);
            InitializeComponent();
            model.CloseHandler += delegate (object sender, EventArgs e) { Close(); };
            model.WarningHandler += delegate (object sender, MessageEventArgs e) {
                MessageBox.Show(e.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            };
            DataContext = model;
        }
    }
}
