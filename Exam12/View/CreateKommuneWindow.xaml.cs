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
    /// Interaction logic for CreateKommuneWindow.xaml
    /// </summary>
    public partial class CreateKommuneWindow : Window
    {
        private KommuneDataViewModel model = new KommuneDataViewModel(new KommuneData(), KommuneMainViewModel.repository);
        public CreateKommuneWindow()
        {
            InitializeComponent();
            model.CloseHandler += delegate (object sender, EventArgs e) { Close(); };
            model.WarningHandler += delegate (object sender, MessageEventArgs e) {
                MessageBox.Show(e.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            };
            DataContext = model;
        }
    }
}
