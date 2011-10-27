using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WaveTech.Dafuscator.Model;

namespace WaveTech.Dafuscator.WpfApplication.UserControls
{
	/// <summary>
	/// Interaction logic for ColumnsGrid.xaml
	/// </summary>
	public partial class ColumnsGrid : UserControl
	{
		public static readonly DependencyProperty SelectedTableProperty =
						DependencyProperty.Register("SelectedTable", typeof(Model.Table), typeof(ColumnsGrid),
						new FrameworkPropertyMetadata(
								null,
								FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
								SelectedTablePropertyChanged));

		private Dictionary<Guid, GeneratorOptions> _generatorOptions;

		public ColumnsGrid()
		{
			_generatorOptions = new Dictionary<Guid, GeneratorOptions>();

			InitializeComponent();
		}

		public Model.Table SelectedTable
		{
			get { return (Model.Table)GetValue(SelectedTableProperty); }
			set { SetValue(SelectedTableProperty, value); }
		}

		public Dictionary<Guid, GeneratorOptions> GeneratorOptions
		{
			get { return _generatorOptions; }
		}

		private static void SelectedTablePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			//if (d is ColumnsGrid)
			//{
			//  ColumnsGrid cg = d as ColumnsGrid;

			//  foreach (var go in cg.GeneratorOptions.Values)
			//  {
			//    if (go.GetGeneratorOptions().Count > 0)
			//      go.GeneratorData = go.GetGeneratorOptions();

			//    go.SetData();
			//  }
			//}
		}

		public bool IsValid()
		{
			bool isValid = true;

			if (SelectedTable != null)
			{
				foreach (Column c in SelectedTable.Columns)
				{
					if (c.IsValid() == false)
						isValid = false;
				}
			}

			return isValid;
		}

		public void HookGeneratorOptions(Guid id, GeneratorOptions generatorOptions)
		{
			if (_generatorOptions.ContainsKey(id) == false)
				_generatorOptions.Add(id, generatorOptions);
		}
	}
}