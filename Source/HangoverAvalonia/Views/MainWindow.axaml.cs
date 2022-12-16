using AppScaffolding;
using Avalonia.Controls;
using HangoverAvalonia.ViewModels;
using System;
using System.Linq;

namespace HangoverAvalonia.Views
{
	public partial class MainWindow : Window
	{
		MainVM _viewModel => DataContext as MainVM;
		public MainWindow()
		{
			InitializeComponent();

			var config = LibationScaffolding.RunPreConfigMigrations();
			LibationScaffolding.RunPostConfigMigrations(config);
			LibationScaffolding.RunPostMigrationScaffolding(config);
		}

		public void OnLoad()
		{
			deletedCbl.ItemCheck += Deleted_CheckedListBox_ItemCheck;
			databaseTab.PropertyChanged += (_, e) => { if (e.Property.Name == nameof(TabItem.IsSelected)) databaseTab_VisibleChanged(databaseTab.IsSelected); };
			deletedTab.PropertyChanged += (_, e) => { if (e.Property.Name == nameof(TabItem.IsSelected)) deletedTab_VisibleChanged(deletedTab.IsSelected); };
			cliTab.PropertyChanged += (_, e) => { if (e.Property.Name == nameof(TabItem.IsSelected)) cliTab_VisibleChanged(cliTab.IsSelected); };
		}
	}
}
