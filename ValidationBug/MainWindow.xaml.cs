using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ValidationBug
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			DataContext = new List<Item>() { new Item() };
		}

		void Button_Click(object sender, RoutedEventArgs e)
		{
			var builder = new StringBuilder();
			var g = dataGrid.ItemContainerGenerator;

			GC.Collect();

			for(var i = 0; i < g.Items.Count; i++)
			{
				var row = g.ContainerFromIndex(i);

				builder.AppendLine("row:");
				var errors = Validation.GetErrors(row);

				foreach(var error in errors)
				{
					var expression = (BindingExpression)error.BindingInError;

					builder.Append('\t');
					builder.Append(error.ErrorContent);
					builder.Append(": ");
					builder.AppendLine(expression.Target?.ToString());
				}
			}

			MessageBox.Show(builder.ToString());
		}
	}
}
