using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Windows.Controls.Primitives;
using WaveTech.Dafuscator.Framework;
using WaveTech.Dafuscator.Model;

namespace WaveTech.Dafuscator.WpfApplication.UserControls
{
	/// <summary>
	/// Interaction logic for GeneratorOptions.xaml
	/// </summary>
	public partial class GeneratorOptions : UserControl
	{
		#region Public Depedency Properties
		public static readonly DependencyProperty GeneratorTypeProperty =
					DependencyProperty.Register("GeneratorType", typeof(Guid?), typeof(GeneratorOptions),
					new FrameworkPropertyMetadata(null,
						FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnGeneratorTypeChanged));

		public static readonly DependencyProperty GeneratorDataProperty =
					DependencyProperty.Register("GeneratorData", typeof(List<object>), typeof(GeneratorOptions),
					new FrameworkPropertyMetadata(null,
						FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnGeneratorDataChanged));

		public static readonly DependencyProperty ColumnProperty =
					DependencyProperty.Register("Column", typeof(Column), typeof(GeneratorOptions),
					new FrameworkPropertyMetadata(null,
						FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnColumnChanged));
		#endregion Public Depedency Properties

		#region Private Variables
		private ColumnsGrid _parentGrid;
		private Guid _internalId;
		#endregion Private Variables

		#region Constructor
		static GeneratorOptions()
		{
			EventManager.RegisterClassHandler(typeof(TreeViewItem),
					Mouse.MouseDownEvent, new MouseButtonEventHandler(OnTreeViewItemMouseDown), false);
		}

		public GeneratorOptions()
		{
			_internalId = Guid.NewGuid();

			InitializeComponent();

			HideAll();
			//SetData();
		}
		#endregion Contructor

		#region Public Properties
		public Guid? GeneratorType
		{
			get { return (Guid?)GetValue(GeneratorTypeProperty); }
			set { SetValue(GeneratorTypeProperty, value); }
		}

		public List<object> GeneratorData
		{
			get { return (List<object>)GetValue(GeneratorDataProperty); }
			set { SetValue(GeneratorDataProperty, value); }
		}

		public Column Column
		{
			get { return (Column)GetValue(ColumnProperty); }
			set { SetValue(ColumnProperty, value); }
		}
		#endregion Public Properties

		#region Private Events Handelers
		private static void OnGeneratorTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			GeneratorOptions control = d as GeneratorOptions;

			if (control != null)
			{
				if (control.Column != null)
					control.Column.GeneratorData = new List<object>();

				control.HideAll();

				//TODO: This code below needs to be pushed into the generator itself, i.e. it has it's own little form that's pulled in

				// Only have the generators that have 'special' needs here (i.e. something other then count)
				if (control.GeneratorType == new Guid("A7AC88F5-8C61-4F3E-8066-23A4CCF19ED5"))	// Character Generator
					control.CharacterGeneratorOptionsGrid.Visibility = Visibility.Visible;

				else if (control.GeneratorType == new Guid("577E56A7-83BD-4087-B042-4FFA54E5F193")) // Date Generator
					control.DateGeneratorOptionsGrid.Visibility = Visibility.Visible;

				else if (control.GeneratorType == new Guid("68290CBD-A327-41BE-A9E6-D6FFD089B953")) // Number Generator
					control.NumberGeneratorOptionsGrid.Visibility = Visibility.Visible;

				else if (control.GeneratorType == new Guid("0086042D-C5E1-4013-9901-2FABDD679136")) // Phone Number Generator
					control.PhoneNumberGeneratorOptionsGrid.Visibility = Visibility.Visible;

				else if (control.GeneratorType == new Guid("7A9EC03D-6713-4C57-84D5-65A45DD3854F")) // String Generator
					control.StringGeneratorOptionsGrid.Visibility = Visibility.Visible;

				else if (control.GeneratorType == new Guid("08707085-1263-497E-B008-1CCE0C02EA05")) // Zip Code Generator
					control.ZipCodeGeneratorOptionsGrid.Visibility = Visibility.Visible;

				else if (control.GeneratorType == new Guid("815C682E-0690-48E7-8F7F-75BCD47DC3E6")) // Clear Generator
					control.ClearGeneratorOptionsGrid.Visibility = Visibility.Visible;

				else if (control.GeneratorType == new Guid("8440D22A-7ACD-4359-A5D7-3347F933DA54")) // Full Name Generator
					control.FullNameGeneratorOptionsGrid.Visibility = Visibility.Visible;

				else if (control.GeneratorType == new Guid("6653E317-2034-4B41-A1BB-84B1FE822728")) // Token Generator
					control.TokenGeneratorOptionsGrid.Visibility = Visibility.Visible;

				else if (control.GeneratorType == SystemConstants.DefaultGuid) // None
					control.HideAll();

				else
					control.NoOptionsGrid.Visibility = Visibility.Visible;



				//switch (control.GeneratorType)
				//{
				//  // Only have the generators that have 'special' needs here (i.e. something other then count)
				//  case GeneratorTypes.Character:
				//    control.CharacterGeneratorOptionsGrid.Visibility = Visibility.Visible;
				//    break;
				//  case GeneratorTypes.Date:
				//    control.DateGeneratorOptionsGrid.Visibility = Visibility.Visible;
				//    break;
				//  case GeneratorTypes.Number:
				//    control.NumberGeneratorOptionsGrid.Visibility = Visibility.Visible;
				//    break;
				//  case GeneratorTypes.PhoneNumber:
				//    control.PhoneNumberGeneratorOptionsGrid.Visibility = Visibility.Visible;
				//    break;
				//  case GeneratorTypes.String:
				//    control.StringGeneratorOptionsGrid.Visibility = Visibility.Visible;
				//    break;
				//  case GeneratorTypes.ZipCode:
				//    control.ZipCodeGeneratorOptionsGrid.Visibility = Visibility.Visible;
				//    break;
				//  case GeneratorTypes.Clear:
				//    control.ClearGeneratorOptionsGrid.Visibility = Visibility.Visible;
				//    break;
				//  case GeneratorTypes.FullName:
				//    control.FullNameGeneratorOptionsGrid.Visibility = Visibility.Visible;
				//    break;
				//  case GeneratorTypes.Token:
				//    control.TokenGeneratorOptionsGrid.Visibility = Visibility.Visible;
				//    break;
				//  case GeneratorTypes.None:
				//    control.HideAll();
				//    break;
				//  default:
				//    control.NoOptionsGrid.Visibility = Visibility.Visible;
				//    break;
				//}
			}
		}

