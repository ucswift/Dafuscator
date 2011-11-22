# Dafuscator

Dafuscator is a database data obfuscation system that allows you to tactically obfuscate or delete data out of your production database while leaving most of the data intact. Real data behaves differently than fake generated data, so when testing, performing QA or sending a database off to a 3rd party for testing you can maintain Sarbanes–Oxley and HIPPA compliance by changing PIA (Personally Identifiable Information) while still keeping most data intact for testing or analysis.

## License

Licensed under the Microsoft Public License (MS-PL)

## Development

Dafuscator was developed in cooperation with Online Advisors, Inc. (<http://www.onlineadvisrs.co>)

## Resources

* **WaveTech's Home page:** <http://www.wtdt.com>
* **Dafuscator's Home Page:** <http://www.dafuscator.com>
* **Dafuscator's Codeplex:** <http://dafuscator.codeplex.com/>

## Prerequisites

You will need the .Net Framework 4 and Database to use Dafuscator.

## Downloading Builds

You can download a installer for Dafuscator at either WaveTech's website or CodePlex.

## Provided Generators

* **Account Number** 
An account number that could be used to replace an external or internal account number 
 (Example: G5B-2M1QU1F)

* **Address** 
Can generate 3 address lines (Street, Suite and C/O) 
 (Example: 3127 Lafayette Walk)
 
* **Character** 
Random single character generator 
 (Example: Z)

* **City Name** 
Real city names from the United States 
 (Example: Truesdale) 

* **Company Name** 
Generates a real looking company name with optional suffix (Inc, LLC, PLC, etc) 
 (Example: Microcada L.L.C.) 

* **Country**
Real country names 
 (Example: Honduras)

* **Date** 
Generate a random date within a range 
 (Example: 10/5/1986) 

* **Email Address** 
Real looking email address 
 (Example: pBlevins@Ventureloft.net) 

* **First Name** 
String 
 (Example: Cori)

* **Last Name** 
String 
 (Example: Pressman) 

* **Login** 
Unique, realistic login name generator 
 (Example: AObando187) 

* **Number** 
Random Number Generator 
 (Example: 19875) 

* **Phone Number** 
Phone number generator with optional area code 
 (Example: 597-458-1974) 

* **Social Security Number** 
Generates an invalid, but real looking SSN 
 (Example: 937-58-0360) 

* **State** 
Real state names 
 (Example: Nevada) 

* **String** 
Random Strings (Character, Numbers and Special Characters) 
 (Example: eFRmdMGj) 

* **Url** 
Top Level Domain Url generator, human readable and real looking 
 (Example: www.Graynet.org) 

* **Zip Code (Postal Code)** 
Zip codes and optional 4 digit suffix 
 (Example: 23341-1377) 

* **Clear (Eraser)** 
Replaces data with empty strings or NULL 
 (Example: ""/NULL)

* **Full Name** 
Full real name with optional middle initial or full middle name 
 (Example: Maira Nidia Lamothe)

* **Stock Symbol** 
Real stock symbol generator from multiple exchanges (Equities, Indexes, ETF's, Funds) 
 (Example: ASCRX) 

* **Stock Name** 
Equity, Index, ETF and Fund names from multiple exchanges 
 (Example: Lodgian Inc.) 

* **Hex**
Random Hex Generator 
 (Example: 0xF31A59D) 

* **Guid** 
Random Guid Generator 
 (Example: {21EC2020-3AEA-1069-A2DD-08002B30309D})

* **Token** 
Generates a string given a user supplied pattern 
 (Example: {random})