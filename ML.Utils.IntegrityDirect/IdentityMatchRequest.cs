using System;

namespace ML.Utils.IntegrityDirect
{
	[Serializable]
	public class IdentityMatchRequest
	{
		/// <summary>
		/// The first/given name of the person to verify. Title, middle initial or middle name cannot be part of the First Name
		/// </summary>
		/// <para> </para> 
		/// <para>Ex: FirstName=John</para>
		[QueryParam("first")]
		[MatchDescription("First")]
		public string FirstName { get; set; }

		/// <summary>
		/// The middle name of the person to verify
		/// </summary>
		/// <para> </para> 
		/// <para>Ex: MiddleName=Q</para>
		[QueryParam("middle")]
		[MatchDescription("First Initial")]
		public string MiddleName { get; set; }

		/// <summary>
		/// The last name or surname of the person to verify. Suffix can‘t be part of the Last Name
		/// </summary>
		/// <para> </para> 
		/// <para>Ex: LastName=Carter</para>
		[QueryParam("last")]
		[MatchDescription("Last")]
		public string LastName { get; set; }

		/// <summary>
		/// Name Suffix of the person e.g. Jr, Sr
		/// </summary>
		/// <para> </para> 
		/// <para>Ex: Suffix=jr</para>
		[QueryParam("suffix")]
		[MatchDescription("Suffix")]
		public string Suffix { get; set; }

		/// <summary>
		/// Allowed values are 'F' for female or 'M' for male
		/// </summary>
		/// <para> </para> 
		/// <para>Ex: Gender=M</para>
		[QueryParam("gender")]
		[MatchDescription("Gender")]
		public string Gender { get; set; }

		/// <summary>
		/// The street address including house/building number and street name
		/// </summary>
		/// <para> </para> 
		/// <para>Ex: Addresss=1234 Main Street</para>
		[QueryParam("address")]
		[MatchDescription("Home Address")]
		public string Addresss { get; set; }

		/// <summary>
		/// Name of City
		/// </summary>
		/// <para> </para> 
		/// <para>Ex: City=Los Angeles</para>
		[QueryParam("city")]
		[MatchDescription("Home City")]
		public string City { get; set; }

		/// <summary>
		/// Two character State code (US Only)
		/// </summary>
		/// <para> </para> 
		/// <para>Ex: State=CA</para>
		[QueryParam("state")]
		[MatchDescription("Home State")]
		public string State { get; set; }

		/// <summary>
		/// The Zip/Postal Code is required in all countries except Ireland 
		/// </summary>
		/// <para> </para> 
		/// <para>Ex: Zip=90234</para>
		[QueryParam("zip")]
		[MatchDescription("Home Zip")]
		public string Zip { get; set; }

		/// <summary>
		/// Submit either the County code or County name
		/// </summary>
		/// <para> </para> 
		/// <para>Ex: County=CE</para>
		[QueryParam("county")]
		public string County { get; set; }

		/// <summary>
		/// County name (IRELAND only)
		/// </summary>
		/// <remarks>- Clare (CE); Cork (CK); Cavan (CN); Carlow (CW)</remarks>
		/// <remarks>- Donegal (DL); Dublin (DN)</remarks>
		/// <remarks>- Galway (GY)</remarks>
		/// <remarks>- Kildare (KE); Kilkenny (KK); Kerry (KY)</remarks>
		/// <remarks>- Longford (LD); Louth (LH); Limerick (LK); Leitrim (LM); Laois (LS)</remarks>
		/// <remarks>- Meath (MH); Monaghan (MN); Mayo (MO)</remarks>
		/// <remarks>- Offaly (OY)</remarks>
		/// <remarks>- Roscommon (RN)</remarks>
		/// <remarks>- Sligo (SO)</remarks>
		/// <remarks>- Tipperary (TY)</remarks>
		/// <remarks>- Waterford (WD); Westmeath (WH); Wicklow (WW); Wexford (WX)</remarks>
		/// <para> </para> 
		/// <para>Ex: CountyName=Clare</para>
		[QueryParam("county_name")]
		public string CountyName { get; set; }

