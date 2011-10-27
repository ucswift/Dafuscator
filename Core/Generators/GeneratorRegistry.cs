using StructureMap.Configuration.DSL;
using WaveTech.Dafuscator.Model.Interfaces.Generators;

namespace WaveTech.Dafuscator.Generators
{
	internal class GeneratorRegistry : Registry
	{
		public GeneratorRegistry()
		{
			For<IAccountNumberGenerator>().Use<AccountNumberGenerator>();
			For<IAddressGenerator>().Use<AddressGenerator>();
			For<ICharacterGenerator>().Use<CharacterGenerator>();
			For<ICityGenerator>().Use<CityGenerator>();
			For<ICompanyNameGenerator>().Use<CompanyNameGenerator>();
			For<ICountryGenerator>().Use<CountryGenerator>();
			For<IDateGenerator>().Use<DateGenerator>();
			For<IEmailAddressGenerator>().Use<EmailAddressGenerator>();
			For<IFirstNameGenerator>().Use<FirstNameGenerator>();
			For<ILastNameGenerator>().Use<LastNameGenerator>();
			For<ILoginNameGenerator>().Use<LoginNameGenerator>();
			For<INumberGenerator>().Use<NumberGenerator>();
			For<IPhoneNumberGenerator>().Use<PhoneNumberGenerator>();
			For<ISsnGenerator>().Use<SsnGenerator>();
			For<IStateGenerator>().Use<StateGenerator>();
			For<IUrlGenerator>().Use<UrlGenerator>();
			For<IZipCodeGenerator>().Use<ZipCodeGenerator>();
			For<IFullNameGenerator>().Use<FullNameGenerator>();
			For<IClearGenerator>().Use<ClearGenerator>();
			For<ISymbolGenerator>().Use<SymbolGenerator>();
			For<ISecurityNameGenerator>().Use<SecurityNameGenerator>();
			For<IHexGenerator>().Use<HexGenerator>();
			For<IGuidGenerator>().Use<GuidGenerator>();
			For<ITokenReplacementGenerator>().Use<TokenReplacementGenerator>();
			For<IStringGenerator>().Use<StringGenerator>();
			For<INoneGenerator>().Use<NoneGenerator>();

			For<IGeneratorInfo>().Add<AccountNumberGeneratorInfo>();
			For<IGeneratorInfo>().Add<AddressGeneratorInfo>();
			For<IGeneratorInfo>().Add<CharacterGeneratorInfo>();
			For<IGeneratorInfo>().Add<CityGeneratorInfo>();
			For<IGeneratorInfo>().Add<CompanyNameGeneratorInfo>();
			For<IGeneratorInfo>().Add<CountryGeneratorInfo>();
			For<IGeneratorInfo>().Add<DateGeneratorInfo>();
			For<IGeneratorInfo>().Add<EmailAddressGeneratorInfo>();
			For<IGeneratorInfo>().Add<FirstNameGeneratorInfo>();
			For<IGeneratorInfo>().Add<LastNameGeneratorInfo>();
			For<IGeneratorInfo>().Add<LoginNameGeneratorInfo>();
			For<IGeneratorInfo>().Add<NumberGeneratorInfo>();
			For<IGeneratorInfo>().Add<PhoneNumberGeneratorInfo>();
			For<IGeneratorInfo>().Add<SsnGeneratorInfo>();
			For<IGeneratorInfo>().Add<StateGeneratorInfo>();
			For<IGeneratorInfo>().Add<StringGeneratorInfo>();
			For<IGeneratorInfo>().Add<UrlGeneratorInfo>();
			For<IGeneratorInfo>().Add<ZipCodeGeneratorInfo>();
			For<IGeneratorInfo>().Add<ClearGeneratorInfo>();
			For<IGeneratorInfo>().Add<FullNameGeneratorInfo>();
			For<IGeneratorInfo>().Add<SymbolGeneratorInfo>();
			For<IGeneratorInfo>().Add<SecurityNameGeneratorInfo>();
			For<IGeneratorInfo>().Add<HexGeneratorInfo>();
			For<IGeneratorInfo>().Add<GuidGeneratorInfo>();
			For<IGeneratorInfo>().Add<TokenReplacementGeneratorInfo>();
			For<IGeneratorInfo>().Add<NoneGeneratorInfo>();

			//For<IGeneratorInfo>()
			//      .AddInstances(x =>
			//      {
			//        x.Is.OfConcreteType<AccountNumberGeneratorInfo>();
			//        x.Is.OfConcreteType<AddressGeneratorInfo>();
			//        x.Is.OfConcreteType<CharacterGeneratorInfo>();
			//        x.Is.OfConcreteType<CityGeneratorInfo>();
			//        x.Is.OfConcreteType<CompanyNameGeneratorInfo>();
			//        x.Is.OfConcreteType<CountryGeneratorInfo>();
			//        x.Is.OfConcreteType<DateGeneratorInfo>();
			//        x.Is.OfConcreteType<EmailAddressGeneratorInfo>();
			//        x.Is.OfConcreteType<FirstNameGeneratorInfo>();
			//        x.Is.OfConcreteType<LastNameGeneratorInfo>();
			//        x.Is.OfConcreteType<LoginNameGeneratorInfo>();
			//        x.Is.OfConcreteType<NumberGeneratorInfo>();
			//        x.Is.OfConcreteType<PhoneNumberGeneratorInfo>();
			//        x.Is.OfConcreteType<SsnGeneratorInfo>();
			//        x.Is.OfConcreteType<StateGeneratorInfo>();
			//        x.Is.OfConcreteType<StringGeneratorInfo>();
			//        x.Is.OfConcreteType<UrlGeneratorInfo>();
			//        x.Is.OfConcreteType<ZipCodeGeneratorInfo>();
			//        x.Is.OfConcreteType<ClearGeneratorInfo>();
			//        x.Is.OfConcreteType<FullNameGeneratorInfo>();
			//        x.Is.OfConcreteType<SymbolGeneratorInfo>();
			//        x.Is.OfConcreteType<SecurityNameGeneratorInfo>();
			//        x.Is.OfConcreteType<HexGeneratorInfo>();
			//        x.Is.OfConcreteType<GuidGeneratorInfo>();
			//        x.Is.OfConcreteType<TokenReplacementGeneratorInfo>();
			//      });

			For<IGeneratorBuilder>().Add<AccountNumberGeneratorBuilder>();
			For<IGeneratorBuilder>().Add<AddressGeneratorBuilder>();
			For<IGeneratorBuilder>().Add<CharacterGeneratorBuilder>();
			For<IGeneratorBuilder>().Add<CityGeneratorBuilder>();
			For<IGeneratorBuilder>().Add<CompanyNameGeneratorBuilder>();
			For<IGeneratorBuilder>().Add<CountryGeneratorBuilder>();
			For<IGeneratorBuilder>().Add<DateGeneratorBuilder>();
			For<IGeneratorBuilder>().Add<EmailAddressGeneratorBuilder>();
			For<IGeneratorBuilder>().Add<FirstNameGeneratorBuilder>();
			For<IGeneratorBuilder>().Add<LastNameGeneratorBuilder>();
			For<IGeneratorBuilder>().Add<LoginNameGeneratorBuilder>();
			For<IGeneratorBuilder>().Add<NumberGeneratorBuilder>();
			For<IGeneratorBuilder>().Add<PhoneNumberGeneratorBuilder>();
			For<IGeneratorBuilder>().Add<SsnGeneratorBuilder>();
			For<IGeneratorBuilder>().Add<StateGeneratorBuilder>();
			For<IGeneratorBuilder>().Add<StringGeneratorBuilder>();
			For<IGeneratorBuilder>().Add<UrlGeneratorBuilder>();
			For<IGeneratorBuilder>().Add<ZipCodeGeneratorBuilder>();
			For<IGeneratorBuilder>().Add<ClearGeneratorBuilder>();
			For<IGeneratorBuilder>().Add<FullNameGeneratorBuilder>();
			For<IGeneratorBuilder>().Add<SymbolGeneratorBuilder>();
			For<IGeneratorBuilder>().Add<SecurityNameGeneratorBuilder>();
			For<IGeneratorBuilder>().Add<HexGeneratorBuilder>();
			For<IGeneratorBuilder>().Add<GuidGeneratorBuilder>();
			For<IGeneratorBuilder>().Add<TokenReplacementGeneratorBuilder>();
			For<IGeneratorBuilder>().Add<NoneGeneratorBuilder>();

			//For<IGeneratorBuilder>()
			//      .AddInstances(x =>
			//      {
			//        x.Is.OfConcreteType<AccountNumberGeneratorBuilder>();
			//        x.Is.OfConcreteType<AddressGeneratorBuilder>();
			//        x.Is.OfConcreteType<CharacterGeneratorBuilder>();
			//        x.Is.OfConcreteType<CityGeneratorBuilder>();
			//        x.Is.OfConcreteType<ClearGeneratorBuilder>();
			//        x.Is.OfConcreteType<CompanyNameGeneratorBuilder>();
			//        x.Is.OfConcreteType<CountryGeneratorBuilder>();
			//        x.Is.OfConcreteType<DateGeneratorBuilder>();
			//        x.Is.OfConcreteType<EmailAddressGeneratorBuilder>();
			//        x.Is.OfConcreteType<FirstNameGeneratorBuilder>();
			//        x.Is.OfConcreteType<FullNameGeneratorBuilder>();
			//        x.Is.OfConcreteType<GuidGeneratorBuilder>();
			//        x.Is.OfConcreteType<HexGeneratorBuilder>();
			//        x.Is.OfConcreteType<LastNameGeneratorBuilder>();
			//        x.Is.OfConcreteType<LoginNameGeneratorBuilder>();
			//        x.Is.OfConcreteType<NumberGeneratorBuilder>();
			//        x.Is.OfConcreteType<PhoneNumberGeneratorBuilder>();
			//        x.Is.OfConcreteType<SecurityNameGeneratorBuilder>();
			//        x.Is.OfConcreteType<SsnGeneratorBuilder>();
			//        x.Is.OfConcreteType<StateGeneratorBuilder>();
			//        x.Is.OfConcreteType<StringGeneratorBuilder>();
			//        x.Is.OfConcreteType<SymbolGeneratorBuilder>();
			//        x.Is.OfConcreteType<TokenReplacementGeneratorBuilder>();
			//        x.Is.OfConcreteType<UrlGeneratorBuilder>();
			//        x.Is.OfConcreteType<ZipCodeGeneratorBuilder>();
			//      });
		}
	}
}