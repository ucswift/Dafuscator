using System;
using System.Collections.Generic;
using System.Linq;
using WaveTech.Dafuscator.Framework;
using WaveTech.Dafuscator.Model;
using WaveTech.Dafuscator.Model.Events;
using WaveTech.Dafuscator.Model.Interfaces.Framework;
using WaveTech.Dafuscator.Model.Interfaces.Generators;
using WaveTech.Dafuscator.Model.Interfaces.Services;

namespace WaveTech.Dafuscator.Services
{
	public class DataGenerationService : IDataGenerationService
	{
		private readonly IDatabaseInteractionService _databaseInteractionService;
		private readonly IEventAggregator _eventAggregator;

		public DataGenerationService(IDatabaseInteractionService databaseInteractionService, IEventAggregator eventAggregator)
		{
			_databaseInteractionService = databaseInteractionService;
			_eventAggregator = eventAggregator;
		}

		public Table GenerateDataForTable(ConnectionString connectionString, Table table, bool updateRecordCount)
		{
			if (table.AreAnyGeneratorsActive && table.HandlerType == TableHandlerTypes.None)
			{
				_eventAggregator.SendMessage<StatusUpdateEvent>(new StatusUpdateEvent(string.Format("Generating Data for table: {0}", table.FullTableName)));

				for (int i = 0; i < table.Columns.Count; i++)
				{
					if (table.Columns[i].GeneratorType.HasValue && (table.Columns[i].GeneratorType.Value.Equals(SystemConstants.DefaultGuid) == false))
					{
						List<object> generationData = new List<object>();
						double recordCount = 0;

						if (updateRecordCount)
						{
							try
							{
								recordCount = _databaseInteractionService.GetRecrodCountForTable(connectionString, table);
							}
							catch
							{ }
						}

						if (recordCount <= 0)
							recordCount = table.RecordCount;
						else
							table.RecordCount = recordCount;

						generationData.Add(recordCount);

						foreach (object o in table.Columns[i].GeneratorData)
						{
							if (o != null)
								generationData.Add(o);
						}

						HashSet<string> existingColumnData = new HashSet<string>();

						try
						{
							existingColumnData = _databaseInteractionService.GetDataForColumn(connectionString, table, table.Columns[i]);
						}
						catch
						{ }

						table.Columns[i] = GenerateDataForColumn(table.Columns[i], generationData.ToArray(), existingColumnData);
					}
				}
			}

			return table;
		}

