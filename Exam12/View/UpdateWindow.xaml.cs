using System;
using System.Windows;
using ContactsEditor_MVVM.Model;
using ContactsEditor_MVVM.ViewModel;

namespace ContactsEditor_MVVM.View
{
  public partial class UpdateWindow : Window
  {
    private ContactViewModel model;

    public UpdateWindow(Contact contact)
    {
      model = new ContactViewModel(contact, MainViewModel.repository);
      InitializeComponent();
      model.CloseHandler += delegate(object sender, EventArgs e) { Close(); };
      model.WarningHandler += delegate(object sender, MessageEventArgs e) { 
          MessageBox.Show(e.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning); 
      };
      DataContext = model;
    }
  }
}