using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ContactsEditor_MVVM.ViewModel;
using ContactsEditor_MVVM.Model;

namespace ContactsEditor_MVVM
{
  public partial class KommuneMainWindow : Window
  {
    private KommuneMainViewModel model = new KommuneMainViewModel();

    public KommuneMainWindow()
    {
      InitializeComponent();
      DataContext = model;
      model.WarningHandler += delegate(object sender, MessageEventArgs e) { 
          MessageBox.Show(e.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning); 
      };
    }

    private void grid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      if (sender != null)
      {
        try
        {
          DataGridRow row = sender as DataGridRow;
          Contact contact = (Contact)row.Item;
          model.UpdateContact(contact);
        }
        catch
        {
        }
      }
    }

        private void grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