		/// <summary>
		/// ISO 3166 Country Code (2-letter)
		/// </summary>
		/// <seealso cref="http://www.iso.org/iso/country_codes/iso_3166_code_lists/english_country_names_and_code_elements.htm"/>
		/// <para> </para> 
		/// <para>Ex: Country=US</para>
		[QueryParam("country")]
		public string Country { get; set; }

		/// <summary>
		/// Format: mm/dd/yyyy
		/// </summary>
		/// <para> </para> 
		/// <para>Ex: Dob=11/25/1971</para>
		[QueryParam("dob")]
		[MatchDescription("DOB(mm/dd/yyyy)", FieldFormat.DateTime, "MM/dd/yyyy")]
		[MatchDescription("DOB(mm/yyyy)", FieldFormat.DateTime, "MM/yyyy")]
		[MatchDescription("DOB(yyyy)", FieldFormat.DateTime, "yyyy")]
		public string Dob { get; set; }

		/// <summary>
		/// 7 or 10 Digit Phone Number. Remove any special formatting characters such as hyphens, dashes, or parenthesis
		/// </summary>
		/// <para> </para> 
		/// <para>Ex: Phone=1234567890 or Phone=1234567</para>
		[QueryParam("phone")]
		[MatchDescription("Home Phone")]
		[MatchDescription("Phone4", FieldFormat.StringRight, "4")]
		public string Phone { get; set; }

		/// <summary>
		/// Social Security Number (US ONLY));  Either the full 9 digit number can be sent or the last 4 digits. Remove any special formatting characters such as hyphens or dashes
		/// </summary>
		/// <para> </para> 
		/// <para>Ex: Ssn=123456789 or Ssn=6789</para>
		[QueryParam("ssn")]
		[MatchDescription("SSN")]
		[MatchDescription("SSN4", FieldFormat.StringRight, "4")]
		public string Ssn { get; set; }

		/// <summary>
		/// The number displayed on the actual government issued ID document
		/// </summary>
		/// <para> </para> 
		/// <para>Ex: Id=1000001886USA4301288M0303103<![CDATA[<<<<<<<<<<<<<<<]]>2</para>
		[QueryParam("ID")]
		[MatchDescription("ID")]
		public string Id { get; set; }

		/// <summary>
		/// The type of government issued ID document);  Enter the number corresponding to the submitted ID type
		/// </summary>
		/// <remarks>- 1: Passport Registry No.</remarks>
		/// <remarks>- 2: Personal Identification No.</remarks>
		/// <remarks>- 3: Identity Card No.</remarks>
		/// <remarks>- 4: Driver's License No.</remarks>
		/// <remarks>- 8: Travel Document</remarks>
		/// <remarks>- 12: Residence Permit.</remarks>
		/// <remarks>- 13: Identity Certificate No.</remarks>
		/// <remarks>- 16: Registro Federal de Contribuyentes.</remarks>
		/// <remarks>- 17: Credencial de Elector.</remarks>
		/// <remarks>- 19: Social Security Number (US ONLY).</remarks>
		/// <remarks>- 20: Tax File Number (Australia Only).</remarks>
		/// <para> </para> 
		/// <para>Ex: IdType=1</para>
		[QueryParam("ID_Type")]
		public string IdType { get; set; }

		/// <summary>
		/// UserID should be under 40 characters and can be separated with hyphens. It can contain both alpha and numeric characters
		/// </summary>
		/// <para> </para> 
		/// <para>Ex: UserID=2309424024080840994</para>
		[QueryParam("UserID")]
		public string UserId { get; set; }

		/// <summary>
		/// UserIP is to the IP Address of the user and should be sent in the standard IP Address notation
		/// </summary>
		/// <para> </para> 
		/// <para>Ex: UserIP=12.35.67.89</para>
		[QueryParam("UserIP")]
		public string UserIp { get; set; }

	}
}