		public Column GenerateDataForColumn(Column column, object[] data, HashSet<string> existingColumnData)
		{
			if (column.GeneratorType != null && column.GeneratorType != SystemConstants.DefaultGuid)
			{
				List<IGeneratorInfo> infos = ObjectLocator.GetAllInstances<IGeneratorInfo>();
				List<IGeneratorBuilder> builders = ObjectLocator.GetAllInstances<IGeneratorBuilder>();
				Type generatorType = infos.Where(x => x.Id == column.GeneratorType.Value).First().Type;
				IGeneratorBuilder builder = builders.Where(x => x.GeneratorId == column.GeneratorType.Value).First();

				var generator = ObjectLocator.GetInstance(generatorType);
				List<string> generatedData = null;

				if (generator != null)
					generatedData = builder.BuildGenerator(generator, data, existingColumnData);

				#region Old Swtich Based Code

				//switch (column.GeneratorType)
				//{
				//  case GeneratorTypes.AccountNumber:
				//    generatedData = ObjectLocator.GetInstance<IAccountNumberGenerator>().GenerateAccountNumbers((double)data[0]);
				//    break;
				//  case GeneratorTypes.Address:
				//    generatedData = ObjectLocator.GetInstance<IAddressGenerator>().GenerateAddressLine1s((double)data[0]);
				//    break;
				//  case GeneratorTypes.Character:

				//    bool charBool = false;

				//    try
				//    {
				//      charBool = (bool)data[1];
				//    }
				//    catch { }

				//    var characters = ObjectLocator.GetInstance<ICharacterGenerator>().GenerateRandomCharacters((double)data[0], charBool);
				//    generatedData = new List<string>();
				//    foreach (var c in characters)
				//    {
				//      generatedData.Add(c.ToString());
				//    }
				//    break;
				//  case GeneratorTypes.City:
				//    generatedData = ObjectLocator.GetInstance<ICityGenerator>().GenerateCityNames((double)data[0]);
				//    break;
				//  case GeneratorTypes.CompanyName:
				//    generatedData = ObjectLocator.GetInstance<ICompanyNameGenerator>().GenerateCompanyNames((double)data[0], existingColumnData);
				//    break;
				//  case GeneratorTypes.Country:
				//    generatedData = ObjectLocator.GetInstance<ICountryGenerator>().GenerateCountryNames((double)data[0]);
				//    break;
				//  case GeneratorTypes.Date:
				//    var dates = ObjectLocator.GetInstance<IDateGenerator>().GenerateDate((double)data[0], (DateTime)data[1], (DateTime)data[2]);
				//    generatedData = new List<string>();
				//    foreach (var d in dates)
				//    {
				//      generatedData.Add(d.ToString());
				//    }
				//    break;
				//  case GeneratorTypes.EmailAddress:
				//    generatedData = ObjectLocator.GetInstance<IEmailAddressGenerator>().GenerateEmailAddresses((double)data[0], existingColumnData);
				//    break;
				//  case GeneratorTypes.FirstName:
				//    generatedData = ObjectLocator.GetInstance<IFirstNameGenerator>().GenerateFirstNames((double)data[0]);
				//    break;
				//  case GeneratorTypes.LastName:
				//    generatedData = ObjectLocator.GetInstance<ILastNameGenerator>().GenerateLastNames((double)data[0]);
				//    break;
				//  case GeneratorTypes.Login:
				//    generatedData = ObjectLocator.GetInstance<ILoginNameGenerator>().GenerateLoginNames((double)data[0]);
				//    break;
				//  case GeneratorTypes.Number:
				//    var numbers = ObjectLocator.GetInstance<INumberGenerator>().GenerateRandomNumbers(double.Parse(data[0].ToString()), int.Parse(data[1].ToString()), int.Parse(data[2].ToString()));
				//    generatedData = new List<string>();
				//    foreach (var n in numbers)
				//    {
				//      generatedData.Add(n.ToString());
				//    }
				//    break;
				//  case GeneratorTypes.PhoneNumber:

				//    bool phoneBool = false;

				//    try
				//    {
				//      phoneBool = (bool)data[1];
				//    }
				//    catch { }

				//    generatedData = ObjectLocator.GetInstance<IPhoneNumberGenerator>().GeneratePhoneNumbers((double)data[0], phoneBool);
				//    break;
				//  case GeneratorTypes.SSN:
				//    generatedData = ObjectLocator.GetInstance<ISsnGenerator>().GenerateSocialSecurityNumbers((double)data[0]);
				//    break;
				//  case GeneratorTypes.State:
				//    generatedData = ObjectLocator.GetInstance<IStateGenerator>().GenerateStateNames((double)data[0]);
				//    break;
				//  case GeneratorTypes.String:

				//    bool stringBool1 = false;

				//    try
				//    {
				//      stringBool1 = (bool)data[3];
				//    }
				//    catch { }

				//    bool stringBool2 = false;

				//    try
				//    {
				//      stringBool2 = (bool)data[4];
				//    }
				//    catch { }

				//    generatedData = ObjectLocator.GetInstance<IStringGenerator>().GenerateRandomStrings(double.Parse(data[0].ToString()), int.Parse(data[1].ToString()), int.Parse(data[2].ToString()), stringBool1, stringBool2, existingColumnData);
				//    break;
				//  case GeneratorTypes.Url:

				//    bool data2 = false;

				//    try
				//    {
				//      data2 = (bool)data[1];
				//    }
				//    catch { }

				//    generatedData = ObjectLocator.GetInstance<IUrlGenerator>().GenerateUrls((double)data[0], data2, existingColumnData);
				//    break;
				//  case GeneratorTypes.ZipCode:

				//    bool data1 = false;

				//    try
				//    {
				//      data1 = (bool)data[1];
				//    }
				//    catch { }

				//    generatedData = ObjectLocator.GetInstance<IZipCodeGenerator>().GenerateZipCodes((double)data[0], data1);
				//    break;
				//  case GeneratorTypes.Clear:
				//    generatedData = new List<string>();
				//    break;
				//  case GeneratorTypes.FullName:


				//    bool fullNameBool1 = false;

				//    try
				//    {
				//      fullNameBool1 = (bool)data[1];
				//    }
				//    catch { }

				//    bool fullNameBool2 = false;

				//    try
				//    {
				//      fullNameBool2 = (bool)data[2];
				//    }
				//    catch { }

				//    generatedData = ObjectLocator.GetInstance<IFullNameGenerator>().GenerateFullNames((double)data[0], fullNameBool1, fullNameBool2);
				//    break;
				//  case GeneratorTypes.Symbol:
				//    generatedData = ObjectLocator.GetInstance<ISymbolGenerator>().GenerateSymbols((double)data[0], existingColumnData);
				//    break;
				//  case GeneratorTypes.SecurityName:
				//    generatedData = ObjectLocator.GetInstance<ISecurityNameGenerator>().GenerateSecurityNames((double)data[0], existingColumnData);
				//    break;
				//  case GeneratorTypes.Hex:
				//    generatedData = ObjectLocator.GetInstance<IHexGenerator>().GenerateHexNumbers((double)data[0]);
				//    break;
				//  case GeneratorTypes.Guid:
				//    generatedData = ObjectLocator.GetInstance<IGuidGenerator>().GenerateGuids((double)data[0]);
				//    break;
				//  case GeneratorTypes.Token:
				//    generatedData = ObjectLocator.GetInstance<ITokenReplacementGenerator>().GenerateReplacedStrings((double)data[0], (string)data[1], existingColumnData);
				//    break;

				//  default:
				//    throw new InvalidOperationException(string.Format("Cannot generate data for type {0}", column.GeneratorType));
				//}
				#endregion Old Swtich Based Code

				column.Data = generatedData;
			}

			return column;
		}
	}
}