		private static void OnGeneratorDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			GeneratorOptions control = d as GeneratorOptions;

			if (control != null)
			{
				if (control.GeneratorData == null || control.GeneratorData.Count < 6)
				{
					int delta = 6 - control.GeneratorData.Count;

					for (int i = 0; i < delta; i++)
					{
						control.GeneratorData.Add(null);
					}
				}
			}
		}

		private static void OnColumnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			GeneratorOptions control = d as GeneratorOptions;

			if (control != null)
			{
				//control.DataContext = control.Column;
			}
		}

		private static void OnTreeViewItemMouseDown(object sender, MouseButtonEventArgs e)
		{
			//GeneratorOptions go = sender as GeneratorOptions;

			//go.LockToParentGrid();

			//if (go.IsFormValid())
			//{
			//  if (go.GetGeneratorOptions().Count > 0)
			//    go.GeneratorData = go.GetGeneratorOptions();

			//  go.SetData();
			//}
		}

		private void characterGenerator_IncludeDigits_Click(object sender, RoutedEventArgs e)
		{
			//if (GetGeneratorOptions().Count > 0)
			//  GeneratorData = GetGeneratorOptions();
		}

		private void stringGenerator_MinLength_KeyUp(object sender, KeyEventArgs e)
		{
			//if (GetGeneratorOptions().Count > 0)
			//  GeneratorData = GetGeneratorOptions();
		}

		private void stringGenerator_MaxLength_KeyUp(object sender, KeyEventArgs e)
		{
			//if (GetGeneratorOptions().Count > 0)
			//  GeneratorData = GetGeneratorOptions();
		}

		private void stringGenerator_IncludeNumbers_Click(object sender, RoutedEventArgs e)
		{
			//if (GetGeneratorOptions().Count > 0)
			//  GeneratorData = GetGeneratorOptions();
		}

		private void stringGenerator_IncludeSpecialCharacters_Click(object sender, RoutedEventArgs e)
		{
			//if (GetGeneratorOptions().Count > 0)
			//  GeneratorData = GetGeneratorOptions();
		}
		#endregion Private Events Handelers

		#region Private Methods
		public void SetData()
		{
			LockToParentGrid();

			if (GeneratorData != null && GeneratorData.Count > 0)
			{
				if (GeneratorType == new Guid("A7AC88F5-8C61-4F3E-8066-23A4CCF19ED5"))	// Character Generator
				{
					characterGenerator_IncludeDigits.IsChecked = (bool)GeneratorData[0];
				}

				else if (GeneratorType == new Guid("577E56A7-83BD-4087-B042-4FFA54E5F193")) // Date Generator
				{
					dateGenerator_MinDate.SelectedDate = DateTime.Parse(GeneratorData[0].ToString());
					dateGenerator_MaxDate.SelectedDate = DateTime.Parse(GeneratorData[1].ToString());
				}

				else if (GeneratorType == new Guid("68290CBD-A327-41BE-A9E6-D6FFD089B953")) // Number Generator
				{
					numberGenerator_MinNumber.Text = GeneratorData[0].ToString();
					numberGenerator_MaxNumber.Text = GeneratorData[1].ToString();
				}

				else if (GeneratorType == new Guid("0086042D-C5E1-4013-9901-2FABDD679136")) // Phone Number Generator
				{
					phoneNumberGenerator_IncludeAreaCode.IsChecked = (bool)GeneratorData[0];
				}

				else if (GeneratorType == new Guid("7A9EC03D-6713-4C57-84D5-65A45DD3854F")) // String Generator
				{
					stringGenerator_MinLength.Text = GeneratorData[0].ToString();
					stringGenerator_MaxLength.Text = GeneratorData[1].ToString();
					stringGenerator_IncludeNumbers.IsChecked = (bool)GeneratorData[2];
					stringGenerator_IncludeSpecialCharacters.IsChecked = (bool)GeneratorData[3];
				}

				else if (GeneratorType == new Guid("08707085-1263-497E-B008-1CCE0C02EA05")) // Zip Code Generator
				{
					zipCodeGenerator_Include4Digits.IsChecked = (bool)GeneratorData[0];
				}

				else if (GeneratorType == new Guid("815C682E-0690-48E7-8F7F-75BCD47DC3E6")) // Clear Generator
				{
					clearGenerator_UseNullInstead.IsChecked = (bool)GeneratorData[0];
				}

				else if (GeneratorType == new Guid("8440D22A-7ACD-4359-A5D7-3347F933DA54")) // Full Name Generator
				{
					fullNameGenerator_IncludeMiddle.IsChecked = (bool)GeneratorData[0];
					fullNameGenerator_FullNameMiddle.IsChecked = (bool)GeneratorData[0];
				}

				else if (GeneratorType == new Guid("D98D89C8-ECAE-4AB3-897A-8478B0A6EC89")) // Token Generator
				{
					tokenGenerator_TokenString.Text = (string)GeneratorData[0];
				}


				//switch (GeneratorType)
				//{
				//  // Only have the generators that have 'special' needs here (i.e. somthing other then count)
				//  case GeneratorTypes.Character:
				//    characterGenerator_IncludeDigits.IsChecked = (bool)GeneratorData[0];
				//    break;
				//  case GeneratorTypes.Number:
				//    numberGenerator_MinNumber.Text = GeneratorData[0].ToString();
				//    numberGenerator_MaxNumber.Text = GeneratorData[1].ToString();
				//    break;
				//  case GeneratorTypes.Date:
				//    dateGenerator_MinDate.SelectedDate = DateTime.Parse(GeneratorData[0].ToString());
				//    dateGenerator_MaxDate.SelectedDate = DateTime.Parse(GeneratorData[1].ToString());
				//    break;
				//  case GeneratorTypes.PhoneNumber:
				//    phoneNumberGenerator_IncludeAreaCode.IsChecked = (bool)GeneratorData[0];
				//    break;
				//  case GeneratorTypes.String:
				//    stringGenerator_MinLength.Text = GeneratorData[0].ToString();
				//    stringGenerator_MaxLength.Text = GeneratorData[1].ToString();
				//    stringGenerator_IncludeNumbers.IsChecked = (bool)GeneratorData[2];
				//    stringGenerator_IncludeSpecialCharacters.IsChecked = (bool)GeneratorData[3];
				//    break;
				//  case GeneratorTypes.ZipCode:
				//    zipCodeGenerator_Include4Digits.IsChecked = (bool)GeneratorData[0];
				//    break;
				//  case GeneratorTypes.Clear:
				//    clearGenerator_UseNullInstead.IsChecked = (bool)GeneratorData[0];
				//    break;
				//  case GeneratorTypes.FullName:
				//    fullNameGenerator_IncludeMiddle.IsChecked = (bool)GeneratorData[0];
				//    fullNameGenerator_FullNameMiddle.IsChecked = (bool)GeneratorData[0];
				//    break;
				//  case GeneratorTypes.Token:
				//    tokenGenerator_TokenString.Text = (string)GeneratorData[0];
				//    break;
				//}
			}
		}

		private void LockToParentGrid()
		{
			_parentGrid =
				(ColumnsGrid)
				((DependencyObject)this).Ancestors().Where(x => x.GetType() == typeof(ColumnsGrid)).FirstOrDefault();

			if (_parentGrid != null)
				_parentGrid.HookGeneratorOptions(_internalId, this);
		}
		#endregion Private Methods

		#region Public Methods
		public void HideAll()
		{
			NoOptionsGrid.Visibility = Visibility.Collapsed;
			CharacterGeneratorOptionsGrid.Visibility = Visibility.Collapsed;
			DateGeneratorOptionsGrid.Visibility = Visibility.Collapsed;
			NumberGeneratorOptionsGrid.Visibility = Visibility.Collapsed;
			PhoneNumberGeneratorOptionsGrid.Visibility = Visibility.Collapsed;
			StringGeneratorOptionsGrid.Visibility = Visibility.Collapsed;
			ZipCodeGeneratorOptionsGrid.Visibility = Visibility.Collapsed;
			ClearGeneratorOptionsGrid.Visibility = Visibility.Collapsed;
			FullNameGeneratorOptionsGrid.Visibility = Visibility.Collapsed;
			TokenGeneratorOptionsGrid.Visibility = Visibility.Collapsed;
		}

		public List<object> GetGeneratorOptions()
		{
			List<object> generatorData = new List<object>();

			if (GeneratorType == new Guid("A7AC88F5-8C61-4F3E-8066-23A4CCF19ED5"))	// Character Generator
			{
				generatorData.Add(characterGenerator_IncludeDigits.IsChecked);
			}

			else if (GeneratorType == new Guid("577E56A7-83BD-4087-B042-4FFA54E5F193")) // Date Generator
			{
				generatorData.Add(dateGenerator_MinDate.SelectedDate);
				generatorData.Add(dateGenerator_MaxDate.SelectedDate);
			}

			else if (GeneratorType == new Guid("68290CBD-A327-41BE-A9E6-D6FFD089B953")) // Number Generator
			{
				generatorData.Add(numberGenerator_MinNumber.Text);
				generatorData.Add(numberGenerator_MaxNumber.Text);
			}

			else if (GeneratorType == new Guid("0086042D-C5E1-4013-9901-2FABDD679136")) // Phone Number Generator
			{
				generatorData.Add(phoneNumberGenerator_IncludeAreaCode.IsChecked);
			}

			else if (GeneratorType == new Guid("7A9EC03D-6713-4C57-84D5-65A45DD3854F")) // String Generator
			{
				generatorData.Add(stringGenerator_MinLength.Text);
				generatorData.Add(stringGenerator_MaxLength.Text);
				generatorData.Add(stringGenerator_IncludeNumbers.IsChecked);
				generatorData.Add(stringGenerator_IncludeSpecialCharacters.IsChecked);
			}

			else if (GeneratorType == new Guid("08707085-1263-497E-B008-1CCE0C02EA05")) // Zip Code Generator
			{
				generatorData.Add(zipCodeGenerator_Include4Digits.IsChecked);
			}

			else if (GeneratorType == new Guid("815C682E-0690-48E7-8F7F-75BCD47DC3E6")) // Clear Generator
			{
				generatorData.Add(clearGenerator_UseNullInstead.IsChecked);
			}

			else if (GeneratorType == new Guid("8440D22A-7ACD-4359-A5D7-3347F933DA54")) // Full Name Generator
			{
				generatorData.Add(fullNameGenerator_IncludeMiddle.IsChecked);
				generatorData.Add(fullNameGenerator_FullNameMiddle.IsChecked);
			}

			else if (GeneratorType == new Guid("D98D89C8-ECAE-4AB3-897A-8478B0A6EC89")) // Token Generator
			{
				generatorData.Add(tokenGenerator_TokenString.Text);
			}

			//switch (GeneratorType)
			//{
			//  // Only have the generators that have 'special' needs here (i.e. somthing other then count)
			//  case GeneratorTypes.Character:
			//    generatorData.Add(characterGenerator_IncludeDigits.IsChecked);
			//    break;
			//  case GeneratorTypes.Number:
			//    generatorData.Add(numberGenerator_MinNumber.Text);
			//    generatorData.Add(numberGenerator_MaxNumber.Text);
			//    break;
			//  case GeneratorTypes.Date:
			//    generatorData.Add(dateGenerator_MinDate.SelectedDate);
			//    generatorData.Add(dateGenerator_MaxDate.SelectedDate);
			//    break;
			//  case GeneratorTypes.PhoneNumber:
			//    generatorData.Add(phoneNumberGenerator_IncludeAreaCode.IsChecked);
			//    break;
			//  case GeneratorTypes.String:
			//    generatorData.Add(stringGenerator_MinLength.Text);
			//    generatorData.Add(stringGenerator_MaxLength.Text);
			//    generatorData.Add(stringGenerator_IncludeNumbers.IsChecked);
			//    generatorData.Add(stringGenerator_IncludeSpecialCharacters.IsChecked);
			//    break;
			//  case GeneratorTypes.ZipCode:
			//    generatorData.Add(zipCodeGenerator_Include4Digits.IsChecked);
			//    break;
			//  case GeneratorTypes.Clear:
			//    generatorData.Add(clearGenerator_UseNullInstead.IsChecked);
			//    break;
			//  case GeneratorTypes.FullName:
			//    generatorData.Add(fullNameGenerator_IncludeMiddle.IsChecked);
			//    generatorData.Add(fullNameGenerator_FullNameMiddle.IsChecked);
			//    break;
			//  case GeneratorTypes.Token:
			//    generatorData.Add(tokenGenerator_TokenString.Text);
			//    break;
			//}

			return generatorData;
		}

		public bool IsFormValid()
		{
			bool isValid = true;

			if (GeneratorType == new Guid("A7AC88F5-8C61-4F3E-8066-23A4CCF19ED5"))	// Character Generator
			{

			}

			else if (GeneratorType == new Guid("577E56A7-83BD-4087-B042-4FFA54E5F193")) // Date Generator
			{
				TextBox tb1 = ((DependencyObject)dateGenerator_MinDate).FindChild<DatePickerTextBox>("PART_TextBox");
				TextBox tb2 = ((DependencyObject)dateGenerator_MaxDate).FindChild<DatePickerTextBox>("PART_TextBox");

				if (dateGenerator_MinDate.SelectedDate == null)
				{
					tb1.BorderBrush = new SolidColorBrush(Colors.Red);
					isValid = false;
				}
				else
				{
					tb1.Background = new SolidColorBrush(Colors.White);
				}

				if (dateGenerator_MaxDate.SelectedDate == null)
				{
					tb2.BorderBrush = new SolidColorBrush(Colors.Red);
					isValid = false;
				}
				else
				{
					tb2.Background = new SolidColorBrush(Colors.White);
				}
			}

			else if (GeneratorType == new Guid("68290CBD-A327-41BE-A9E6-D6FFD089B953")) // Number Generator
			{
				int tp;

				if (String.IsNullOrEmpty(numberGenerator_MinNumber.Text) || int.TryParse(numberGenerator_MinNumber.Text, out tp))
				{
					numberGenerator_MinNumber.Background = new SolidColorBrush(Colors.Red);
					isValid = false;
				}
				else
				{
					numberGenerator_MinNumber.Background = new SolidColorBrush(Colors.White);
				}

				if (String.IsNullOrEmpty(numberGenerator_MaxNumber.Text) || int.TryParse(numberGenerator_MaxNumber.Text, out tp))
				{
					numberGenerator_MaxNumber.Background = new SolidColorBrush(Colors.Red);
					isValid = false;
				}
				else
				{
					numberGenerator_MaxNumber.Background = new SolidColorBrush(Colors.White);
				}
			}

			else if (GeneratorType == new Guid("0086042D-C5E1-4013-9901-2FABDD679136")) // Phone Number Generator
			{

			}

			else if (GeneratorType == new Guid("7A9EC03D-6713-4C57-84D5-65A45DD3854F")) // String Generator
			{

			}

			else if (GeneratorType == new Guid("08707085-1263-497E-B008-1CCE0C02EA05")) // Zip Code Generator
			{

			}

			else if (GeneratorType == new Guid("815C682E-0690-48E7-8F7F-75BCD47DC3E6")) // Clear Generator
			{

			}

			else if (GeneratorType == new Guid("8440D22A-7ACD-4359-A5D7-3347F933DA54")) // Full Name Generator
			{

			}

			else if (GeneratorType == new Guid("D98D89C8-ECAE-4AB3-897A-8478B0A6EC89")) // Token Generator
			{

			}

			return isValid;
		}
		#endregion Public Methods
	}
}